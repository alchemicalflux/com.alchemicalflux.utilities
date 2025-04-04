/*------------------------------------------------------------------------------
File:       BasicTweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines the base test class for tween tests, providing common 
            setup and basic tests for ITween implementations. This class 
            ensures that all tween implementations can be tested for basic 
            functionality such as showing the tween and applying progress.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-03 23:03:42 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Unit tests for the BasicTween class.
    /// </summary>
    public class BasicTweenTests : BaseTweenTests<int>
    {
        #region Methods

        /// <summary>
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public override void Setup()
        {
            base.Setup();
        }

        /// <summary>
        /// Creates an instance of BasicTween with the specified interpolator
        /// and easing function.
        /// </summary>
        /// <param name="interpolator">
        /// The interpolator to use for the tween.
        /// </param>
        /// <param name="easing">
        /// The easing function to use for the tween.
        /// </param>
        /// <returns>An instance of BasicTween.</returns>
        protected override ITween CreateTween(IInterpolator<int> interpolator,
            Func<float, float> easing)
        {
            return new BasicTween<int>(interpolator, easing);
        }

        #endregion Methods
    }
}