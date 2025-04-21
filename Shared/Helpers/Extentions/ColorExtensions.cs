/*------------------------------------------------------------------------------
File:       ColorExtensions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Helper functions to extend the Color class.
            Details for algorithm implementation:
                https://stackoverflow.com/questions/596216/formula-to-determine-perceived-brightness-of-rgb-color
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 23:55:24 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Constants;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Stores extensions for the <see cref="Color"/> class.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Returns the luminance of a color using the Rec. 709 formula.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>Luminance value of the color.</returns>
        public static float Luminance(this Color color)
        {
            // Rec. 709 luminance formula
            Color linearColor = color.linear;
            return 0.2126f * linearColor.r +
                   0.7152f * linearColor.g +
                   0.0722f * linearColor.b;
        }

        /// <summary>
        /// Determines if two colors are approximately equal within a threshold.
        /// </summary>
        /// <param name="color">The base color.</param>
        /// <param name="toTest">The color to compare against.</param>
        /// <returns>
        /// True if the colors are approximately equal; otherwise, false.
        /// </returns>
        public static bool IsApproximately(this Color color, Color toTest)
        {
            return Mathf.Abs(toTest.r - color.r) <= ColorConstants.Threshold &&
                   Mathf.Abs(toTest.g - color.g) <= ColorConstants.Threshold &&
                   Mathf.Abs(toTest.b - color.b) <= ColorConstants.Threshold &&
                   Mathf.Abs(toTest.a - color.a) <= ColorConstants.Threshold;
        }
    }
}
