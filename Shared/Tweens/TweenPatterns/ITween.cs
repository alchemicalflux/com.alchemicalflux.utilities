/*------------------------------------------------------------------------------
File:       ITween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Interface for handling the tweening of inheriting structure.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-16 22:55:50 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Interface for defining a basic tweening structure, where data is 
    /// animated or transitioned from a start value to an end value. The 
    /// implementing class is expected to provide the logic for visualizing and 
    /// updating the tween based on progress.
    /// </summary>
    public interface ITween
    {
        #region Properties

        /// <summary>
        /// Gets the minimum valid progress value.
        /// </summary>
        float MinProgress { get; }

        /// <summary>
        /// Gets the maximum valid progress value.
        /// </summary>
        float MaxProgress { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Expects the inheriting structure to tween its relevant data based on 
        /// the progress value.
        /// </summary>
        /// <param name="progress">
        /// 'Time' value indicating how far along the tween the structure needs 
        /// to be.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown when the progress value is outside the valid range.
        /// </exception>
        void ApplyProgress(float progress);

        #endregion Methods
    }
}
