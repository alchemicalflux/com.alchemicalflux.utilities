/*------------------------------------------------------------------------------
File:       Vector3SlerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector3 spherical interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
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

        #region Overrides

        /// <inheritdoc />
        protected override Vector3 ProcessInterpolation(float progress)
        {
            return Vector3.Slerp(Start, End, progress);
        }
        
        #endregion Overrides

        #endregion Methods
    }
}