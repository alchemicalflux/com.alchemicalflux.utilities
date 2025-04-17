/*------------------------------------------------------------------------------
File:       TwoPointInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base class for unit tests of TwoPointInterpolator 
            implementations. This class includes common setup and basic tests to
            ensure that interpolator implementations can be tested for basic
            functionality such as generating interpolated values based on 
            progress.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-16 19:18:32 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using Moq;
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Provides a base class for unit tests of TwoPointInterpolator implementations.
    /// This class includes common setup and basic tests to ensure that 
    /// interpolator implementations can be tested for basic functionality 
    /// such as generating interpolated values based on progress.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of the value being interpolated.
    /// </typeparam>
    public abstract class TwoPointInterpolatorTests<TType>
        : IInterpolatorTests<TType>
        where TType : IEquatable<TType>
    {
        #region Properties

        protected abstract IInterpolator<TType> _interpolator { get; }

        /// <summary>
        /// Mock TwoPointInterpolator used for testing.
        /// </summary>
        protected TwoPointInterpolator<TType> TwoPointInterpolator { get; set; }

        #endregion Properties

        #region IInterpolator

        public abstract void InterpolatorTests_ValidProgress_ReturnsExpectedValue(float progress, TType expectedValue);

        #endregion IInterpolator

        #region Methods

        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that the Start property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public virtual void StartProperty_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            TType startValue = default;
            TwoPointInterpolator.Start = startValue;

            // Act
            TType actualValue = TwoPointInterpolator.Start;

            // Assert
            Assert.AreEqual(startValue, actualValue);
        }

        /// <summary>
        /// Tests that the End property can be set and retrieved correctly.
        /// </summary>
        [Test]
        public virtual void EndProperty_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            TType endValue = default;
            TwoPointInterpolator.End = endValue;

            // Act
            TType actualValue = TwoPointInterpolator.End;

            // Assert
            Assert.AreEqual(endValue, actualValue);
        }

        #endregion Methods
    }
}
