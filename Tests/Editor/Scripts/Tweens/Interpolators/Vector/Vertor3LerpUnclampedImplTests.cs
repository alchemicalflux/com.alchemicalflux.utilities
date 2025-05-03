/*------------------------------------------------------------------------------
File:       Vertor3LerpUnclampedImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the Vector3LerpUnclampedImpl class, which performs
            unclamped linear interpolation for Vector3 values.
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
    /// Unit tests for the <see cref="Vector3LerpUnclampedImpl"/> class.
    /// </summary>
    public sealed class Vector3LerpUnclampedImplTests
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
            Dictionary<string, TestCaseData> _passOverrides = new()
            {
                {
                    TestCases.ProgressOfNegativeOne,
                    new TestCaseData(-1.0f, new Vector3(-1.0f, -1.0f, -1.0f))
                },
                {
                    TestCases.ProgressOfTwo,
                    new TestCaseData(2.0f, new Vector3(2.0f, 2.0f, 2.0f))
                },
                {
                    TestCases.PositiveInfinityProgress,
                    new TestCaseData(float.PositiveInfinity,
                        new Vector3(float.PositiveInfinity,
                            float.PositiveInfinity,
                            float.PositiveInfinity))
                },
                {
                    TestCases.NegativeInfinityProgress,
                    new TestCaseData(float.NegativeInfinity,
                        new Vector3(float.NegativeInfinity,
                            float.NegativeInfinity,
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
            InterpolatorTests<Vector3> _interpolatorTests = new();

        #endregion Constants

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        protected override
            TwoPointInterpolator<Vector3> TwoPointInterpolator { get; set; }

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
        static Vector3LerpUnclampedImplTests()
        {
            var progressTests = InterpolatorTests<Vector3>.CreateProgressTests(
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
                new Vector3LerpUnclampedImpl(_startVector, _endVector);
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
