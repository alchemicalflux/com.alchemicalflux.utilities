/*------------------------------------------------------------------------------
File:       StateBasedPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base implementation for state-based playback controllers,
            extending <see cref="BasicPlaybackController"/> with state
            transition operations and associated callbacks.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-07 06:01:47 
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
        #region Properties

        /// <summary>
        /// Gets or sets the state-based playback options and callbacks.
        /// </summary>
        protected IStateBasedPlaybackOptions StateOptions { get; set; }

        #endregion Properties

        #region Methods

        #region Overrides

        /// <inheritdoc />
        public bool Pause()
        {
            var result = PauseCore();
            StateOptions?.OnPause?.Invoke();
            return result;
        }

        /// <inheritdoc />
        public bool Resume()
        {
            var result = ResumeCore();
            StateOptions.OnResume?.Invoke();
            return result;
        }

        /// <inheritdoc />
        public virtual bool OnComplete()
        {
            StateOptions.OnComplete?.Invoke();
            return true;
        }

        #endregion Overrides

        #region Internal Methods

        /// <summary>
        /// Pauses playback. Must be implemented by derived classes.
        /// </summary>
        protected abstract bool PauseCore();

        /// <summary>
        /// When overridden in a derived class, resumes playback from a paused
        /// state.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully resumed;
        /// otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool ResumeCore();

        #endregion Internal Methods

        #endregion Methods
    }
}
