/*------------------------------------------------------------------------------
File:       WaitManagerImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a container for maintaining Unity coroutine yield objects
            to reduce allocation occurances.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-08 07:47:32 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    public sealed class WaitManagerImpl
    {
        #region Constants

        private const int _defaultCapacity = 100;

        #endregion Constants

        #region Members

        private WaitForEndOfFrame _waitForEndOfFrame;
        private WaitForFixedUpdate _waitForFixedFrame;
        private LRUCache<float, WaitForSeconds> _waitForSeconds;
        private LRUCache<float, WaitForSecondsRealtime> _waitForSecondsRealtime;

        #endregion Members

        #region Properties

        public WaitForEndOfFrame WaitForEndOfFrame 
        { 
            get { return _waitForEndOfFrame; } 
        }
        public WaitForFixedUpdate WaitForFixedUpdate 
        {
            get { return _waitForFixedFrame; } 
        }
        public int Capacity { get { return _waitForSeconds.Capacity; } }

        #endregion Properties

        #region Methods

        public WaitManagerImpl(int capacity = _defaultCapacity)
        {
            _waitForEndOfFrame = new();
            _waitForFixedFrame = new();
            _waitForSeconds = 
                new((value) => new WaitForSeconds(value), null, capacity);
            _waitForSecondsRealtime = 
                new((value) => new WaitForSecondsRealtime(value), null, 
                    capacity);
        }

        public WaitForSeconds WaitForSeconds(float seconds)
        {
            return _waitForSeconds.Get(seconds);
        }

        public WaitForSecondsRealtime WaitForSecondsRealtime(float seconds)
        {
            return _waitForSecondsRealtime.Get(seconds);
        }

        public WaitUntil WaitUntil(Func<bool> condition)
        {
            return new WaitUntil(condition);
        }

        public WaitWhile WaitWhile(Func<bool> condition)
        {
            return new WaitWhile(condition);
        }

        #endregion Methods
    }
}