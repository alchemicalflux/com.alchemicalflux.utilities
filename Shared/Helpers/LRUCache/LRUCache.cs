/*------------------------------------------------------------------------------
  File:           LRUCache.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Implements a reference-based Least Recently Used (LRU) cache 
                    container.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-31 08:56:31 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using static UnityEditor.Experimental.GraphView.Port;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Implements a Least Recently Used (LRU) cache to store <typeparamref name="TValue"/> items
    /// associated with <typeparamref name="TKey"/> keys. The cache evicts the least recently used
    /// key-value pair when the capacity is exceeded, allowing efficient reuse of stored values.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys used to access cached values.</typeparam>
    /// <typeparam name="TValue">The type of the values stored in the cache.</typeparam>
    public sealed class LRUCache<TKey, TValue>
    {
        #region Constants

        /// <summary>Default capacity for the cache if no specific value is provided.</summary>
        private const int _initialCapacity = 500;

        #endregion Constants

        #region Classes

        /// <summary>Represents a cache node containing a key-value pair for tracking usage.</summary>
        private struct CacheNode
        {
            public TKey Key;
            public TValue Value;
        };

        #endregion Classes

        #region Members

        private int _capacity;
        private Dictionary<TKey, LinkedListNode<CacheNode>> _mapping;
        private LinkedList<CacheNode> _nodes;

        private Func<TKey, TValue> _onCreateValue;
        private Action<TValue> _onDestroyValue;

        #endregion Members

        #region Properties

        /// <summary> Gets the current number of items in the cache.</summary>
        public int Count => _mapping.Count;

        /// <summary>Gets the maximum number of items the cache can hold.</summary>
        public int Capacity => _capacity;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="LRUCache{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="onCreateValue">
        ///     A function to create <typeparamref name="TValue"/> objects based on <typeparamref name="TKey"/> values.
        /// </param>
        /// <param name="capacity">
        ///     The maximum capacity of the cache. Defaults to _initialCapacity. Must be greater than zero.
        /// </param>
        /// <param name="onDestroyValue">
        ///     A function called when the least recently used object is removed from cache.
        ///     Will recieve the reference to the least recently used object.
        ///     Can be null, in which case no actions will be taken.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown if <paramref name="onCreateValue"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        ///     Thrown if <paramref name="capacity"/> is less than or equal to zero.
        /// </exception>
        public LRUCache(Func<TKey, TValue> onCreateValue, Action<TValue> onDestroyValue,
            int capacity = _initialCapacity)
        {
            _onCreateValue = onCreateValue ?? 
                throw new ArgumentNullException(nameof(onCreateValue), "Value creation function cannot be null.");
            _onDestroyValue = onDestroyValue;

            if(capacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be greater than zero.");
            }

            _capacity = capacity;
            _mapping = new();
            _nodes = new();
        }

        /// <summary>
        /// Retrieves the value associated with the specified key. If the key is not in the cache,
        /// a new value is created and added to the cache.
        /// </summary>
        /// <param name="key">The key associated with the desired value.</param>
        /// <returns>The value associated with the specified key.</returns>
        public TValue Get(TKey key)
        {
            if(_mapping.TryGetValue(key, out var node)) { return MoveToFront(node); }
            return AssignNewValue(key);
        }

        /// <summary>
        /// Clears all key-value pairs from the cache.
        /// </summary>
        public void Clear()
        {
            DestroyLastNodes(Count);
        }

        public void Resize(int newCapacity)
        {
            if(newCapacity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(newCapacity), "Capacity must be greater than zero.");
            }

            DestroyLastNodes(Capacity - newCapacity);
            _capacity = newCapacity;
        }

        /// <summary>
        /// Moves the specified node to the front of the linked list to mark it as recently used.
        /// </summary>
        /// <param name="toMove">The node to move to the front.</param>
        /// <returns>The value of the node that was moved.</returns>
        private TValue MoveToFront(LinkedListNode<CacheNode> toMove)
        {
            if(toMove != _nodes.First)
            {
                _nodes.Remove(toMove);
                _nodes.AddFirst(toMove);
            }
            return toMove.Value.Value;
        }

        /// <summary>
        /// Creates a new key-value pair in the cache, either by adding a new entry or overwriting the least recently 
        /// used one.
        /// </summary>
        /// <param name="key">The key for the new value.</param>
        /// <returns>The newly created value.</returns>
        private TValue AssignNewValue(TKey key)
        {
            CacheNode cacheNode;
            LinkedListNode<CacheNode> node;

            if(Count < Capacity)
            {
                cacheNode = new() { Key = key, Value = _onCreateValue(key) };
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

        /// <summary>
        /// Removes the least recently used key-value pair from the cache, destroying its value.
        /// </summary>
        /// <param name="cacheNode">Outputs the removed cache node for reuse.</param>
        /// <param name="node">Outputs the removed linked list node for reuse.</param>
        private void RemoveLastNode(out CacheNode cacheNode, out LinkedListNode<CacheNode> node)
        {
            node = _nodes.Last;
            cacheNode = node.Value;
            _onDestroyValue?.Invoke(cacheNode.Value);
            _nodes.RemoveLast();
            _mapping.Remove(cacheNode.Key);
        }

        /// <summary>
        /// Removes the last 'count' nodes from the the cache.
        /// </summary>
        /// <param name="count">
        ///     Number of nodes to be removed.
        ///     Must be less than or equal to current Count.
        /// </param>
        private void DestroyLastNodes(int count)
        {
            for(var iter = 0; iter < count; ++iter)
            {
                RemoveLastNode(out var cacheNode, out var node);
            }
        }

        #endregion Methods
    }
}
