/*------------------------------------------------------------------------------
File:       BasicPlaybackController.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base implementation for playback controllers, handling
            common playback operations and delegating core behavior to derived
            classes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-16 00:45:05 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides a base implementation for playback controllers, handling common
    /// playback operations and delegating core behavior to derived classes.
    /// </summary>
    public abstract class BasicPlaybackController : IBasicPlaybackController
    {
        /// <summary>
        /// Gets or sets the current playback options and callbacks.
        /// </summary>
        protected IBasicPlaybackOptions Options { get; set; }

        /// <summary>
        /// Starts playback.
        /// </summary>
        public void Play()
        {
            PlayCore();
            Options?.OnPlay?.Invoke();
        }

        /// <summary>
        /// Pauses the current playback.
        /// </summary>
        public void Pause()
        {
            PauseCore();
            Options?.OnPause?.Invoke();
        }

        /// <summary>
        /// Instantly moves the playback position to the start.
        /// </summary>
        public void SnapToStart()
        {
            SnapToStartCore();
            Options?.OnSnapStart?.Invoke();
        }

        /// <summary>
        /// Instantly moves the playback position to the end.
        /// </summary>
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
