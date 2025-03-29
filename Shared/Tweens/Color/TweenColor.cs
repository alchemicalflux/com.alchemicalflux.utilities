/*------------------------------------------------------------------------------
File:       TweenColor.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a two point tween for the Color class.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 18:44:55 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// A specific implementation of a tween that interpolates between two 
    /// `Color` values, smoothly transitioning the color over time. This class 
    /// overrides the `ApplyProgress` method to provide logic for tweening 
    /// colors, including hue, saturation, and value transitions.
    /// </summary>
    public sealed class TweenColor : BasicTween<Color>
    {
        #region Properties

        public override Color Start { get; set; }
        public override Color End { get; set; }

        private IInterpolation<Color> _interpolation { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Constructor for the TweenColor class.
        /// </summary>
        /// <param name="lerp">
        /// Linear interpolation that will be traversed using a range of [0-1].
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when lerp arguement is null.
        /// </exception>
        public TweenColor(IInterpolation<Color> lerp)
        {
            if(lerp == null) { throw new ArgumentNullException(nameof(lerp)); }
            _interpolation = lerp;
        }

        /// <inheritdoc />
        public override bool ApplyProgress(float progress)
        {
            if(base.ApplyProgress(progress)) { return true; }

            OnUpdate?.Invoke(_interpolation.Interpolate(Start, End, progress));
            return false;
        }

        #endregion Methods
    }
}
