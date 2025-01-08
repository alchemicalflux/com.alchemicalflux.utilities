/*------------------------------------------------------------------------------
File:       Easings.cs 
Project:    AlchemicalFlux Utilities
Overview:   Contains easing functions that convert a range [0-1] into their 
            associated curve.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// A collection of static easing functions commonly used for animation or 
    /// interpolation. Easing functions adjust the progress of a value to create
    /// more natural, non-linear motion effects.
    /// </summary>
    public static class Easings
    {
        #region Linear Easings

        /// <summary>
        /// A linear easing function that returns the percentage unchanged.
        /// This produces a constant rate of change over time.
        /// </summary>
        /// <param name="percentage">
        /// The input value in the range [0, 1] representing the progress.
        /// </param>
        /// <returns>
        /// The same value as the input percentage, producing a linear
        /// transition.
        /// </returns>
        public static float Linear(float percentage)
        {
            return percentage;
        }

        #endregion Linear Easings

        #region Mononomial Easings

        /// <summary>
        /// EaseIn applies a power function to the value, easing in from the 
        /// start. The rate of change starts slow and then accelerates as 
        /// progress increases.
        /// </summary>
        /// <param name="power">
        /// The power to which the progress is raised (determines the curve).
        /// </param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static Func<float, float> EaseIn(float power) 
        {
            return (value) => { return Mathf.Pow(value, power); };
        }

        /// <summary>
        /// EaseOut applies a power function to the inverse of the value, easing
        /// out toward the end. The rate of change starts fast and decelerates 
        /// as progress increases.
        /// </summary>
        /// <param name="power">
        /// The power to which the inverted progress is raised (determines the 
        /// curve).
        /// </param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static Func<float, float> EaseOut(float power)
        {
            return (value) => { return 1 - Mathf.Pow(1 - value, power); };
        }

        /// <summary>
        /// EaseInOut combines both EaseIn and EaseOut behaviors, easing in at 
        /// the start and easing out at the end. The rate of change starts slow,
        /// accelerates in the middle, and decelerates towards the end.
        /// </summary>
        /// <param name="power">
        /// The power to which the progress is raised (determines the curve).
        /// </param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static Func<float, float> EaseInOut(float power)
        {
            return (value) =>
            {
                if(value < .5) 
                { 
                    return Mathf.Pow(2, power - 1) * Mathf.Pow(value, power); 
                }
                return 1 - Mathf.Pow(-2 * value + 2, power) / 2;
            };
        }

        #endregion Mononomial Easings

        #region Exponential Easings

        /// <summary>
        /// EaseInExponential applies an exponential function that accelerates 
        /// quickly at the start. The rate of change starts slow but increases 
        /// sharply as progress increases.
        /// </summary>
        /// <param name="power">
        /// The power that defines the exponential curve.
        /// </param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static Func<float, float> EaseInExponential(float power)
        {
            return (value) => 
                { return Mathf.Pow(2, Mathf.Pow(value, power)) - 1; };
        }

        /// <summary>
        /// EaseOutExponential applies an exponential function that decelerates 
        /// quickly at the end. The rate of change starts fast but slows down as
        /// progress nears the end.
        /// </summary>
        /// <param name="power">
        /// The power that defines the exponential curve.
        /// </param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static Func<float, float> EaseOutExponential(float power)
        {
            return (value) => 
                { return 2 - Mathf.Pow(2, Mathf.Pow(1 - value, power)); };
        }

        /// <summary>
        /// EaseInOutExponential combines both EaseIn and EaseOut exponential 
        /// behaviors. It accelerates at the start, decelerates toward the end,
        /// and has a smooth transition in the middle.
        /// </summary>
        /// <param name="power">
        /// The power that defines the exponential curve.
        /// </param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static Func<float, float> EaseInOutExponential(float power)
        {
            return (value) =>
            {
                if(value < .5) 
                { 
                    return (Mathf.Pow(2, Mathf.Pow(2 * value, power)) - 1) / 2; 
                }
                return (3 - Mathf.Pow(2, Mathf.Pow(2 - 2 * value, power))) / 2;
            };
        }

        #endregion Exponential Easings

        #region Sinusoidal Easings

        /// <summary>
        /// EaseInSine applies a sine curve that starts slow and accelerates as
        /// it progresses.This easing simulates a smooth start to an animation.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static float EaseInSine(float value)
        {
            return 1 - Mathf.Cos(Mathf.PI * value / 2);
        }

        /// <summary>
        /// EaseOutSine applies a sine curve that starts fast and decelerates 
        /// toward the end. This easing simulates a smooth deceleration.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static float EaseOutSine(float value)
        {
            return Mathf.Sin(Mathf.PI * value / 2);
        }

        /// <summary>
        /// EaseInOutSine combines both EaseIn and EaseOut sine behaviors. It 
        /// starts slow, accelerates in the middle, and decelerates toward the 
        /// end.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static float EaseInOutSine(float value)
        {
            return (1 - Mathf.Cos(Mathf.PI * value)) / 2;
        }

        /// <summary>
        /// EaseInSineInverted is an inverted version of EaseInSine that adjusts
        /// the curve to start fast and decelerate.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>
        /// A function that applies the inverted easing to a value.
        /// </returns>
        public static float EaseInSineInverted(float value)
        {
            return 2 * Mathf.Asin(value) / Mathf.PI;
        }

        /// <summary>
        /// EaseOutSineInverted is an inverted version of EaseOutSine that 
        /// adjusts the curve to start slow and accelerate.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>
        /// A function that applies the inverted easing to a value.
        /// </returns>
        public static float EaseOutSineInverted(float value)
        {
            return 2 * Mathf.Acos(1 - value) / Mathf.PI;
        }

        /// <summary>
        /// EaseInOutSineInverted is an inverted version of EaseInOutSine that
        /// adjusts the curve to start fast, then slow down.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>
        /// A function that applies the inverted easing to a value.
        /// </returns>
        public static float EaseInOutSineInverted(float value)
        {
            return Mathf.Acos(1 - 2 * value) / Mathf.PI;
        }

        #endregion Sinusoidal Easings

        #region Circular Easings

        /// <summary>
        /// EaseInCircle applies a circular curve that accelerates from the 
        /// start. It starts slow and then accelerates towards the end.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static float EaseInCircle(float value)
        {
            return 1 - Mathf.Sqrt(1 - Mathf.Pow(value, 2));
        }

        /// <summary>
        /// EaseOutCircle applies a circular curve that decelerates toward the 
        /// end. It starts fast and then slows down as it approaches the end.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static float EaseOutCircle(float value)
        {
            return Mathf.Sqrt(1 - Mathf.Pow(value - 1, 2));
        }

        /// <summary>
        /// EaseInOutCircle combines both EaseIn and EaseOut circular behaviors.
        /// It starts slow, accelerates in the middle, and decelerates towards 
        /// the end.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>A function that applies the easing to a value.</returns>
        public static float EaseInOutCircle(float value)
        {
            if(value < .5f) 
            { 
                return (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * value, 2))) / 2; 
            }
            return (1 + Mathf.Sqrt(1 - Mathf.Pow(2 * value - 2, 2))) / 2;
        }

        /// <summary>
        /// EaseInOutCircleInverted is an inverted version of EaseInOutCircle 
        /// that adjusts the curve to start fast, decelerate, and end fast.
        /// </summary>
        /// <param name="value">The progress value in the range [0, 1].</param>
        /// <returns>
        /// A function that applies the inverted easing to a value.
        /// </returns>
        public static float EaseInOutCircleInverted(float value)
        {
            if(value < .5f)
            { 
                return Mathf.Sqrt(1 - Mathf.Pow(2 * value - 1, 2)) / 2;
            }
            return (2 - Mathf.Sqrt(1 - Mathf.Pow(2 * value - 1, 2))) / 2;
        }

        #endregion Circular Easings
    }
}