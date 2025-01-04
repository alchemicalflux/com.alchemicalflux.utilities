/*------------------------------------------------------------------------------
  File:           BasicTween.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Sets up a generic two point tween setup.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-10 22:22:29 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Represents a base class for tweening operations, which interpolate values of type <typeparamref name="TType"/> 
    ///   between a start and end value. This abstract class is intended to be extended by more specific tween 
    ///   implementations that apply the tweening logic.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of value being tweened. It must implement <see cref="IEquatable{T}"/> to allow comparison between the
    ///   start and end values.
    /// </typeparam>
    public abstract class BasicTween<TType> : ITween where TType : IEquatable<TType>
    {
        #region Members

        /// <summary>Defines the start value for the current tween.</summary>
        [SerializeField] 
        private TType _start;

        /// <summary>Defines the end value for the current tween.</summary>
        [SerializeField] 
        private TType _end;

        #endregion Members

        #region Properties

        /// <summary>Accessor for the private start value.</summary>
        public TType Start 
        {
            get { return _start; }
            set { _start = value; }
        }

        /// <summary>Accessor for the private end value.</summary>
        public TType End
        {
            get { return _end; }
            set { _end = value; }
        }

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