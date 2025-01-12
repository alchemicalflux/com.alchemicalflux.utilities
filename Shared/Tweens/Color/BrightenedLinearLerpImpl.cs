/*------------------------------------------------------------------------------
File:       BrightenedLinearLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the linear color space and color
            intensity to improve from RGB Color transition.
            Details for algorithm implementation:
                https://stackoverflow.com/questions/22607043/color-gradient-algorithm
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 12:03:42 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to linear to RGB conversion,
    /// performing the lerp in linear color space and factoring in the intensity
    /// to create a smoother transition for light/dark transitions.
    /// </summary>
    public class BrightenedLinearLerpImpl : ILerpImplementation<Color>
    {
        #region Constants

        private const float Gamma = 0.43f;
        private const float InverseGamma = 1 / Gamma;

        #endregion Constants

        #region Methods

        #region ILerpImpementation Implemenation

        /// <inheritdoc/>
        public Color Lerp(in Color start, in Color end, float progress)
        {
            var sLin = start.linear;
            var eLin = end.linear;
            var color = Color.Lerp(sLin, eLin, progress);

            var sum = color.r + color.g + color.b;
            if(sum != 0)
            {
                var bright1 = Mathf.Pow(sLin.r + sLin.g + sLin.b, Gamma);
                var bright2 = Mathf.Pow(eLin.r + eLin.g + eLin.b, Gamma);
                var brightness = Mathf.Lerp(bright1, bright2, progress);
                var intensity = Mathf.Pow(brightness, InverseGamma);

                var factor = intensity / sum;
                color.r *= factor;
                color.g *= factor;
                color.b *= factor;
            }

            return color.gamma;
        }

        #endregion ILerpImpementation Implemenation

        #endregion Methods
    }
}