/*------------------------------------------------------------------------------
File:       TwoPointVector3InterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of TwoPointInterpolator<Vector3>.
            Provides a foundation for testing Vector3 interpolation logic.
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
    /// <see cref="TwoPointInterpolator{TType}"/> with <see cref="Vector3"/>.
    /// </summary>
    public abstract class TwoPointVector3InterpolatorTests
        : TwoPointInterpolatorTests<Vector3>
    {
        /// <summary>
        /// A lambda function for comparing two <see cref="Vector3"/> values
        /// approximately. This is useful for floating-point comparisons where
        /// exact equality is not guaranteed due to precision issues.
        /// </summary>
        protected static readonly Func<Vector3, Vector3, bool> IsApproximately =
            (expected, actual) => expected.IsApproximately(actual);
    }
}
