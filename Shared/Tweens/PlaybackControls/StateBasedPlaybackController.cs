/*------------------------------------------------------------------------------
File:       StateBasedPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base implementation for state-based playback controllers,
            extending <see cref="BasicPlaybackController"/> with state
            transition operations and associated callbacks.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-04 22:39:36 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides a base implementation for state-based playback controllers,
    /// extending <see cref="BasicPlaybackController"/> with state transition
    /// operations and associated callbacks.
    /// </summary>
    public abstract class StateBasedPlaybackController
        : BasicPlaybackController,
        IStateBasedPlaybackControls
    {
        /// <summary>
        /// Gets or sets the state-based playback options and callbacks.
        /// </summary>
        protected IStateBasedPlaybackOptions StateOptions { get; set; }

        /// <inheritdoc />
        public virtual bool OnComplete()
        {
            StateOptions.OnComplete();
            return true;
        }

        /// <inheritdoc />
        public bool Resume()
        {
            var result = ResumeCore();
            StateOptions.OnResume();
            return result;
        }

        /// <inheritdoc />
        public bool Stop()
        {
            var result = StopCore();
            StateOptions.OnStop();
            return result;
        }

        /// <summary>
        /// When overridden in a derived class, resumes playback from a paused
        /// state.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully resumed;
        /// otherwise, <c>false</c>.
        /// </returns>
        public abstract bool ResumeCore();

        /// <summary>
        /// When overridden in a derived class, stops playback and resets the
        /// sequence.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully stopped;
        /// otherwise, <c>false</c>.
        /// </returns>
        public abstract bool StopCore();
    }
}
