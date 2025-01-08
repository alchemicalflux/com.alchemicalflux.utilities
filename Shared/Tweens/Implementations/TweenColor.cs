/*------------------------------------------------------------------------------
File:       TweenColor.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a two point tween for the Color class.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
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
    [System.Serializable]
    public class TweenColor : BasicTween<Color>
    {
        /// <inheritdoc />
        public override bool ApplyProgress(float progress)
        {
            if(base.ApplyProgress(progress)) { return true; }

            Color.RGBToHSV(Start, out float h1, out float s1, out float v1);
            Color.RGBToHSV(End, out float h2, out float s2, out float v2);

            // Slerp the hue (ensure the shortest path on the color wheel).
            float h = Mathf.LerpAngle(h1 * 360f, h2 * 360f, progress);
            h = Mathf.Repeat(h, 360f) / 360f;

            // Transition the saturation and value consistently between colors.
            s1 = Mathf.Lerp(s1, s2, progress);
            v1 = Mathf.Lerp(v1, v2, progress);

            var color = Color.HSVToRGB(h, s1, v1);
            color.a = Mathf.Lerp(Start.a, End.a, progress);

            OnUpdate?.Invoke(color);
            return false;
        }
    }
}
