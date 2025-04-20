/*------------------------------------------------------------------------------
File:       InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a utility class for testing interpolator implementations.
            This class includes methods for validating interpolator behavior
            with both valid and invalid progress values, as well as generating
            test cases for interpolation scenarios.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-20 06:05:04 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Provides utility methods and test cases for testing implementations of
    /// the <see cref="IInterpolator{TType}"/> interface.
    /// </summary>
    /// <typeparam name="TType">
    /// The type of the value being interpolated.
    /// </typeparam>
    public sealed class InterpolatorTests<TType> where TType : IEquatable<TType>
    {
        #region Fields

        /// <summary>
        /// Stores valid progress test cases for interpolation.
        /// </summary>
        private readonly Dictionary<string, TestCaseData>
            _validProgressTests;

        /// <summary>
        /// Helper for managing valid progress test cases.
        /// </summary>
        private readonly TestCaseSourceHelper _validProgressHelper;

        /// <summary>
        /// Helper for managing invalid progress test cases.
        /// </summary>
        private readonly TestCaseSourceHelper _invalidProgressHelper;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the collection of valid progress test cases.
        /// </summary>
        public IEnumerable<TestCaseData> ValidProgressTestCases =>
            _validProgressHelper.GetTestCases();

        /// <summary>
        /// Gets the collection of invalid progress test cases.
        /// </summary>
        public IEnumerable<TestCaseData> InvalidProgressTestCases =>
            _invalidProgressHelper.GetTestCases();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="InterpolatorTests{TType}"/> class.
        /// </summary>
        public InterpolatorTests()
        {
            // Initialize valid progress test cases with default values.
            _validProgressTests = CreateProgressTests(
                default, default, default, default);

            // Initialize helpers with appropriate test cases.
            _validProgressHelper = new(_validProgressTests);
            _invalidProgressHelper = new();
        }

        /// <summary>
        /// Adds or overwrites valid progress test cases.
        /// </summary>
        /// <param name="validTests">
        /// The dictionary of valid test cases to add.
        /// </param>
        /// <returns>
        /// The current instance of <see cref="InterpolatorTests{TType}"/>
        /// .</returns>
        public InterpolatorTests<TType> AddProgressTests(
            Dictionary<string, TestCaseData> validTests)
        {
            _validProgressHelper.Overwrite(validTests);
            return this;
        }

        /// <summary>
        /// Validates that the interpolator returns the expected value for a 
        /// given progress.
        /// </summary>
        /// <param name="interpolator">The interpolator to test.</param>
        /// <param name="progress">The progress value to test.</param>
        /// <param name="expectedValue">The expected interpolated value.</param>
        public void ValidProgress(IInterpolator<TType> interpolator,
            float progress, TType expectedValue)
        {
            // Act
            var result = interpolator.Interpolate(progress);

            // Assert
            Assert.AreEqual(expectedValue, result,
                $"Expected {expectedValue} " +
                $"but got {result} for progress {progress}");
        }

        /// <summary>
        /// Validates that the interpolator throws an exception for invalid 
        /// progress values.
        /// </summary>
        /// <param name="interpolator">The interpolator to test.</param>
        /// <param name="progress">The invalid progress value to test.</param>
        public void InvalidProgress(IInterpolator<TType> interpolator,
            float progress)
        {
            // Act
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                { var result = interpolator.Interpolate(progress); },
                $"Expected ArgumentOutOfRangeException for progress {progress}"
            );
        }

        /// <summary>
        /// Creates a dictionary of test cases for various progress values.
        /// </summary>
        /// <param name="start">The starting value for interpolation.</param>
        /// <param name="end">The ending value for interpolation.</param>
        /// <param name="half">The value at 50% progress.</param>
        /// <param name="nanValue">
        /// A value representing NaN for invalid cases.
        /// </param>
        /// <returns>A dictionary of test cases for interpolation.</returns>
        public static Dictionary<string, TestCaseData> CreateProgressTests(
            TType start, TType end, TType half, TType nanValue)
        {
            return new()
            {
                {
                    TestCases.ProgressOfZero,
                    new TestCaseData(0.0f, start)
                },
                {
                    TestCases.ProgressOfHalf,
                    new TestCaseData(0.5f, half)
                },
                {
                    TestCases.ProgressOfOne,
                    new TestCaseData(1.0f, end)
                },
                {
                    TestCases.ProgressOfNegativeOne,
                    new TestCaseData(-1.0f, start)
                },
                {
                    TestCases.ProgressOfTwo,
                    new TestCaseData(2.0f, end)
                },
                {
                    TestCases.NaNProgress,
                    new TestCaseData(float.NaN, nanValue)
                },
                {
                    TestCases.PositiveInfinityProgress,
                    new TestCaseData(float.PositiveInfinity, end)
                },
                {
                    TestCases.NegativeInfinityProgress,
                    new TestCaseData(float.NegativeInfinity, start)
                }
            };
        }

        #endregion Methods
    }
}
