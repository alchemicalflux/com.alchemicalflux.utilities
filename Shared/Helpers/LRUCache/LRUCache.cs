/*------------------------------------------------------------------------------
  File:           LRUCache.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Implements a reference based Least Recently Used container.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-15 15:34:25 
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
                Prev = Next = null;
            }

            public TKey Key;
            public TValue Value;
            public Node Prev;
            public Node Next;
        };

        #endregion Classes

        #region Members

        private int _maxSize;
        private Dictionary<TKey, Node> _mapping;
        private Func<TKey, TValue> _onCreateValue;

        private Node _head;
        private Node _tail;

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
            _onCreateValue = onCreateValue;

            _head = _tail = null;
        }

        public TValue Get(TKey key)
        {
            if(_mapping.ContainsKey(key) ) { return PullValueToFront(_mapping[key]); }
            return AssignNewValue(key);
        }

        private TValue PullValueToFront(Node toMove)
        {
            if(toMove == _head) { return toMove.Value; }
            RemoveNode(toMove);
            InsertAtFront(toMove);
            return toMove.Value;
        }

        private TValue AssignNewValue(TKey key)
        {
            Node node;
            if(_mapping.Count < _maxSize)
            {
                node = new(key, _onCreateValue(key));
                _mapping[key] = node;
            }
            else
            {
                node = RemoveTail();
                _mapping.Remove(node.Key);
                node.Key = key;
                node.Value = _onCreateValue(key);
                _mapping.Add(key, node);
            }
            InsertAtFront(node);
            return node.Value;
        }

        private void InsertAtFront(Node node)
        {
            node.Prev = null;
            if(_head == null)
            {
                _head = _tail = node;
                node.Next = null;
            }
            else
            {
                node.Next = _head;
                _head.Prev = node;
                _head = node;
            }
        }

        private void InsertAtEnd(Node node)
        {
            node.Next = null;
            if(_head == null)
            {
                _head = _tail = node;
                node.Prev = null;
            }
            else
            {
                node.Prev = _tail;
                _tail.Next = node;
                _tail = node;
            }
        }

        private void RemoveNode(Node node)
        {
            if(_head == _tail) { _head = _tail = null; }
            else if(node == _head)
            {
                _head = _head.Next;
                if(_head != null) { _head.Prev = null; }
            }
            else if(node == _tail)
            {
                _tail = _tail.Prev;
                if(_tail != null) { _tail.Next = null; }
            }
            else
            {
                node.Prev.Next = node.Next;
                node.Next.Prev = node.Prev;
            }
        }

        private Node RemoveHead()
        {
            var node = _head;
            if(_head == _tail) { _head = _tail = null; }
            else 
            {
                _head = _head.Next;
                _head.Prev = null;
            }
            node.Prev = node.Next = null;
            return node;
        }

        private Node RemoveTail()
        {
            var node = _tail;
            if(_head == _tail) { _head = _tail = null; }
            else 
            {
                _tail = _tail.Prev;
                _tail.Next = null;
            }
            node.Prev = node.Next = null;
            return node;
        }

        #endregion Methods
    }
}
