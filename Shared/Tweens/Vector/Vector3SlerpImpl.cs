/*------------------------------------------------------------------------------
File:       Vector3SlerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector3 spherical interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 22:41:08 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Vector3 spherical interpolation.
    /// </summary>
    public sealed class Vector3SlerpImpl : TwoPointInterpolator<Vector3>
    {
        #region Methods

        #region TwoPointInterpolator Implementation

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
        public override Vector3 Interpolate(float progress)
        {
            return Vector3.Slerp(Start, End, progress);
        }

        #endregion TwoPointInterpolator Implementation

        #endregion Methods
    }
}