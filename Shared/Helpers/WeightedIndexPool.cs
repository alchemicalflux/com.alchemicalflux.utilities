/*------------------------------------------------------------------------------
File:       WeightedIndexPool.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a container for randomly selecting weighted indices while 
            tracking those indices that are in the pool, discarded and available
            for reshuffling, and currently in use.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-09 09:49:13 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Text;

namespace AlchemicalFlux.Utilities.Helpers
{
    public sealed class WeightedIndexPool
    {
        #region Constants

        private static readonly Comparer<Data> _comparer = Comparer<Data>
            .Create((x, y) => x.WeightRange.CompareTo(y.WeightRange));

        #endregion Constants

        #region Definitions

        private struct Data
        {
            public int RefIndex;
            public double WeightRange;
            public bool IsLocked;
        }

        #endregion Definitions

        #region Fields

        private Data[] _data;
        private int[] _indexToData;
        private int _offset = 0;

        #endregion Fields

        #region Properties

        public int Capacity { get; private set; }

        public int Count { get; private set; }

        public bool HasIndices => Count > _offset;

        public Func<int, double> IndexToWeight { get; private set; }
        public Func<double> Randomizer { get; private set; }

        #endregion Properties

        #region Methods

        public WeightedIndexPool(int capacity, Func<int, double> indexToWeight,
            Func<double> randomizer)
        {
            if(capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity));
            }
            if(indexToWeight == null)
            {
                throw new ArgumentNullException(nameof(indexToWeight));
            }
            if(randomizer == null)
            {
                throw new ArgumentNullException(nameof(randomizer));
            }

            Capacity = Count = capacity;
            _data = new Data[Capacity];
            _indexToData = new int[Capacity];
            IndexToWeight = indexToWeight;
            Randomizer = randomizer;
            Reset();
        }

        public List<int> Add(int count)
        {
            return null;
        }

        //public void RemoveWeights(IEnumerable<int> indices)
        //{
        //}

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

        public void DiscardIndex(int index)
        {
            MarkAsUnlocked(index);
        }

        public void ReturnIndex(int index)
        {
            var dataIndex = MarkAsUnlocked(index);
            var last = Count - _offset;
            Swap(dataIndex, last);
            --_offset;

            _data[last].WeightRange = IndexToWeight(_data[last].RefIndex) + 
                PrevIndexWeight(last);
        }

        public void Shuffle()
        {
            // Find first unlocked index from end of pool.
            var right = Count - 1;
            for(; right >= 0 && _data[right].IsLocked; --right) {}
            if(right < 0) { return; } // No indices are unlocked for shuffle.

            // Group remaining locked indices to the end of the pool.
            for(var left = 0; left < right; )
            {
                for(; left < right && !_data[left].IsLocked; ++left) {}
                for(; left < right && _data[right].IsLocked; --right) {}
                if(left < right) { Swap(left, right); }
            }
            _offset = Count - ++right;

            RebuildWeights(0, right);
        }

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

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Capacity: {Capacity}, Count: {Count}, Offset: {_offset}");
            for(var index = 0; index < Capacity; ++index)
            {
                var val = _data[index];
                builder.AppendLine($"  " +
                    $"Index:  {index}, " +
                    $"RefIdx: {val.RefIndex}, " +
                    $"WgtRng: {val.WeightRange}, " +
                    $"IsRem:  {val.IsLocked}");
            }
            return builder.ToString();
        }

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

        private double PrevIndexWeight(int index)
        {
            return (index == 0) ? 0 : _data[index - 1].WeightRange;
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

        #endregion Methods
    }
}