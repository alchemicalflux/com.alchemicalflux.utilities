/*------------------------------------------------------------------------------
File:       BasicTweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines the base test class for tween tests, providing common 
            setup and basic tests for ITween implementations. This class 
            ensures that all tween implementations can be tested for basic 
            functionality such as showing the tween and applying progress.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-20 22:54:35 
------------------------------------------------------------------------------*/
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Unit tests for the BasicTween class.
    /// </summary>
    public class BasicTweenTests : BaseTweenTests<int>
    {
        #region Fields

        /// <summary>
        /// A helper for managing IInterpolator test cases.
        /// </summary>
        private static readonly TweenTests _tweenTests = new();

        #endregion Fields

        #region Properties

        #region Overrides

        /// <inheritdoc />
        protected override
            Mock<IInterpolator<int>> MockInterpolator { get; set; }

        /// <inheritdoc />
        protected override Func<float, float> EasingFunction { get; set; }

        /// <inheritdoc />
        protected override BaseTween<int> BaseTweenRef { get; set; }

        #endregion Overrides

        /// <summary>
        /// Gets the valid progress test cases for interpolation.
        /// </summary>
        private static IEnumerable<TestCaseData> ValidProgressTests =>
            _tweenTests.ValidProgressTests.GetTestCases();

        /// <summary>
        /// Gets the invalid progress test cases for interpolation.
        /// </summary>
        private static IEnumerable<TestCaseData> InvalidProgressTests =>
            _tweenTests.InvalidProgressTests.GetTestCases();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Static constructor to initialize test cases for the class.
        /// </summary>
        static BasicTweenTests()
        {
            var progressTests = 
                TweenTests.CreateValidProgressTests(0, 1, null);
            _tweenTests.ValidProgressTests
                .Overwrite(progressTests);

            var invalidProgressTests = 
                TweenTests.CreateInvalidProgressTests(0, 1, null);
            _tweenTests.InvalidProgressTests
                .Overwrite(invalidProgressTests);
        }

        /// <summary>
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            MockInterpolator = new Mock<IInterpolator<int>>();
            EasingFunction = x => x; // Default to linear easing for tests
            BaseTweenRef = new BasicTween<int>(
                MockInterpolator.Object,
                EasingFunction);
        }

        #region Overrides

        /// <inheritdoc/>
        [TestCaseSource(nameof(InvalidProgressTests))]
        public override void ApplyProgress_InvalidProgress_ThrowsArgumentOutOfRangeException(float progress)
        {
            _tweenTests.InvalidProgress(BaseTweenRef, progress);
        }

        /// <inheritdoc/>
        [TestCaseSource(nameof(ValidProgressTests))]
        public override void ApplyProgress_ValidProgress_DoesNotThrowException(float progress)
        {
            _tweenTests.ValidProgress(BaseTweenRef, progress);
        }

        #endregion Overrides

        #endregion Methods
    }
}