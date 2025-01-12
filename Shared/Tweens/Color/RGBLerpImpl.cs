/*------------------------------------------------------------------------------
File:       RGBLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using an linear interpolation across all
            values of the Color class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 12:03:42 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements the naive linear interpolation of all 
    /// values in the Color struct.
    /// </summary>
    public class RGBLerpImpl : ILerpImplementation<Color>
    {
        #region Methods

        #region ILerpImpementation Implemenation
        
        /// <inheritdoc/>
        public Color Lerp(in Color start, in Color end, float progress)
        {
            return Color.Lerp(start, end, progress);
        }

        #endregion ILerpImpementation Implemenation

        #endregion Methods
    }
}