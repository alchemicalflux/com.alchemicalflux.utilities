/*------------------------------------------------------------------------------
File:       ColorRGBLerpImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides unit tests for the ColorRGBLerpImpl class, which performs
            linear interpolation (lerp) between two Color values. This test
            class validates the behavior of the interpolator for valid and
            invalid progress values, as well as property correctness.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-24 19:30:32 
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
        /// A color that is halfway between the start and end colors.
        /// </summary>
        private static readonly Color _halfColor =
            new Color(0.5f, 0.0f, 0.5f, 1.0f);

        #endregion Constants

        /// <summary>
        /// A helper for managing valid and invalid progress test cases.
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
            _interpolatorTests.ValidProgressTestCases;

        /// <summary>
        /// Gets the invalid progress test cases for interpolation.
        /// </summary>
        private static IEnumerable<TestCaseData> InvalidProgressTests =>
            _interpolatorTests.InvalidProgressTestCases;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Static constructor to initialize test cases for the class.
        /// </summary>
        static ColorRGBLerpImplTests()
        {
            var progressTests = InterpolatorTests<Color>.CreateProgressTests(
                    _startColor, _endColor, _halfColor, NanColor);
            _interpolatorTests.AddProgressTests(progressTests);
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

