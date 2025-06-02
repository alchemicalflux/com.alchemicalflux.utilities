/*------------------------------------------------------------------------------
File:       DeCasteljauBezierCurveQuaternionInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of 
            DeCasteljauBezierCurveInterpolatorTests<Quaternion>. Provides a
            foundation for testing Quaternion interpolation logic using Bezier
            curves.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-01 20:18:43 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="DeCasteljauBezierCurveInterpolatorTests{TType}"/> with 
    /// <see cref="Quaternion"/>.
    /// </summary>
    public abstract class DeCasteljauBezierCurveQuaternionInterpolatorTests
        : DeCasteljauBezierCurveInterpolatorTests<Quaternion>
    {
        #region Constants

        /// <summary>
        /// <see cref="Quaternion"/> value representing NaN (Not a Number) for
        /// invalid test cases. Used as a sentinel value when interpolation
        /// fails or is not possible.
        /// </summary>
        protected static readonly Quaternion NanVector =
            new Quaternion(float.NaN, float.NaN, float.NaN, float.NaN);

        /// <summary>
        /// A lambda function for comparing two <see cref="Quaternion"/>
        /// values approximately. This is useful for floating-point comparisons
        /// where exact equality is not guaranteed due to precision issues.
        /// </summary>
        /// <remarks>
        /// Uses the <see cref="IsApproximately(Quaternion, Quaternion)"/>
        /// extension method to compare both <see cref="Quaternion"/> values
        /// within a small epsilon.
        /// </remarks>
        protected static readonly
            Func<Quaternion, Quaternion, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);

        #endregion Constants
    }
}
