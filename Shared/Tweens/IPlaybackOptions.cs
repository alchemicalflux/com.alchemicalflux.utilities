/*------------------------------------------------------------------------------
File:       IPlaybackOptions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Represents a set of playback options and callbacks for controlling a
            sequence.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-05 18:42:38 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Represents a set of playback options and callbacks for controlling a
    /// sequence.
    /// </summary>
    public interface IPlaybackOptions
    {
        /// <summary>
        /// Gets the callback invoked when playback starts.
        /// </summary>
        Action OnPlay { get; }

        /// <summary>
        /// Gets the callback invoked when playback is paused.
        /// </summary>
        Action OnPause { get; }

        /// <summary>
        /// Gets the callback invoked when playback is resumed.
        /// </summary>
        Action OnResume { get; }

        /// <summary>
        /// Gets the callback invoked when playback is restarted.
        /// </summary>
        Action OnRestart { get; }

        /// <summary>
        /// Gets the callback invoked when playback completes.
        /// </summary>
        Action OnComplete { get; }

        /// <summary>
        /// Gets the callback invoked when the playback position is snapped to a
        /// specific time.
        /// </summary>
        Action<float> OnSnap { get; }
    }
}
