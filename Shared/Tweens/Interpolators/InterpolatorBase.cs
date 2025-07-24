/*------------------------------------------------------------------------------
File:       InterpolatorBase.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for interpolators that generate values of type
            TType. Implements the IInterpolator<TType> interface and provides a
            template method for interpolation with progress validation and
            default value handling.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-20 07:06:41 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base class for interpolators that generate values of type
    /// <typeparamref name="TType"/>. Implements the
    /// <see cref="IInterpolator{TType}"/> interface and provides a template
    /// method for interpolation with progress validation and default value
    /// handling.
    /// </summary>
    /// <typeparam name="TType">The type to interpolate.</typeparam>
    public abstract class InterpolatorBase<TType> : IInterpolator<TType>
    {
        #region Methods

        #region IInterpolator Implementation

        /// <inheritdoc />
        public TType Interpolate(float progress)
        {
            TType failValue = GetDefault();
            if(CheckAndLimitProgress(ref progress, ref failValue))
            {
                return ProcessInterpolation(progress);
            }
            return failValue;
        }

        #endregion IInterpolator Implementation

        /// <summary>
        /// Checks and optionally limits the progress value before interpolation.
        /// Can also set a failure value if progress is invalid.
        /// </summary>
        /// <param name="progress">
        /// The progress value to check and possibly limit.
        /// </param>
        /// <param name="failValue">
        /// Reference to the value to return if progress is invalid.
        /// </param>
        /// <returns>
        /// <c>true</c> if progress is valid and interpolation should proceed;
        /// otherwise, <c>false</c>.
        /// </returns>
        protected virtual bool CheckAndLimitProgress(
            ref float progress,
            ref TType failValue)
        {
            if(float.IsNaN(progress))
            {
                throw new System.ArgumentOutOfRangeException(
                    nameof(progress), "Progress cannot be NaN.");
            }
            return true;
        }

        /// <summary>
        /// Performs the actual interpolation logic for the given progress
        /// value.
        /// Must be implemented by derived classes.
        /// </summary>
        /// <param name="progress">The progress value for interpolation.</param>
        /// <returns>The interpolated value.</returns>
        protected abstract TType ProcessInterpolation(float progress);

        /// <summary>
        /// Gets the default value to return when interpolation fails or is not
        /// possible.
        /// </summary>
        /// <returns>
        /// The default value for <typeparamref name="TType"/>.
        /// </returns>
        protected virtual TType GetDefault()
        {
            return default;
        }

        #endregion Methods
    }
}
