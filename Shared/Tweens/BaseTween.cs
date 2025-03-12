/*------------------------------------------------------------------------------
File:       BaseTween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a foundational component for performing tweens using
            interpolator and easing components to generate a transitional value.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-12 00:48:47 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Events;
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Implements a foundational component for performing tweens using
    /// interpolator and easing components to generate a transitional value.
    /// </summary>
    /// <typeparam name="TType">The type of the value being tweened.</typeparam>
    public class BaseTween<TType> : ITween, IOnUpdateEvent<TType>
        where TType : IEquatable<TType>
    {
        #region Fields

        /// <summary>
        /// Handle for interpolating portion of the tween.
        /// </summary>
        protected IInterpolator<TType> _interpolator;

        /// <summary>
        /// Handle for the easing portion of the tween.
        /// </summary>
        protected Func<float, float> _easing;

        /// <summary>
        /// Handle allowing for updating of associated value.
        /// Required as referencing gets wonky, especially with value types.
        /// </summary>
        protected Action<TType> OnUpdate;

        #endregion Fields

        #region Methods

        /// <summary>
        /// Constructor for the BaseTween class.
        /// </summary>
        /// <param name="interpolator">
        /// An object implementing IInterpolator<TType> that calculates 
        /// intermediate values during the tween.
        /// </param>
        /// <param name="easing">
        /// A function that takes a float input (typically 0 to 1) and returns a
        /// modified float value, used to control the rate of change of the 
        /// tween.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when either the interpolator or easing parameter is null.
        /// </exception>
        public BaseTween(IInterpolator<TType> interpolator,
            Func<float, float> easing)
        {
            _interpolator = interpolator ??
                throw new ArgumentNullException(nameof(interpolator));
            _easing = easing ?? throw new ArgumentNullException(nameof(easing));
        }

        #endregion Methods

        #region ITween Implementation

        /// <inheritdoc />
        public virtual void Show(bool show) { }

        /// <inheritdoc />
        public void ApplyProgress(float progress)
        {
            OnUpdate?.Invoke(_interpolator.Interpolate(_easing(progress)));
        }

        #endregion ITween Implementation

        #region IOnUpdateEvent Implementation

        /// <summary>
        /// Adds an update listener.
        /// </summary>
        /// <param name="action">The action to be called on update.</param>
        public void AddOnUpdateListener(Action<TType> action)
        {
            OnUpdate += action;
        }

        /// <summary>
        /// Removes an update listener.
        /// </summary>
        /// <param name="action">The action to be removed.</param>
        public void RemoveOnUpdateListener(Action<TType> action)
        {
            OnUpdate -= action;
        }

        #endregion IOnUpdateEvent Implementation
    }
}