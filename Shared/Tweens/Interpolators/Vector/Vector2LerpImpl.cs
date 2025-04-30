/*------------------------------------------------------------------------------
File:       Vector2LerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector2 linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-29 20:16:53 
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

        /// <inheritdoc />
        public override Vector2 Interpolate(float progress)
        {
            if(float.IsNaN(progress))
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(progress), $"The value '{progress}' is invalid."
                );
            }
            return Vector2.Lerp(Start, End, progress);
        }

        #endregion Methods
    }
}