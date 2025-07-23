/*------------------------------------------------------------------------------
File:       BaseTweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base class for unit tests of tween implementations. 
            This class includes common setup and tests for tween functionality 
            such as applying progress and managing update listeners.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-22 20:37:12 
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

        protected abstract 
            Mock<IInterpolator<TType>> MockInterpolator { get; set; }

        protected abstract Func<float, float> EasingFunction { get; set; }

        /// <summary>
        /// Reference to the BaseTween instance being tested.
        /// </summary>
        protected abstract BaseTween<TType> BaseTweenRef { get; set; }

        #endregion Properties

        #region Methods

        #region Overrides

        /// <inheritdoc/>
        public abstract void ApplyProgress_ValidProgress_DoesNotThrowException(float progress);

        /// <inheritdoc/>
        public abstract void ApplyProgress_InvalidProgress_ThrowsArgumentOutOfRangeException(float progress);

        #endregion Overrides

        #region Exposed Methods

        /// <summary>
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that ApplyProgress invokes the OnUpdate event with a valid
        /// progress value.
        /// </summary>
        [Test]
        public virtual 
            void ApplyProgress_ValidProgress_InvokesOnUpdate()
        {
            // Arrange
            bool onUpdateCalled = false;
            BaseTweenRef.OnUpdate += value => onUpdateCalled = true;

            // Act
            BaseTweenRef.ApplyProgress(1);

            // Assert
            MockInterpolator.Verify(t => t.Interpolate(1), Times.Once);
            Assert.IsTrue(onUpdateCalled);
        }

        /// <summary>
        /// Tests that ApplyProgress invokes the OnUpdate event with the correct
        /// interpolated value.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_ValidProgress_InvokesOnUpdateWithCorrectValue()
        {
            // Arrange
            TType expectedValue = default;
            MockInterpolator.Setup(i => i.Interpolate(It.IsAny<float>()))
                .Returns(expectedValue);
            TType actualValue = default;
            BaseTweenRef.OnUpdate += value => actualValue = value;

            // Act
            BaseTweenRef.ApplyProgress(1);

            // Assert
            Assert.AreEqual(expectedValue, actualValue);
        }

        /// <summary>
        /// Tests that multiple update listeners are called.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_MultipleListeners_AllListenersCalled()
        {
            // Arrange
            bool listener1Called = false;
            bool listener2Called = false;
            BaseTweenRef.OnUpdate += value => listener1Called = true;
            BaseTweenRef.OnUpdate += value => listener2Called = true;

            // Act
            BaseTweenRef.ApplyProgress(0.5f);

            // Assert
            Assert.IsTrue(listener1Called);
            Assert.IsTrue(listener2Called);
        }

        #endregion Exposed Methods

        #endregion Methods
    }
}
