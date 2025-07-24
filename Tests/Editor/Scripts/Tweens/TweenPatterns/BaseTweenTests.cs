/*------------------------------------------------------------------------------
File:       BaseTweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base class for unit tests of tween implementations. 
            This class includes common setup and tests for tween functionality 
            such as applying progress and managing update listeners.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-23 21:12:05 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using Moq;
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Provides a base class for unit tests of tween implementations.
    /// This class includes common setup and tests for tween functionality 
    /// such as applying progress and managing update listeners.
    /// </summary>
    /// <typeparam name="TType">The type of the value being tweened.</typeparam>
    public abstract class BaseTweenTests<TType> : ITweenTests
        where TType : IEquatable<TType>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the mock interpolator used for testing.
        /// </summary>
        protected abstract Mock<IInterpolator<TType>> MockInterpolator { get; set; }

        /// <summary>
        /// Gets or sets the easing function used for testing.
        /// </summary>
        protected abstract Func<float, float> EasingFunction { get; set; }

        /// <summary>
        /// Gets or sets the reference to the <see cref="BaseTween{TType}"/>
        /// instance being tested.
        /// </summary>
        protected abstract BaseTween<TType> BaseTweenRef { get; set; }

        #endregion Properties

        #region Methods

        #region Overrides

        /// <summary>
        /// Applies a valid progress value to the tween and verifies that no
        /// exception is thrown.
        /// </summary>
        /// <param name="progress">
        /// A valid progress value, typically in the range [0, 1].
        /// </param>
        public abstract void ApplyProgress_ValidProgress_DoesNotThrowException(float progress);

        /// <summary>
        /// Applies an invalid progress value to the tween and verifies that an
        /// <see cref="ArgumentOutOfRangeException"/> is thrown.
        /// </summary>
        /// <param name="progress">
        /// An invalid progress value, typically outside the range [0, 1].
        /// </param>
        public abstract void ApplyProgress_InvalidProgress_ThrowsArgumentOutOfRangeException(float progress);

        #endregion Overrides

        #region Exposed Methods

        /// <summary>
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that applying the minimum progress value invokes the
        /// <see cref="BaseTween{TType}.OnUpdate"/> event with the correct
        /// value.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_MinProgress_InvokesOnUpdateWithCorrectValue()
        {
            // Arrange
            TType expectedValue = GetMinExpectedValue();
            MockInterpolator.Setup(i => i.Interpolate(It.IsAny<float>()))
                .Returns(expectedValue);
            TType actualValue = default;
            BaseTweenRef.OnUpdate += value => actualValue = value;

            // Act
            BaseTweenRef.ApplyProgress(BaseTweenRef.MinProgress);

            // Assert
            MockInterpolator.Verify(
                t => t.Interpolate(BaseTweenRef.MinProgress), Times.Once);
            Assert.AreEqual(expectedValue, actualValue);
        }

        /// <summary>
        /// Tests that applying the maximum progress value invokes the
        /// <see cref="BaseTween{TType}.OnUpdate"/> event with the correct
        /// value.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_MaxProgress_InvokesOnUpdateWithCorrectValue()
        {
            // Arrange
            var expectedValue = GetMinExpectedValue();
            MockInterpolator.Setup(i => i.Interpolate(It.IsAny<float>()))
                .Returns(expectedValue);
            TType actualValue = default;
            BaseTweenRef.OnUpdate += value => actualValue = value;

            // Act
            BaseTweenRef.ApplyProgress(BaseTweenRef.MaxProgress);

            // Assert
            MockInterpolator.Verify(
                t => t.Interpolate(BaseTweenRef.MaxProgress), Times.Once);
            Assert.AreEqual(expectedValue, actualValue);
        }

        #endregion Exposed Methods

        #region Internal Methods

        /// <summary>
        /// Gets the expected value for the minimum progress.
        /// </summary>
        /// <returns>The expected value for the minimum progress.</returns>
        protected abstract TType GetMinExpectedValue();

        /// <summary>
        /// Gets the expected value for the maximum progress.
        /// </summary>
        /// <returns>The expected value for the maximum progress.</returns>
        protected abstract TType GetMaxExpectedValue();

        #endregion Internal Methods

        #endregion Methods
    }
}
