/*------------------------------------------------------------------------------
  File:           LRUCache.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Implements a reference based Least Recently Used container.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-16 20:15:25 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class LRUCache<TKey, TValue>
    {
        #region Constants

        private const int _initialCapacity = 500;

        #endregion Constants

        #region Classes

        private class CacheNode
        {
            public CacheNode(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public TKey Key;
            public TValue Value;
        };

        #endregion Classes

        #region Members

        private int _capacity;
        private Dictionary<TKey, LinkedListNode<CacheNode>> _mapping;
        private LinkedList<CacheNode> _nodes;
        private Func<TKey, TValue> _onCreateValue;

        #endregion Members

        #region Properties

        public int Count { get { return _mapping.Count; } }
        public int Capacity { get { return _capacity; } }

        #endregion Properties

        #region Methods

        public LRUCache(Func<TKey, TValue> onCreateValue, int capacity = _initialCapacity)
        {
            if(_initialCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), 
                    "Capacity must be greater than zero.)");
            }

            _capacity = capacity;
            _mapping = new();
            _nodes = new();
            _onCreateValue = onCreateValue;
        }

        public TValue Get(TKey key)
        {
            if(_mapping.TryGetValue(key, out var node)) { return MoveToFront(node); }
            return AssignNewValue(key);
        }

        public void Clear()
        {
            _mapping.Clear();
            _nodes.Clear();
        }

        private TValue MoveToFront(LinkedListNode<CacheNode> toMove)
        {
            if(toMove != _nodes.First)
            {
                _nodes.Remove(toMove);
                _nodes.AddFirst(toMove);
            }
            return toMove.Value.Value;
        }

        private TValue AssignNewValue(TKey key)
        {
            CacheNode cacheNode;
            LinkedListNode<CacheNode> node;
            if(_mapping.Count < _capacity)
            {
                cacheNode = new(key, _onCreateValue(key));
                node = new(cacheNode);
            }
            else
            {
                RemoveLastNode(out cacheNode, out node);
                cacheNode.Key = key;
                cacheNode.Value = _onCreateValue(key);
            }
            _nodes.AddFirst(node);
            _mapping[key] = node;
            return cacheNode.Value;
        }

        private void RemoveLastNode(out CacheNode cacheNode, out LinkedListNode<CacheNode> node)
        {
            node = _nodes.Last;
            cacheNode = node.Value;
            _nodes.RemoveLast();
            _mapping.Remove(cacheNode.Key);
        }

        #endregion Methods
    }
}
