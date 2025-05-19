/*------------------------------------------------------------------------------
File:       ColorRGBBezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the ColorRGBBezierCurveImplTests class. Validates
            correct interpolation results, exception handling for invalid
            progress values, and property correctness for the interpolator.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Colors
{
    /// <summary>
    /// Unit tests for the <see cref="ColorRGBBezierCurveImpl"/> class.
    /// Validates correct interpolation results, exception handling for invalid
    /// progress values, and property correctness for the interpolator.
    /// </summary>
    public sealed class ColorRGBBezierCurveImplTests
        : BezierCurveColorInterpolatorTests
    {
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

        #region Fields

        /// <summary>
        /// A helper for managing <see cref="IInterpolator{Color}"/> test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Color> _interpolatorTests = new();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the instance of <see cref="ColorRGBBezierCurveImpl"/>
        /// being tested.
        /// </summary>
        private ColorRGBBezierCurveImpl ColorRGBBezierCurveInterpolator
        { get; set; }

        /// <inheritdoc/>
        protected override PolynomialBezierCurveInterpolator<Color>
            PolynomialBezierCurveInterpolator
        {
            get => ColorRGBBezierCurveInterpolator;
            set => ColorRGBBezierCurveInterpolator =
                (ColorRGBBezierCurveImpl)value;
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
        static ColorRGBBezierCurveImplTests()
        {
            var progressTests = InterpolatorTests<Color>.CreateProgressTests(
                _startColor, _endColor, _testRange, NanColor);
            _interpolatorTests.ValidProgressTests.Overwrite(progressTests);
        }

        /// <inheritdoc />
        [SetUp]
        public override void Setup()
        {
            var list = new List<Color>() { _startColor, _midColor, _endColor };
            PolynomialBezierCurveInterpolator = new ColorRGBBezierCurveImpl(list);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Color expectedValue)
        {
            _interpolatorTests.ValidProgress(
                ColorRGBBezierCurveInterpolator,
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
                ColorRGBBezierCurveInterpolator,
                progress);
        }

        #endregion Methods
    }
}
