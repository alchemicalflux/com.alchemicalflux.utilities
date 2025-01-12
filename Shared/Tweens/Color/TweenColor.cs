/*------------------------------------------------------------------------------
File:       TweenColor.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a two point tween for the Color class.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 13:37:27 
------------------------------------------------------------------------------*/
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
        public override Color Start { get; set; }
        public override Color End { get; set; }

        private ILerpImplementation<Color> LerpImplementation { get; set; }

        public TweenColor(ILerpImplementation<Color> lerp)
        {
            LerpImplementation = lerp;
        }

        /// <inheritdoc />
        public override bool ApplyProgress(float progress)
        {
            if(base.ApplyProgress(progress)) { return true; }

            OnUpdate?.Invoke(LerpImplementation.Lerp(Start, End, progress));
            return false;
        }
    }
}
