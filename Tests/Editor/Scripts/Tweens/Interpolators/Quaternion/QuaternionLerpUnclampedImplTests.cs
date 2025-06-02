/*------------------------------------------------------------------------------
File:       QuaternionLerpUnclampedImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the QuaternionLerpUnclampedImpl class, which performs
            linear interpolation for Quaternion values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-01 20:14:04 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Quaternions
{
    /// <summary>
    /// Unit tests for the <see cref="QuaternionLerpUnclampedImpl"/> class.
    /// </summary>
    public sealed class QuaternionLerpUnclampedImplTests
        : TwoPointQuaternionInterpolatorTests
    {
        #region Constants

        /// <summary>
        /// The starting quaternion for interpolation tests.
        /// </summary>
        private static readonly
            Quaternion _startQuaternion = Quaternion.Euler(0, 0, 0);

        /// <summary>
        /// The ending quaternion for interpolation tests.
        /// </summary>
        private static readonly
            Quaternion _endQuaternion = Quaternion.Euler(0, 90, 0);

        /// <summary>
        /// A dictionary of progress values and their expected interpolated
        /// results for valid test cases.
        /// </summary>
        private static readonly 
            Dictionary<float, Quaternion> _testRange = new()
        {
            { 0.1f, new Quaternion(0.0f, 0.07265174f, 0.0f, 0.9973573f) },
            { 1.0f / 3.0f, new Quaternion(0.0f, 0.2527247f, 0.0f, 0.9675382f) },
            { 0.5f, new Quaternion(0.0f, 0.3826834f, 0.0f, 0.92388f) },
            { 2.0f / 3.0f, new Quaternion(0.0f, 0.50545f, 0.0f, 0.8628563f) },
            { 0.9f, new Quaternion(0.0f, 0.6538656f, 0.0f, 0.75661f) },
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
                    new TestCaseData(-1.0f,
                        new Quaternion(0.0f, -0.4798415f, 0.0f, 0.8773552f))
                },
                {
                    TestCases.ProgressOfTwo,
                    new TestCaseData(2.0f,
                        new Quaternion(0.0f, 0.959683f, 0.0f, 0.2810846f))
                },
                {
                    TestCases.PositiveInfinityProgress,
                    new TestCaseData(
                        float.PositiveInfinity,
                        new Quaternion(
                            float.NaN,
                            float.NaN,
                            float.NaN,
                            float.NaN))
                },
                {
                    TestCases.NegativeInfinityProgress,
                    new TestCaseData(
                        float.NegativeInfinity,
                        new Quaternion(
                            float.NaN,
                            float.NaN,
                            float.NaN,
                            float.NaN))
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

        #endregion Constants

        #region Fields

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Quaternion> _interpolatorTests = new();

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        protected override
            TwoPointInterpolator<Quaternion> TwoPointInterpolator
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
        static QuaternionLerpUnclampedImplTests()
        {
            var progressTests = InterpolatorTests<Quaternion>.CreateProgressTests(
                _startQuaternion, _endQuaternion, _testRange, default);
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
                new QuaternionLerpUnclampedImpl(
                    _startQuaternion, 
                    _endQuaternion);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Quaternion expectedValue)
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
