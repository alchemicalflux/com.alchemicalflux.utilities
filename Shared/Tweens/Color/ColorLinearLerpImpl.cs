/*------------------------------------------------------------------------------
File:       ColorLinearLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the linear color space to improve from
            RGB Color transition.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 18:44:55 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to linear to RGB conversion,
    /// performing the lerp in linear color space.
    /// </summary>
    public class ColorLinearLerpImpl : IInterpolation<Color>
    {
        #region Methods

        #region IInterpolation Implemenation

        /// <inheritdoc/>
        public Color Interpolate(in Color start, in Color end, float progress)
        {
            return Color.Lerp(start.linear, end.linear, progress).gamma;
        }

        #endregion IInterpolation Implemenation

        #endregion Methods
    }
}