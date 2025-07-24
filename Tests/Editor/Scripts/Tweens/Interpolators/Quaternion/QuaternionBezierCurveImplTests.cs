/*------------------------------------------------------------------------------
File:       QuaternionBezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Contains unit tests for QuaternionBezierCurveImpl, verifying correct
            interpolation of Quaternion values using De Casteljau's algorithm
            for Bezier curves.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-01 20:18:43 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Quaternions
{
    /// <summary>
    /// Contains unit tests for <see cref="QuaternionBezierCurveImpl"/>,
    /// verifying correct interpolation of <see cref="Quaternion"/> values
    /// using De Casteljau's algorithm for Bezier curves.
    /// </summary>
    public sealed class QuaternionBezierCurveImplTests
    : DeCasteljauBezierCurveQuaternionInterpolatorTests
    {
        #region Constants

        /// <summary>
        /// The starting quaternion for interpolation tests.
        /// </summary>
        private static readonly
            Quaternion _startQuaternion = Quaternion.Euler(0, 0, 0);

        /// <summary>
        /// The middle quaternion for interpolation tests.
        /// </summary>
        private static readonly Quaternion _midQuaternion = 
            Quaternion.Euler(0, 0, 90);

        /// <summary>
        /// The ending quaternion for interpolation tests.
        /// </summary>
        private static readonly
            Quaternion _endQuaternion = Quaternion.Euler(0, 90, 0);

        /// <summary>
        /// A dictionary of progress values and their expected interpolated
        /// results for valid test cases.
        /// </summary>
        private static readonly Dictionary<float, Quaternion> _testRange = new()
        {
            {
                0.1f, new Quaternion(0.0f, 0.009162422f, 0.1425072f, 0.9897513f)
            },
            {
                1.0f / 3.0f,
                new Quaternion(0.0f, 0.09558383f, 0.3550699f, 0.9299403f)
            },
            {
                0.5f, new Quaternion(0.0f, 0.2088466f, 0.4046151f, 0.8903201f)
            },
            {
                2.0f / 3.0f,
                new Quaternion(0.0f, 0.3611802f, 0.367501f, 0.857025f)
            },
            { 0.9f, new Quaternion(0.0f, 0.6107231f, 0.1533952f, 0.7768444f) },
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
            DeCasteljauBezierCurveInterpolator<Quaternion>
            DeCasteljauBezierCurveInterpolator
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

        /// <summary>
        /// Static constructor to initialize test cases for the class.
        /// </summary>
        static QuaternionBezierCurveImplTests()
        {
            var progressTests = InterpolatorTests<Quaternion>.CreateProgressTests(
                _startQuaternion, _endQuaternion, _testRange, default);
            _interpolatorTests.ValidProgressTests
                .Overwrite(progressTests)
                .Remove(_invalidOverrides.Keys);

            _interpolatorTests.InvalidProgressTests
                .Overwrite(_invalidOverrides);
        }

        /// <inheritdoc />
        [SetUp]
        public override void Setup()
        {
            var list = new List<Quaternion>() 
            {
                _startQuaternion, _midQuaternion, _endQuaternion 
            };
            DeCasteljauBezierCurveInterpolator =
                new QuaternionBezierCurveImpl(list);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Quaternion expectedValue)
        {
            _interpolatorTests.ValidProgress(
                DeCasteljauBezierCurveInterpolator,
                progress,
                expectedValue,
                IsApproximately);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(InvalidProgressTests))]
        public override void InterpolatorTests_Progress_ThrowsArgumentOutOfRangeException(
            float progress)
        {
            _interpolatorTests.InvalidProgress(
                DeCasteljauBezierCurveInterpolator,
                progress);
        }
    }
}