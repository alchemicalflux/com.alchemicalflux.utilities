/*------------------------------------------------------------------------------
File:       ColorRGBLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using a linear interpolation across all
            values of the Color class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 06:05:04 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Color lerp class that implements the naive linear interpolation of all 
    /// RGB values in the Color struct.
    /// </summary>
    public sealed class ColorRGBLerpImpl : TwoPointInterpolator<Color>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the ColorRGBLerpImpl class, which 
        /// implements color interpolation using RGB (Red, Green, Blue) color 
        /// space.
        /// </summary>
        /// <param name="start">The initial color for the interpolation.</param>
        /// <param name="end">The final color for the interpolation.</param>
        public ColorRGBLerpImpl(Color start, Color end) :
            base(start, end)
        {
        }

        /// <inheritdoc />
        public override Color Interpolate(float progress)
        {
            // Handle NaN progress values by returning a default color.
            if(float.IsNaN(progress)) { return Color.clear; }
            return Color.Lerp(Start, End, progress);
        }

        #endregion Methods
    }
}
