/*------------------------------------------------------------------------------
File:       ILerpImplementation.cs 
Project:    AlchemicalFlux Utilities
Overview:   Interface for generic type lerp processes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-12 12:03:42 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Interface that requires an implementation of a type specific lerp
    /// funtion.
    /// </summary>
    /// <typeparam name="TType">Type to be lerped upon.</typeparam>
    public interface ILerpImplementation<TType>
    {
        /// <summary>
        /// Performs a lerp from a start to end point with a give progress time.
        /// </summary>
        /// <param name="start">The start value for the lerp.</param>
        /// <param name="end">The end value for the lerp.</param>
        /// <param name="progress">Progress time with range [0-1].</param>
        /// <returns></returns>
        public TType Lerp(in TType start, in TType end, float progress);
    }
}