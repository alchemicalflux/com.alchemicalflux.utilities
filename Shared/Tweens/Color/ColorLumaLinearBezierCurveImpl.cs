/*------------------------------------------------------------------------------
File:       ColorLumaLinearBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation in the linear color space 
            while factoring in the intensity.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-12 00:48:47 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    using Constants = Constants.ColorConstants;

    /// <summary>
    /// Class that implements a Bezier curve interpolation in the linear color 
    /// space while factoring in the intensity.
    /// </summary>
    public sealed class ColorLumaLinearBezierCurveImpl
        : BezierCurveInterpolator<Color>
    {
        #region Fields

        private List<Color> _linearNodes = new();
        private List<float> _brightnesses = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Constructor for the ColorLumaLinearBezierCurveImpl class, which 
        /// implements a Bezier curve interpolation in the linear color space 
        /// while factoring in the intensity.
        /// </summary>
        /// <param name="nodes">
        /// Reference to the list of nodes for generating the Bezier curve.
        /// </param>
        public ColorLumaLinearBezierCurveImpl(IList<Color> nodes) : base(nodes)
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

        /// <inheritdoc />
        public override Color Interpolate(float progress)
        {
            if(Nodes.Count != NodeCount) { RebuildNodes(); }
            if(NodeCount == 0) { return default; }
            if(NodeCount == 1) { return Nodes[0]; }

            GenerateInterpolationMultipliers(progress, 1 - progress);

            var color = MultiplyBy(_linearNodes[0], TempMults[0]);
            var brightness = _brightnesses[0] * TempMults[0];
            for(var index = 1; index < NodeCount; ++index)
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

        /// <inheritdoc />
        protected override void RebuildNodes()
        {
            base.RebuildNodes();
            while(_linearNodes.Count < NodeCount) // Ensure max size matches.
            {
                _linearNodes.Add(default);
                _brightnesses.Add(default);
            }

            for(var index = 0; index < NodeCount; ++index)
            {
                var node = _linearNodes[index] = Nodes[index].linear;
                _brightnesses[index] = 
                    Mathf.Pow(node.r + node.g + node.b, Constants.Gamma);
            }
        }

        #endregion Methods
    }
}
