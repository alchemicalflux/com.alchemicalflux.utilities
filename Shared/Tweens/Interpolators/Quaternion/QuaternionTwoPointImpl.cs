/*------------------------------------------------------------------------------
File:       QuaternionTwoPointImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for two-point quaternion interpolators. Provides
            a foundation for interpolating between two Quaternion values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for two-point quaternion interpolators.
    /// Provides a foundation for interpolating between two
    /// <see cref="Quaternion"/> values.
    /// </summary>
    public abstract class QuaternionTwoPointImpl
        : TwoPointInterpolator<Quaternion>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="QuaternionTwoPointImpl"/> class with the specified start
        /// and end quaternions.
        /// </summary>
        /// <param name="start">
        /// The initial quaternion value for interpolation.
        /// </param>
        /// <param name="end">
        /// The final quaternion value for interpolation.
        /// </param>
        public QuaternionTwoPointImpl(Quaternion start, Quaternion end)
            : base(start, end)
        {
        }

        #endregion Methods
    }
}
