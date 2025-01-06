/*------------------------------------------------------------------------------
File:       LRUValueCache.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a value based Least Recently Used container.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class LRUValueCache<TKey, TValue> 
        where TKey : struct where TValue : struct
    {
        #region Constants

        private const int _initialCapacity = 500;

        #endregion Constants

        #region Classes

        private struct CacheNode
        {
            public CacheNode(TKey key, TValue value)
            {
                Key = key;
                Value = value;
                Prev = Next = int.MinValue;
            }

            public TKey Key;
            public TValue Value;
            public int Prev;
            public int Next;
        };

        #endregion Classes

        #region Members

        private int _capacity;
        private Dictionary<TKey, int> _mapping;
        private CacheNode[] _storage;

        private Func<TKey, TValue> _onCreateValue;

        private int _head;
        private int _tail;
        private int _unused;

        #endregion Members

        #region Properties

        public int Count { get { return _mapping.Count; } }
        public int Capacity { get { return _capacity; } }

        #endregion Properties

        #region Methods

        public LRUValueCache(Func<TKey, TValue> onCreateValue, 
            int capacity = _initialCapacity)
        {
            if(_initialCapacity <= 0) 
            { 
                throw new ArgumentOutOfRangeException(nameof(capacity),
                    "Capacity must be greater than zero.)");
            }

            _capacity = capacity;
            _mapping = new();
            _storage = new CacheNode[_capacity];
            _onCreateValue = onCreateValue;

            _head = _tail = int.MinValue;
            _unused = 0;
        }

        public TValue Get(TKey key)
        {
            if(_mapping.TryGetValue(key, out var node)) 
            { 
                return MoveToFront(node); 
            }
            return AssignNewValue(key);
        }

        public void Clear()
        {
            _mapping.Clear();
            _unused = 0;
        }

        private TValue MoveToFront(int toMove)
        {
            if(toMove == _head) { return _storage[toMove].Value; }
            RemoveNode(toMove);
            InsertAtFront(toMove);
            return _storage[toMove].Value;
        }

        private TValue AssignNewValue(TKey key)
        {
            int node;
            if(_mapping.Count < _capacity)
            {
                node = _unused;
                ++_unused;
                _storage[node].Key = key;
                _storage[node].Value = _onCreateValue(key);
                _mapping[key] = node;
            }
            else
            {
                node = RemoveTail();
                _mapping.Remove(_storage[node].Key);
                _storage[node].Key = key;
                _storage[node].Value = _onCreateValue(key);
                _mapping.Add(key, node);
            }
            InsertAtFront(node);
            return _storage[node].Value;
        }

        private void InsertAtFront(int node)
        {
            _storage[node].Prev = int.MinValue;
            if(_head == int.MinValue)
            {
                _head = _tail = node;
                _storage[node].Next = int.MinValue;
            }
            else
            {
                _storage[node].Next = _head;
                _storage[_head].Prev = node;
                _head = node;
            }
        }

        private void InsertAtEnd(int node)
        {
            _storage[node].Next = int.MinValue;
            if(_head == int.MinValue)
            {
                _head = _tail = node;
                _storage[node].Prev = int.MinValue;
            }
            else
            {
                _storage[node].Prev = _tail;
                _storage[_tail].Next = node;
                _tail = node;
            }
        }

        private void RemoveNode(int node)
        {
            if(_head == _tail) { _head = _tail = int.MinValue; }
            else if(node == _head)
            {
                _head = _storage[_head].Next;
                if(_head != int.MinValue) 
                { 
                    _storage[_head].Prev = int.MinValue; 
                }
            }
            else if(node == _tail)
            {
                _tail = _storage[_tail].Prev;
                if(_tail != int.MinValue) 
                { 
                    _storage[_tail].Next = int.MinValue; 
                }
            }
            else
            {
                _storage[_storage[node].Prev].Next = _storage[node].Next;
                _storage[_storage[node].Next].Prev = _storage[node].Prev;
            }
        }

        private int RemoveHead()
        {
            var node = _head;
            if(_head == _tail) { _head = _tail = int.MinValue; }
            else 
            {
                _head = _storage[_head].Next;
                _storage[_head].Prev = int.MinValue;
            }
            _storage[node].Prev = _storage[node].Next = int.MinValue;
            return node;
        }

        private int RemoveTail()
        {
            var node = _tail;
            if(_head == _tail) { _head = _tail = int.MinValue; }
            else 
            {
                _tail = _storage[_tail].Prev;
                _storage[_tail].Next = int.MinValue;
            }
            _storage[node].Prev = _storage[node].Next = int.MinValue;
            return node;
        }

        #endregion Methods
    }
}
