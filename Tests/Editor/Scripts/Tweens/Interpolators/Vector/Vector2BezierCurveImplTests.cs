/*------------------------------------------------------------------------------
File:       Vector2BezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the Vector2BezierCurveImpl class, which performs
            Bezier curve interpolation for Vector2 values.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-05 02:50:49 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Vectors
{
    /// <summary>
    /// Unit tests for the <see cref="Vector2BezierCurveImpl"/> class, which
    /// performs Bezier curve interpolation for <see cref="Vector2"/> values.
    /// </summary>
    public sealed class Vector2BezierCurveImplTests
        : BezierCurveVector2InterpolatorTests
    {
        #region Fields

        #region Constants

        /// <summary>
        /// The starting vector for interpolation tests.
        /// </summary>
        private static readonly Vector2 _startVector = Vector2.zero;

        /// <summary>
        /// The middle vector for interpolation tests.
        /// </summary>
        private static readonly Vector3 _midVector = -Vector3.one;

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
            { 0.1f, new Vector2(-0.17f, -0.17f) },
            { 1.0f / 3.0f, new Vector2(-1.0f / 3.0f, -1.0f / 3.0f) },
            { 0.5f, new Vector2(-0.25f, -0.25f) },
            { 2.0f / 3.0f, new Vector2(0.0f, 0.0f) },
            { 0.9f, new Vector2(0.6299999f, 0.6299999f) },
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
            InterpolatorTests<Vector2> _interpolatorTests = new();

        #endregion Constants

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        protected override
            BezierCurveInterpolator<Vector2> BezierCurveInterpolator
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
        static Vector2BezierCurveImplTests()
        {
            var progressTests = InterpolatorTests<Vector2>.CreateProgressTests(
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
                new List<Vector2>() { _startVector, _midVector, _endVector };
            BezierCurveInterpolator = new Vector2BezierCurveImpl(list);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Vector2 expectedValue)
        {
            _interpolatorTests.ValidProgress(
                BezierCurveInterpolator, progress, expectedValue, IsApproximately);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(InvalidProgressTests))]
        public override void InterpolatorTests_Progress_ThrowsArgumentOutOfRangeException(
            float progress)
        {
            _interpolatorTests.InvalidProgress(
                BezierCurveInterpolator,
                progress);
        }

        #endregion Methods
    }
}
