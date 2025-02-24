/*------------------------------------------------------------------------------
File:       Vector3BezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Vector3 Bezier curve interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-24 02:00:04 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Vector3 Bezier curve interpolation.
    /// </summary>
    public sealed class Vector3BezierCurveImpl 
        : BezierCurveInterpolator<Vector3>
    {
        #region Methods

        /// <summary>
        /// Constructor for the Vector3BezierCurveImpl class, which implements a
        /// Vector3 Bezier curve interpolation.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public Vector3BezierCurveImpl(IList<Vector3> nodes)
        {
            Nodes = nodes;
        }

        /// <inheritdoc />
        protected override void AddTo(ref Vector3 result, Vector3 node)
        {
            result += node;
        }

        /// <inheritdoc />
        protected override Vector3 MultiplyBy(Vector3 node, float progress)
        {
            return node * progress;
        }

        #endregion Methods
    }
}