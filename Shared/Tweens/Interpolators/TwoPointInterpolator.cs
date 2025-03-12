/*------------------------------------------------------------------------------
File:       TwoPointInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for interpolations spanning only two values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-12 00:48:47 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Abstract base for interpolations spanning only two values.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public abstract class TwoPointInterpolator<TType> : IInterpolator<TType>
    {
        #region Properties

        /// <summary>Initial value for the interpolation.</summary>
        public virtual TType Start { get; set; }

        /// <summary>Final value for the interpolation.</summary>
        public virtual TType End { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Parameterless constructor for the TwoPointInterpolator class.
        /// </summary>
        public TwoPointInterpolator()
        {
        }

        /// <summary>
        /// Parametered constructor for the TwoPointInterpolator class.
        /// </summary>
        /// <param name="start">Initial value for the interpolation.</param>
        /// <param name="end">Final value for the interpolation.</param>
        public TwoPointInterpolator(TType start, TType end)
        {
            Start = start;
            End = end;
        }

        #endregion Methods

        #region IInterpolator Implementation

        /// <inheritdoc />
        public abstract TType Interpolate(float progress);

        #endregion IInterpolator Implementation
    }
}