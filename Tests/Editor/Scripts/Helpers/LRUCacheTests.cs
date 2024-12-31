/*------------------------------------------------------------------------------
  File:           LRUCacheTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Test cases fir the LRUCache class.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-31 08:57:22 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Reflection;
namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class LRUCacheTests
    {
        private const int _maxCapacity = 1000;

        [Test]
        public void Constructor_OnCreateNull_ThrowsArguementNullException()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() =>
            {
                new LRUCache<int, object>(null, null, 0);
            });
        }

        [Test]
        public void Constructor_CapacityZero_ThrowsArguementOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new LRUCache<int, object>(create => new(), null, 0);
            });
        }

        [Test]
        public void Constructor_CapacityNegative_ThrowsArguementOutOfRangeException()
        {
            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                new LRUCache<int, object>(create => new(), null, -1);
            });
        }

        [Test]
        public void Capacity_NoAlterations_MatchesInitilization()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Assert
            Assert.AreEqual(cache.Capacity, _maxCapacity);
        }

        [Test]
        public void Count_NoAlterations_MatchesInitilization()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Assert
            Assert.AreEqual(cache.Count, 0);
        }

        [Test]
        public void Clear_RemoveAllItems_CountIsZero()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                cache.Get(index);
            }
            cache.Clear();

            // Assert
            Assert.AreEqual(cache.Count, 0);
        }

        [Test]
        public void Clear_RemoveAllItems_FutureGetsDoNotMatch()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Clear();

            // Assert
            for(var index = 0; index < _maxCapacity; ++index)
            {
                Assert.AreNotEqual(tests[index], cache.Get(index), $"Object {index} has not been removed.");
            }
        }

        [Test]
        public void Resize_SizeIsZero_ThrowsArguementOutOfRangeException()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                cache.Resize(0);
            });
        }

        [Test]
        public void Resize_SizeIsNegative_ThrowsArguementOutOfRangeException()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                cache.Resize(-1);
            });
        }

        [Test]
        public void Resize_SetSizeToOne_CapacityIsOne()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                cache.Get(index);
            }
            cache.Resize(1);

            // Assert
            Assert.AreEqual(cache.Capacity, 1);
        }

        [Test]
        public void Resize_SetSizeToOne_CountIsOne()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                cache.Get(index);
            }
            cache.Resize(1);

            // Assert
            Assert.AreEqual(cache.Count, 1);
        }

        [Test]
        public void Resize_SetSizeToOne_NoValuesMatchExceptNewst()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity];

            // Act
            for(var index = _maxCapacity - 1; index >= 0; --index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Resize(1);

            // Assert
            Assert.AreEqual(tests[0], cache.Get(0), $"Object 0 does not match.");
            for(var index = 1; index < _maxCapacity; ++index)
            {
                Assert.AreNotEqual(tests[index], cache.Get(index), $"Object {index} has not been removed.");
            }
        }

        [Test]
        public void Resize_SetSizeToOneLarger_AllValuesMatch()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity + 1];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Resize(_maxCapacity + 1);
            tests[_maxCapacity] = cache.Get(_maxCapacity);

            // Assert
            for(var index = 0; index <= _maxCapacity; ++index)
            {
                Assert.AreEqual(tests[index], cache.Get(index), $"Object {index} does not match.");
            }
        }

        [Test]
        public void Get_SingleItemAccess_ResultsMatch()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            
            // Act
            var value = cache.Get(0);

            // Assert
            Assert.AreEqual(value, cache.Get(0));
        }

        [Test]
        public void Get_FullCacheAccess_AllValuesMatch()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }

            // Assert
            for(var index = _maxCapacity - 1; index >= 0; --index)
            {
                Assert.AreEqual(tests[index], cache.Get(index), $"Object {index} does not match.");
            }
        }

        [Test]
        public void Get_OverflowCacheAccess_AllValuesMatchExceptOldest()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Get(_maxCapacity);

            // Assert
            for(var index = _maxCapacity - 1; index > 0; --index)
            {
                Assert.AreEqual(tests[index], cache.Get(index), $"Object {index} does not match.");
            }
            Assert.AreNotEqual(tests[0], cache.Get(0), "Object 0 has not been removed.");
        }

        [Test]
        public void Get_MoveOldestToFront_AllValuesMatchExepctSecondOldest()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity + 1];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Get(0);
            cache.Get(_maxCapacity);

            // Assert
            Assert.AreEqual(tests[0], cache.Get(0));
            for(var index = 2; index < _maxCapacity; ++index)
            {
                Assert.AreEqual(tests[index], cache.Get(index), $"Object {index} does not match.");
            }
            Assert.AreNotEqual(tests[1], cache.Get(1), $"Object 1 has not been removed.");
        }

        [Test]
        public void Get_LeastRecentlyUsedRemoved_OnDestroyInvokedForAllExceptLast()
        {
            // Arrange
            var destroyed = 0;
            var cache = new LRUCache<int, object>(create => new(),
                destroy => { ++destroyed; }, 1);

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                cache.Get(index);
            }

            // Assert
            Assert.AreEqual(destroyed, _maxCapacity - 1);
        }


        [Test]
        public void Resize_CapacitySetToZero_ThrowsArguementOutOfRangeException()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                cache.Resize(0);
            });
        }

        [Test]
        public void Resize_CapacityDoubled_CapacityMatches()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Act
            cache.Resize(_maxCapacity * 2);

            // Assert
            Assert.AreEqual(cache.Capacity, _maxCapacity * 2);
        }

        [Test]
        public void Resize_CapacityDoubled_CountMatches()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                cache.Get(index);
            }
            cache.Resize(_maxCapacity * 2);

            // Assert
            Assert.AreEqual(cache.Count, _maxCapacity);
        }

        [Test]
        public void Resize_CapacityDoubled_ValuesMatch()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Resize(_maxCapacity * 2);

            // Assert
            for(var index = 0; index < _maxCapacity; ++index)
            {
                Assert.AreEqual(tests[index], cache.Get(index), $"Object {index} does not match.");
            }
        }

        [Test]
        public void Resize_ReduceToOneCapacity_CountShouldBeOne()
        {
            // Arrange
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                cache.Get(index);
            }
            cache.Resize(1);

            // Assert
            Assert.AreEqual(cache.Count, 1);
        }

        [Test]
        public void Resize_ReduceToOneCapacity_NoValuesMatchExceptLatest()
        {
            // Arrange
            var last = _maxCapacity - 1;
            var cache = new LRUCache<int, object>(create => new(), null, _maxCapacity);
            var tests = new object[_maxCapacity];

            // Act
            for(var index = 0; index < _maxCapacity; ++index)
            {
                tests[index] = cache.Get(index);
            }
            cache.Resize(1);

            // Assert
            Assert.AreEqual(tests[last], cache.Get(last), $"Object {last} does not match.");
            for(var index = last - 1; index >= 0; --index)
            {
                Assert.AreNotEqual(tests[index], cache.Get(index), $"Object {index} has not been removed.");
            }
        }
    }
}
