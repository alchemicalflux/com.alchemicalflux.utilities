/*------------------------------------------------------------------------------
File:       Vector3LerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a clamped Vector3 linear interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 20:21:36 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a clamped Vector3 linear interpolation.
    /// </summary>
    public class Vector3LerpImpl : TwoPointInterpolator<Vector3>
    {
        #region Methods

        #region TwoPointInterpolator Implemenation

        /// <summary>
        /// Constructor for the Vector3LerpImpl class, which implements a 
        /// linear Vector3 interpolation.
        /// </summary>
        /// <param name="start">The initial vector for interpolation.</param>
        /// <param name="end">The final vector for interpolation.</param>
        public Vector3LerpImpl(Vector2 start, Vector2 end) :
            base(start, end)
        {
        }

        /// <inheritdoc/>
        public override Vector3 Interpolate(float progress)
        {
            return Vector3.Lerp(Start, End, progress);
        }

        #endregion TwoPointInterpolator Implemenation

        #endregion Methods
    }
}
