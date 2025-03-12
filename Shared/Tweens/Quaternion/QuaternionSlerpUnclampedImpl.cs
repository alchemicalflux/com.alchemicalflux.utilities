/*------------------------------------------------------------------------------
File:       QuaternionSlerpUnclampedImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements an unclamped Quaternion spherical interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 22:59:16 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements an unclamped Quaternion spherical interpolation.
    /// </summary>
    public sealed class QuaternionSlerpUnclampedImpl :
        TwoPointInterpolator<Quaternion>
    {
        #region Methods

        /// <summary>
        /// Constructor for the QuaternionSlerpUnclampedImpl class, which 
        /// implements an unclamped spherical Quaternion interpolation.
        /// </summary>
        /// <param name="start">
        /// The initial quaternion for interpolation.
        /// </param>
        /// <param name="end">The final quaternion for interpolation.</param>
        public QuaternionSlerpUnclampedImpl(Quaternion start, Quaternion end) :
            base(start, end)
        {
        }

        #endregion Methods

        #region TwoPointInterpolator Implementation

        /// <inheritdoc />
        public override Quaternion Interpolate(float progress)
        {
            return Quaternion.SlerpUnclamped(Start, End, progress);
        }

        #endregion TwoPointInterpolator Implementation
    }
}

