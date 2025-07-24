/*------------------------------------------------------------------------------
File:       BezierCurveVector2InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of 
            PolynomialBezierCurveInterpolatorTests<Vector2>. Provides a
            foundation for testing Vector2 interpolation logic using Bezier
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
    /// <see cref="Vector2"/>.
    /// </summary>
    public abstract class BezierCurveVector2InterpolatorTests
        : PolynomialBezierCurveInterpolatorTests<Vector2>
    {
        #region Constants

        /// <summary>
        /// <see cref="Vector2"/> value representing NaN (Not a Number) for
        /// invalid test cases. Used as a sentinel value when interpolation
        /// fails or is not possible.
        /// </summary>
        protected static readonly Vector2 NanVector =
            new Vector2(float.NaN, float.NaN);

        /// <summary>
        /// A lambda function for comparing two <see cref="Vector2"/>
        /// values approximately. This is useful for floating-point comparisons
        /// where exact equality is not guaranteed due to precision issues.
        /// </summary>
        /// <remarks>
        /// Uses the <see cref="IsApproximately(Vector2, Vector2)"/> extension
        /// method to compare both <see cref="Vector2"/> values within a small
        /// epsilon.
        /// </remarks>
        protected static readonly Func<Vector2, Vector2, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);

        #endregion Constants
    }
}
