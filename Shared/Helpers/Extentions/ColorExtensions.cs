/*------------------------------------------------------------------------------
File:       ColorExtensions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Helper functions to extend the Color class.
            Details for algorithm implementation:
                https://stackoverflow.com/questions/596216/formula-to-determine-perceived-brightness-of-rgb-color
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-02 19:42:56 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides extension methods for the <see cref="Color"/> class.
    /// </summary>
    public static class ColorExtensions
    {
        /// <summary>
        /// Calculates the luminance of a color using the Rec. 709 formula.
        /// </summary>
        /// <param name="color">
        /// The color for which to calculate luminance.
        /// </param>
        /// <returns>
        /// A <see cref="float"/> representing the luminance of the color.
        /// </returns>
        public static float Luminance(this Color color)
        {
            // Rec. 709 luminance formula
            Color linearColor = color.linear;
            return 0.2126f * linearColor.r +
                   0.7152f * linearColor.g +
                   0.0722f * linearColor.b;
        }

        /// <summary>
        /// Determines whether two <see cref="Color"/> instances are
        /// approximately equal.
        /// </summary>
        /// <param name="color">The base color.</param>
        /// <param name="toTest">The color to compare against.</param>
        /// <returns>
        /// <c>true</c> if the colors are approximately equal;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsApproximately(this Color color, Color toTest)
        {
            return color.r.Approximately(toTest.r) &&
                   color.g.Approximately(toTest.g) &&
                   color.b.Approximately(toTest.b) &&
                   color.a.Approximately(toTest.a);
        }
    }
}
