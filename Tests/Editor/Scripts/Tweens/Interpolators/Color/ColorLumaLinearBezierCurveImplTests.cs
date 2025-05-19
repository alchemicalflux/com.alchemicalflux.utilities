/*------------------------------------------------------------------------------
File:       ColorLumaLinearBezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the ColorLumaLinearBezierCurveImpl class. Validates
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
    /// Unit tests for the <see cref="ColorLumaLinearBezierCurveImpl"/> class.
    /// Validates correct interpolation results, exception handling for invalid
    /// progress values, and property correctness for the interpolator.
    /// </summary>
    public class ColorLumaLinearBezierCurveImplTests
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
            { 0.1f, new Color(0.9113204f, 0.461356f, 0.09985279f, 1.0f) },
            {
                1.0f / 3.0f, new Color(0.6975055f, 0.6975055f, 0.3673294f, 1.0f)
            },
            { 0.5f, new Color(0.5370987f, 0.7353569f, 0.5370987f, 1.0f) },
            {
                2.0f / 3.0f, new Color(0.3673294f, 0.6975055f, 0.6975055f, 1.0f)
            },
            { 0.9f, new Color(0.09985279f, 0.461356f, 0.9113204f, 1.0f) },
        };

        #endregion Constants

        #region Fields

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Color> _interpolatorTests = new();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="ColorLumaLinearBezierCurveImpl"/>
        /// instance under test.
        /// </summary>
        protected ColorLumaLinearBezierCurveImpl
            ColorLumaLinearBezierCurveInterpolator
        { get; set; }

        /// <inheritdoc />
        protected override PolynomialBezierCurveInterpolator<Color>
            PolynomialBezierCurveInterpolator
        {
            get => ColorLumaLinearBezierCurveInterpolator;
            set => ColorLumaLinearBezierCurveInterpolator =
                (ColorLumaLinearBezierCurveImpl)value;
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
        static ColorLumaLinearBezierCurveImplTests()
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
            ColorLumaLinearBezierCurveInterpolator =
                new ColorLumaLinearBezierCurveImpl(list);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Color expectedValue)
        {
            _interpolatorTests.ValidProgress(
                ColorLumaLinearBezierCurveInterpolator,
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
                ColorLumaLinearBezierCurveInterpolator,
                progress);
        }

        #endregion Methods
    }
}
