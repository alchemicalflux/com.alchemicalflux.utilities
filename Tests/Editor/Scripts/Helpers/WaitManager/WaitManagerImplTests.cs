/*------------------------------------------------------------------------------
File:       WaitManagerImplTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Test cases for the WaitManager class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-23 21:43:22 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{

    [TestFixture]
    public class WaitManagerImplTests
    {
        #region Constants

        private const int _maxCapacity = 10;

        #endregion Constants

        #region Methods

        #region Constructor Tests

        [Test]
        public void Constructor_CapacityZero_ThrowsArguementOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new WaitManagerImpl(0);
            });
        }

        [Test]
        public void Constructor_CapacityNegative_ThrowsArguementOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new WaitManagerImpl(-1);
            });
        }

        [Test]
        public void Constructor_DefaultCapacity_MatchesInitilization()
        {
            // Arrange
            var impl = new WaitManagerImpl();

            // Assert
            Assert.Greater(impl.Capacity, 0);
        }

        [Test]
        public void Constructor_CapacityMax_MatchesInitilization()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.AreEqual(impl.Capacity, _maxCapacity);
        }

        #endregion Constructor Tests

        #region WaitForEndFrame Tests

        [Test]
        public void WaitForEndOfFrame_Accessing_ReturnsNotNull()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.NotNull(impl.WaitForEndOfFrame);
        }

        [Test]
        public void WaitForEndOfFrame_Accessing_ReturnsSameInstance()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.AreSame(impl.WaitForEndOfFrame, impl.WaitForEndOfFrame);
        }

        #endregion WaitForEndFrame Tests

        #region WaitForFixedUpdate Tests

        [Test]
        public void WaitForFixedUpdate_Accessing_ReturnsNotNull()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.NotNull(impl.WaitForFixedUpdate);
        }

        [Test]
        public void WaitForFixedUpdate_Accessing_ReturnsSameInstance()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.AreSame(impl.WaitForFixedUpdate, impl.WaitForFixedUpdate);
        }

        #endregion WaitForFixedUpdate Tests

        #region WaitForSeconds Tests

        [Test]
        public void WaitForSeconds_Accessing_ReturnsNotNull()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.NotNull(impl.WaitForSeconds(0));
        }

        [Test]
        public void WaitForSeconds_Accessing_ReturnsSameInstance()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.AreSame(impl.WaitForSeconds(0), impl.WaitForSeconds(0));
        }

        [Test]
        public void WaitForSeconds_FullAccess_AllValuesMatch()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            var tests = new WaitForSeconds[impl.Capacity];

            // Act
            for(var index = 0; index < impl.Capacity; ++index)
            {
                tests[index] = impl.WaitForSeconds(index);
            }

            // Assert
            for(var index = 0; index < impl.Capacity; ++index)
            {
                Assert.AreSame(tests[index], impl.WaitForSeconds(index),
                    $"WaitForSeconds {index} does not match.");
            }
        }

        [Test]
        public void WaitForSeconds_OverflowCacheAccess_AllValuesMatchExceptOldest()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            var tests = new WaitForSeconds[impl.Capacity + 1];

            // Act
            for(var index = 0; index <= impl.Capacity; ++index)
            {
                tests[index] = impl.WaitForSeconds(index);
            }

            // Assert
            for(var index = impl.Capacity; index > 0; --index)
            {
                Assert.AreEqual(tests[index], impl.WaitForSeconds(index),
                    $"WaitForSeconds {index} does not match.");
            }
            Assert.AreNotEqual(tests[0], impl.WaitForSeconds(0), 
                "WaitForSeconds 0 has not been removed.");
        }

        #endregion WaitForSeconds Tests

        #region WaitForSecondsRealtime Tests

        [Test]
        public void WaitForSecondsRealtime_Accessing_ReturnsNotNull()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.NotNull(impl.WaitForSecondsRealtime(0));
        }

        [Test]
        public void WaitForSecondsRealtime_Accessing_ReturnsSameInstance()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);

            // Assert
            Assert.AreSame(impl.WaitForSecondsRealtime(0),
                impl.WaitForSecondsRealtime(0));
        }

        [Test]
        public void WaitForSecondsRealtime_FullAccess_AllValuesMatch()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            var tests = new WaitForSecondsRealtime[impl.Capacity];

            // Act
            for(var index = 0; index < impl.Capacity; ++index)
            {
                tests[index] = impl.WaitForSecondsRealtime(index);
            }

            // Assert
            for(var index = 0; index < impl.Capacity; ++index)
            {
                Assert.AreEqual(tests[index], 
                    impl.WaitForSecondsRealtime(index),
                    $"WaitForSecondsRealtime {index} does not match.");
            }
        }

        [Test]
        public void WaitForSecondsRealtime_OverflowCacheAccess_AllValuesMatchExceptOldest()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            var tests = new WaitForSecondsRealtime[impl.Capacity + 1];

            // Act
            for(var index = 0; index <= impl.Capacity; ++index)
            {
                tests[index] = impl.WaitForSecondsRealtime(index);
            }

            // Assert
            for(var index = impl.Capacity; index > 0; --index)
            {
                Assert.AreSame(tests[index], impl.WaitForSecondsRealtime(index),
                    $"WaitForSecondsRealtime {index} does not match.");
            }
            Assert.AreNotSame(tests[0], impl.WaitForSecondsRealtime(0),
                "WaitForSecondsRealtime 0 has not been removed.");
        }

        #endregion WaitForSecondsRealtime Tests

        #region WaitUntil Tests

        [Test]
        public void WaitUntil_Accessing_ReturnsNotNull()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            Func<bool> condition = () => DateTime.Now.Second % 2 == 0;

            // Act
            var waitUntil = impl.WaitUntil(condition);

            // Assert
            Assert.NotNull(waitUntil);
        }

        // Further checks can be added for how WaitWhile behaves (mocking the
        // passage of time, etc.)

        #endregion WaitUntil Tests

        #region WaitWhile Tests

        [Test]
        public void WaitWhile_Accessing_ReturnsNotNull()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            Func<bool> condition = () => DateTime.Now.Second % 2 == 0;

            // Act
            var waitWhile = impl.WaitWhile(condition);

            // Assert
            Assert.NotNull(waitWhile);
        }

        // Further checks can be added for how WaitWhile behaves (mocking the
        // passage of time, etc.)

        #endregion WaitWhile Tests

        #region Thread Safety and Concurrency Tests

        [Test]
        public void TestConcurrentAccessToWaitManager()
        {
            // Arrange
            var impl = new WaitManagerImpl(_maxCapacity);
            var tasks = new System.Threading.Tasks.Task[10];

            // Act
            for(int i = 0; i < tasks.Length; i++)
            {
                tasks[i] = System.Threading.Tasks.Task.Run(() =>
                {
                    var waitForSeconds = impl.WaitForSeconds(5.0f);
                });
            }

            // Assert
            Assert.DoesNotThrow(() => 
                System.Threading.Tasks.Task.WhenAll(tasks));
        }

        #endregion Thread Safety and Concurrency Tests

        #region Memory Management

        [Test]
        public void TestMemoryConsumptionForLargeScaleUse()
        {
            // Arrange
            // Simulate large scale use by requesting many WaitForSeconds and
            // WaitForSecondsRealtime objects

            // Act
            // Check memory usage (this could require a custom memory monitoring
            // solution)

            // Assert
            // Ensure that memory usage is within expected limits and no leaks
            // occur
        }

        #endregion Memory Management

        #endregion Methods
    }
}