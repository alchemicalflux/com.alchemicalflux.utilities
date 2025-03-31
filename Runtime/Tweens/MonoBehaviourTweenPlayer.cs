/*------------------------------------------------------------------------------
File:       MonoBehaviourTweenPlayer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements the ITweenPlayer for MonoBehaviour coroutines.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-31 00:52:30 
------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    /// <summary>
    /// Implements the ITweenPlayer for MonoBehaviour coroutines.
    /// </summary>
    public class MonoBehaviourTweenPlayer : MonoBehaviour, ITweenPlayer
    {
        #region Members

        /// <summary>
        /// Tracks the current amount of time the tween has run.
        /// </summary>
        private float _curTime;

        /// <summary>
        /// Reference to the reusable coroutine handler.
        /// </summary>
        private readonly SmartCoroutine _coroutine;

        /// <summary>
        /// Reference to the list of actions to occur on tween completion.
        /// </summary>
        private Action _onComplete;

        #endregion Members

        #region Properties

        /// <summary>List of unique effects to be tweened.</summary>
        public HashSet<ITween> Tweens { get; private set; }

        /// <summary>
        /// Length of uninterrupted time the tween should cover.
        /// </summary>
        public float PlayTime { get; private set; }

        /// <summary>
        /// Function taking a range [0-1] and interpolating it for tween easing.
        /// </summary>
        public Func<float, float> EasingInterpreter { get; private set; }

        /// <summary>
        /// Function determining length of time passed per coroutine cycle.
        /// </summary>
        public Func<float> TimeIncrement { get; private set; }

        /// <summary>
        /// Flag indicating if tweening items should be hidden on complete.
        /// </summary>
        public bool HideOnComplete { get; private set; }

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

        #region ITweenPlayer Implementation

        /// <inheritdoc />
        public void Play(float playTime, Func<float, float> easingInterpreter,
            Action onComplete = null, bool hideOnComplete = false)
        {
            if(playTime <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(playTime),
                    "Play time must be greater than zero.");
            }

            if(easingInterpreter == null)
            {
                throw new ArgumentNullException(nameof(easingInterpreter),
                    "Easing interpreter cannot be null.");
            }

            PlayTime = playTime;
            EasingInterpreter = easingInterpreter;
            HideOnComplete = hideOnComplete;

            _coroutine.Stop();

            _coroutine.OnComplete -= _onComplete;
            _onComplete = onComplete;
            _coroutine.OnComplete += _onComplete;

            foreach(var tween in Tweens) { tween.Show(true); }
            _curTime = 0;
            Resume();
        }

        /// <inheritdoc />
        public void Pause()
        {
            _coroutine.Stop();
        }

        /// <inheritdoc />
        public bool Resume()
        {
            if(_curTime >= PlayTime) { return false; }
            _coroutine.Start(Process());
            return true;
        }

        /// <inheritdoc />
        public void SnapToStart()
        {
            _coroutine.Stop();
            SnapToFrame(0);
        }

        /// <inheritdoc />
        public void SnapToEnd()
        {
            _coroutine.Stop();
            SnapToFrame(1);
        }

        /// <inheritdoc />
        public void SnapToTime(float time)
        {
            if(time < 0 || time > PlayTime)
            {
                throw new ArgumentOutOfRangeException(nameof(time),
                    "Time must be within the range of the play time.");
            }

            _coroutine.Stop();
            SnapToFrame(time / PlayTime);
        }

        #endregion ITweenPlayer Implementation

        #region Helpers

        /// <summary>
        /// Handles the application of the easing function to all tweens.
        /// </summary>
        /// <returns>IEnumerator for coroutine processing.</returns>
        private IEnumerator Process()
        {
            for(; _curTime < PlayTime; _curTime += TimeIncrement())
            {
                var easedTime =
                    EasingInterpreter(Mathf.Clamp01(_curTime / PlayTime));
                foreach(var tween in Tweens) { tween.ApplyProgress(easedTime); }
                yield return null;
            }
            SnapToFrame(1);

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

        /// <summary>
        /// Snaps the tween to a specific frame.
        /// </summary>
        /// <param name="time">The time value to snap to.</param>
        private void SnapToFrame(float time)
        {
            foreach(var tween in Tweens) { tween.ApplyProgress(time); }
        }

        #endregion Helpers

        #endregion Methods
    }
}
