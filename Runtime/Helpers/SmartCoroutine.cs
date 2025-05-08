/*------------------------------------------------------------------------------
File:       SmartCoroutine.cs 
Project:    AlchemicalFlux Utilities
Overview:   Wrapper for MonoBehavior driven coroutines, providing simple
            function calls and event handles.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-07 23:17:54 
------------------------------------------------------------------------------*/
using System;
using System.Collections;
using UnityEngine;

namespace AlchemicalFlux.Utilities
{
    /// <summary>
    /// A wrapper for MonoBehaviour-driven coroutines, providing simplified
    /// function calls and event handling for coroutine lifecycle management.
    /// </summary>
    public class SmartCoroutine
    {
        #region Members

        /// <summary>
        /// Event triggered when the coroutine completes successfully.
        /// </summary>
        public Action OnComplete;

        /// <summary>
        /// Event triggered when the coroutine is stopped manually.
        /// </summary>
        public Action OnStop;

        /// <summary>
        /// Reference to the MonoBehaviour instance used to start and stop 
        /// coroutines.
        /// </summary>
        private readonly MonoBehaviour _mono;

        /// <summary>
        /// The currently running coroutine instance.
        /// </summary>
        private Coroutine _coroutine;

        #endregion Members

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the coroutine is currently running.
        /// </summary>
        public bool IsRunning { get { return _coroutine != null; } }

        #endregion Properties

        #region Methods

        #region Constructors

        /// <summary>
        /// Private constructor to prevent instantiation without a MonoBehaviour
        /// reference.
        /// </summary>
        private SmartCoroutine() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SmartCoroutine"/>
        /// class.
        /// </summary>
        /// <param name="mono">
        /// The MonoBehaviour instance used to manage the coroutine.
        /// </param>
        public SmartCoroutine(MonoBehaviour mono)
        {
            _mono = mono;
        }

        /// <summary>
        /// Finalizer to ensure the coroutine is stopped when the object is
        /// garbage collected.
        /// </summary>
        ~SmartCoroutine()
        {
            Stop();
        }

        #endregion Constructors

        #region Controls

        /// <summary>
        /// Starts a new coroutine with the specified process.
        /// If a coroutine is already running, it will be stopped before
        /// starting the new one.
        /// </summary>
        /// <param name="process">
        /// The IEnumerator process to run as a coroutine.
        /// </param>
        public void Start(IEnumerator process)
        {
            Stop();
            _coroutine = _mono.StartCoroutine(RunCoroutine(process));
        }

        /// <summary>
        /// Stops the currently running coroutine, if any, and triggers the
        /// <see cref="OnStop"/> event.
        /// </summary>
        public void Stop()
        {
            if(_coroutine == null) { return; }

            _mono.StopCoroutine(_coroutine);
            OnStop?.Invoke();
            _coroutine = null;
        }

        #endregion Controls

        #region Helpers

        /// <summary>
        /// Internal helper method to run the coroutine and handle its
        /// completion.
        /// </summary>
        /// <param name="process">The IEnumerator process to run.</param>
        /// <returns>An IEnumerator for coroutine execution.</returns>
        private IEnumerator RunCoroutine(IEnumerator process)
        {
            yield return process;

            OnComplete?.Invoke();
            _coroutine = null;
        }

        #endregion Helpers

        #endregion Methods
    }
}
