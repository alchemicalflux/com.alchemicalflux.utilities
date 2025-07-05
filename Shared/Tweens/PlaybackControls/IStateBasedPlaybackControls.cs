/*------------------------------------------------------------------------------
File:       IStateBasedPlaybackControls.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines state-based playback control operations for a sequence.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-04 22:39:36 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Defines state-based playback control operations for a sequence.
    /// </summary>
    public interface IStateBasedPlaybackControls
    {
        /// <summary>
        /// Attempts to resume playback if it was previously paused.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully resumed;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool Resume();

        /// <summary>
        /// Attempts to stop playback and reset the sequence to a defined state.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully stopped;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool Stop();

        /// <summary>
        /// Attempts to complete playback, moving the sequence to its finished
        /// state.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully completed;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool OnComplete();
    }
}
