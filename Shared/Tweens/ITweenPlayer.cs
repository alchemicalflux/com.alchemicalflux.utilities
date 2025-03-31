/*------------------------------------------------------------------------------
File:       ITweenPlayer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Interface for controlling ITween instances.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-31 00:52:30 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Interface for controlling ITween instances.
    /// </summary>
    public interface ITweenPlayer
    {
        /// <summary>
        /// Begins a timed sequence for a tween using the supplied interpolator.
        /// </summary>
        /// <param name="playTime">
        /// How long the tween will take to complete.
        /// </param>
        /// <param name="easingInterpreter">
        /// Converts time progression into eased tween value.
        /// </param>
        /// <param name="onComplete">
        /// Optional event handle for when tween completes.
        /// </param>
        /// <param name="hideOnComplete">
        /// Optional flag for hiding structure on complete.
        /// </param>
        void Play(float playTime, Func<float, float> easingInterpreter,
            Action onComplete = null, bool hideOnComplete = false);

        /// <summary>
        /// Pauses the current tween while allowing for the possibility to 
        /// continue later.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resumes the current tween from its last point, if possible.
        /// </summary>
        /// <returns>Flag indicating if the tween was resumed.</returns>
        bool Resume();

        /// <summary>
        /// Snaps the tween to the start value.
        /// </summary>
        void SnapToStart();

        /// <summary>
        /// Snaps the tween to the end value.
        /// </summary>
        void SnapToEnd();

        /// <summary>
        /// Snaps the tween to a specific time value.
        /// </summary>
        /// <param name="time">The time value to snap to.</param>
        void SnapToTime(float time);
    }
}
