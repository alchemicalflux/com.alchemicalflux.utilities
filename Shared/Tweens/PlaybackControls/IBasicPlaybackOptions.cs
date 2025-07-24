/*------------------------------------------------------------------------------
File:       IBasicPlaybackOptions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Represents a set of playback options and callbacks for controlling a
            sequence.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-07 06:01:47 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Represents a set of playback options and callbacks for controlling a
    /// sequence.
    /// </summary>
    public interface IBasicPlaybackOptions
    {
        /// <summary>
        /// Gets the callback invoked when playback starts.
        /// </summary>
        Action OnPlay { get; }

        /// <summary>
        /// Gets the callback invoked when playback is stopped.
        /// </summary>
        Action OnStop { get; }

        /// <summary>
        /// Gets the callback invoked when playback position is snapped to the
        /// start.
        /// </summary>
        Action OnSnapStart { get; }

        /// <summary>
        /// Gets the callback invoked when playback position is snapped to the
        /// end.
        /// </summary>
        Action OnSnapEnd { get; }
    }
}
