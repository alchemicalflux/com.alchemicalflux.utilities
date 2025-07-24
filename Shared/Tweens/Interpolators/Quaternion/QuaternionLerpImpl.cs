/*------------------------------------------------------------------------------
File:       QuaternionLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a clamped Quaternion linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a clamped Quaternion linear interpolation.
    /// </summary>
    public sealed class QuaternionLerpImpl : QuaternionTwoPointImpl
    {
        #region Methods

        /// <summary>
        /// Constructor for the QuaternionLerpImpl class, which implements a
        /// clamped linear Quaternion interpolation.
        /// </summary>
        /// <param name="start">
        /// The initial quaternion for interpolation.
        /// </param>
        /// <param name="end">The final quaternion for interpolation.</param>
        public QuaternionLerpImpl(Quaternion start, Quaternion end) :
            base(start, end)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override Quaternion ProcessInterpolation(float progress)
        {
            return Quaternion.Lerp(Start, End, progress);
        }

        #endregion Overrides

        #endregion Methods
    }
}
