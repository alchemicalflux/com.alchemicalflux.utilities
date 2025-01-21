/*------------------------------------------------------------------------------
File:       BaseTween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a foundational component for performing tweens using
            interpolator and easing components to generate a transitional value.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 16:48:58 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    public class BaseTween<TType> : ITween where TType : IEquatable<TType>
    {
        #region Members

        /// <summary>Handle for interpolating portion of the tween.</summary>
        protected IInterpolator<TType> _interpolator;

        /// <summary>Handle for the easing portion of the tween.</summary>
        protected Func<float, float> _easing;

        /// <summary>
        /// Handle allowing for updating of associated value.
        /// Required as referencing gets wonky, especially with value types.
        /// </summary>
        [SerializeField]
        public Action<TType> OnUpdate;

        #endregion Members

        #region Methods

        /// <summary>Constructor for the BaseTween class.</summary>
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

        /// <inheritdoc />
        public virtual void Show(bool show) { }

        /// <inheritdoc />
        public void ApplyProgress(float progress)
        {
            OnUpdate?.Invoke(_interpolator.Interpolate(_easing(progress)));
        }

        #endregion Methods
    }
}