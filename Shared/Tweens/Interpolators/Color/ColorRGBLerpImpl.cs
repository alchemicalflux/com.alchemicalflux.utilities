/*------------------------------------------------------------------------------
File:       ColorRGBLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using a linear interpolation across all
            values of the Color class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Color lerp class that implements the naive linear interpolation of all 
    /// RGB values in the Color struct.
    /// </summary>
    public sealed class ColorRGBLerpImpl : ColorTwoPointImpl
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

        #region Overrides

        /// <inheritdoc />
        protected override Color ProcessInterpolation(float progress)
        {
            return Color.Lerp(Start, End, progress);
        }

        #endregion Overrides

        #endregion Methods
    }
}
