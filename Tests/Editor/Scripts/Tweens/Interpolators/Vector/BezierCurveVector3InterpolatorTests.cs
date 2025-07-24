/*------------------------------------------------------------------------------
File:       BezierCurveVector3InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of 
            PolynomialBezierCurveInterpolatorTests<Vector3>. Provides a
            foundation for testing Vector3 interpolation logic using Bezier
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
    /// <see cref="Vector3"/>.
    /// </summary>
    public abstract class BezierCurveVector3InterpolatorTests
        : PolynomialBezierCurveInterpolatorTests<Vector3>
    {
        /// <summary>
        /// <see cref="Vector3"/> value representing NaN (Not a Number) for
        /// invalid test cases. Used as a sentinel value when interpolation
        /// fails or is not possible.
        /// </summary>
        protected static readonly Vector3 NanVector =
            new Vector3(float.NaN, float.NaN, float.NaN);

        /// <summary>
        /// A lambda function for comparing two <see cref="Vector3"/>
        /// values approximately. This is useful for floating-point comparisons
        /// where exact equality is not guaranteed due to precision issues.
        /// </summary>
        /// <remarks>
        /// Uses the <see cref="IsApproximately(Vector3, Vector3)"/> extension
        /// method to compare both <see cref="Vector3"/> values within a small
        /// epsilon.
        /// </remarks>
        protected static readonly Func<Vector3, Vector3, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);
    }
}
