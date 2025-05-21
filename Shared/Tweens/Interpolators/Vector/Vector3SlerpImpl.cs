/*------------------------------------------------------------------------------
File:       Vector3SlerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector3 spherical interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-20 18:44:50 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Vector3 spherical interpolation.
    /// </summary>
    public sealed class Vector3SlerpImpl : Vector3TwoPointImpl
    {
        #region Methods

        /// <summary>
        /// Constructor for the Vector3SlerpImpl class, which implements a
        /// spherical Vector3 interpolation.
        /// </summary>
        /// <param name="start">The initial vector for interpolation.</param>
        /// <param name="end">The final vector for interpolation.</param>
        public Vector3SlerpImpl(Vector3 start, Vector3 end) :
            base(start, end)
        {
        }

        /// <inheritdoc />
        protected override Vector3 ProcessInterpolation(float progress)
        {
            if(float.IsNaN(progress))
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(progress), "Progress cannot be NaN.");
            }
            return Vector3.Slerp(Start, End, progress);
        }

        #endregion Methods
    }
}