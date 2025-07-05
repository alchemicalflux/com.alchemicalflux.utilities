/*------------------------------------------------------------------------------
File:       BasicPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base implementation for playback controllers, handling
            common playback operations and delegating core behavior to derived
            classes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-04 22:39:36 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides a base implementation for playback controllers, handling common
    /// playback operations and delegating core behavior to derived classes.
    /// </summary>
    public abstract class BasicPlaybackController : IBasicPlaybackControls
    {
        /// <summary>
        /// Gets or sets the current playback options and callbacks.
        /// </summary>
        protected IBasicPlaybackOptions Options { get; set; }

        /// <inheritdoc />
        public void Play()
        {
            PlayCore();
            Options?.OnPlay?.Invoke();
        }

        /// <inheritdoc />
        public void Pause()
        {
            PauseCore();
            Options?.OnPause?.Invoke();
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

        /// <summary>
        /// Starts playback. Must be implemented by derived classes.
        /// </summary>
        protected abstract void PlayCore();

        /// <summary>
        /// Pauses playback. Must be implemented by derived classes.
        /// </summary>
        protected abstract void PauseCore();

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
    }
}
