/*------------------------------------------------------------------------------
File:       ColorLinearLerpImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a Color lerp using the linear color space to improve from
            RGB Color transition.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 16:48:58 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Coler lerp class that implements a RGB to linear to RGB conversion,
    /// performing the lerp in linear color space.
    /// </summary>
    public class ColorLinearLerpImpl : TwoPointInterpolator<Color>
    {
        #region Methods

        /// <summary>
        /// Initializes a new instance of the ColorLinearLerpImpl class, which 
        /// implements color interpolation using linear color space.
        /// </summary>
        /// <param name="start">The initial color for the interpolation.</param>
        /// <param name="end">The final color for the interpolation.</param>
        public ColorLinearLerpImpl(Color start, Color end) :
            base(start, end)
        {
        }

        #region TwoPointInterpolator Implemenation

        /// <inheritdoc/>
        public override Color Interpolate(float progress)
        {
            return Color.Lerp(Start, End, progress).gamma;
        }

        #endregion TwoPointInterpolator Implemenation

        #endregion Methods
    }
}