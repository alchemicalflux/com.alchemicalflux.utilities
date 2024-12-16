/*------------------------------------------------------------------------------
  File:           LRUCache.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Implements a reference based Least Recently Used container.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-15 19:06:40 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class LRUCache<TKey, TValue>
    {
        #region Constants

        private const int _initialSize = 500;

        #endregion Constants

        #region Classes

        private class Node
        {
            public Node(TKey key, TValue value)
            {
                Key = key;
                Value = value;
            }

            public TKey Key;
            public TValue Value;
        };

        #endregion Classes

        #region Members

        private int _maxSize;
        private Dictionary<TKey, LinkedListNode<Node>> _mapping;
        private LinkedList<Node> _nodes;
        private Func<TKey, TValue> _onCreateValue;

        #endregion Members

        #region Methods

        public LRUCache(Func<TKey, TValue> onCreateValue, int maxSize = _initialSize)
        {
            if(_initialSize <= 0)
            {
                throw new ArgumentOutOfRangeException($"{nameof(maxSize)} must be greater than zero.)");
            }

            _maxSize = maxSize;
            _mapping = new();
            _nodes = new();
            _onCreateValue = onCreateValue;
        }

        public TValue Get(TKey key)
        {
            if(_mapping.ContainsKey(key) ) { return PullValueToFront(_mapping[key]); }
            return AssignNewValue(key);
        }

        private TValue PullValueToFront(LinkedListNode<Node> toMove)
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
            Node data;
            LinkedListNode<Node> node;
            if(_mapping.Count < _maxSize)
            {
                data = new(key, _onCreateValue(key));
                node = new(data);
            }
            else
            {
                node = _nodes.Last;
                data = node.Value;
                _nodes.RemoveLast();
                _mapping.Remove(data.Key);
                data.Key = key;
                data.Value = _onCreateValue(key);
            }
            _nodes.AddFirst(node);
            _mapping[key] = node;
            return data.Value;
        }

        #endregion Methods
    }
}
