/*------------------------------------------------------------------------------
File:       WeightedIndexPoolTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Test cases for the WeightedIndexPool class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-29 05:13:51 
------------------------------------------------------------------------------*/

using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    [TestFixture]
    public class WeightedIndexPoolTests
    {
        #region Constants

        private const int _maxCapacity = 10;

        private double SingleWeight(int index) { return index; }

        private double DrawFirst() { return 0; }

        private double DrawRandom() { return new Random().NextDouble(); }

        #endregion Constants

        #region Methods

        #region Constructors Tests

        [Test]
        public void Constructor_CapacityZero_ThrowsArguementOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new WeightedIndexPool(0, null, null);
            });
        }

        [Test]
        public void Constructor_CapacityNegative_ThrowsArguementOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new WeightedIndexPool(-1, null, null);
            });
        }

        [Test]
        public void Constructor_NullIndexWeightFunctor_ThrowsArguementNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new WeightedIndexPool(_maxCapacity, null, null);
            });
        }

        [Test]
        public void Constructor_NullRandomizerFunctor_ThrowsArguementNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new WeightedIndexPool(_maxCapacity, SingleWeight, null);
            });
        }

        [Test]
        public void Count_Accessing_ReturnsInitialSize()
        {
            // Arrange
            var pool = 
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawRandom);

            // Assert
            Assert.AreEqual(pool.Count, _maxCapacity);
        }

        [Test]
        public void HasIndices_AccessingUnpulledPool_ReturnsTrue()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(1, SingleWeight, DrawRandom);

            // Assert
            Assert.IsTrue(pool.HasIndices);
        }

        [Test]
        public void HasIndices_AccessingFullyPulledPool_ReturnsFalse()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(1, SingleWeight, DrawRandom);

            // Act
            pool.PullIndex();

            // Assert
            Assert.IsFalse(pool.HasIndices);
        }


        #endregion Constructors Tests

        #region PullIndex Tests

        [Test]
        public void PullIndex_PullAll_NoIndicesRemain()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawRandom);

            // Act
            for(var iter = 0; iter < _maxCapacity; ++iter)
            {
                pool.PullIndex();
            }

            // Assert
            var indices = pool.AvailableIndices();
            Assert.AreEqual(indices.Count, 0);
        }

        [Test]
        public void PullIndex_PullAll_AllIndicesAreDifferent()
        {
            // Arrange
            var indices = new List<int>();
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawRandom);

            // Act
            for(var iter = 0; iter < _maxCapacity; ++iter)
            {
                indices.Add(pool.PullIndex());
            }

            // Assert
            Assert.AreEqual(indices.Distinct().Count(), _maxCapacity);
        }

        [Test]
        public void PullIndex_PullAllPlusOne_NoIndicesRemain()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawRandom);

            // Act
            for(var iter = 0; iter < _maxCapacity; ++iter)
            {
                pool.PullIndex();
            }

            // Assert
            Assert.Throws<InvalidOperationException>(() =>
            {
                pool.PullIndex();
            });
        }

        #endregion PullIndex Tests

        #region PullIndexWithReplacement Tests

        [Test]
        public void PullIndexWithReplacement_PullSingleIndex_AllIndicesRemain()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawFirst);

            // Act
            pool.PullIndexWithReplacement();

            // Assert
            var indices = pool.AvailableIndices();
            Assert.AreEqual(indices.Count, _maxCapacity);
        }

        [Test]
        public void PullIndexWithReplacement_PullAll_AllIndicesRemain()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawFirst);

            // Act
            for(var iter = 0; iter < _maxCapacity; ++iter)
            {
                pool.PullIndexWithReplacement();
            }

            // Assert
            var indices = pool.AvailableIndices();
            Assert.AreEqual(indices.Count, _maxCapacity);
        }

        [Test]
        public void PullIndexWithReplacement_PullAllPlusOne_AllIndicesRemain()
        {
            // Arrange
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawFirst);

            // Act
            for(var iter = 0; iter <= _maxCapacity; ++iter)
            {
                pool.PullIndexWithReplacement();
            }

            // Assert
            var indices = pool.AvailableIndices();
            Assert.AreEqual(indices.Count, _maxCapacity);
        }

        [Test]
        public void PullIndexWithReplacement_PullAllWithDrawFirst_AllIndicesAreTheSame()
        {
            // Arrange
            var indices = new List<int>();
            var pool =
                new WeightedIndexPool(_maxCapacity, SingleWeight, DrawFirst);

            // Act
            for(var iter = 0; iter < _maxCapacity; ++iter)
            {
                indices.Add(pool.PullIndexWithReplacement());
            }

            // Assert
            Assert.AreEqual(indices.Distinct().Count(), 1);
        }

        #endregion PullIndexWithReplacement Tests

        #endregion Methods
    }
}
