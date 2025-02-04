/*------------------------------------------------------------------------------
File:       WeightedIndexPoolTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Test cases for the WeightedIndexPool class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-02-03 17:41:24 
------------------------------------------------------------------------------*/

using NUnit.Framework;
using System;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    [TestFixture]
    public class WeightedIndexPoolTests
    {
        #region Constants

        private const int _maxCapacity = 10;

        public double SingleWeight(int index)
        {
            return index;
        }

        #endregion Constants

        [Test]
        public void Constructor_CapacityZero_ThrowsArguementOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new WeightedIndexPool(0, null, null);
            });
        }

        [Test]
        public void Constructor_CapacityNegative_ThrowsArguementOutOfRangeException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new WeightedIndexPool(-1, null, null);
            });
        }

        [Test]
        public void Constructor_NullIndexWeightFunctor_ThrowsArguementNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new WeightedIndexPool(_maxCapacity, null, null);
            });
        }

        [Test]
        public void Constructor_NullRandomizerFunctor_ThrowsArguementNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new WeightedIndexPool(_maxCapacity, SingleWeight, null);
            });
        }

    }
}
