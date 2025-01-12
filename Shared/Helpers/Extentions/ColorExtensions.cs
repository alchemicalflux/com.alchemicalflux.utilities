/*------------------------------------------------------------------------------
File:       ColorExtensions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Helper functions to extend the Color class.
            Details for algorithm implementation:
                https://stackoverflow.com/questions/596216/formula-to-determine-perceived-brightness-of-rgb-color
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 14:35:05 
------------------------------------------------------------------------------*/
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
    }
}
