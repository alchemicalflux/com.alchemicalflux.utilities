/*------------------------------------------------------------------------------
File:       Vector3TwoPointImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for two-point vector3 interpolators. Provides a
            foundation for interpolating between two Vector3 values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-20 18:35:42 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for two-point vector3 interpolators. Provides a
    /// foundation for interpolating between two <see cref="Vector3"/> values.
    /// </summary>
    public abstract class Vector3TwoPointImpl : TwoPointInterpolator<Vector3>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3TwoPointImpl"/>
        /// class with the specified start and end <see cref="Vector3"/> values.
        /// </summary>
        /// <param name="start">
        /// The initial <see cref="Vector3"/> value for interpolation.
        /// </param>
        /// <param name="end">
        /// The final <see cref="Vector3"/> value for interpolation.
        /// </param>
        public Vector3TwoPointImpl(Vector3 start, Vector3 end)
            : base(start, end)
        {
        }
    }
}
