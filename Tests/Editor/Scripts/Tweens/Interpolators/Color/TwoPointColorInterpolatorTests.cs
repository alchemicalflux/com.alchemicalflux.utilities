/*------------------------------------------------------------------------------
File:       TwoPointColorInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of two-point color interpolators.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-21 00:20:12 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of two-point color interpolators.
    /// </summary>
    public abstract class TwoPointColorInterpolatorTests
        : TwoPointInterpolatorTests<Color>
    {
        #region Fields

        /// <summary>
        /// A color with clear for NaN test case.
        /// </summary>
        protected static readonly Color NanColor = Color.clear;

        /// <summary>
        /// A lambda for comparing two colors approximately.
        /// </summary>
        protected static readonly Func<Color, Color, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);

        #endregion Fields
    }
}
