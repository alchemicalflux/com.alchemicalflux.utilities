/*------------------------------------------------------------------------------
File:       MonoBehaviourTweenPlayerTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for MonoBehaviourTweenPlayer in Edit Mode.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-16 22:55:50 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using Moq;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Unit tests for the <see cref="MonoBehaviourTweenPlayer"/> class.
    /// </summary>
    public sealed class MonoBehaviourTweenPlayerTests
    {
        #region Constants

        /// <summary>
        /// The duration of the test playback in seconds.
        /// </summary>
        private const float TestPlayTime = 5.0f;

        #endregion Constants

        #region Fields

        /// <summary>
        /// The GameObject used to host the 
        /// <see cref="MonoBehaviourTweenPlayer"/> component during testing.
        /// </summary>
        private GameObject _testGameObject;

        /// <summary>
        /// The instance of <see cref="MonoBehaviourTweenPlayer"/> being tested.
        /// </summary>
        private MonoBehaviourTweenPlayer _tweenPlayer;

        #endregion Fields

        #region Methods

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
            UnityEngine.Object.DestroyImmediate(_testGameObject);
        }

        /// <summary>
        /// Tests that the <see cref="MonoBehaviourTweenPlayer.Tweens"/> 
        /// property is initialized as an empty <see cref="HashSet{T}"/>.
        /// </summary>
        [Test]
        public void Tweens_InitializedAsEmptyHashSet()
        {
            Assert.IsNotNull(_tweenPlayer.Tweens);
            Assert.IsEmpty(_tweenPlayer.Tweens);
        }

        /// <summary>
        /// Tests that call <see cref="MonoBehaviourTweenPlayer.Play"/> with an
        /// invalid play time throws an 
        /// <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        [Test]
        public void Play_InvalidPlayTime_ThrowsException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                _tweenPlayer.Play(0, progress => progress));
        }

        /// <summary>
        /// Tests that call <see cref="MonoBehaviourTweenPlayer.Play"/> with a
        /// null easing interpreter throws an 
        /// <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test]
        public void Play_NullEasingInterpreter_ThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
                _tweenPlayer.Play(TestPlayTime, null));
        }

        [Test]
        public void Play_SetsPropertiesCorrectly()
        {
            // Act
            _tweenPlayer.Play(TestPlayTime, t => t * t, null, true);

            // Assert
            Assert.AreEqual(TestPlayTime, _tweenPlayer.PlayTime);
            Assert.IsNotNull(_tweenPlayer.EasingInterpreter);
        }

        [Test]
        public void Play_MultipleTimes_ResetsState()
        {
            // Arrange
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.Play(TestPlayTime * 2, t => t);
            _tweenPlayer.Pause();
            _tweenPlayer.Play(TestPlayTime, t => t);

            // Assert
            Assert.AreEqual(TestPlayTime, _tweenPlayer.PlayTime, 
                "PlayTime should be updated on second Play call.");
        }

        [Test]
        public void Play_WithOptions_AssignsCallbacks()
        {
            // Arrange
            bool onCompleteCalled = false;
            var options = new TweenPlaybackOptions 
            {
                OnComplete = () => onCompleteCalled = true
            };

            // Act
            _tweenPlayer.Play(TestPlayTime, t => t, options);

            // Simulate completion
            options.OnComplete?.Invoke();

            // Assert
            Assert.IsTrue(onCompleteCalled,
                "OnComplete callback should be assigned and invoked.");
        }

        [Test]
        public void Stop_StopsCoroutine()
        {
            // Arrange
            _tweenPlayer.Play(TestPlayTime, t => t);

            // Act
            var result = _tweenPlayer.Stop();

            // Assert
            Assert.IsTrue(result,
                "Stop should return true when coroutine is running.");
        }

        [Test]
        public void Pause_StopsCoroutine()
        {
            // Arrange
            _tweenPlayer.Play(TestPlayTime, t => t);
            var result = _tweenPlayer.Pause();

            // Assert
            Assert.IsTrue(result,
                "Pause should return true when coroutine is running.");
        }

        [Test]
        public void Resume_StartsCoroutine()
        {
            // Arrange
            _tweenPlayer.Play(TestPlayTime, t => t);
            _tweenPlayer.Pause();

            // Act
            var result = _tweenPlayer.Resume();

            // Assert
            Assert.IsTrue(result,
                "Resume should return true when not running and not complete.");
        }

        /// <summary>
        /// Tests that call <see cref="MonoBehaviourTweenPlayer.SnapToStart"/> 
        /// sets the progress of all tweens to zero.
        /// </summary>
        [Test]
        public void SnapToStart_SetsProgressToZero()
        {
            // Arrange
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.SnapToStart();

            // Assert
            mockTween.Verify(t => t.ApplyProgress(0), Times.Once);
        }

        /// <summary>
        /// Tests that call <see cref="MonoBehaviourTweenPlayer.SnapToEnd"/>
        /// sets the progress of all tweens to one.
        /// </summary>
        [Test]
        public void SnapToEnd_SetsProgressToOne()
        {
            // Arrange
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.SnapToEnd();

            // Assert
            mockTween.Verify(t => t.ApplyProgress(1), Times.Once);
        }

        /// <summary>
        /// Tests that call <see cref="MonoBehaviourTweenPlayer.SnapToTime"/>
        /// with a time value outside the valid range throws an
        /// <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="time">The time value to test.</param>
        [Test]
        [TestCase(-1f, TestName = "TimeIsUnderRange")]
        [TestCase(TestPlayTime + 1, TestName = "TimeIsOverRange")]
        public void SnapToTime_ThrowsArgumentOutOfRangeException(float time)
        {
            // Arrange
            _tweenPlayer.Play(TestPlayTime, t => t);

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                _tweenPlayer.SnapToTime(time));
        }

        [Test]
        [TestCase(TestPlayTime * 0.00f, TestName = "SnapToTime - 0%")]
        [TestCase(TestPlayTime * 0.25f, TestName = "SnapToTime - 25%")]
        [TestCase(TestPlayTime * 0.66f, TestName = "SnapToTime - 66%")]
        [TestCase(TestPlayTime * 1.00f, TestName = "SnapToTime - 100%")]
        public void SnapToTime_ProcessesValidTime(float time)
        {
            // Arrange
            _tweenPlayer.Play(TestPlayTime, t => t);
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.SnapToTime(time);

            // Assert
            mockTween.Verify(
                t => t.ApplyProgress(time / TestPlayTime),
                Times.Once);
        }

        #endregion Methods
    }
}
