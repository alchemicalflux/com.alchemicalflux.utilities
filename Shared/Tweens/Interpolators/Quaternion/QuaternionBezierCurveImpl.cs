/*------------------------------------------------------------------------------
File:       QuaternionBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation for Quaternion values. This
            class provides smooth interpolation between multiple quaternion
            nodes using spherical linear interpolation (Slerp) at each recursive
            level.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:03:35 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Implements a Bezier curve interpolation for <see cref="Quaternion"/>
    /// values. This class provides smooth interpolation between multiple
    /// quaternion nodes using spherical linear interpolation (Slerp) at each
    /// recursive level.
    /// </summary>
    public sealed class QuaternionBezierCurveImpl
        : BezierCurveInterpolator<Quaternion>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="QuaternionBezierCurveImpl"/> class with the specified
        /// list of quaternion nodes.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of <see cref="Quaternion"/> nodes for
        /// generating the Bezier curve.
        /// </param>
        public QuaternionBezierCurveImpl(IList<Quaternion> nodes) : base(nodes)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override Quaternion ProcessInterpolation(float progress)
        {
            return BezierSlerp(Nodes, progress);
        }

        #endregion Overrides

        /// <summary>
        /// Recursively computes the Bezier interpolation of a list of
        /// <see cref="Quaternion"/> points using spherical linear interpolation
        /// (Slerp).
        /// </summary>
        /// <param name="points">
        /// The list of <see cref="Quaternion"/> control points.
        /// </param>
        /// <param name="t">
        /// The interpolation parameter, typically in the range [0, 1].
        /// </param>
        /// <returns>
        /// The interpolated <see cref="Quaternion"/> at the specified parameter
        /// <paramref name="t"/>.
        /// </returns>
        private Quaternion BezierSlerp(IList<Quaternion> points, float t)
        {
            if(points.Count == 1) { return points[0]; }

            // Store and rebuild the temp lists.
            var nextLevel = new List<Quaternion>(points.Count - 1);
            for(int i = 0; i < points.Count - 1; i++)
            {
                nextLevel.Add(Quaternion.Slerp(points[i], points[i + 1], t));
            }
            return BezierSlerp(nextLevel, t);
        }

        #endregion Methods
    }
}
