/*------------------------------------------------------------------------------
File:       TweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides utility methods and test cases for testing implementations
            of the <see cref="ITween"/> interface.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-23 21:12:05 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Math;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Provides utility methods and test cases for testing implementations of
    /// the <see cref="ITween"/> interface.
    /// </summary>
    public sealed class TweenTests
    {
        #region Constants

        /// <summary>
        /// The minimum progress value for tween tests.
        /// </summary>
        private const float _minimumProgress = 0;

        /// <summary>
        /// The maximum progress value for tween tests.
        /// </summary>
        private const float _maximumProgress = 1;

        #endregion Constants

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
        /// Initializes a new instance of the <see cref="TweenTests"/> class.
        /// Sets up valid and invalid progress test case helpers using default
        /// min and max progress values.
        /// </summary>
        public TweenTests()
        {
            // Initialize valid progress test cases with default values.
            var validProgressTests = CreateValidProgressTests(
                _minimumProgress,
                _maximumProgress,
                null);
            var invalidProgressTests = CreateInvalidProgressTests(
                _minimumProgress,
                _maximumProgress,
                null);

            // Initialize helpers with appropriate test cases.
            ValidProgressTests = new(validProgressTests);
            InvalidProgressTests = new(invalidProgressTests);
        }

        /// <summary>
        /// Asserts that applying a valid progress value to the tween does not
        /// throw an exception.
        /// </summary>
        /// <param name="tween">The tween instance to test.</param>
        /// <param name="progress">A valid progress value.</param>
        public void ValidProgress(ITween tween, float progress)
        {
            // Arrange
            ValidProgressTests.IgnoreIfNoTestCases();

            // Act & Assert
            Assert.DoesNotThrow(() => tween.ApplyProgress(progress));
        }

        /// <summary>
        /// Asserts that applying an invalid progress value to the tween throws
        /// an <see cref="ArgumentOutOfRangeException"/>.
        /// </summary>
        /// <param name="tween">The tween instance to test.</param>
        /// <param name="progress">An invalid progress value.</param>
        public void InvalidProgress(ITween tween, float progress)
        {
            // Arrange
            InvalidProgressTests.IgnoreIfNoTestCases();

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                tween.ApplyProgress(progress));
        }

        /// <summary>
        /// Creates a dictionary of valid progress test cases for use in tween
        /// tests.
        /// </summary>
        /// <param name="minProgress">The minimum valid progress value.</param>
        /// <param name="maxProgress">The maximum valid progress value.</param>
        /// <param name="safeTests">
        /// Optional additional valid progress values.
        /// </param>
        /// <returns>
        /// A dictionary mapping test case names to <see cref="TestCaseData"/>.
        /// </returns>
        public static Dictionary<string, TestCaseData> CreateValidProgressTests(
            float minProgress,
            float maxProgress,
            IList<float> safeTests)
        {
            var baseLineTests = new Dictionary<string, TestCaseData>()
            {
                {
                    TestCases.MinProgress,
                    new TestCaseData(minProgress)
                },
                {
                    TestCases.ProgressOfHalf,
                    new TestCaseData(
                        (minProgress + maxProgress) / 2)
                },
                {
                    TestCases.MaxProgress,
                    new TestCaseData(maxProgress)
                },
            };

            if(safeTests?.Count > 0)
            {
                foreach(var value in safeTests)
                {
                    baseLineTests.Add($"Progress: {value:0.###}",
                        new TestCaseData(value));
                }
            }

            return baseLineTests;
        }

        /// <summary>
        /// Creates a dictionary of invalid progress test cases for use in tween
        /// tests.
        /// </summary>
        /// <param name="minProgress">The minimum valid progress value.</param>
        /// <param name="maxProgress">The maximum valid progress value.</param>
        /// <param name="failTests">
        /// Optional additional invalid progress values.
        /// </param>
        /// <returns>
        /// A dictionary mapping test case names to <see cref="TestCaseData"/>.
        /// </returns>
        public static Dictionary<string, TestCaseData>
            CreateInvalidProgressTests(
                float minProgress,
                float maxProgress,
                IList<float> failTests)
        {
            var baseLineTests = new Dictionary<string, TestCaseData>()
            {
                {
                    TestCases.MinProgressNextDown,
                    new TestCaseData(MathUtils.NextDown(minProgress))
                },
                {
                    TestCases.MaxProgressNextUp,
                    new TestCaseData(MathUtils.NextUp(maxProgress))
                },
                {
                    TestCases.NaNProgress,
                    new TestCaseData(float.NaN)
                },
                {
                    TestCases.PositiveInfinityProgress,
                    new TestCaseData(float.PositiveInfinity)
                },
                {
                    TestCases.NegativeInfinityProgress,
                    new TestCaseData(float.NegativeInfinity)
                }
            };

            if(failTests?.Count > 0)
            {
                foreach(var value in failTests)
                {
                    baseLineTests.Add($"Progress: {value:0.###}",
                        new TestCaseData(value));
                }
            }

            return baseLineTests;
        }

        #endregion Methods
    }
}