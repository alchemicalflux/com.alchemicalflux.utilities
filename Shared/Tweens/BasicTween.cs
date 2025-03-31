/*------------------------------------------------------------------------------
File:       BasicTween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a basic tween using interpolator and easing components
            to generate a transitional value.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-31 02:49:32 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Implements a basic tween using interpolator and easing components
    /// to generate a transitional value.
    /// </summary>
    /// <typeparam name="TType">The type of the value being tweened.</typeparam>
    public class BasicTween<TType> : BaseTween<TType>
        where TType : IEquatable<TType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BasicTween{TType}"/> 
        /// class.
        /// </summary>
        /// <param name="interpolator">The interpolator to use.</param>
        /// <param name="easing">The easing function to use.</param>
        public BasicTween(IInterpolator<TType> interpolator,
            Func<float, float> easing)
            : base(interpolator, easing)
        {
        }
    }
}

