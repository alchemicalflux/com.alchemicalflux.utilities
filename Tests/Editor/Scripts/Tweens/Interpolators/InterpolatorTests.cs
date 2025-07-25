/*------------------------------------------------------------------------------
File:       InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a utility class for testing interpolator implementations.
            This class includes methods for validating interpolator behavior
            with both valid and invalid progress values, as well as generating
            test cases for interpolation scenarios.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-23 21:12:05 
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
        #region Properties

        /// <summary>
        /// Gets the helper for managing valid progress test cases.
        /// </summary>
        public TestCaseSourceHelper ValidProgressTests { get; private set; }

        /// <summary>
        /// Gets the helper for managing invalid progress test cases.
        /// </summary>
        public TestCaseSourceHelper InvalidProgressTests { get; private set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="InterpolatorTests{TType}"/> class.
        /// </summary>
        public InterpolatorTests()
        {
            // Initialize valid progress test cases with default values.
            var validProgressTests = CreateProgressTests(
                default, default, null, default);

            // Initialize helpers with appropriate test cases.
            ValidProgressTests = new(validProgressTests);
            InvalidProgressTests = new();
        }

        /// <summary>
        /// Validates that the interpolator returns the expected value for a 
        /// given progress.
        /// </summary>
        /// <param name="interpolator">The interpolator to test.</param>
        /// <param name="progress">The progress value to test.</param>
        /// <param name="expectedValue">The expected interpolated value.</param>
        public void ValidProgress(
            IInterpolator<TType> interpolator,
            float progress,
            TType expectedValue)
        {
            ValidProgress(interpolator, progress, expectedValue, CheckIfEqual);
        }

        /// <summary>
        /// Validates that the interpolator returns the expected value for a 
        /// given progress using a custom equality function.
        /// </summary>
        /// <param name="interpolator">The interpolator to test.</param>
        /// <param name="progress">The progress value to test.</param>
        /// <param name="expectedValue">The expected interpolated value.</param>
        /// <param name="checkEqualFunc">
        /// A function to compare the expected and actual values.
        /// </param>
        public void ValidProgress(
            IInterpolator<TType> interpolator,
            float progress,
            TType expectedValue,
            Func<TType, TType, bool> checkEqualFunc)
        {
            // Arrange
            ValidProgressTests.IgnoreIfNoTestCases();

            // Act
            var result = interpolator.Interpolate(progress);

            // Assert
            Assert.IsTrue(checkEqualFunc(expectedValue, result),
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
            // Arrange
            InvalidProgressTests.IgnoreIfNoTestCases();

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                interpolator.Interpolate(progress),
                $"Expected ArgumentOutOfRangeException for progress {progress}"
            );
        }

        /// <summary>
        /// Creates a dictionary of test cases for various progress values.
        /// </summary>
        /// <param name="start">The starting value for interpolation.</param>
        /// <param name="end">The ending value for interpolation.</param>
        /// <param name="safeTests">
        /// A dictionary of safe progress values and their expected results.
        /// </param>
        /// <param name="nanValue">
        /// A value representing NaN for invalid cases.
        /// </param>
        /// <returns>A dictionary of test cases for interpolation.</returns>
        public static Dictionary<string, TestCaseData> CreateProgressTests(
            TType start,
            TType end,
            IDictionary<float, TType> safeTests,
            TType nanValue)
        {
            var baseLineTests = new Dictionary<string, TestCaseData>()
            {
                {
                    TestCases.ProgressOfZero,
                    new TestCaseData(0.0f, start)
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

            if(safeTests?.Count > 0)
            {
                foreach(var pair in safeTests)
                {
                    baseLineTests.Add($"Progress: {pair.Key:0.###}",
                        new TestCaseData(pair.Key, pair.Value));
                }
            }

            return baseLineTests;
        }

        /// <summary>
        /// Default method to compare two objects of type TType for equality.
        /// </summary>
        /// <param name="expected">The expected value.</param>
        /// <param name="actual">The actual value.</param>
        /// <returns>True if the values are equal; otherwise, false.</returns>
        private static bool CheckIfEqual(TType expected, TType actual)
        {
            return expected != null && expected.Equals(actual);
        }

        #endregion Methods
    }
}
