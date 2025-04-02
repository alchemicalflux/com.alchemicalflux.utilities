/*------------------------------------------------------------------------------
File:       ColorRGBBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation in the RGB Color space.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-01 19:18:22 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Bezier curve interpolation in the RGB Color 
    /// space.
    /// </summary>
    public sealed class ColorRGBBezierCurveImpl
        : BezierCurveInterpolator<Color>
    {
        #region Methods

        /// <summary>
        /// Constructor for the ColorRGBBezierCurveImpl class, which implements 
        /// a Bezier curve interpolation in the RGB Color space.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public ColorRGBBezierCurveImpl(IList<Color> nodes)
            : base(nodes)
        {
        }

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

        #endregion Methods
    }
}