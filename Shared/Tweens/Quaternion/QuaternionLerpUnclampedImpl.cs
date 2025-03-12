/*------------------------------------------------------------------------------
File:       QuaternionLerpUnclampedImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements an unclamped Quaternion linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-12 00:48:47 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements an unclamped Quaternion linear interpolation.
    /// </summary>
    public sealed class QuaternionLerpUnclampedImpl :
        TwoPointInterpolator<Quaternion>
    {
        #region Methods

        /// <summary>
        /// Constructor for the QuaternionLerpUnclampedImpl class, which 
        /// implements an unclamped linear Quaternion interpolation.
        /// </summary>
        /// <param name="start">
        /// The initial quaternion for interpolation.
        /// </param>
        /// <param name="end">The final quaternion for interpolation.</param>
        public QuaternionLerpUnclampedImpl(Quaternion start, Quaternion end) :
            base(start, end)
        {
        }

        /// <inheritdoc />
        public override Quaternion Interpolate(float progress)
        {
            return Quaternion.LerpUnclamped(Start, End, progress);
        }

        #endregion Methods
    }
}
