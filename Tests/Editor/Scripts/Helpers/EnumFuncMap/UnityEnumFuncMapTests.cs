/*------------------------------------------------------------------------------
File:       UnityEnumFuncMapTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the UnityEnumFuncMap class.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-23 21:43:22 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Moq;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    [TestFixture]
    public class UnityEnumFuncMapTests
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
            var unityEnumFuncMap =
                new UnityEnumFuncMap<TestEnum, TestDelegate>();
            Assert.NotNull(unityEnumFuncMap);
        }

        [Test]
        public void AssignFuncs_NullDelegates_ThrowsArgumentNullException()
        {
            var unityEnumFuncMap = 
                new UnityEnumFuncMap<TestEnum, TestDelegate>();
            Assert.Throws<ArgumentNullException>(() =>
            {
                unityEnumFuncMap.AssignFuncs(null);
            });
        }

        [Test]
        public void AssignFuncs_MismatchedDelegatesAndEnumValues_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var unityEnumFuncMap = CreateHelper(2, out var delegates);
            });
        }

        [Test]
        public void Enum_SetEnumValue_UpdatesFuncCorrectly()
        {
            var unityEnumFuncMap = CreateHelper(3, out var delegates);

            unityEnumFuncMap.Enum = TestEnum.First;
            Assert.AreEqual(delegates[0], unityEnumFuncMap.Func);

            unityEnumFuncMap.Enum = TestEnum.Second;
            Assert.AreEqual(delegates[1], unityEnumFuncMap.Func);

            unityEnumFuncMap.Enum = TestEnum.Third;
            Assert.AreEqual(delegates[2], unityEnumFuncMap.Func);
        }

        [Test]
        public void Func_NoDelegateAssigned_ReturnsDefaultDelegate()
        {
            var unityEnumFuncMap = 
                new UnityEnumFuncMap<TestEnum, TestDelegate>();

            // Invoke the default delegate and verify its behavior
            Assert.DoesNotThrow(() => unityEnumFuncMap.Func.Invoke());
        }

        [Test]
        public void AssignFuncs_NullDelegate_ThrowsArgumentNullException()
        {
            var unityEnumFuncMap = 
                new UnityEnumFuncMap<TestEnum, TestDelegate>();
            var delegates = new List<TestDelegate>
            {
                new Mock<TestDelegate>().Object,
                null,
                new Mock<TestDelegate>().Object
            };

            Assert.Throws<ArgumentNullException>(() =>
            {
                unityEnumFuncMap.AssignFuncs(delegates);
            });
        }

        private UnityEnumFuncMap<TestEnum, TestDelegate> CreateHelper(
            int delegateCount,
            out IList<TestDelegate> delegates)
        {
            delegates = new List<TestDelegate>();
            for(var iter = 0; iter < delegateCount; ++iter)
            {
                delegates.Add(new Mock<TestDelegate>().Object);
            }
            var unityEnumFuncMap = 
                new UnityEnumFuncMap<TestEnum, TestDelegate>();
            unityEnumFuncMap.AssignFuncs(delegates);
            return unityEnumFuncMap;
        }

        #endregion Methods
    }
}
