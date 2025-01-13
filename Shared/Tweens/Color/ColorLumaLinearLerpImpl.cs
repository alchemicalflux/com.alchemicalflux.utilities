/*------------------------------------------------------------------------------
File:       ColorLumaLinearLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the linear color space and color
            intensity to improve from RGB Color transition.
            Details for algorithm implementation:
                https://stackoverflow.com/questions/22607043/color-gradient-algorithm
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 18:44:55 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to linear to RGB conversion,
    /// performing the lerp in linear color space and factoring in the intensity
    /// to create a smoother transition for light/dark transitions.
    /// </summary>
    public class ColorLumaLinearLerpImpl : IInterpolation<Color>
    {
        #region Constants

        private const float _gamma = 0.43f;
        private const float _inverseGamma = 1 / _gamma;
        private const float _threshold = 1e-6f;

        #endregion Constants

        #region Methods

        #region IInterpolation Implemenation

        /// <inheritdoc/>
        public Color Interpolate(in Color start, in Color end, float progress)
        {
            var sLinear = start.linear;
            var eLinear = end.linear;

            var sAproxLuminance = sLinear.r + sLinear.g + sLinear.b;
            var eAproxLuminace = eLinear.r + eLinear.g + eLinear.b;

            // Return early if both linear colors are black.
            if(sAproxLuminance <= _threshold && 
                eAproxLuminace <= _threshold) { return start; }

            var color = Color.Lerp(sLinear, eLinear, progress);
            var sum = color.r + color.g + color.b;

            if(sum <= _threshold) { return color; } // Lerped to black.

            var bright1 = Mathf.Pow(sAproxLuminance, _gamma);
            var bright2 = Mathf.Pow(eAproxLuminace, _gamma);
            var brightness = Mathf.Lerp(bright1, bright2, progress);
            var intensity = Mathf.Pow(brightness, _inverseGamma);

            var factor = intensity / sum;
            color.r *= factor;
            color.g *= factor;
            color.b *= factor;

            return color.gamma;
        }

        #endregion IInterpolation Implemenation

        #endregion Methods
    }
}