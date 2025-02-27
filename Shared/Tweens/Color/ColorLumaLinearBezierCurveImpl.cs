/*------------------------------------------------------------------------------
File:       ColorLumaLinearBezierCurveImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Bezier curve interpolation in the linear color space 
            while factoring in the intensity.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-27 13:02:05 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Class that implements a Bezier curve interpolation in the linear color 
    /// space while factoring in the intensity.
    /// space.
    /// </summary>
    public sealed class ColorLumaLinearBezierCurveImpl
        : BezierCurveInterpolator<Color>
    {
        #region Constants

        private const float _gamma = 0.43f;
        private const float _inverseGamma = 1 / _gamma;
        private const float _threshold = 1e-6f;

        #endregion Constants

        #region Fields

        private List<Color> _linearNodes = new();
        private List<float> _brightnesses = new();

        #endregion Fields

        #region Methods

        /// <summary>
        /// Constructor for the ColorRGBBezierCurveImpl class, which implements 
        /// a Bezier curve interpolation in the linear color space while 
        /// factoring in the intensity.
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
            if(Nodes.Count != _nodeCount) { RebuildNodes(); }
            if(_nodeCount == 0) { return default; }

            GenerateInterpolationMultipliers(progress, 1 - progress);

            var color = MultiplyBy(_linearNodes[0], TempMults[0]);
            var brightness = _brightnesses[0] * TempMults[0]; 
            for(var index = 1; index < _nodeCount; ++index)
            {
                AddTo(ref color, MultiplyBy(_linearNodes[index], TempMults[index]));
                brightness += _brightnesses[index] * TempMults[index];
            }

            var sum = color.r + color.g + color.b;
            if(sum <= _threshold) { return color; } // Lerped to black.
            
            var intensity = Mathf.Pow(brightness, _inverseGamma);
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
            while(_linearNodes.Count < _nodeCount)
            {
                _linearNodes.Add(default);
                _brightnesses.Add(default);
            }

            for(var index = 0; index < _nodeCount; ++index)
            {
                var node = _linearNodes[index] = Nodes[index].linear;
                _brightnesses[index] = Mathf.Pow(node.r + node.g + node.b, _gamma);
            }
        }

        #endregion Methods
    }
}
