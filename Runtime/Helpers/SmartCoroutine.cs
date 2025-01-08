/*------------------------------------------------------------------------------
File:       SmartCoroutine.cs 
Project:    AlchemicalFlux Utilities
Overview:   Wrapper for MonoBehavior driven coroutines, providing simple
            function calls and event handles.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using System;
using System.Collections;
using UnityEngine;

namespace AlchemicalFlux.Utilities
{
    public class SmartCoroutine
    {
        #region Members

        public Action OnComplete;

        public Action OnStop;

        private readonly MonoBehaviour _mono;

        private Coroutine _coroutine;

        #endregion Members

        #region Properties

        public bool IsRunning { get { return _coroutine != null; } }

        #endregion Properties

        #region Methods

        #region Constructors

        private SmartCoroutine() { }

        public SmartCoroutine(MonoBehaviour mono)
        {
            _mono = mono;
        }

        ~SmartCoroutine()
        {
            Stop();
        }

        #endregion Constructors

        #region Controls

        public void Start(IEnumerator process)
        {
            Stop();
            _coroutine = _mono.StartCoroutine(RunCoroutine(process));
        }

        public void Stop()
        {
            if(_coroutine == null) { return; }

            _mono.StopCoroutine(_coroutine);
            OnStop?.Invoke();
            _coroutine = null;
        }

        #endregion Controls

        #region Helpers

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
