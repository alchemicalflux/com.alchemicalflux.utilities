/*------------------------------------------------------------------------------
File:       IBasicPlaybackControls.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines playback control operations for a sequence.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-04 22:39:36 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Defines playback control operations for a sequence.
    /// </summary>
    public interface IBasicPlaybackControls
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
