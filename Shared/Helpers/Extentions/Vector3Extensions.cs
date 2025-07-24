/*------------------------------------------------------------------------------
File:       Vector3Extensions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Helper functions to extend the Vector3 class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-02 19:42:56 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides extension methods for the <see cref="Vector3"/> class.
    /// </summary>
    public static class Vector3Extensions
    {
        /// <summary>
        /// Determines whether two <see cref="Vector3"/> instances are
        /// approximately equal.
        /// </summary>
        /// <param name="vector">The base vector to compare.</param>
        /// <param name="toTest">The vector to compare against.</param>
        /// <returns>
        /// <c>true</c> if the components of the two vectors are approximately
        /// equal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsApproximately(this Vector3 vector, Vector3 toTest)
        {
            return vector.x.Approximately(toTest.x) &&
                   vector.y.Approximately(toTest.y) &&
                   vector.z.Approximately(toTest.z);
        }
    }
}
