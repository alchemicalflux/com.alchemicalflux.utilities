/*------------------------------------------------------------------------------
File:       Vector2BezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation for Vector2 values. This
            class provides smooth interpolation between multiple Vector2 nodes
            using the Bernstein polynomial formulation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Implements a Bezier curve interpolation for <see cref="Vector2"/>
    /// values. This class provides smooth interpolation between multiple
    /// <see cref="Vector2"/> nodes using the Bernstein polynomial formulation.
    /// </summary>
    public sealed class Vector2BezierCurveImpl
        : PolynomialBezierCurveInterpolator<Vector2>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="Vector2BezierCurveImpl"/> class with the specified list
        /// of <see cref="Vector2"/> nodes.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of <see cref="Vector2"/> nodes for generating
        /// the Bezier curve.
        /// </param>
        public Vector2BezierCurveImpl(IList<Vector2> nodes) : base(nodes)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override void AddTo(ref Vector2 result, Vector2 node)
        {
            result += node;
        }

        /// <inheritdoc />
        protected override Vector2 MultiplyBy(Vector2 node, float progress)
        {
            return node * progress;
        }

        #endregion Overrides

        #endregion Methods
    }
}