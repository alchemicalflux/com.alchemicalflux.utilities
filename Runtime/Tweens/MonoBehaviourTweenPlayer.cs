/*------------------------------------------------------------------------------
File:       MonoBehaviourTweenPlayer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a MonoBehaviour-based tween player that manages a
            collection of <see cref="ITween"/>s and controls their playback
            using coroutines. Supports state-based and basic playback options.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-07 06:01:47 
------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Provides a MonoBehaviour-based tween player that manages a collection of
    /// <see cref="ITween"/>s and controls their playback using coroutines.
    /// Supports state-based and basic playback options.
    /// </summary>
    public class MonoBehaviourTweenPlayer : MBStateBasedlaybackController
    {
        #region Fields

        /// <summary>
        /// Reference to the reusable coroutine handler.
        /// </summary>
        private readonly SmartCoroutine _coroutine;

        #endregion Fields

        #region Properties

        /// <summary>List of unique effects to be tweened.</summary>
        public HashSet<ITween> Tweens { get; private set; }

        /// <summary>
        /// Length of uninterrupted time the tween should cover.
        /// </summary>
        public float PlayTime { get; private set; }

        /// <summary>
        /// Tracks the current amount of time the tween has run.
        /// </summary>
        public float CurrentTime { get; private set; }

        /// <summary>
        /// Function taking a range [0-1] and interpolating it for tween easing.
        /// </summary>
        public Func<float, float> EasingInterpreter { get; private set; }
            = Easings.Linear;

        /// <summary>
        /// Function determining length of time passed per coroutine cycle.
        /// </summary>
        public Func<float> TimeIncrement { get; private set; }

        /// <summary>
        /// Flag indicating if tweening items should be hidden on complete.
        /// </summary>
        public bool HideOnComplete { get; private set; }

        /// <summary>
        /// Gets or sets the callback invoked when SnapToTime is called.
        /// </summary>
        public Action OnSnapToTime{ get; set; }

        #endregion Properties

        #region Methods

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="MonoBehaviourTweenPlayer"/> class.
        /// </summary>
        public MonoBehaviourTweenPlayer()
        {
            _coroutine = new(this);
            Tweens = new();
            TimeIncrement = DefaultTimeInc;
        }

        #endregion Constructors

        #region Exposed Methods

        /// <summary>
        /// Starts playback with the specified options.
        /// </summary>
        /// <param name="playTime">
        /// The total duration of the tween playback.
        /// </param>
        /// <param name="easingInterpreter">
        /// The easing function to use for interpolation.
        /// </param>
        /// <param name="options">
        /// The playback options and callbacks to use.
        /// </param>
        /// <param name="hideOnComplete">
        /// Whether to hide tweened items on completion.
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="playTime"/> is not greater than zero.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="easingInterpreter"/> is null.
        /// </exception>
        public void Play(
            float playTime,
            Func<float, float> easingInterpreter,
            TweenPlayOptions options = null,
            bool hideOnComplete = false)
        {
            if(playTime <= 0 || float.IsNaN(playTime))
            {
                throw new ArgumentOutOfRangeException(nameof(playTime),
                    $"Play time must be a positive value ({playTime}).");
            }

            if(easingInterpreter == null)
            {
                throw new ArgumentNullException(nameof(easingInterpreter),
                    "Easing interpreter cannot be null.");
            }

            PlayTime = playTime;
            EasingInterpreter = easingInterpreter;
            HideOnComplete = hideOnComplete;

            _coroutine.OnComplete -= StateOptions?.OnComplete;
            Options = options;
            StateOptions = options;
            _coroutine.OnComplete += StateOptions?.OnComplete;

            Play();
        }

        /// <summary>
        /// Instantly moves the playback position to a specific time.
        /// </summary>
        /// <param name="time">The time value to snap to, in seconds.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// Thrown if <paramref name="time"/> is outside the valid range.
        /// </exception>
        public void SnapToTime(float time)
        {
            if(time < 0 || time > PlayTime)
            {
                throw new ArgumentOutOfRangeException(nameof(time),
                    "Time must be within the range of the play time.");
            }

            PauseCore();
            CurrentTime = time / PlayTime;
            foreach(var tween in Tweens) { tween.ApplyProgress(time); }
            OnSnapToTime?.Invoke();
        }

        #endregion Exposed Methods

        #region Interface Methods

        /// <summary>
        /// Handles the application of the easing function to all tweens.
        /// </summary>
        /// <returns>IEnumerator for coroutine processing.</returns>
        private IEnumerator Process()
        {
            for(; CurrentTime < PlayTime; CurrentTime += TimeIncrement())
            {
                var easedTime =
                    EasingInterpreter(Mathf.Clamp01(CurrentTime / PlayTime));
                foreach(var tween in Tweens) { tween.ApplyProgress(easedTime); }
                yield return null;
            }
            SnapToEndCore();

            if(HideOnComplete)
            {
                foreach(var tween in Tweens) { tween.Show(false); }
            }
        }

        /// <summary>
        /// Default function for tween time advancement.
        /// </summary>
        /// <returns>
        /// Amount of time that has passed for one cycle of the coroutine.
        /// </returns>
        private float DefaultTimeInc()
        {
            return Time.deltaTime;
        }

        #endregion Interface Methods

        #region Overrides

        /// <inheritdoc />
        protected override void PlayCore()
        {
            SnapToStartCore();
            foreach(var tween in Tweens) { tween.Show(true); }
            ResumeCore();
        }

        /// <inheritdoc />
        protected override bool StopCore()
        {
            if(!_coroutine.IsRunning) { return false; }
            _coroutine.Stop();
            return true;
        }

        /// <inheritdoc />
        protected override void SnapToStartCore()
        {
            SnapToTime(0);
        }

        /// <inheritdoc />
        protected override void SnapToEndCore()
        {
            SnapToTime(PlayTime);
        }

        /// <inheritdoc />
        protected override bool PauseCore()
        {
            if(!_coroutine.IsRunning) { return false; }
            _coroutine.Stop();
            return true;
        }

        /// <inheritdoc />
        protected override bool ResumeCore()
        {
            if(_coroutine.IsRunning || CurrentTime >= PlayTime) { return false; }
            _coroutine.Start(Process());
            return true;
        }

        #endregion Overrides

        #endregion Methods
    }
}
