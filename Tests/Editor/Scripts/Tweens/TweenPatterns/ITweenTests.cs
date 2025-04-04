/*------------------------------------------------------------------------------
File:       ITweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base class for unit tests of ITween implementations. 
            This class includes common setup and basic tests to ensure that 
            tween implementations can be tested for basic functionality such 
            as showing the tween and applying progress.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-03 23:03:42 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;

namespace AlchemicalFlux.Utilities.Tweens.Tests
{
    /// <summary>
    /// Provides a base class for unit tests of ITween implementations.
    /// This class includes common setup and basic tests to ensure that 
    /// tween implementations can be tested for basic functionality such 
    /// as showing the tween and applying progress.
    /// </summary>
    public abstract class IITweenTests
    {
        #region Properties

        /// <summary>
        /// Gets or sets the ITween instance being tested.
        /// </summary>
        public ITween ITweenRef { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Sets up the test environment before each test. Requires the setting
        /// of the ITweenRef property to the instance being tested.
        /// </summary>
        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that calling Show on the ITween instance does not throw an
        /// exception.
        /// </summary>
        [Test]
        public virtual void Show_TrueCalled_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.Show(true));
        }

        /// <summary>
        /// Tests that calling Show with false on the ITween instance does not 
        /// throw an exception.
        /// </summary>
        [Test]
        public virtual void Show_FalseCalled_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.Show(false));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with the minimum progress value on 
        /// the ITween instance does not throw an exception.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_MinProgress_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
                ITweenRef.ApplyProgress(ITweenRef.MinProgress));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with the maximum progress value on 
        /// the ITween instance does not throw an exception.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_MaxProgress_DoesNotThrow()
        {
            Assert.DoesNotThrow(() =>
                ITweenRef.ApplyProgress(ITweenRef.MaxProgress));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with a middle progress value on 
        /// the ITween instance does not throw an exception.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_MiddleValue_DoesNotThrow()
        {
            var middleValue = ITweenRef.MinProgress + 
                (ITweenRef.MaxProgress - ITweenRef.MinProgress) / 2;
            Assert.DoesNotThrow(() =>
                ITweenRef.ApplyProgress(middleValue));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with a value less than the minimum 
        /// progress value on the ITween instance throws an 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_InvalidMinProgress_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                ITweenRef.ApplyProgress(ITweenRef.MinProgress - 0.1f));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with a value greater than the 
        /// maximum progress value on the ITween instance throws an 
        /// ArgumentOutOfRangeException.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_InvalidMaxProgress_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
                ITweenRef.ApplyProgress(ITweenRef.MaxProgress + 0.1f));
        }

        #endregion Methods
    }
}
