/*------------------------------------------------------------------------------
File:       TweenBuilder.cs 
Project:    AlchemicalFlux Utilities
Overview:   Builder pattern wrapper for the BaseTween class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 04:42:53 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Builder pattern wrapper for the BaseTween class.
    /// </summary>
    /// <typeparam name="T">The type of the value being tweened.</typeparam>
    public class TweenBuilder<T> where T : IEquatable<T>
    {
        private readonly List<Action<T>> _updateActions = new();
        private IInterpolator<T> _interpolator;
        private Func<float, float> _easingFunction = Easings.Linear;

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenBuilder{T}"/> 
        /// class.
        /// </summary>
        public TweenBuilder() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenBuilder{T}"/> 
        /// class by copying another builder.
        /// </summary>
        /// <param name="builder">The builder to copy.</param>
        public TweenBuilder(TweenBuilder<T> builder)
        {
            _interpolator = builder._interpolator;
            _easingFunction = builder._easingFunction;
            _updateActions = new List<Action<T>>(builder._updateActions);
        }

        /// <summary>
        /// Creates a copy of the current builder.
        /// </summary>
        /// <returns>A new <see cref="TweenBuilder{T}"/> instance.</returns>
        public TweenBuilder<T> Copy() => new(this);

        /// <summary>
        /// Sets the interpolator for the tween.
        /// </summary>
        /// <param name="interpolator">The interpolator to use.</param>
        /// <returns>The current builder instance.</returns>
        public TweenBuilder<T> SetInterpolator(IInterpolator<T> interpolator)
        {
            _interpolator = interpolator ??
                throw new ArgumentNullException(nameof(interpolator));
            return this;
        }

        /// <summary>
        /// Sets the easing function for the tween.
        /// </summary>
        /// <param name="easingFunction">The easing function to use.</param>
        /// <returns>The current builder instance.</returns>
        public TweenBuilder<T> SetEasing(Func<float, float> easingFunction)
        {
            _easingFunction = easingFunction ??
                throw new ArgumentNullException(nameof(easingFunction));
            return this;
        }

        /// <summary>
        /// Adds an update action to be called during the tween.
        /// </summary>
        /// <param name="updateAction">The action to add.</param>
        /// <returns>The current builder instance.</returns>
        public TweenBuilder<T> AddUpdateAction(Action<T> updateAction)
        {
            if(updateAction == null)
            {
                throw new ArgumentNullException(nameof(updateAction));
            }
            _updateActions.Add(updateAction);
            return this;
        }

        /// <summary>
        /// Builds the tween with the specified parameters.
        /// </summary>
        /// <returns>A new <see cref="BaseTween{T}"/> instance.</returns>
        public BaseTween<T> Build()
        {
            if(_interpolator == null)
            {
                throw new InvalidOperationException(
                    "Interpolator must be set before building the tween.");

            }
            if(_easingFunction == null)
            {
                throw new InvalidOperationException(
                    "Easing function must be set before building the tween.");
            }

            var tween = new BaseTween<T>(_interpolator, _easingFunction);
            foreach(var action in _updateActions)
            {
                tween.AddOnUpdateListener(action);
            }
            return tween;
        }
    }
}