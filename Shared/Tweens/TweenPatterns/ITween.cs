/*------------------------------------------------------------------------------
File:       ITween.cs 
Project:    AlchemicalFlux Utilities
Overview:   Interface for handling the tweening of inheriting structure.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-03 21:17:25 
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
        /// <summary>
        /// Toggle the display of the associated structure. May not always be 
        /// needed.
        /// </summary>
        /// <param name="show">
        /// Flag indicating if the structure should be displayed.
        /// </param>
        void Show(bool show);

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

        /// <summary>
        /// Gets the minimum valid progress value.
        /// </summary>
        float MinProgress { get; }

        /// <summary>
        /// Gets the maximum valid progress value.
        /// </summary>
        float MaxProgress { get; }
    }
}
