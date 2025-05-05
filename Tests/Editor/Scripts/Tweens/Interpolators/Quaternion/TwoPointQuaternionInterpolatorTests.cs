/*------------------------------------------------------------------------------
File:       TwoPointQuaternionInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of
            TwoPointInterpolator<Quaternion>. Provides a foundation for testing
            Quaternion interpolation logic.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-05 02:52:38 
------------------------------------------------------------------------------*/
using AlchemicalFlux.Utilities.Helpers;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="TwoPointInterpolator{TType}"/> with <see cref="Quaternion"/>.
    /// </summary>
    public abstract class TwoPointQuaternionInterpolatorTests
        : TwoPointInterpolatorTests<Quaternion>
    {
        /// <summary>
        /// A lambda function for comparing two <see cref="Quaternion"/> values
        /// approximately. This is useful for floating-point comparisons where
        /// exact equality is not guaranteed due to precision issues.
        /// </summary>
        protected static readonly 
            Func<Quaternion, Quaternion, bool> IsApproximately =
                (expected, actual) => expected.IsApproximately(actual);
    }
}
