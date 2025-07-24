/*------------------------------------------------------------------------------
File:       Vector3LerpUnclampedImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements an unclamped Vector3 linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements an unclamped Vector3 linear interpolation.
    /// </summary>
    public sealed class Vector3LerpUnclampedImpl : Vector3TwoPointImpl
    {
        #region Methods

        /// <summary>
        /// Constructor for the Vector3LerpUnclampedImpl class, which implements
        /// an unclamped linear Vector3 interpolation.
        /// </summary>
        /// <param name="start">The initial vector for interpolation.</param>
        /// <param name="end">The final vector for interpolation.</param>
        public Vector3LerpUnclampedImpl(Vector3 start, Vector3 end) :
            base(start, end)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override Vector3 ProcessInterpolation(float progress)
        {
            return Vector3.LerpUnclamped(Start, End, progress);
        }

        #endregion Overrides

        #endregion Methods
    }
}
