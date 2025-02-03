/*------------------------------------------------------------------------------
File:       WeightedIndexPool.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a container for randomly selecting weighted indices while 
            tracking those indices that are in the pool, discarded and available
            for reshuffling, and currently in use.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-03 06:29:19 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class WeightedIndexPool
    {
        #region Constants

        private static readonly Comparer<Data> _comparer =
            Comparer<Data>.Create((x, y) =>
                x.WeightRange.CompareTo(y.WeightRange));

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

        private int _length;
        private Data[] _data;
        private int[] _indexToData;
        private int _offset = 0;

        #endregion Fields

        #region Properties

        public Func<int, double> _indexToWeight { get; protected set; }
        public Func<double> _randomizer { get; protected set; }

        #endregion Properties

        #region Methods

        public WeightedIndexPool(int count, Func<int, double> indexToWeight,
            Func<double> randomizer)
        {
            if(count <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }
            if(indexToWeight == null)
            {
                throw new ArgumentNullException(nameof(indexToWeight));
            }
            if(randomizer == null)
            {
                throw new ArgumentNullException(nameof(randomizer));
            }

            _length = count;
            _data = new Data[_length];
            _indexToData = new int[_length];
            _indexToWeight = indexToWeight;
            _randomizer = randomizer;
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
            var lastIndex = _length - _offset;
            Swap(dataIndex, lastIndex);

            RebuildWeights(dataIndex, lastIndex);
            return _data[lastIndex].RefIndex;
        }

        public int PullIndexWithReplacement()
        {
            if(_offset >= _length)
            {
                throw new InvalidOperationException("No pulls remain.");
            }

            var searchItem = new Data()
            {
                WeightRange = _data[^(_offset + 1)].WeightRange * _randomizer()
            };
            var dataIndex = Array.BinarySearch(_data, 0, _length - _offset,
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

        private void Reset()
        {
            _offset = 0;
            var totalWeight = 0.0;
            for(var index = 0; index < _length; ++index)
            {
                totalWeight += _indexToWeight(index);
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
                prevValue += _indexToWeight(_data[first].RefIndex);
                _data[first].WeightRange = prevValue;
            }
        }
        #endregion Methods
    }
}