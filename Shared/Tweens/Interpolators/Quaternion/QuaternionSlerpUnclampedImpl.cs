/*------------------------------------------------------------------------------
File:       QuaternionSlerpUnclampedImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements an unclamped Quaternion spherical interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements an unclamped Quaternion spherical interpolation.
    /// </summary>
    public sealed class QuaternionSlerpUnclampedImpl : QuaternionTwoPointImpl
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

        #region Overrides

        /// <inheritdoc />
        protected override Quaternion ProcessInterpolation(float progress)
        {
            return Quaternion.SlerpUnclamped(Start, End, progress);
        }

        #endregion Overrides

        #endregion Methods
    }
}

