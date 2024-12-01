/*------------------------------------------------------------------------------
  File:           Easings.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains easing functions that convert a range [0-1] into
                    their associated curve.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-01 10:31:25 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    public static class Easings
    {
        public static float Linear(float percentage)
        {
            return percentage;
        }

        public static Func<float, float> EaseIn(float power) 
        {
            return (value) => { return Mathf.Pow(value, power); };
        }
    }
}