/*------------------------------------------------------------------------------
File:       ColorRGBLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using an linear interpolation across all
            values of the Color class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-03 01:59:43 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements the naive linear interpolation of all 
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

        #region TwoPointInterpolator Implemenation

        /// <inheritdoc/>
        public override Color Interpolate(float progress)
        {
            return Color.Lerp(Start, End, progress);
        }

        #endregion TwoPointInterpolator Implemenation

        #endregion Methods
    }
}