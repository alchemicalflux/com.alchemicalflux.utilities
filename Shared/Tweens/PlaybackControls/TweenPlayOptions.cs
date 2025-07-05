/*------------------------------------------------------------------------------
File:       TweenPlayOptions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Represents a set of playback options and callbacks for controlling a
            sequence.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-04 22:39:36 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Represents a set of playback options and callbacks for controlling a
    /// sequence.
    /// </summary>
    public class TweenPlayOptions : IBasicPlaybackOptions, 
        IStateBasedPlaybackOptions
    {
        /// <inheritdoc />
        public Action OnPlay { get; set; }

        /// <inheritdoc />
        public Action OnPause { get; set; }

        /// <inheritdoc />
        public Action OnSnapStart { get; set; }

        /// <inheritdoc />
        public Action OnSnapEnd { get; set; }

        /// <inheritdoc />
        public Action OnResume { get; set; }

        /// <inheritdoc />
        public Action OnStop { get; set; }

        /// <inheritdoc />
        public Action OnComplete { get; set; }

    }
}
