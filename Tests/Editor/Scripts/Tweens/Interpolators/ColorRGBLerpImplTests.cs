/*------------------------------------------------------------------------------
File:       ColorRGBLerpImplTests.cs 
Project:    YourProjectName  # Replace with project name
Overview:   YourOverview  # Replace with overview
Copyright:  2025 YourName/YourCompany. All rights reserved.  # Replace with copyright

Last commit by: alchemicalflux 
Last commit at: 2025-04-16 19:18:32 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    public class ColorRGBLerpImplTests : TwoPointInterpolatorTests<Color>
    {
        private static Dictionary<string, TestCaseData> _validTests = new()
        {
            { "ZeroCase", new(0.0f, default(Color)) }, //Color.red) },
            { "HalfCase", new(0.5f, Color.Lerp(Color.red, Color.blue, .5f)) },
            { "OneCase", new(1.0f, Color.blue) }
        };

        static readonly InterpolatorTests<Color> InterpolatorTests = new();
        static IEnumerable<TestCaseData> ValidTests => InterpolatorTests.ValidProgressTestCases;

        protected override IInterpolator<Color> _interpolator => null;

        static ColorRGBLerpImplTests()
        {
            InterpolatorTests.AddValidTests(_validTests);
        }

        [SetUp]
        public override void Setup()
        {
            TwoPointInterpolator = new ColorRGBLerpImpl(Color.red, Color.blue);
        }

        [Test]
        [TestCaseSource(nameof(ValidTests))]
        public override void InterpolatorTests_ValidProgress_ReturnsExpectedValue(float progress, Color expectedValue)
        {
            InterpolatorTests.InterpolatorTests_ValidProgress_ReturnsExpectedValue(progress, expectedValue);
        }
    }
}
