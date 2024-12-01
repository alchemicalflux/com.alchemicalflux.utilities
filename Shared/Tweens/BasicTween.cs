/*------------------------------------------------------------------------------
  File:           BasicTween.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Sets up a generic two point tween setup.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-01 12:56:27 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    public abstract class BasicTween<T> : ITween where T : IEquatable<T>
    {
        #region Properties

        /// <summary>Defines the start value for the current tween.</summary>
        public T Start { get; set; }

        /// <summary>Defines the end value for the current tween.</summary>
        public T End { get; set; }

        /// <summary>
        /// Handle allowing for updating of associated value.
        /// Required as referencing gets wonky, especially with value types.
        /// </summary>
        public Action<T> OnUpdate;

        #endregion Properties

        #region Methods

        #region ITween Implementation

        /// <inheritdoc />
        public virtual void Show(bool show) { }

        /// <inheritdoc />
        public virtual bool ApplyProgress(float progress)
        {
            // Tweening calculations unneeded if start-end values are identical.
            return !Start.Equals(End);
        }

        #endregion ITween Implementation

        #endregion Methods
    }
}