/*------------------------------------------------------------------------------
File:       ITweenTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a base class for unit tests of ITween implementations. 
            This class includes common setup and basic tests to ensure that 
            tween implementations can be tested for basic functionality such 
            as showing the tween and applying progress.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-03 20:02:04 
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
        /// Sets up the test environment before each test.
        /// </summary>
        [SetUp]
        public abstract void Setup();

        /// <summary>
        /// Tests that calling Show on the ITween instance does not throw an
        /// exception.
        /// </summary>
        [Test]
        public virtual void Show_Called_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.Show(true));
        }

        /// <summary>
        /// Tests that calling Show with false on the ITween instance does not 
        /// throw an exception.
        /// </summary>
        [Test]
        public virtual void Show_False_Called_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.Show(false));
        }

        /// <summary>
        /// Tests that calling ApplyProgress on the ITween instance does not
        /// throw an exception.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_Called_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.ApplyProgress(0.5f));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with 0.0f on the ITween instance 
        /// does not throw an exception.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_Zero_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.ApplyProgress(0.0f));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with 1.0f on the ITween instance
        /// does not throw an exception.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_One_DoesNotThrow()
        {
            Assert.DoesNotThrow(() => ITweenRef.ApplyProgress(1.0f));
        }

        /// <summary>
        /// Tests that calling ApplyProgress with invalid values on the ITween
        /// instance throws an ArgumentOutOfRangeException.
        /// </summary>
        [Test]
        public virtual void ApplyProgress_InvalidValues_ThrowsArgumentOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                ITweenRef.ApplyProgress(-0.1f));
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                ITweenRef.ApplyProgress(1.1f));
        }

        #endregion Methods
    }
}
