/*------------------------------------------------------------------------------
File:       BasicTween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Sets up a generic two point tween setup.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 13:37:27 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Represents a base class for tweening operations, which interpolate 
    /// values of type <typeparamref name="TType"/> between a start and end 
    /// value. This abstract class is intended to be extended by more specific
    /// tween implementations that apply the tweening logic.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of value being tweened. It must implement 
    /// <see cref="IEquatable{T}"/> to allow comparison between the start and 
    /// end values.
    /// </typeparam>
    public abstract class BasicTween<TType> : ITween 
        where TType : IEquatable<TType>
    {
        #region Properties

        public abstract TType Start { get; set; }
        public abstract TType End { get; set; }

        /// <summary>
        /// Handle allowing for updating of associated value.
        /// Required as referencing gets wonky, especially with value types.
        /// </summary>
        [SerializeField]
        public Action<TType> OnUpdate;

        #endregion Properties

        #region Methods

        #region ITween Implementation

        /// <inheritdoc />
        public virtual void Show(bool show) { }

        /// <inheritdoc />
        public virtual bool ApplyProgress(float progress)
        {
            // Tweening calculations unneeded if start-end values are identical.
            var areEqual = Start.Equals(End);
            if(areEqual) { OnUpdate?.Invoke(Start); }
            return areEqual;
        }

        #endregion ITween Implementation

        #endregion Methods
    }
}