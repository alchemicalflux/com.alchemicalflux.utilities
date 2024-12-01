/*------------------------------------------------------------------------------
  File:           TweenColor.cs 
  Project:        AlchemicalFlux Utilities
  Description:    
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-01 12:57:09 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    public class TweenColor : BasicTween<Color>
    {
        /// <inheritdoc />
        public override bool ApplyProgress(float progress)
        {
            if(!base.ApplyProgress(progress))
            {
                OnUpdate?.Invoke(Start);
                return false; 
            }

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
            return true;
        }
    }
}
