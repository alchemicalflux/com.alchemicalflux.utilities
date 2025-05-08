/*------------------------------------------------------------------------------
File:       MonoBehaviourTweenPlayerTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for MonoBehaviourTweenPlayer in Play Mode.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-08 00:34:36 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using Moq;
using UnityEngine;
using UnityEngine.TestTools;
using System.Collections;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="MonoBehaviourTweenPlayer"/> class in Play
    /// Mode.
    /// </summary>
    public class MonoBehaviourTweenPlayerTests
    {
        private GameObject _testGameObject;
        private MonoBehaviourTweenPlayer _tweenPlayer;

        /// <summary>
        /// Sets up the test environment by creating a new GameObject and 
        /// adding a <see cref="MonoBehaviourTweenPlayer"/> component to it.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _testGameObject = new GameObject("TestGameObject");
            _tweenPlayer =
                _testGameObject.AddComponent<MonoBehaviourTweenPlayer>();
        }

        /// <summary>
        /// Cleans up the test environment by destroying the test GameObject.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_testGameObject);
        }

        /// <summary>
        /// Tests that calling <see cref="MonoBehaviourTweenPlayer.Play"/> with 
        /// the hideOnComplete flag set to true hides all tweens after
        /// completion.
        /// </summary>
        [UnityTest]
        public IEnumerator Play_WithHideOnComplete_CallsShowFalseOnCompletion()
        {
            // Arrange
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.Play(0.1f, progress => progress, hideOnComplete: true);

            // Wait for the tween to complete
            yield return new WaitForSeconds(0.2f);

            // Assert
            mockTween.Verify(t => t.Show(false), Times.Once);
        }

        /// <summary>
        /// Tests that calling <see cref="MonoBehaviourTweenPlayer.Pause"/>
        /// stops the coroutine and prevents further progress.
        /// </summary>
        [UnityTest]
        public IEnumerator Pause_StopsCoroutine()
        {
            // Arrange
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.Play(0.1f, progress => progress, hideOnComplete: true);

            // Wait for a short duration and then pause
            yield return new WaitForSeconds(0.05f);
            _tweenPlayer.Pause();
            yield return new WaitForSeconds(0.1f);

            // Assert
            mockTween.Verify(t => t.Show(false), Times.Never);
        }
    }
}
