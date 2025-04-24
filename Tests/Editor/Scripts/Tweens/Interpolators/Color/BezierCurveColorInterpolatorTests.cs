/*------------------------------------------------------------------------------
File:       BezierCurveColorInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of bezier curve color 
            interpolators.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-24 04:08:04 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of bezier curve color interpolators.
    /// Provides common functionality and utilities for testing color 
    /// interpolation.
    /// </summary>
    public abstract class BezierCurveColorInterpolatorTests
        : BezierCurveInterpolatorTests<Color>
    {
        #region Fields

        /// <summary>
        /// A color representing NaN (Not a Number) for invalid test cases.
        /// </summary>
        protected static readonly Color NanColor = Color.clear;

        /// <summary>
        /// A lambda function for comparing two colors approximately.
        /// This is useful for floating-point comparisons where exact equality
        /// is not guaranteed due to precision issues.
        /// </summary>
        protected static readonly Func<Color, Color, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);

        #endregion Fields
    }
}
