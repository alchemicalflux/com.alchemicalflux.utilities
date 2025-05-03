/*------------------------------------------------------------------------------
File:       BezierCurveVector2InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of 
            BezierCurveInterpolator<Vector2>. Provides a foundation for testing
            Vector2 interpolation logic using Bezier curves.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-03 05:14:56 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="BezierCurveInterpolator{TType}"/> with <see cref="Vector2"/>.
    /// </summary>
    public abstract class BezierCurveVector2InterpolatorTests
        : BezierCurveInterpolatorTests<Vector2>
    {
        /// <summary>
        /// A <see cref="Vector2"/> value representing NaN (Not a Number) for
        /// invalid test cases.
        /// </summary>
        protected static readonly Vector2 NanVector =
            new Vector2(float.NaN, float.NaN);

        /// <summary>
        /// A lambda function for comparing two <see cref="Vector2"/> values
        /// approximately. This is useful for floating-point comparisons where
        /// exact equality is not guaranteed due to precision issues.
        /// </summary>
        protected static readonly Func<Vector2, Vector2, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);
    }
}
