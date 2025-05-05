/*------------------------------------------------------------------------------
File:       Vector3BezierCurveImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-05 02:50:49 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests.Vectors
{
    public sealed class Vector3BezierCurveImplTests
        : BezierCurveVector3InterpolatorTests
    {
        #region Fields

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
            { 1.0f / 3.0f, new Vector3(-1.0f / 3.0f, -1.0f / 3.0f, -1.0f / 3.0f) },
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
            BezierCurveInterpolator<Vector3> BezierCurveInterpolator
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
            BezierCurveInterpolator = new Vector3BezierCurveImpl(list);
        }

        /// <inheritdoc />
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void InterpolatorTests_Progress_ReturnsExpectedValue(
            float progress, Vector3 expectedValue)
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