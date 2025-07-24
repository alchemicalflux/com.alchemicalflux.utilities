/*------------------------------------------------------------------------------
File:       IInterpolator.cs 
Project:    AlchemicalFlux Utilities
Overview:   Interface for generic interpolations.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 05:00:37 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Interface for interpolations over generic types.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public interface IInterpolator<TType>
    {
        /// <summary>
        /// Generates an interpolated value based on the given progress amount.
        /// </summary>
        /// <param name="progress">
        /// Interpolation value, typically expected to be between [0-1] but not 
        /// guaranteed.
        /// </param>
        /// <returns> A value generated based on the progress amount. </returns>
        TType Interpolate(float progress);
    }
}
