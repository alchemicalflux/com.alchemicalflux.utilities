/*------------------------------------------------------------------------------
File:       BezierCurveColorInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of 
            PolynomialBezierCurveInterpolatorTests<Color>. Provides a
            foundation for testing Color interpolation logic using Bezier
            curves.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="PolynomialBezierCurveInterpolator{TType}"/> with 
    /// <see cref="Color"/>.
    /// </summary>
    public abstract class BezierCurveColorInterpolatorTests
        : PolynomialBezierCurveInterpolatorTests<Color>
    {
        #region Constants

        /// <summary>
        /// <see cref="Color"/> value representing NaN (Not a Number) for
        /// invalid test cases. Used as a sentinel value when interpolation
        /// fails or is not possible.
        /// </summary>
        protected static readonly Color NanColor = Color.clear;

        /// <summary>
        /// A lambda function for comparing two <see cref="Color"/>
        /// values approximately. This is useful for floating-point comparisons
        /// where exact equality is not guaranteed due to precision issues.
        /// </summary>
        /// <remarks>
        /// Uses the <see cref="IsApproximately(Color, Color)"/> extension
        /// method to compare both <see cref="Color"/> values within a small
        /// epsilon.
        /// </remarks>
        protected static readonly Func<Color, Color, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);

        #endregion Constants
    }
}
