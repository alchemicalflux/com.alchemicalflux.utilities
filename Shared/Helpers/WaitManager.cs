/*------------------------------------------------------------------------------
  File:           WaitManager.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Implements a singleton container that maintains Unity 
                    coroutine wait objects to reduce the number of allocations.
  Copyright:      2025 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2025-01-03 06:15:13 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    public sealed class WaitManager : Singleton<WaitManager>
    {
        #region Constants

        private const int _maxCapacity = 100;

        #endregion Constants

        #region Members

        private WaitForEndOfFrame _waitForEndOfFrame;
        private WaitForFixedUpdate _waitForFixedFrame;
        private LRUCache<float, WaitForSeconds> _waitForSeconds;
        private LRUCache<float, WaitForSecondsRealtime> _waitForSecondsRealtime;

        #endregion Members

        #region Properties

        public static WaitForEndOfFrame WaitForEndOfFrame { get { return Get._waitForEndOfFrame; } }
        public static WaitForFixedUpdate WaitForFixedUpdate { get { return Get._waitForFixedFrame; } }

        #endregion Properties

        #region Methods

        private WaitManager() 
        {
            _waitForEndOfFrame = new();
            _waitForFixedFrame = new();
            _waitForSeconds = new((value) => new WaitForSeconds(value), null, _maxCapacity);
            _waitForSecondsRealtime = new((value) => new WaitForSecondsRealtime(value), null, _maxCapacity);
        }

        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            return Get._waitForSeconds.Get(seconds);
        }

        public static WaitForSecondsRealtime WaitForSecondsRealtime(float seconds)
        {
            return Get._waitForSecondsRealtime.Get(seconds);
        }

        public static WaitUntil WaitUntil(Func<bool> condition)
        {
            return new WaitUntil(condition);
        }
        public static WaitWhile WaitWhile(Func<bool> condition)
        {
            return new WaitWhile(condition);
        }

        #endregion Methods
    }
}