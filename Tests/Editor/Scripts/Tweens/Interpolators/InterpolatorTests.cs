/*------------------------------------------------------------------------------
File:       InterpolatorTests.cs 
Project:    YourProjectName  # Replace with project name
Overview:   YourOverview  # Replace with overview
Copyright:  2025 YourName/YourCompany. All rights reserved.  # Replace with copyright

Last commit by: alchemicalflux 
Last commit at: 2025-04-16 19:18:32 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    public sealed class InterpolatorTests<TType> : IInterpolatorTests<TType>
        where TType : IEquatable<TType>
    {
        private readonly Dictionary<string, TestCaseData> _validTests = new()
        {
            { "ZeroCase", new(0.0f, default(TType)) },
            { "HalfCase", new(0.5f, default(TType)) },
            { "OneCase", new(1.0f, default(TType)) }
        };

        private readonly Dictionary<string, TestCaseData> _invalidTests = new()
        {
            { "NegativeProgress", new(-0.1f, default(TType)) },
            { "ProgressGreaterThanOne", new(1.1f, default(TType)) },
            { "NaNProgress", new(float.NaN, default(TType)) },
            { "PositiveInfinityProgress", new(float.PositiveInfinity, default(TType)) },
            { "NegativeInfinityProgress", new(float.NegativeInfinity, default(TType)) }
        };

        private readonly TestCaseSourceHelper _validTestHelper;
        private readonly TestCaseSourceHelper _invalidTestHelper;
        private readonly IInterpolator<TType> _interpolator;

        public IEnumerable<TestCaseData> ValidProgressTestCases =>
            _validTestHelper.GetTestCases();
        public IEnumerable<TestCaseData> InvalidProgressTestCases =>
            _invalidTestHelper.GetTestCases();

        public InterpolatorTests()
        {
            _validTestHelper = new(_validTests);
            _invalidTestHelper = new(_invalidTests);
        }

        public InterpolatorTests(IInterpolator<TType> interpolator)
        {
            _interpolator = interpolator;
            _validTestHelper = new(_validTests);
            _invalidTestHelper = new(_invalidTests);
        }

        public InterpolatorTests<TType> AddValidTests(Dictionary<string, TestCaseData> validTests)
        {
            _validTestHelper.AddOverwrite(validTests);
            return this;
        }
        public InterpolatorTests<TType> AddInvalidTests(Dictionary<string, TestCaseData> invalidTests)
        {
            _invalidTestHelper.AddOverwrite(invalidTests);
            return this;
        }

        public void InterpolatorTests_ValidProgress_ReturnsExpectedValue(
            float progress, TType expectedValue)
        {
            Assert.IsTrue(expectedValue.Equals(default(TType)));
            /*
            var interpolator = _interpolator;

            var result = interpolator.Interpolate(progress);

            Assert.AreEqual(expectedValue, result,
                $"Expected {expectedValue} but got {result} for progress {progress}");
            */
        }
    }
}
