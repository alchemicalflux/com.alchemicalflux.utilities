/*------------------------------------------------------------------------------
File:       NullCheck.cs 
Project:    AlchemicalFlux Utilities
Overview:   Property attribute alerting users when object field is null.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Indicates that the associated field cannot be left as null within the 
    /// Unity inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = false, 
        AllowMultiple = false)]
    public class NullCheck : PropertyAttribute 
    {
        /// <summary>
        /// Indicates if the NullCheck should be ignored when assoiated with 
        /// prefabs.
        /// </summary>
        public bool IgnorePrefab { get; set; }
    }
}