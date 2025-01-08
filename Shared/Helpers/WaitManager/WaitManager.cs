/*------------------------------------------------------------------------------
File:       WaitManager.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a singleton container that maintains Unity 
                    coroutine wait objects to reduce the number of allocations.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-08 07:47:32 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    public sealed class WaitManager : Singleton<WaitManager>
    {
        private WaitManagerImpl _impl;

        public static WaitForEndOfFrame WaitForEndOfFrame 
        { 
            get { return Get._impl.WaitForEndOfFrame; } 
        }
        public static WaitForFixedUpdate WaitForFixedUpdate 
        { 
            get { return Get._impl.WaitForFixedUpdate; } 
        }
        public static int Capacity { get { return Get._impl.Capacity; } }

        private WaitManager()
        {
            _impl = new();
        }

        public static WaitForSeconds WaitForSeconds(float seconds)
        {
            return Get._impl.WaitForSeconds(seconds);
        }

        public static 
            WaitForSecondsRealtime WaitForSecondsRealtime(float seconds)
        {
            return Get._impl.WaitForSecondsRealtime(seconds);
        }

        public static WaitUntil WaitUntil(Func<bool> condition)
        {
            return Get._impl.WaitUntil(condition);
        }

        public static WaitWhile WaitWhile(Func<bool> condition)
        {
            return Get._impl.WaitWhile(condition);
        }
    }
}