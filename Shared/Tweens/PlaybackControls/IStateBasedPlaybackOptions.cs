/*------------------------------------------------------------------------------
File:       IStateBasedPlaybackOptions.cs 
Project:    AlchemicalFlux Utilities
Overview:   
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-07 06:01:47 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Defines callbacks for state-based playback events such as resume, stop,
    /// and complete.
    /// </summary>
    public interface IStateBasedPlaybackOptions
    {
        /// <summary>
        /// Gets the callback invoked when playback is paused.
        /// </summary>
        Action OnPause { get; }

        /// <summary>
        /// Gets the callback invoked when playback is resumed.
        /// </summary>
        Action OnResume { get; }

        /// <summary>
        /// Gets the callback invoked when playback completes naturally.
        /// </summary>
        Action OnComplete { get; }
    }
}
