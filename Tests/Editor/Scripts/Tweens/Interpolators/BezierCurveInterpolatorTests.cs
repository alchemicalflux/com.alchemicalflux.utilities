/*------------------------------------------------------------------------------
File:       BezierCurveInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of BezierCurveInterpolator.
            Provides common test cases and utilities for derived test classes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-20 22:43:12 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of
    /// <see cref="BezierCurveInterpolator{TType}"/>.
    /// Provides common test cases and utilities for derived test classes.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of the value being interpolated.
    /// </typeparam>
    public abstract class BezierCurveInterpolatorTests<TType>
        : IInterpolatorTests<TType>
        where TType : IEquatable<TType>
    {
        #region Properties

        /// <summary>
        /// Gets or sets the instance of the 
        /// <see cref="BezierCurveInterpolator{TType}"/> being tested.
        /// </summary>
        protected abstract
            BezierCurveInterpolator<TType> BezierCurveInterpolator
        { get; set; }

        /// <summary>
        /// Gets or sets the instance of the <see cref="IInterpolator{TType}"/>
        /// being tested.
        /// </summary>
        /// <remarks>
        /// This property is used to provide a generic interface for the
        /// interpolator under test, allowing for polymorphic test code.
        /// </remarks>
        protected IInterpolator<TType> IInterpolator
        {
            get => BezierCurveInterpolator;
            set => BezierCurveInterpolator =
                (BezierCurveInterpolator<TType>)value;
        }

        #endregion Properties

        #region Methods

        #region Overrides

        /// <summary>
        /// Tests that the interpolator returns the expected value for valid
        /// progress inputs.
        /// </summary>
        /// <param name="progress">
        /// The progress value to test, typically between 0 and 1.
        /// </param>
        /// <param name="expectedValue">
        /// The expected interpolated value for the given progress.
        /// </param>
        [Test]
        public abstract void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress,
            TType expectedValue);

        /// <summary>
        /// Tests that the interpolator throws an 
        /// <see cref="ArgumentOutOfRangeException"/> 
        /// for invalid progress inputs.
        /// </summary>
        /// <param name="progress">
        /// The invalid progress value to test, such as values less than 0 or 
        /// greater than 1.
        /// </param>
        [Test]
        public abstract void InterpolatorTests_Progress_ThrowsArgumentOutOfRangeException(
            float progress);

        #endregion Overrides

        /// <summary>
        /// Sets up the test environment before each test is executed.
        /// Override this method in derived classes to perform custom setup.
        /// </summary>
        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that the <see cref="BezierCurveInterpolator{TType}.Nodes"/>
        /// property correctly stores and retrieves the list of nodes.
        /// </summary>
        [Test]
        public void NodesProperty_SetAndGet_ReturnsCorrectValue()
        {
            // Arrange
            var nodes = new List<TType>
            {
                GetDefaultValue(), GetDefaultValue(), GetDefaultValue()
            };

            // Act
            BezierCurveInterpolator.Nodes = nodes;
            var actualNodes = BezierCurveInterpolator.Nodes;

            // Assert
            Assert.AreEqual(nodes, actualNodes,
                "The Nodes property did not return the expected value.");
        }

        /// <summary>
        /// Tests that the 
        /// <see cref="BezierCurveInterpolator{TType}.Nodes"/> property
        /// correctly returns the number of nodes.
        /// </summary>
        [Test]
        public void NodeCountProperty_ReturnsCorrectValue()
        {
            // Arrange
            var nodes = new List<TType>
            {
                GetDefaultValue(), GetDefaultValue(), GetDefaultValue()
            };
            BezierCurveInterpolator.Nodes = nodes;

            // Act
            var nodeCount = BezierCurveInterpolator.Nodes.Count;

            // Assert
            Assert.AreEqual(nodes.Count, nodeCount,
                "The NodeCount property did not return the expected value.");
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

