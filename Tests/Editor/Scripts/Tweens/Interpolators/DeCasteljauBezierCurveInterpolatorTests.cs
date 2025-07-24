/*------------------------------------------------------------------------------
File:       DeCasteljauBezierCurveInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of
            DeCasteljauBezierCurveInterpolator.
            Provides common test cases and utilities for derived test classes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-06-01 20:18:43 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="DeCasteljauBezierCurveInterpolator{TType}"/>.
    /// Provides common test cases and utilities for derived test classes.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public abstract class DeCasteljauBezierCurveInterpolatorTests<TType>
        : BezierCurveInterpolatorTests<TType>
        where TType : IEquatable<TType>
    {
        /// <inheritdoc />
        protected abstract DeCasteljauBezierCurveInterpolator<TType>
            DeCasteljauBezierCurveInterpolator
        { get; set; }

        /// <inheritdoc />
        protected override
            BezierCurveInterpolator<TType> BezierCurveInterpolator
        {
            get => DeCasteljauBezierCurveInterpolator;
            set => DeCasteljauBezierCurveInterpolator =
                (DeCasteljauBezierCurveInterpolator<TType>)value;
        }
    }
}
