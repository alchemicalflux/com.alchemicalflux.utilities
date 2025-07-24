/*------------------------------------------------------------------------------
File:       ColorLumaLinearBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation in the linear color space
            while factoring in the intensity (luma). This class converts all
            input nodes to linear color space and computes brightness for each
            node, then interpolates both color and brightness using the
            Bernstein polynomial formulation. The final color is gamma-corrected
            and intensity-adjusted to preserve perceived brightness.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    using Constants = Constants.ColorConstants;

    /// <summary>
    /// Implements a Bezier curve interpolation in the linear color space
    /// while factoring in the intensity (luma).
    /// This class converts all input nodes to linear color space and
    /// computes brightness for each node, then interpolates both color and
    /// brightness using the Bernstein polynomial formulation.
    /// The final color is gamma-corrected and intensity-adjusted to
    /// preserve perceived brightness.
    /// </summary>
    public sealed class ColorLumaLinearBezierCurveImpl : ColorBezierCurveImpl
    {
        #region Fields

        /// <summary>
        /// The list of color nodes converted to linear color space.
        /// </summary>
        private List<Color> _linearNodes = new();

        /// <summary>
        /// The list of brightness values (luma) for each node, precomputed
        /// in linear space and gamma-encoded for interpolation.
        /// </summary>
        private List<float> _brightnesses = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ColorLumaLinearBezierCurveImpl"/> class with the
        /// specified list of <see cref="Color"/> nodes.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public ColorLumaLinearBezierCurveImpl(IList<Color> nodes) : base(nodes)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override void RebuildNodes()
        {
            base.RebuildNodes();
            while(_linearNodes.Count < Nodes.Count) // Ensure max size matches.
            {
                _linearNodes.Add(default);
                _brightnesses.Add(default);
            }

            for(var index = 0; index < Nodes.Count; ++index)
            {
                var node = _linearNodes[index] = Nodes[index].linear;
                _brightnesses[index] =
                    Mathf.Pow(node.r + node.g + node.b, Constants.Gamma);
            }
        }

        /// <inheritdoc />
        protected override Color ProcessInterpolation(float progress)
        {
            GenerateInterpolationMultipliers(progress, 1 - progress);

            var color = MultiplyBy(_linearNodes[0], TempMults[0]);
            var brightness = _brightnesses[0] * TempMults[0];
            for(var index = 1; index < Nodes.Count; ++index)
            {
                AddTo(ref color,
                    MultiplyBy(_linearNodes[index], TempMults[index]));
                brightness += _brightnesses[index] * TempMults[index];
            }

            var sum = color.r + color.g + color.b;
            if(sum <= Constants.Threshold) { return color; } // Lerped to black.

            var intensity = Mathf.Pow(brightness, Constants.InverseGamma);
            var factor = intensity / sum;
            color.r *= factor;
            color.g *= factor;
            color.b *= factor;
            return color.gamma;
        }

        #endregion Overrides

        #endregion Methods
    }
}