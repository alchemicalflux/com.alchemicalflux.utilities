/*------------------------------------------------------------------------------
File:       Vector3LerpImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the Vector3LerpImpl class, which performs linear
            interpolation for Vector3 values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-03 06:23:38 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Vectors
{
    /// <summary>
    /// Unit tests for the <see cref="Vector3LerpImpl"/> class.
    /// </summary>
    public sealed class Vector3LerpImplTests
        : TwoPointVector3InterpolatorTests
    {
        #region Fields

        #region Constants

        /// <summary>
        /// The starting vector for interpolation tests.
        /// </summary>
        private static readonly Vector3 _startVector = Vector3.zero;

        /// <summary>
        /// The ending vector for interpolation tests.
        /// </summary>
        private static readonly Vector3 _endVector = Vector3.one;

        /// <summary>
        /// A dictionary of progress values and their expected interpolated
        /// results for valid test cases.
        /// </summary>
        private static readonly Dictionary<float, Vector3> _testRange = new()
        {
            { 0.1f, new Vector3(0.1f, 0.1f, 0.1f) },
            { 1.0f / 3.0f, new Vector3(1.0f / 3.0f, 1.0f / 3.0f, 1.0f / 3.0f) },
            { 0.5f, new Vector3(0.5f, 0.5f, 0.5f) },
            { 2.0f / 3.0f, new Vector3(2.0f / 3.0f, 2.0f / 3.0f, 2.0f / 3.0f) },
            { 0.9f, new Vector3(0.9f, 0.9f, 0.9f) },
        };

        /// <summary>
        /// Overrides for valid progress test cases that deviate from the
        /// default behavior.
        /// </summary>
        private static readonly
            Dictionary<string, TestCaseData> _validOverrides = new()
            {
                {
                    TestCases.ProgressOfNegativeOne,
                    new TestCaseData(-1.0f, _startVector)
                },
                {
                    TestCases.ProgressOfTwo,
                    new TestCaseData(2.0f, _endVector)
                },
                {
                    TestCases.PositiveInfinityProgress,
                    new TestCaseData(float.PositiveInfinity, _endVector)
                },
                {
                    TestCases.NegativeInfinityProgress,
                    new TestCaseData(float.NegativeInfinity, _startVector)
                }
            };

        /// <summary>
        /// Overrides for invalid progress test cases that deviate from the
        /// default behavior.
        /// </summary>
        private static readonly
            Dictionary<string, TestCaseData> _invalidOverrides = new()
            {
                { TestCases.NaNProgress, new TestCaseData(float.NaN) },
            };

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Vector3> _interpolatorTests = new();

        #endregion Constants

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        protected override
            TwoPointInterpolator<Vector3> TwoPointInterpolator
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
        static Vector3LerpImplTests()
        {
            var progressTests = InterpolatorTests<Vector3>.CreateProgressTests(
                _startVector, _endVector, _testRange, default);
            _interpolatorTests.ValidProgressTests
                .Overwrite(progressTests)
                .Overwrite(_validOverrides)
                .Remove(_invalidOverrides.Keys);

            _interpolatorTests.InvalidProgressTests
                .Overwrite(_invalidOverrides);
        }

        /// <inheritdoc />
        [SetUp]
        public override void Setup()
        {
            TwoPointInterpolator =
                new Vector3LerpImpl(_startVector, _endVector);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Vector3 expectedValue)
        {
            _interpolatorTests.ValidProgress(
                TwoPointInterpolator, progress, expectedValue, IsApproximately);
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
