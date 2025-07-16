/*------------------------------------------------------------------------------
File:       ITweenPlaybackOptions.cs 
Project:    AlchemicalFlux Utilities
Overview:   Represents a set of playback options and callbacks for controlling a
            tween sequence. Combines basic playback options and state-based
            playback options.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-15 23:34:06 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Represents a set of playback options and callbacks for controlling a
    /// tween sequence. Combines basic playback options and state-based playback
    /// options.
    /// </summary>
    public interface ITweenPlaybackOptions : IBasicPlaybackOptions,
        IStateBasedPlaybackOptions
    {
    }
}