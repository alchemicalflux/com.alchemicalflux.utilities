/*------------------------------------------------------------------------------
File:       WeightedIndexPool.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a container for randomly selecting weighted indices while 
            tracking those indices that are in the pool, discarded and available
            for reshuffling, and currently in use.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-04 23:05:08 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

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
            public bool InUse;
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

            _data[dataIndex].InUse = true;
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
        }

        public void ReturnIndex(int index)
        {
        }

        public void Shuffle()
        {
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
            var prevValue = (first == 0) ? 0 : _data[first - 1].WeightRange;
            for(; first <= end; ++first)
            {
                prevValue += IndexToWeight(_data[first].RefIndex);
                _data[first].WeightRange = prevValue;
            }
        }
        #endregion Methods
    }
}