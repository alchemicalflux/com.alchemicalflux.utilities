/*------------------------------------------------------------------------------
File:       BaseTween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a foundational component for performing tweens using
            interpolator and easing components to generate a transitional value.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-20 22:54:35 
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
    public abstract class BaseTween<TType> : ITween, IOnUpdateEvent<TType>
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

        #region Properties

        #region Overrides

        /// <inheritdoc />
        public virtual float MinProgress => 0.0f;

        /// <inheritdoc />
        public virtual float MaxProgress => 1.0f;

        #endregion Overrides

        #endregion Properties

        #region Methods

        #region Constructors

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

        #endregion Constructors

        #region Overrides

        /// <inheritdoc />
        public virtual void ApplyProgress(float progress)
        {
            if(progress < MinProgress ||
                progress > MaxProgress ||
                float.IsNaN(progress))
            {
                throw new ArgumentOutOfRangeException(nameof(progress),
                $"Progress must be between {MinProgress} and {MaxProgress}.");
            }
            OnUpdate?.Invoke(_interpolator.Interpolate(_easing(progress)));
        }

        /// <inheritdoc />
        public virtual void AddOnUpdateListener(Action<TType> action)
        {
            OnUpdate += action;
        }

        /// <inheritdoc />
        public virtual void RemoveOnUpdateListener(Action<TType> action)
        {
            OnUpdate -= action;
        }

        #endregion Overrides

        #endregion Methods
    }
}
