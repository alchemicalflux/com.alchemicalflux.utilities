/*------------------------------------------------------------------------------
File:       HSVLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the HSV color space to improve from
            RGB Color transition.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 12:03:42 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to HSV to RGB conversion,
    /// performing the lerp in HSV color space.
    /// </summary>
    public class HSVLerpImpl : ILerpImplementation<Color>
    {
        #region Methods

        #region ILerpImpementation Implemenation

        /// <inheritdoc/>
        public Color Lerp(in Color start, in Color end, float progress)
        {
            Color.RGBToHSV(start, out float h1, out float s1, out float v1);
            Color.RGBToHSV(end, out float h2, out float s2, out float v2);

            // Slerp the hue (ensure the shortest path on the color wheel).
            float h = Mathf.LerpAngle(h1 * 360f, h2 * 360f, progress);
            h = Mathf.Repeat(h, 360f) / 360f;

            // Transition the saturation and value consistently between colors.
            s1 = Mathf.Lerp(s1, s2, progress);
            v1 = Mathf.Lerp(v1, v2, progress);

            var color = Color.HSVToRGB(h, s1, v1);
            color.a = Mathf.Lerp(start.a, end.a, progress);
            return color;
        }

        #endregion ILerpImpementation Implemenation

        #endregion Methods
    }
}