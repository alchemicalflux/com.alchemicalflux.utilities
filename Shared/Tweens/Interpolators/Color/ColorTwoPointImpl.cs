/*------------------------------------------------------------------------------
File:       ColorTwoPointImpl.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for two-point color interpolators. Provides
            validation and limiting logic for color interpolation progress.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-21 08:02:05 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for two-point color interpolators.
    /// Provides validation and limiting logic for color interpolation progress.
    /// </summary>
    public abstract class ColorTwoPointImpl : TwoPointInterpolator<Color>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorTwoPointImpl"/>
        /// class with the specified start and end colors.
        /// </summary>
        /// <param name="start">
        /// The initial color value for interpolation.
        /// </param>
        /// <param name="end">The final color value for interpolation.</param>
        public ColorTwoPointImpl(Color start, Color end) : base(start, end)
        {
        }

        #region Overrides

        /// <inheritdoc />
        protected override bool CheckAndLimitProgress(
            ref float progress,
            ref Color failValue)
        {
            if(float.IsNaN(progress))
            {
                failValue = Color.clear;
                return false;
            }
            return true;
        }

        #endregion Overrides
    }
}
