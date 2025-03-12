/*------------------------------------------------------------------------------
File:       Vector2BezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector2 Bezier curve interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 22:41:08 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Vector2 Bezier curve interpolation.
    /// </summary>
    public sealed class Vector2BezierCurveImpl
        : BezierCurveInterpolator<Vector2>
    {
        #region Methods

        /// <summary>
        /// Constructor for the Vector2BezierCurveImpl class, which implements a
        /// Vector2 Bezier curve interpolation.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public Vector2BezierCurveImpl(IList<Vector2> nodes) : base(nodes)
        {
        }

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

        #endregion Methods
    }
}