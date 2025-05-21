/*------------------------------------------------------------------------------
File:       QuaternionSlerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a clamped Quaternion spherical interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-20 18:44:50 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a clamped Quaternion spherical interpolation.
    /// </summary>
    public sealed class QuaternionSlerpImpl : QuaternionTwoPointImpl
    {
        #region Methods

        /// <summary>
        /// Constructor for the QuaternionSlerpImpl class, which implements a
        /// clamped spherical Quaternion interpolation.
        /// </summary>
        /// <param name="start">
        /// The initial quaternion for interpolation.
        /// </param>
        /// <param name="end">The final quaternion for interpolation.</param>
        public QuaternionSlerpImpl(Quaternion start, Quaternion end) :
            base(start, end)
        {
        }

        /// <inheritdoc />
        protected override Quaternion ProcessInterpolation(float progress)
        {
            return Quaternion.Slerp(Start, End, progress);
        }

        #endregion Methods
    }
}
