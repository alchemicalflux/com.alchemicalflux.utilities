/*------------------------------------------------------------------------------
File:       ColorBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for Bezier curve interpolators that operate on
            Color values using the Bernstein polynomial formulation. Provides
            the structure for interpolating between a set of Color nodes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for Bezier curve interpolators that operate on
    /// <see cref="Color"/> values using the Bernstein polynomial formulation.
    /// Provides the structure for interpolating between a set of
    /// <see cref="Color"/> nodes.
    /// </summary>
    public abstract class ColorBezierCurveImpl
        : PolynomialBezierCurveInterpolator<Color>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorBezierCurveImpl"/>
        /// class with the specified list of <see cref="Color"/> nodes.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public ColorBezierCurveImpl(IList<Color> nodes) : base(nodes)
        {
        }

        #region BezierCurveInterpolator Implementaion

        /// <inheritdoc />
        protected override bool CheckAndLimitProgress(
            ref float progress,
            out Color failValue)
        {
            if(float.IsNaN(progress))
            {
                failValue = Color.clear;
                return true;
            }
            else if(Nodes.Count == 0)
            {
                throw new System.InvalidOperationException(
                    $"{nameof(Nodes.Count)} cannot be zero.");
            }
            else if(Nodes.Count == 1)
            {
                failValue = Nodes[0];
                return true;
            }

            progress = Mathf.Clamp01(progress);
            failValue = Color.clear;
            return false;
        }

        #endregion BezierCurveInterpolator Implementaion

        #region PolynomialBezierCurveInterpolator Implementation

        /// <inheritdoc />
        protected override void AddTo(ref Color result, Color node)
        {
            result += node;
        }

        /// <inheritdoc />
        protected override Color MultiplyBy(Color node, float progress)
        {
            return node * progress;
        }

        #endregion PolynomialBezierCurveInterpolator Implementation

        #endregion Methods
    }
}
