/*------------------------------------------------------------------------------
File:       Vector2TwoPointImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for two-point vector2 interpolators. Provides a
            foundation for interpolating between two Vector2 values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for two-point vector2 interpolators. Provides a
    /// foundation for interpolating between two <see cref="Vector2"/> values.
    /// </summary>
    public abstract class Vector2TwoPointImpl : TwoPointInterpolator<Vector2>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector2TwoPointImpl"/>
        /// class with the specified start and end <see cref="Vector2"/> values.
        /// </summary>
        /// <param name="start">
        /// The initial <see cref="Vector2"/> value for interpolation.
        /// </param>
        /// <param name="end">
        /// The final <see cref="Vector2"/> value for interpolation.
        /// </param>
        public Vector2TwoPointImpl(Vector2 start, Vector2 end)
            : base(start, end)
        {
        }

        #endregion Methods
    }
}
