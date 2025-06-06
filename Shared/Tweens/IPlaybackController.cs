/*------------------------------------------------------------------------------
File:       IPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines playback control operations for a sequence.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-05 18:42:38 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Defines playback control operations for a sequence.
    /// </summary>
    public interface IPlaybackController
    {
        /// <summary>
        /// Starts playback with the specified options.
        /// </summary>
        /// <param name="options">Playback options to use when playing.</param>
        void Play(IPlaybackOptions options);

        /// <summary>
        /// Pauses the current playback.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes playback if it was previously paused.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully resumed;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool Resume();

        /// <summary>
        /// Restarts playback from the beginning.
        /// </summary>
        void Restart();

        /// <summary>
        /// Instantly moves the playback position to the start.
        /// </summary>
        void SnapToStart();

        /// <summary>
        /// Instantly moves the playback position to the end.
        /// </summary>
        void SnapToEnd();

        /// <summary>
        /// Instantly moves the playback position to the specified time.
        /// </summary>
        /// <param name="time">The time, in seconds, to snap to.</param>
        void SnapToTime(float time);
    }
}
