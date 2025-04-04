/*------------------------------------------------------------------------------
File:       BaseTweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base class for unit tests of tween implementations. 
            This class includes common setup and tests for tween functionality 
            such as applying progress and managing update listeners.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-03 21:17:25 
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
    public abstract class BaseTweenTests<TType> : IITweenTests
        where TType : IEquatable<TType>
    {
        #region Fields

        /// <summary>
        /// Mock interpolator used for testing.
        /// </summary>
        protected Mock<IInterpolator<TType>> _mockInterpolator;

        /// <summary>
        /// Easing function used for testing.
        /// </summary>
        protected Func<float, float> _easing;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Reference to the BaseTween instance being tested.
        /// </summary>
        protected BaseTween<TType> BaseTweenRef { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            _mockInterpolator = new Mock<IInterpolator<TType>>();
            _easing = progress => progress; // Simple linear easing function
            ITweenRef = BaseTweenRef =
                CreateTween(_mockInterpolator.Object, _easing)
                as BaseTween<TType>;
        }

        /// <summary>
        /// Creates an instance of the tween being tested.
        /// </summary>
        /// <param name="interpolator">
        /// The interpolator to use for the tween.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the tween.
        /// </param>
        /// <returns>An instance of the tween being tested.</returns>
        protected abstract ITween CreateTween(
            IInterpolator<TType> interpolator,
            Func<float, float> easing);

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the 
        /// interpolator is null.
        /// </summary>
        [Test]
        public virtual void Constructor_NullInterpolator_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => CreateTween(null, _easing));
        }

        /// <summary>
        /// Tests that the constructor throws an ArgumentNullException when the
        /// easing function is null.
        /// </summary>
        [Test]
        public virtual void Constructor_NullEasing_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(
                () => CreateTween(_mockInterpolator.Object, null));
        }

        /// <summary>
        /// Tests that ApplyProgress invokes the OnUpdate event with a valid
        /// progress value.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_ValidProgress_InvokesOnUpdate()
        {
            // Arrange
            TType interpolatedValue = default;
            _mockInterpolator.Setup(i => i.Interpolate(It.IsAny<float>()))
                .Returns(interpolatedValue);
            bool onUpdateCalled = false;
            BaseTweenRef.AddOnUpdateListener(value => onUpdateCalled = true);

            // Act
            BaseTweenRef.ApplyProgress(0.5f);

            // Assert
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
            _mockInterpolator.Setup(i => i.Interpolate(It.IsAny<float>()))
                .Returns(expectedValue);
            TType actualValue = default;
            BaseTweenRef.AddOnUpdateListener(value => actualValue = value);

            // Act
            BaseTweenRef.ApplyProgress(0.5f);

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
            BaseTweenRef.AddOnUpdateListener(value => listener1Called = true);
            BaseTweenRef.AddOnUpdateListener(value => listener2Called = true);

            // Act
            BaseTweenRef.ApplyProgress(0.5f);

            // Assert
            Assert.IsTrue(listener1Called);
            Assert.IsTrue(listener2Called);
        }

        /// <summary>
        /// Tests that a valid action can be added as an update listener.
        /// </summary>
        [Test]
        public virtual void AddOnUpdateListener_ValidAction_ActionAdded()
        {
            // Arrange
            bool onUpdateCalled = false;
            Action<TType> action = value => onUpdateCalled = true;
            BaseTweenRef.AddOnUpdateListener(action);

            // Act
            BaseTweenRef.ApplyProgress(0.5f);

            // Assert
            Assert.IsTrue(onUpdateCalled);
        }

        /// <summary>
        /// Tests that a valid action can be removed as an update listener.
        /// </summary>
        [Test]
        public virtual void RemoveOnUpdateListener_ValidAction_ActionRemoved()
        {
            // Arrange
            bool onUpdateCalled = false;
            Action<TType> action = value => onUpdateCalled = true;
            BaseTweenRef.AddOnUpdateListener(action);

            // Act
            BaseTweenRef.RemoveOnUpdateListener(action);
            BaseTweenRef.ApplyProgress(0.5f);

            // Assert
            Assert.IsFalse(onUpdateCalled);
        }

        #endregion Methods
    }
}
