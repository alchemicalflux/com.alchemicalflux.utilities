/*------------------------------------------------------------------------------
File:       EnumFuncMapTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests shared by implementors of EnumFuncMap.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-22 15:04:31 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    [TestFixture]
    public class EnumFuncMapTests
    {
        #region Declarations

        public delegate void TestDelegate();

        private enum TestEnum
        {
            First,
            Second,
            Third
        }

        #endregion Declarations

        #region Methods

        [Test]
        public void Constructor_ValidDelegates_InitializesCorrectly()
        {
            var enumFuncMap = CreateHelper(3, out var delegates);
            Assert.NotNull(enumFuncMap);
        }

        [Test]
        public void Constructor_NullDelegates_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new EnumFuncMap<TestEnum, TestDelegate>(null);
            });
        }

        [Test]
        public void Constructor_EmptyDelegates_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var enumFuncMap = CreateHelper(0, out var delegates);
            });
        }

        [Test]
        public void Constructor_MismatchedDelegatesAndEnumValues_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var enumFuncMap = CreateHelper(2, out var delegates);
            });
        }

        [Test]
        public void Constructor_NullDelegate_ThrowsArgumentNullException()
        {
            var delegates = new List<TestDelegate>
            {
                new Mock<TestDelegate>().Object,
                null,
                new Mock<TestDelegate>().Object
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                new EnumFuncMap<TestEnum, TestDelegate>(delegates);
            });
        }

        [Test]
        public void Enum_InvalidEnumValue_ThrowsKeyNotFoundException()
        {
            var enumFuncMap = CreateHelper(3, out var delegates);

            Assert.Throws<KeyNotFoundException>(() =>
            {
                enumFuncMap.Enum = (TestEnum)999;
            });
        }

        [Test]
        public void Enum_SetEnumValue_UpdatesFuncCorrectly()
        {
            var enumFuncMap = CreateHelper(3, out var delegates);

            enumFuncMap.Enum = TestEnum.First;
            Assert.AreEqual(delegates[0], enumFuncMap.Func);

            enumFuncMap.Enum = TestEnum.Second;
            Assert.AreEqual(delegates[1], enumFuncMap.Func);

            enumFuncMap.Enum = TestEnum.Third;
            Assert.AreEqual(delegates[2], enumFuncMap.Func);
        }

        [Test]
        public void Func_NoDelegateAssigned_ReturnsDefaultDelegate()
        {
            var enumFuncMap = CreateHelper(3, out var delegates);

            // Invoke the default delegate and verify its behavior
            Assert.DoesNotThrow(() => enumFuncMap.Func.Invoke());
        }

        [Test]
        public void Func_DelegateInvocation_InvokesCorrectDelegate()
        {
            var enumFuncMap = CreateHelper(3, out var delegates);

            var firstDelegateMock = Mock.Get(delegates[0]);
            var secondDelegateMock = Mock.Get(delegates[1]);
            var thirdDelegateMock = Mock.Get(delegates[2]);

            enumFuncMap.Enum = TestEnum.First;
            enumFuncMap.Func.Invoke();
            firstDelegateMock.Verify(d => d.Invoke(), Times.Once);

            enumFuncMap.Enum = TestEnum.Second;
            enumFuncMap.Func.Invoke();
            secondDelegateMock.Verify(d => d.Invoke(), Times.Once);

            enumFuncMap.Enum = TestEnum.Third;
            enumFuncMap.Func.Invoke();
            thirdDelegateMock.Verify(d => d.Invoke(), Times.Once);
        }

        private EnumFuncMap<TestEnum, TestDelegate> CreateHelper(
            int delegateCount,
            out IList<TestDelegate> delegates)
        {
            delegates = new List<TestDelegate>();
            for(var iter = 0; iter < delegateCount; ++iter)
            {
                delegates.Add(new Mock<TestDelegate>().Object);
            }
            return new EnumFuncMap<TestEnum, TestDelegate>(delegates);
        }

        #endregion Methods
    }
}
