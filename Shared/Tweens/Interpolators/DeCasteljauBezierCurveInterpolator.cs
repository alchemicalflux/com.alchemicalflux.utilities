/*------------------------------------------------------------------------------
File:       DeCasteljauBezierCurveInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides an abstract base for Bezier curve interpolation using
            De Casteljau's algorithm. This class recursively blends control
            points using a type-specific interpolation method, allowing derived
            classes to define the blending logic for each pair of points.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-31 12:00:08 
------------------------------------------------------------------------------*/
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides an abstract base for Bezier curve interpolation using
    /// De Casteljau's algorithm. This class recursively blends control
    /// points using a type-specific interpolation method, allowing
    /// derived classes to define the blending logic for each pair of
    /// points.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of the control points to interpolate.
    /// </typeparam>
    public abstract class DeCasteljauBezierCurveInterpolator<TType>
        : BezierCurveInterpolator<TType>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DeCasteljauBezierCurveInterpolator{TType}"/> class
        /// with an empty node list.
        /// </summary>
        public DeCasteljauBezierCurveInterpolator() : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="DeCasteljauBezierCurveInterpolator{TType}"/> class
        /// with the specified list of control points.
        /// </summary>
        /// <param name="nodes">
        /// The list of control points to use for the Bezier curve.
        /// </param>
        public DeCasteljauBezierCurveInterpolator(IList<TType> nodes)
            : base(nodes)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override void RebuildNodes()
        {
        }

        /// <inheritdoc />
        protected override TType ProcessInterpolation(float progress)
        {
            return EvaluateRecursive(Nodes, progress);
        }

        #endregion Overrides

        /// <summary>
        /// Blends two control points at the specified interpolation parameter.
        /// Derived classes must implement this to define how two points are
        /// interpolated (e.g., linear, spherical, etc.).
        /// </summary>
        /// <param name="pointA">The first control point.</param>
        /// <param name="pointB">The second control point.</param>
        /// <param name="t">
        /// The interpolation parameter, typically in the range [0, 1].
        /// </param>
        /// <returns>
        /// The interpolated value between <paramref name="pointA"/> and
        /// <paramref name="pointB"/> at <paramref name="t"/>.
        /// </returns>
        protected abstract TType BlendPair(TType pointA, TType pointB, float t);

        /// <summary>
        /// Recursively evaluates the Bezier curve at the specified parameter
        /// using De Casteljau's algorithm.
        /// </summary>
        /// <param name="points">
        /// The list of control points for the current recursion level.
        /// </param>
        /// <param name="t">
        /// The interpolation parameter, typically in the range [0, 1].
        /// </param>
        /// <returns>
        /// The interpolated value at the given parameter.
        /// </returns>
        protected virtual TType EvaluateRecursive(IList<TType> points, float t)
        {
            if(points.Count == 1) { return points[0]; }

            // Store and rebuild the temp lists.
            var nextLevel = new List<TType>(points.Count - 1);
            for(int i = 0; i < points.Count - 1; i++)
            {
                nextLevel.Add(BlendPair(points[i], points[i + 1], t));
            }
            return EvaluateRecursive(nextLevel, t);
        }

        #endregion Methods
    }
}
