/*------------------------------------------------------------------------------
File:       ColorRGBLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using an linear interpolation across all
            values of the Color class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 18:44:55 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements the naive linear interpolation of all 
    /// values in the Color struct.
    /// </summary>
    public class ColorRGBLerpImpl : IInterpolation<Color>
    {
        #region Methods

        #region IInterpolation Implemenation
        
        /// <inheritdoc/>
        public Color Interpolate(in Color start, in Color end, float progress)
        {
            return Color.Lerp(start, end, progress);
        }

        #endregion IInterpolation Implemenation

        #endregion Methods
    }
}