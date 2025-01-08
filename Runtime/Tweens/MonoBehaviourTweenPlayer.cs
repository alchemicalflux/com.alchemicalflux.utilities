/*------------------------------------------------------------------------------
File:       MonoBehaviourTweenPlayer.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements the ITweenPlayer for MonoBehaviour coroutines.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens
{
    public class MonoBehaviourTweenPlayer : MonoBehaviour, ITweenPlayer
    {
        #region Members

        /// <summary>
        /// Tracks the current amount of time the tween has run.
        /// </summary>
        private float _curTime;

        /// <summary>
        /// Reference to the reusuable corountine handler.
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
        /// Flag indicating if tweening items should be hiden on complete.
        /// </summary>
        public bool HideOnComplete { get; private set; }

        #endregion Properties

        #region Methods

        #region Constructors

        public MonoBehaviourTweenPlayer()
        {
            _coroutine = new(this);
            Tweens = new();
            TimeIncrement = TimeInc;
        }

        #endregion Constructors

        #region ITweenPlayer Implementation

        /// <inheritdoc />
        public void Play(float playTime, Func<float, float> easingInterpreter,
            Action onComplete = null, bool hideOnComplete = false)
        {
            _coroutine.OnComplete -= _onComplete;
            _onComplete = onComplete;

            PlayTime = playTime;
            EasingInterpreter = easingInterpreter;
            HideOnComplete = hideOnComplete;

            _coroutine.Stop();
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
            foreach(var tween in Tweens) { tween.ApplyProgress(0); }
        }

        /// <inheritdoc />
        public void SnapToEnd()
        {
            _coroutine.Stop();
            foreach(var tween in Tweens) { tween.ApplyProgress(1); }
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
                foreach(var tween in Tweens)
                {
                    tween.ApplyProgress(EasingInterpreter(
                        Mathf.Clamp01(_curTime / PlayTime)));
                }
                yield return null;
            }

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
        private float TimeInc()
        {
            return Time.deltaTime;
        }

        #endregion Helpers

        #endregion Methods
    }
}
