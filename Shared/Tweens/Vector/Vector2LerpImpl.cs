/*------------------------------------------------------------------------------
File:       Vector2LerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector2 linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 22:41:08 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a clamped Vector2 linear interpolation.
    /// </summary>
    public sealed class Vector2LerpImpl : TwoPointInterpolator<Vector2>
    {
        #region Methods

        /// <summary>
        /// Constructor for the Vector2LerpImpl class, which implements a linear
        /// Vector2 interpolation.
        /// </summary>
        /// <param name="start">The initial vector for interpolation.</param>
        /// <param name="end">The final vector for interpolation.</param>
        public Vector2LerpImpl(Vector2 start, Vector2 end) :
            base(start, end)
        {
        }

        #region TwoPointInterpolator Implementation

        /// <inheritdoc />
        public override Vector2 Interpolate(float progress)
        {
            return Vector2.Lerp(Start, End, progress);
        }

        #endregion TwoPointInterpolator Implementation

        #endregion Methods
    }
}