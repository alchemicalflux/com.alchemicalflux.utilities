/*------------------------------------------------------------------------------
File:       ColorRGBLerpImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides unit tests for the ColorRGBLerpImpl class, which performs
            linear interpolation (lerp) between two Color values. This test
            class validates the behavior of the interpolator for valid and
            invalid progress values, as well as property correctness.
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
    /// Unit tests for the <see cref="ColorRGBLerpImpl"/> class.
    /// </summary>
    public class ColorRGBLerpImplTests : TwoPointColorInterpolatorTests
    {
        #region Fields

        #region Constants

        /// <summary>
        /// The starting color for interpolation tests.
        /// </summary>
        private static readonly Color _startColor = Color.red;

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
            { 0.1f, new Color(0.9f, 0.0f, 0.1f, 1.0f) },
            { 1.0f / 3.0f, new Color(2.0f / 3.0f, 0.0f, 1.0f / 3.0f, 1.0f) },
            { 0.5f, new Color(0.5f, 0.0f, 0.5f, 1.0f) },
            { 2.0f / 3.0f, new Color(1.0f / 3.0f, 0.0f, 2.0f / 3.0f, 1.0f) },
            { 0.9f, new Color(0.1f, 0.0f, 0.9f, 1.0f) },
        };

        #endregion Constants

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly
            InterpolatorTests<Color> _interpolatorTests = new();

        #endregion Fields

        #region Properties

        /// <inheritdoc />
        protected override
            TwoPointInterpolator<Color> TwoPointInterpolator
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
        static ColorRGBLerpImplTests()
        {
            var progressTests = InterpolatorTests<Color>.CreateProgressTests(
                _startColor, _endColor, _testRange, NanColor);
            _interpolatorTests.ValidProgressTests.Overwrite(progressTests);
        }

        /// <inheritdoc />
        [SetUp]
        public override void Setup()
        {
            TwoPointInterpolator = new ColorRGBLerpImpl(_startColor, _endColor);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Color expectedValue)
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

