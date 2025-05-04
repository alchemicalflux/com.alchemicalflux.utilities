/*------------------------------------------------------------------------------
File:       FloatExtensions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Helper functions to extend the float type.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-04 05:46:42 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides extension methods for the <see cref="float"/> type.
    /// </summary>
    public static class FloatExtensions
    {
        #region Constants

        /// <summary>
        /// The epsilon value used for approximate comparisons.
        /// </summary>
        public const float Epsilon = 0.000001f;

        #endregion Constants

        #region Methods

        /// <summary>
        /// Determines whether two <see cref="float"/> values are approximately
        /// equal.
        /// </summary>
        /// <param name="a">The first float value to compare.</param>
        /// <param name="b">The second float value to compare.</param>
        /// <returns>
        /// <c>true</c> if the values are approximately equal, considering a
        /// small threshold; otherwise, <c>false</c>.
        /// </returns>
        public static bool Approximately(this float a, float b)
        {
            // Handle special cases for NaN
            if(float.IsNaN(a) && float.IsNaN(b)) { return true; }
            if(a == b) { return true; }
            if(Mathf.Approximately(a, b)) { return true; }

            #if DEBUG
            if(TestContext.CurrentTestExecutionContext != null)
            {
                // In unit tests, we want to use a custom threshold for
                // approximate comparison.
                Debug.Log($"Using custom threshold for comparison: {a} vs {b}");
                return Mathf.Abs(a - b) < Epsilon;
            }
            #endif

            return false;
        }

        #endregion Methods
    }
}
