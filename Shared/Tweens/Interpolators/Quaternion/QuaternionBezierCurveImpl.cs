/*------------------------------------------------------------------------------
File:       QuaternionBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation for Quaternion values using
            De Casteljau's algorithm. This class provides smooth interpolation
            between multiple quaternion control points by recursively blending
            them with spherical linear interpolation (Slerp).
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-31 13:30:37 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Implements a Bezier curve interpolation for <see cref="Quaternion"/>
    /// values using De Casteljau's algorithm. This class provides smooth
    /// interpolation between multiple quaternion control points by recursively
    /// blending them with spherical linear interpolation (Slerp).
    /// </summary>
    public sealed class QuaternionBezierCurveImpl
        : DeCasteljauBezierCurveInterpolator<Quaternion>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="QuaternionBezierCurveImpl"/> class with the specified
        /// list of quaternion control points.
        /// </summary>
        /// <param name="nodes">
        /// The list of <see cref="Quaternion"/> control points to use for
        /// generating the Bezier curve.
        /// </param>
        public QuaternionBezierCurveImpl(IList<Quaternion> nodes) : base(nodes)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override Quaternion BlendPair(
            Quaternion pointA,
            Quaternion pointB,
            float t)
        {
            return Quaternion.Slerp(pointA, pointB, t);
        }

        #endregion Overrides

        #endregion Methods
    }
}
