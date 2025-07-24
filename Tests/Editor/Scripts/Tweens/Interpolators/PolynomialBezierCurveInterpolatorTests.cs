/*------------------------------------------------------------------------------
File:       PolynomialBezierCurveInterpolatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Abstract base class for unit tests of 
            PolynomialBezierCurveInterpolator.
            Provides common test cases and utilities for derived test classes.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-05-19 01:27:00 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Abstract base class for unit tests of 
    /// <see cref="PolynomialBezierCurveInterpolator{TType}"/>.
    /// Provides common test cases and utilities for derived test classes.
    /// </summary>
    /// <typeparam name="TType">Type to be interpolated.</typeparam>
    public abstract class PolynomialBezierCurveInterpolatorTests<TType>
        : BezierCurveInterpolatorTests<TType>
        where TType : IEquatable<TType>
    {
        /// <inheritdoc />
        protected abstract PolynomialBezierCurveInterpolator<TType>
            PolynomialBezierCurveInterpolator
        { get; set; }

        /// <inheritdoc />
        protected override
            BezierCurveInterpolator<TType> BezierCurveInterpolator
        {
            get => PolynomialBezierCurveInterpolator;
            set => PolynomialBezierCurveInterpolator =
                (PolynomialBezierCurveInterpolator<TType>)value;
        }
    }
}
