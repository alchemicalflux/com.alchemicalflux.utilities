/*------------------------------------------------------------------------------
File:       QuaternionExtensions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Helper functions to extend the Quaternion class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-05 02:52:38 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides extension methods for the <see cref="Quaternion"/> class.
    /// </summary>
    public static class QuaternionExtensions
    {
        /// <summary>
        /// Determines whether two <see cref="Quaternion"/> instances are
        /// approximately equal.
        /// </summary>
        /// <param name="quaternion">The base quaternion to compare.</param>
        /// <param name="toTest">The quaternion to compare against.</param>
        /// <returns>
        /// <c>true</c> if the components of the two quaternions are 
        /// approximately equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsApproximately(
            this Quaternion quaternion,
            Quaternion toTest)
        {
            return quaternion.x.Approximately(toTest.x) &&
                   quaternion.y.Approximately(toTest.y) &&
                   quaternion.z.Approximately(toTest.z) &&
                   quaternion.w.Approximately(toTest.w);
        }
    }
}
