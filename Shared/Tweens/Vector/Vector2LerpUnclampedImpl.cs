/*------------------------------------------------------------------------------
File:       Vector2LerpUnclampedImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements an unclamped Vector2 linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-16 02:36:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements an unclamped Vector2 linear interpolation.
    /// </summary>
    public class Vector2LerpUnclampedImpl : TwoPointInterpolator<Vector2>
    {
        #region Methods

        /// <summary>
        /// Constructor for the Vector2LerpUnclampedImpl class, which implements 
        /// an unclamped linear Vector2 interpolation.
        /// </summary>
        /// <param name="start">The initial vector for interpolation.</param>
        /// <param name="end">The final vector for interpolation.</param>
        public Vector2LerpUnclampedImpl(Vector2 start, Vector2 end) :
            base(start, end)
        {
        }

        #region TwoPointInterpolator Implemenation

        /// <inheritdoc />
        public override Vector2 Interpolate(float progress)
        {
            return Vector2.LerpUnclamped(Start, End, progress);
        }

        #endregion TwoPointInterpolator Implemenation

        #endregion Methods
    }
}