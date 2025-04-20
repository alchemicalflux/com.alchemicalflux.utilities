/*------------------------------------------------------------------------------
File:       TwoPointInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides an abstract base class for unit tests of the 
            TwoPointInterpolator<TType> class. This class defines common test 
            cases for verifying the behavior of interpolators that span two 
            values, including property validation and progress-based 
            interpolation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 06:05:04 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of
    /// <see cref="TwoPointInterpolator{TType}"/>.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of the value being interpolated.
    /// </typeparam>
    public abstract class TwoPointInterpolatorTests<TType>
        : IInterpolatorTests<TType>
        where TType : IEquatable<TType>
    {
        #region Properties

        /// <summary>
        /// Gets the instance of the <see cref="TwoPointInterpolator{TType}"/>
        /// being tested.
        /// </summary>
        protected abstract
            TwoPointInterpolator<TType> TwoPointInterpolator
        { get; set; }

        #endregion Properties

        #region IInterpolator

        /// <inheritdoc />
        [Test]
        public abstract void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, TType expectedValue);

        /// <inheritdoc />
        [Test]
        public abstract void InterpolatorTests_Progress_ThrowsArgumentOutOfRangeException(
            float progress);

        #endregion IInterpolator

        #region Methods

        /// <summary>
        /// Sets up the test environment before each test is executed.
        /// </summary>
        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that the <see cref="TwoPointInterpolator{TType}.Start"/>
        /// property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public virtual void StartProperty_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            TType startValue = GetDefaultValue();

            // Act
            TwoPointInterpolator.Start = startValue;
            TType actualValue = TwoPointInterpolator.Start;

            // Assert
            Assert.AreEqual(startValue, actualValue, 
                "The Start property did not return the expected value.");
        }

        /// <summary>
        /// Tests that the <see cref="TwoPointInterpolator{TType}.End"/> 
        /// property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public virtual void EndProperty_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            TType endValue = GetDefaultValue();

            // Act
            TwoPointInterpolator.End = endValue;
            TType actualValue = TwoPointInterpolator.End;

            // Assert
            Assert.AreEqual(endValue, actualValue, 
                "The End property did not return the expected value.");
        }

        /// <summary>
        /// Provides a default value for the type <typeparamref name="TType"/>.
        /// Override this method in derived classes to provide meaningful test 
        /// values.
        /// </summary>
        /// <returns>
        /// A default value for the type <typeparamref name="TType"/>.
        /// </returns>
        protected virtual TType GetDefaultValue()
        {
            return default;
        }

        #endregion Methods
    }
}
