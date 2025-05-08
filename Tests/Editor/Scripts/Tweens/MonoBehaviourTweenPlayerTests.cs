/*------------------------------------------------------------------------------
File:       MonoBehaviourTweenPlayerTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for MonoBehaviourTweenPlayer in Edit Mode.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-08 00:34:36 
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
        /// Tests that call <see cref="MonoBehaviourTweenPlayer.Play"/> with
        /// valid parameters starts playback and shows all tweens.
        /// </summary>
        [Test]
        public void Play_ValidParameters_StartsPlayback()
        {
            // Arrange
            var mockTween = new Mock<ITween>();
            _tweenPlayer.Tweens.Add(mockTween.Object);

            // Act
            _tweenPlayer.Play(1.0f, progress => progress);

            // Assert
            mockTween.Verify(t => t.Show(true), Times.Once);
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
                _tweenPlayer.Play(1.0f, null));
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
        [TestCase(6f, TestName = "TimeIsOverRange")]
        public void SnapToTime_ThrowsArgumentOutOfRangeException(float time)
        {
            // Arrange
            _tweenPlayer.Play(5f, t => t); // Set PlayTime to 5 seconds

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() =>
                _tweenPlayer.SnapToTime(time));
            Assert.That(ex.Message, Does.Contain(
                    "Time must be within the range of the play time."));
        }

        #endregion Methods
    }
}
