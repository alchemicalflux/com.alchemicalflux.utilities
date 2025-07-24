/*------------------------------------------------------------------------------
File:       BasicPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base implementation for playback controllers, handling
            common playback operations and delegating core behavior to derived
            classes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-07 06:01:47 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides a base implementation for playback controllers, handling common
    /// playback operations and delegating core behavior to derived classes.
    /// </summary>
    public abstract class BasicPlaybackController : IBasicPlaybackControls
    {
        #region Properties

        /// <summary>
        /// Gets or sets the current playback options and callbacks.
        /// </summary>
        protected IBasicPlaybackOptions Options { get; set; }

        #endregion Properties

        #region Methods

        #region Overrides

        /// <inheritdoc />
        public void Play()
        {
            PlayCore();
            Options?.OnPlay?.Invoke();
        }

        /// <inheritdoc />
        public bool Stop()
        {
            var result = StopCore();
            Options.OnStop?.Invoke();
            return result;
        }

        /// <inheritdoc />
        public void SnapToStart()
        {
            SnapToStartCore();
            Options?.OnSnapStart?.Invoke();
        }

        /// <inheritdoc />
        public void SnapToEnd()
        {
            SnapToEndCore();
            Options?.OnSnapEnd?.Invoke();
        }

        #endregion Overrides

        #region Internal Methods

        /// <summary>
        /// Starts playback. Must be implemented by derived classes.
        /// </summary>
        protected abstract void PlayCore();

        /// <summary>
        /// When overridden in a derived class, stops playback and resets the
        /// sequence.
        /// </summary>
        /// <returns>
        /// <c>true</c> if playback was successfully stopped;
        /// otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool StopCore();

        /// <summary>
        /// Sets the playback position to the start. Must be implemented by
        /// derived classes.
        /// </summary>
        protected abstract void SnapToStartCore();

        /// <summary>
        /// Sets the playback position to the end. Must be implemented by
        /// derived classes.
        /// </summary>
        protected abstract void SnapToEndCore();

        #endregion Internal Methods

        #endregion Methods
    }
}
