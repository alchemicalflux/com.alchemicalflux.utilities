/*------------------------------------------------------------------------------
File:       Vector3BezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the Vector3BezierCurveImpl class, which performs
            Bezier curve interpolation for Vector3 values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Vectors
{
    /// <summary>
    /// Unit tests for the <see cref="Vector3BezierCurveImpl"/> class, which
    /// performs Bezier curve interpolation for <see cref="Vector3"/> values.
    /// </summary>
    public sealed class Vector3BezierCurveImplTests
        : BezierCurveVector3InterpolatorTests
    {
        #region Constants

        /// <summary>
        /// The starting vector for interpolation tests.
        /// </summary>
        private static readonly Vector3 _startVector = Vector3.zero;

        /// <summary>
        /// The middle vector for interpolation tests.
        /// </summary>
        private static readonly Vector3 _midVector = -Vector3.one;

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
            { 0.1f, new Vector3(-0.17f, -0.17f, -0.17f) },
            {
                1.0f / 3.0f,
                new Vector3(-1.0f / 3.0f, -1.0f / 3.0f, -1.0f / 3.0f) 
            },
            { 0.5f, new Vector3(-0.25f, -0.25f, -0.25f) },
            { 2.0f / 3.0f, new Vector3(0.0f, 0.0f, 0.0f) },
            { 0.9f, new Vector3(0.6299999f, 0.6299999f, 0.6299999f) },
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
            InterpolatorTests<Vector3> _interpolatorTests = new();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Vector3BezierCurveImpl"/> instance under
        /// test.
        /// </summary>
        private Vector3BezierCurveImpl Vector3BezierCurveInterpolator
        { get; set; }

        /// <inheritdoc />
        protected override PolynomialBezierCurveInterpolator<Vector3>
            PolynomialBezierCurveInterpolator
        {
            get => Vector3BezierCurveInterpolator;
            set => Vector3BezierCurveInterpolator =
                (Vector3BezierCurveImpl)value;
        }

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
        static Vector3BezierCurveImplTests()
        {
            var progressTests = InterpolatorTests<Vector3>.CreateProgressTests(
                _startVector, _endVector, _testRange, default);
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
            var list =
                new List<Vector3>() { _startVector, _midVector, _endVector };
            Vector3BezierCurveInterpolator = new Vector3BezierCurveImpl(list);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress,
            Vector3 expectedValue)
        {
            _interpolatorTests.ValidProgress(
                Vector3BezierCurveInterpolator,
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
                Vector3BezierCurveInterpolator,
                progress);
        }

        #endregion Methods
    }
}
