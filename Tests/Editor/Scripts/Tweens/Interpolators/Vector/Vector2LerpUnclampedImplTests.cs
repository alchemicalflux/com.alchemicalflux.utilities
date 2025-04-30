/*------------------------------------------------------------------------------
File:       Vector2LerpUnclampedImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the Vector2LerpUnclampedImpl class, which performs
            unclamped linear interpolation for Vector2 values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-29 20:16:53 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Vectors
{
    /// <summary>
    /// Unit tests for the <see cref="Vector2LerpUnclampedImpl"/> class.
    /// </summary>
    public sealed class Vector2LerpUnclampedImplTests
        : TwoPointVector2InterpolatorTests
    {
        #region Fields

        #region Constants

        /// <summary>
        /// The starting vector for interpolation tests.
        /// </summary>
        private static readonly Vector2 _startVector = Vector2.zero;

        /// <summary>
        /// The ending vector for interpolation tests.
        /// </summary>
        private static readonly Vector2 _endVector = Vector2.one;

        /// <summary>
        /// A dictionary of progress values and their expected interpolated
        /// results for valid test cases.
        /// </summary>
        private static readonly Dictionary<float, Vector2> _testRange = new()
        {
            { 0.1f, new Vector2(0.1f, 0.1f) },
            { 1.0f / 3.0f, new Vector2(1.0f / 3.0f, 1.0f / 3.0f) },
            { 0.5f, new Vector2(0.5f, 0.5f) },
            { 2.0f / 3.0f, new Vector2(2.0f / 3.0f, 2.0f / 3.0f) },
            { 0.9f, new Vector2(0.9f, 0.9f) },
        };

        /// <summary>
        /// Overrides for valid progress test cases that deviate from the
        /// default behavior.
        /// </summary>
        private static readonly
            Dictionary<string, TestCaseData> _passOverrides = new()
            {
                {
                    TestCases.ProgressOfNegativeOne,
                    new TestCaseData(-1.0f, new Vector2(-1.0f, -1.0f))
                },
                {
                    TestCases.ProgressOfTwo,
                    new TestCaseData(2.0f, new Vector2(2.0f, 2.0f))
                },
                {
                    TestCases.PositiveInfinityProgress,
                    new TestCaseData(float.PositiveInfinity,
                        new Vector2(float.PositiveInfinity,
                            float.PositiveInfinity))
                },
                {
                    TestCases.NegativeInfinityProgress,
                    new TestCaseData(float.NegativeInfinity,
                        new Vector2(float.NegativeInfinity,
                            float.NegativeInfinity))
                }
            };

        /// <summary>
        /// Overrides for invalid progress test cases that deviate from the
        /// default behavior.
        /// </summary>
        private static readonly
            Dictionary<string, TestCaseData> _failOverrides = new()
            {
                { TestCases.NaNProgress, new TestCaseData(float.NaN) },
            };

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Vector2> _interpolatorTests = new();

        #endregion Constants

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        protected override
            TwoPointInterpolator<Vector2> TwoPointInterpolator
        { get; set; }

        /// <summary>
        /// Gets the valid progress test cases for interpolation.
        /// </summary>
        private static IEnumerable<TestCaseData> ValidProgressTests =>
            _interpolatorTests.ValidProgressTests.GetTestCases();

        /// <summary>
        /// Gets the invalid progress test cases for interpolation.
        /// </summary>
        private static IEnumerable<TestCaseData> InvalidProgressTests =>
            _interpolatorTests.InvalidProgressTests.GetTestCases();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Static constructor to initialize test cases for the class.
        /// </summary>
        static Vector2LerpUnclampedImplTests()
        {
            var progressTests = InterpolatorTests<Vector2>.CreateProgressTests(
                _startVector, _endVector, _testRange, default);
            _interpolatorTests.ValidProgressTests
                .Overwrite(progressTests)
                .Overwrite(_passOverrides)
                .Remove(_failOverrides.Keys);

            _interpolatorTests.InvalidProgressTests
                .Overwrite(_failOverrides);
        }

        /// <inheritdoc />
        [SetUp]
        public override void Setup()
        {
            TwoPointInterpolator =
                new Vector2LerpUnclampedImpl(_startVector, _endVector);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Vector2 expectedValue)
        {
            _interpolatorTests.ValidProgress(
                TwoPointInterpolator, progress, expectedValue);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(InvalidProgressTests))]
        public override void InterpolatorTests_Progress_ThrowsArgumentOutOfRangeException(
            float progress)
        {
            _interpolatorTests.InvalidProgress(TwoPointInterpolator, progress);
        }

        #endregion Methods
    }
}
