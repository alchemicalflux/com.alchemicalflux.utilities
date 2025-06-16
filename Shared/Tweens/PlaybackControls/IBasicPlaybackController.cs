/*------------------------------------------------------------------------------
File:       IBasicPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines playback control operations for a sequence.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-16 00:45:05 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Defines playback control operations for a sequence.
    /// </summary>
    public interface IBasicPlaybackController
    {
        /// <summary>
        /// Starts playback with the specified options.
        /// </summary>
        void Play();

        /// <summary>
        /// Pauses the current playback.
        /// </summary>
        void Pause();

        /// <summary>
        /// Instantly moves the playback position to the start.
        /// </summary>
        void SnapToStart();

        /// <summary>
        /// Instantly moves the playback position to the end.
        /// </summary>
        void SnapToEnd();
    }
}
