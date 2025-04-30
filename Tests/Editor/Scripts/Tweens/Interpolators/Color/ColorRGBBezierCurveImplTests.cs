/*------------------------------------------------------------------------------
File:       ColorRGBBezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the ColorRGBBezierCurveImpl class, which performs
            Bezier curve interpolation for colors. This test class validates
            the behavior of the interpolator for valid and invalid progress
            values, as well as property correctness.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-29 20:23:27 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Colors
{
    /// <summary>
    /// Unit tests for the <see cref="ColorRGBBezierCurveImpl"/> class.
    /// </summary>
    public class ColorRGBBezierCurveImplTests
        : BezierCurveColorInterpolatorTests
    {
        #region Fields

        #region Constants

        /// <summary>
        /// The starting color for interpolation tests.
        /// </summary>
        private static readonly Color _startColor = Color.red;

        /// <summary>
        /// The middle color for interpolation tests.
        /// </summary>
        private static readonly Color _midColor = Color.green;

        /// <summary>
        /// The ending color for interpolation tests.
        /// </summary>
        private static readonly Color _endColor = Color.blue;

        /// <summary>
        /// A dictionary of progress values and their expected interpolated
        /// results for valid test cases.
        /// </summary>
        private static readonly Dictionary<float, Color> _testRange = new()
        {
            { 0.1f, new Color(0.8099999f, 0.18f, 0.01f) },
            { 1.0f / 3.0f, new Color(0.4444444f, 0.4444444f, 0.1111111f) },
            { 0.5f, new Color(0.25f, 0.5f, 0.25f) },
            { 2.0f / 3.0f, new Color(0.1111111f, 0.4444444f, 0.4444444f) },
            { 0.9f, new Color(0.01f, 0.18f, 0.8099999f) },
        };

        #endregion Constants

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Color> _interpolatorTests = new();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the instance of the
        /// <see cref="BezierCurveInterpolator{TType}"/> being tested.
        /// </summary>
        protected override
            BezierCurveInterpolator<Color> BezierCurveInterpolator
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
        static ColorRGBBezierCurveImplTests()
        {
            var progressTests = InterpolatorTests<Color>.CreateProgressTests(
                _startColor, _endColor, _testRange, NanColor);
            _interpolatorTests.ValidProgressTests.Overwrite(progressTests);
        }

        /// <summary>
        /// Sets up the test environment before each test is executed.
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            var list = new List<Color>() { _startColor, _midColor, _endColor };
            BezierCurveInterpolator = new ColorRGBBezierCurveImpl(list);
        }

        /// <summary>
        /// Tests that the interpolator returns the expected value for valid
        /// progress values.
        /// </summary>
        /// <param name="progress">The progress value to test.</param>
        /// <param name="expectedValue">
        /// The expected interpolated color value.
        /// </param>
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Color expectedValue)
        {
            _interpolatorTests.ValidProgress(
                BezierCurveInterpolator,
                progress,
                expectedValue,
                IsApproximately);
        }

        /// <summary>
        /// Tests that the interpolator throws an exception for invalid progress
        /// values.
        /// </summary>
        /// <param name="progress">The invalid progress value to test.</param>
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
