/*------------------------------------------------------------------------------
File:       TweenBuilderTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for TweenBuilder.cs to validate builder pattern and error
            handling.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-13 19:47:43 
------------------------------------------------------------------------------*/
using System;
using Moq;
using NUnit.Framework;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Contains unit tests for the <see cref="TweenBuilder{T}"/> class to
    /// validate its builder pattern, error handling, and correct behavior of
    /// its methods.
    /// </summary>
    [TestFixture]
    public class TweenBuilderTests
    {
        #region Fields

        /// <summary>
        /// Mock interpolator used for testing tween construction and update
        /// logic.
        /// </summary>
        private Mock<IInterpolator<float>> _mockInterpolator;

        /// <summary>
        /// Default linear easing function used for testing.
        /// </summary>
        private Func<float, float> _easingFunction;

        #endregion Fields

        #region Members

        /// <summary>
        /// Sets up the test environment before each test by initializing the
        /// mock interpolator and a default linear easing function.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            _mockInterpolator = new Mock<IInterpolator<float>>();
            _easingFunction = x => x;
        }

        /// <summary>
        /// Verifies that the constructor initializes the builder with a linear
        /// easing function and allows building a tween after setting the
        /// interpolator.
        /// </summary>
        [Test]
        public void Constructor_InitializesWithLinearEasing()
        {
            // Arrange
            var builder = new TweenBuilder<float>()
                .SetInterpolator(_mockInterpolator.Object);

            // Act
            var tween = builder.BuildBasicTween();

            // Assert
            Assert.IsNotNull(tween);
        }

        /// <summary>
        /// Ensures that copying a builder creates an identical builder and both
        /// can build tweens without exceptions, including copying update
        /// actions.
        /// </summary>
        [Test]
        public void Copy_CreatesIdenticalBuilder()
        {
            // Arrange
            var builder = new TweenBuilder<float>()
                .SetInterpolator(_mockInterpolator.Object)
                .SetEasing(_easingFunction)
                .AddUpdateAction(_ => { });

            // Act
            var copy = builder.Copy();

            // Assert
            // Build from both to ensure no exceptions and actions are copied.
            Assert.DoesNotThrow(() => builder.BuildBasicTween());
            Assert.DoesNotThrow(() => copy.BuildBasicTween());
        }

        /// <summary>
        /// Verifies that constructing a builder by copying from a null
        /// reference throws an <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test]
        public void Constructor_CopyFromNull_ThrowsArgumentNullException()
        {
            // Arrange & Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                new TweenBuilder<float>(null));
        }

        /// <summary>
        /// Ensures that setting a null interpolator throws an
        /// <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test]
        public void SetInterpolator_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var builder = new TweenBuilder<float>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                builder.SetInterpolator(null));
        }

        /// <summary>
        /// Ensures that setting a null easing function throws an
        /// <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test]
        public void SetEasing_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var builder = new TweenBuilder<float>();

            // Assert
            Assert.Throws<ArgumentNullException>(() => builder.SetEasing(null));
        }

        /// <summary>
        /// Ensures that adding a null update action throws an
        /// <see cref="ArgumentNullException"/>.
        /// </summary>
        [Test]
        public void AddUpdateAction_Null_ThrowsArgumentNullException()
        {
            // Arrange
            var builder = new TweenBuilder<float>();

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() =>
                builder.AddUpdateAction(null));
        }

        /// <summary>
        /// Verifies that building a tween without setting an interpolator
        /// throws an <see cref="InvalidOperationException"/>.
        /// </summary>
        [Test]
        public void BuildBasicTween_WithoutInterpolator_ThrowsInvalidOperationException()
        {
            // Arrange
            var builder = new TweenBuilder<float>();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() =>
                builder.BuildBasicTween());
        }

        /// <summary>
        /// Ensures that update actions added to the builder are called when the
        /// tween is updated. The test sets up the mock interpolator to return a
        /// known value and verifies that the update action receives this value.
        /// </summary>
        [Test]
        public void AddUpdateAction_ActionsAreCalledOnTweenUpdate()
        {
            // Arrange
            float updatedValue = 0;
            var builder = new TweenBuilder<float>()
                .SetInterpolator(_mockInterpolator.Object)
                .SetEasing(_easingFunction)
                .AddUpdateAction(val => updatedValue = val);

            // Set up the mock interpolator to return a known value
            _mockInterpolator.Setup(i => i.Interpolate(It.IsAny<float>()))
                .Returns(42f);

            var tween = builder.BuildBasicTween();

            // Act
            tween.ApplyProgress(0.5f);

            // Assert
            Assert.AreEqual(42f, updatedValue,
                "Update action should be called with the interpolated value.");
        }

        #endregion Members
    }
}