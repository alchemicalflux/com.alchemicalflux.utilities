/*------------------------------------------------------------------------------
File:       WeightedIndexPool.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a container for randomly selecting weighted indices while 
            tracking those indices that are in the pool, discarded and available
            for reshuffling, and currently in use.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-29 15:43:55 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
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
        }

        #endregion Definitions

        #region Fields

        private readonly Data[] _data;
        private readonly int[] _indexToData;
        private int _offset = 0;

        #endregion Fields

        #region Properties

        public int Count { get; private set; }

        public bool HasIndices => Count > _offset;

        public Func<int, double> IndexToWeight { get; private set; }
        public Func<double> Randomizer { get; private set; }

        private double PrevIndexWeight(int index) =>
            (index == 0) ? 0 : _data[index - 1].WeightRange;

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
        /// <param name="randomizer">Function to generate random values.</param>
        public WeightedIndexPool(int count, Func<int, double> indexToWeight,
            Func<double> randomizer)
        {
            if(indexToWeight == null)
            {
                throw new ArgumentNullException(nameof(indexToWeight));
            }
            if(randomizer == null)
            {
                throw new ArgumentNullException(nameof(randomizer));
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
        /// Pulls an index from the pool without replacement.
        /// </summary>
        /// <returns>The pulled index.</returns>
        public int PullIndex()
        {
            var dataIndex = PullIndexWithReplacement();

            _data[dataIndex].IsLocked = true;
            ++_offset;
            var lastIndex = Count - _offset;
            Swap(dataIndex, lastIndex);

            RebuildWeights(dataIndex, lastIndex);
            return _data[lastIndex].RefIndex;
        }

        /// <summary>
        /// Pulls an index from the pool with replacement.
        /// </summary>
        /// <returns>The pulled index.</returns>
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
        /// Discards an index from the pool.
        /// </summary>
        /// <param name="index">The index to discard.</param>
        public void DiscardIndex(int index)
        {
            MarkAsUnlocked(index);
        }

        /// <summary>
        /// Returns an index to the pool.
        /// </summary>
        /// <param name="index">The index to return.</param>
        public void ReturnIndex(int index)
        {
            var dataIndex = MarkAsUnlocked(index);
            var last = Count - _offset;
            Swap(dataIndex, last);
            --_offset;

            _data[last].WeightRange = IndexToWeight(_data[last].RefIndex) +
                PrevIndexWeight(last);
        }

        /// <summary>
        /// Shuffles the pool.
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
                if(left < right) { Swap(left, right); }
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
                if(_data[^index].IsLocked)
                {
                    result.Add(_data[^index].RefIndex);
                }
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
                if(!_data[^index].IsLocked)
                {
                    result.Add(_data[^index].RefIndex);
                }
            }
            return result;
        }

        #endregion Accessing

        #region Helpers

        private void Reset()
        {
            _offset = 0;
            var totalWeight = 0.0;
            for(var index = 0; index < Count; ++index)
            {
                totalWeight += IndexToWeight(index);
                _data[index].RefIndex = index;
                _data[index].WeightRange = totalWeight;
                _indexToData[index] = index;
            }
        }

        private void Swap(int index1, int index2)
        {
            if(index1 == index2) { return; }

            _indexToData[_data[index1].RefIndex] = index2;
            _indexToData[_data[index2].RefIndex] = index1;

            (_data[index1], _data[index2]) = (_data[index2], _data[index1]);
        }

        private void RebuildWeights(int first, int end)
        {
            var prevValue = PrevIndexWeight(first);
            for(; first < end; ++first)
            {
                prevValue += IndexToWeight(_data[first].RefIndex);
                _data[first].WeightRange = prevValue;
            }
        }

        private int MarkAsUnlocked(int index)
        {
            var dataIndex = _indexToData[index];
            if(!_data[dataIndex].IsLocked)
            {
                throw new InvalidOperationException(
                    $"Index {index} already marked as unlocked.");
            }
            _data[dataIndex].IsLocked = false;
            return dataIndex;
        }

        #endregion Helpers

        #endregion Methods
    }
}
