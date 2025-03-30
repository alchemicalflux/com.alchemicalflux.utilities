/*------------------------------------------------------------------------------
File:       WeightedIndexPool.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a container for randomly selecting weighted indices while 
            tracking those indices that are in the pool, discarded and available
            for reshuffling, and currently in use.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-30 10:11:18 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides a container for randomly selecting weighted indices while 
    /// tracking those indices that are in the pool, discarded and available
    /// for reshuffling, and currently in use.
    /// </summary>
    public sealed class WeightedIndexPool
    {
        #region Constants

        /// <summary>
        /// Reusable comparer for the binary search of any Data array.
        /// </summary>
        private static readonly Comparer<Data> _comparer = Comparer<Data>
            .Create((x, y) => x.WeightRange.CompareTo(y.WeightRange));

        #endregion Constants

        #region Definitions

        /// <summary>
        /// Struct used to track pulled, discarded, and returned indices to the
        /// pool. Struct used to ensure data is contiguous in memory.
        /// </summary>
        private struct Data
        {
            /// <summary>Value of the index for a given data entry.</summary>
            public int RefIndex;

            /// <summary>
            /// RefIndex's weight plus all accumulated weights from previous 
            /// data entries when data entry is unlocked.
            /// </summary>
            public double WeightRange;

            /// <summary>
            /// Locked indices are considered in use and cannot be shuffled.
            /// </summary>
            public bool IsLocked;

            /// <summary>
            /// Sets the data entry with the specified reference index 
            /// and weight range.
            /// </summary>
            /// <param name="refIndex">The reference index.</param>
            /// <param name="weightRange">The weight range.</param>
            public void SetValues(int refIndex, double weightRange)
            {
                RefIndex = refIndex;
                WeightRange = weightRange;
            }

            /// <summary>
            /// Sets the weight while including the specified previous value.
            /// </summary>
            /// <param name="indexToWeight">
            /// Function to determine the weight of an index.
            /// </param>
            /// <param name="prevValue">The previous value.</param>
            public double SetWeightWithPrev(Func<int, double> indexToWeight,
                double prevValue)
            {
                return WeightRange = indexToWeight(RefIndex) + prevValue;
            }

            /// <summary>
            /// Unlocks the data entry.
            /// </summary>
            /// <exception cref="InvalidOperationException">
            /// Thrown when the data entry is already unlocked.
            /// </exception>
            public void Unlock()
            {
                if(!IsLocked)
                {
                    throw new InvalidOperationException(
                        "Data entry is already unlocked.");
                }
                IsLocked = false;
            }
        }

        #endregion Definitions

        #region Fields

        /// <summary>
        /// Array of data entries representing the pool.
        /// </summary>
        private readonly Data[] _data;

        /// <summary>
        /// Array mapping indices to their corresponding data entries.
        /// </summary>
        private readonly int[] _indexToData;

        /// <summary>
        /// Offset indicating the number of locked indices.
        /// </summary>
        private int _offset = 0;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the total count of indices in the pool.
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Gets the number of indices in the available pool.
        /// </summary>
        public int IndicesRemaining => Count - _offset;

        /// <summary>
        /// Gets a value indicating whether there are any indices available in 
        /// the pool.
        /// </summary>
        public bool HasIndices => Count > _offset;

        /// <summary>
        /// Gets the function to determine the weight of an index.
        /// </summary>
        private Func<int, double> IndexToWeight { get; set; }

        /// <summary>
        /// Gets the function to generate random values.
        /// </summary>
        private Func<double> Randomizer { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="WeightedIndexPool"/> 
        /// class.
        /// </summary>
        /// <param name="count">The number of indices in the pool.</param>
        /// <param name="indexToWeight">
        /// Function to determine the weight of an index.
        /// </param>
        /// <param name="randomizer">
        /// Function to generate random values.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="indexToWeight"/> or <paramref 
        /// name="randomizer"/> is null.
        /// </exception>
        public WeightedIndexPool(int count, Func<int, double> indexToWeight,
            Func<double> randomizer)
        {
            if(count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count),
                    "Count must be greater than zero.");
            }
            if(indexToWeight == null)
            {
                throw new ArgumentNullException(nameof(indexToWeight),
                    "Index to weight function cannot be null.");
            }
            if(randomizer == null)
            {
                throw new ArgumentNullException(nameof(randomizer),
                    "Randomizer function cannot be null.");
            }

            Count = count;
            _data = new Data[Count];
            _indexToData = new int[Count];
            IndexToWeight = indexToWeight;
            Randomizer = randomizer;
            Reset();
        }

        #region Operations

        /// <summary>
        /// Pulls an index from the available pool without replacement.
        /// </summary>
        /// <returns>The pulled index.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no pulls remain.
        /// </exception>
        public int PullIndex()
        {
            var dataIndex = PullIndexWithReplacement();

            _data[dataIndex].IsLocked = true;
            ++_offset;
            var lastIndex = Count - _offset;
            SwapDataEntries(dataIndex, lastIndex);

            RebuildWeights(dataIndex, lastIndex);
            return _data[lastIndex].RefIndex;
        }

        /// <summary>
        /// Pulls an index from the available pool with replacement.
        /// </summary>
        /// <returns>The pulled index.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when no available pulls remain.
        /// </exception>
        public int PullIndexWithReplacement()
        {
            if(_offset >= Count)
            {
                throw new InvalidOperationException("No pulls remain.");
            }

            var searchItem = new Data()
            {
                WeightRange = _data[^(_offset + 1)].WeightRange * Randomizer()
            };
            var dataIndex = Array.BinarySearch(_data, 0, Count - _offset,
                searchItem, _comparer);
            if(dataIndex < 0) { dataIndex = ~dataIndex; }
            return dataIndex;
        }

        /// <summary>
        /// Discards an index from the pool, preventing it from being repulled
        /// until a shuffle occurs.
        /// </summary>
        /// <param name="index">The index to discard.</param>
        public void DiscardIndex(int index)
        {
            MarkAsUnlocked(index);
        }

        /// <summary>
        /// Returns an index to the available pool.
        /// </summary>
        /// <param name="index">The index to return.</param>
        public void ReturnIndex(int index)
        {
            var dataIndex = MarkAsUnlocked(index);
            var last = Count - _offset;
            SwapDataEntries(dataIndex, last);
            --_offset;

            _data[last].SetWeightWithPrev(IndexToWeight,
                PrevIndexWeight(last));
        }

        /// <summary>
        /// Shuffles the available pool.
        /// </summary>
        public void Shuffle()
        {
            // Find first unlocked index from end of pool.
            var right = Count - 1;
            for(; right >= 0 && _data[right].IsLocked; --right) { }
            if(right < 0) { return; } // No indices are unlocked for shuffle.

            // Group remaining locked indices to the end of the pool.
            for(var left = 0; left < right;)
            {
                for(; left < right && !_data[left].IsLocked; ++left) { }
                for(; left < right && _data[right].IsLocked; --right) { }
                if(left < right) { SwapDataEntries(left, right); }
            }
            _offset = Count - ++right;

            RebuildWeights(0, right);
        }

        #endregion Operations

        #region Accessing

        /// <summary>
        /// Gets the available indices in the pool.
        /// </summary>
        /// <returns>A list of available indices.</returns>
        public IList<int> AvailableIndices()
        {
            var available = Count - _offset;
            var result = new List<int>(available);
            for(var index = 0; index < available; ++index)
            {
                result.Add(_data[index].RefIndex);
            }
            return result;
        }

        /// <summary>
        /// Gets the pulled indices from the pool.
        /// </summary>
        /// <returns>A list of pulled indices.</returns>
        public IList<int> PulledIndices()
        {
            var result = new List<int>();
            for(var index = _offset; index > 0; --index)
            {
                var curData = _data[^index];
                if(curData.IsLocked) { result.Add(curData.RefIndex); }
            }
            return result;
        }

        /// <summary>
        /// Gets the discarded indices from the pool.
        /// </summary>
        /// <returns>A list of discarded indices.</returns>
        public IList<int> DiscardedIndices()
        {
            var result = new List<int>();
            for(var index = _offset; index > 0; --index)
            {
                var curData = _data[^index];
                if(!curData.IsLocked) { result.Add(curData.RefIndex); }
            }
            return result;
        }

        #endregion Accessing

        #region Helpers

        /// <summary>
        /// Gets the weight of the previous index.
        /// </summary>
        /// <param name="index">
        /// The index to get the previous weight for.
        /// </param>
        /// <returns>The weight of the previous index.</returns>
        private double PrevIndexWeight(int index)
        {
            return (index == 0) ? 0 : _data[index - 1].WeightRange;
        }

        /// <summary>
        /// Resets the pool to its initial state.
        /// </summary>
        private void Reset()
        {
            _offset = 0;
            var totalWeight = 0.0;
            for(var index = 0; index < Count; ++index)
            {
                totalWeight += IndexToWeight(index);
                _data[index].SetValues(index, totalWeight);
                _indexToData[index] = index;
            }
        }

        /// <summary>
        /// Swaps two indices in the pool.
        /// </summary>
        /// <param name="index1">The first index to swap.</param>
        /// <param name="index2">The second index to swap.</param>
        private void SwapDataEntries(int index1, int index2)
        {
            if(index1 == index2) { return; }
            SwapDataEntries(ref _data[index1], ref _data[index2],
                index1, index2);
        }

        /// <summary>
        /// Swaps two data entries and updates their indices.
        /// </summary>
        /// <param name="data1">The first data entry.</param>
        /// <param name="data2">The second data entry.</param>
        /// <param name="idx1">The index of the first data entry.</param>
        /// <param name="idx2">The index of the second data entry.</param>
        private void SwapDataEntries(ref Data data1, ref Data data2,
            int idx1, int idx2)
        {
            _indexToData[data1.RefIndex] = idx2;
            _indexToData[data2.RefIndex] = idx1;
            (data1, data2) = (data2, data1);
        }

        /// <summary>
        /// Rebuilds the weight ranges for the specified range of indices.
        /// </summary>
        /// <param name="first">The first index in the range.</param>
        /// <param name="end">The end index in the range.</param>
        private void RebuildWeights(int first, int end)
        {
            var prevValue = PrevIndexWeight(first);
            for(; first < end; ++first)
            {
                prevValue =
                    _data[first].SetWeightWithPrev(IndexToWeight, prevValue);
            }
        }

        /// <summary>
        /// Marks an index as unlocked.
        /// </summary>
        /// <param name="index">The index to mark as unlocked.</param>
        /// <returns>The data index of the unlocked index.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the index is already marked as unlocked.
        /// </exception>
        private int MarkAsUnlocked(int index)
        {
            var dataIndex = _indexToData[index];
            _data[dataIndex].Unlock();
            return dataIndex;
        }

        #endregion Helpers

        #endregion Methods
    }
}
