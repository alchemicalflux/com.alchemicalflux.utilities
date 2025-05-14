/*------------------------------------------------------------------------------
File:       TweenBuilder.cs 
Project:    AlchemicalFlux Utilities
Overview:   Builder pattern wrapper for the BaseTween class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-13 19:47:30 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides a builder pattern wrapper for constructing
    /// <see cref="BaseTween{T}"/> instances. Allows configuration of
    /// interpolator, easing function, and update actions before building a
    /// tween.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the value being tweened. Must implement
    /// <see cref="IEquatable{T}"/>.
    /// </typeparam>
    public class TweenBuilder<T> where T : IEquatable<T>
    {
        #region Fields

        /// <summary>
        /// Stores the combined update actions to be invoked during the tween.
        /// </summary>
        private Action<T> _updateAction;

        /// <summary>
        /// The interpolator used to generate intermediate values for the tween.
        /// </summary>
        private IInterpolator<T> _interpolator;

        /// <summary>
        /// The easing function used to control the rate of change of the tween.
        /// </summary>
        private Func<float, float> _easingFunction;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenBuilder{T}"/>
        /// class with a default linear easing function.
        /// </summary>
        public TweenBuilder()
        {
            _easingFunction = Easings.Linear;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenBuilder{T}"/>
        /// class by copying the configuration from another builder.
        /// </summary>
        /// <param name="builder">The builder to copy.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="builder"/> parameter is null.
        /// </exception>
        public TweenBuilder(TweenBuilder<T> builder)
        {
            if(builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            _interpolator = builder._interpolator;
            _easingFunction = builder._easingFunction;
            _updateAction = builder._updateAction;
        }

        /// <summary>
        /// Creates a copy of the current builder, duplicating its
        /// configuration.
        /// </summary>
        /// <returns>
        /// A new <see cref="TweenBuilder{T}"/> instance with the same
        /// configuration.
        /// </returns>
        public TweenBuilder<T> Copy() => new TweenBuilder<T>(this);

        /// <summary>
        /// Sets the interpolator for the tween.
        /// </summary>
        /// <param name="interpolator">The interpolator to use.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="interpolator"/> parameter is null.
        /// </exception>
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
        /// <returns>The current builder instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="easingFunction"/> parameter is null.
        /// </exception>
        public TweenBuilder<T> SetEasing(Func<float, float> easingFunction)
        {
            _easingFunction = easingFunction ??
                throw new ArgumentNullException(nameof(easingFunction));
            return this;
        }

        /// <summary>
        /// Adds an update action to be called during the tween.
        /// Multiple actions can be added; all will be invoked on update.
        /// </summary>
        /// <param name="updateAction">The action to add.</param>
        /// <returns>The current builder instance for chaining.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the <paramref name="updateAction"/> parameter is null.
        /// </exception>
        public TweenBuilder<T> AddUpdateAction(Action<T> updateAction)
        {
            if(updateAction == null)
            {
                throw new ArgumentNullException(nameof(updateAction));
            }
            _updateAction += updateAction;
            return this;
        }

        /// <summary>
        /// Builds a <see cref="BasicTween{T}"/> with the specified parameters.
        /// </summary>
        /// <returns>A new <see cref="BasicTween{T}"/> instance.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the interpolator or easing function is not set.
        /// </exception>
        public BasicTween<T> BuildBasicTween()
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

            var tween = new BasicTween<T>(_interpolator, _easingFunction);
            if(_updateAction != null)
            {
                tween.AddOnUpdateListener(_updateAction);
            }
            return tween;
        }

        #endregion Methods
    }
}
