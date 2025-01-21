/*------------------------------------------------------------------------------
File:       ColorHSVLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the HSV color space to improve from
            RGB Color transition.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 16:48:58 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to HSV to RGB conversion,
    /// performing the lerp in HSV color space.
    /// </summary>
    public class ColorHSVLerpImpl : TwoPointInterpolator<Color>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the ColorHSVLerpImpl class, which 
        /// implements color interpolation using HSV (Hue, Saturation, Value) 
        /// color space.
        /// </summary>
        /// <param name="start">The initial color for the interpolation.</param>
        /// <param name="end">The final color for the interpolation.</param>
        public ColorHSVLerpImpl(Color start, Color end) :
            base(start, end)
        {
        }

        #region TwoPointInterpolator Implemenation

        /// <inheritdoc/>
        public override Color Interpolate(float progress)
        {
            Color.RGBToHSV(Start, out float h1, out float s1, out float v1);
            Color.RGBToHSV(End, out float h2, out float s2, out float v2);

            // Slerp the hue (ensure the shortest path on the color wheel).
            float h = Mathf.LerpAngle(h1 * 360f, h2 * 360f, progress);
            h = Mathf.Repeat(h, 360f) / 360f;

            // Transition the saturation and value consistently between colors.
            s1 = Mathf.Lerp(s1, s2, progress);
            v1 = Mathf.Lerp(v1, v2, progress);

            var color = Color.HSVToRGB(h, s1, v1);
            color.a = Mathf.Lerp(Start.a, End.a, progress);
            return color;
        }

        #endregion TwoPointInterpolator Implemenation

        #endregion Methods
    }
}