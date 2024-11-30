/*------------------------------------------------------------------------------
  File:           NullCheckTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for the NullCheck attribute.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:48:48 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public partial class NullCheckTests
    {
        #region Members

        #region Test Scenerios

        // Instantiate Scenario Tests
        private const string _emptyGameObjectTestName = "EmptyObject_ReturnsNoErrors";
        private const string _emptyScriptTestName = "EmptyScript_ReturnsNoErrors";
        private const string _valueFieldsTestName = "ValueFields_ReturnsTwelveErrors";
        private const string _nullCheckTestName = "NullCheck_ReturnsOneError";
        private const string _prefabNullCheckTestName = "PrefabNullCheck_ReturnsOneError";
        private const string _linkedNullCheckTestName = "LinkedNullCheck_ReturnsNoError";
        private const string _linkedPrefabNullCheckTestName = "LinkedPrefabNullCheck_ReturnsNoErrors";
        private const string _multiNullCheckTestName = "MultiNullCheck_ReturnsEightErrors";
        private const string _linkedMultiNullCheckTestName = "LinkedMultiNullCheck_ReturnsNoErrors";
        private const string _childNullCheckTestName = "ChildNullCheck_ReturnsOneError";
        private const string _childPrefabNullCheckTestName = "ChildPrefabNullCheck_ReturnsNoErrors";
        private const string _linkedChildNullCheckTestName = "LinkedChildNullCheck_ReturnsNoErrors";
        private const string _linkedChildPrefabNullCheckTestName = "LinkedChildPrefabNullCheck_ReturnsNoErrors";

        private static readonly Dictionary<string, TestCaseData> _instantiatedScenarioData = new()
        {
            {
                _emptyGameObjectTestName,
                new TestCaseData(NullCheckTestType.EmptyObject, 0)
            },
            {
                _emptyScriptTestName,
                new TestCaseData(NullCheckTestType.EmptyScript, 0)
            },
            {
                _valueFieldsTestName,
                new TestCaseData(NullCheckTestType.ValueFields, 12)
            },
            {
                _nullCheckTestName,
                new TestCaseData(NullCheckTestType.NullCheck, 1)
            },
            {
                _prefabNullCheckTestName,
                new TestCaseData(NullCheckTestType.PrefabNullCheck, 1)
            },
            {
                _linkedNullCheckTestName,
                new TestCaseData(NullCheckTestType.LinkedNullCheck, 0)
            },
            {
                _linkedPrefabNullCheckTestName,
                new TestCaseData(NullCheckTestType.LinkedPrefabNullCheck, 0)
            },
            {
                _multiNullCheckTestName,
                new TestCaseData(NullCheckTestType.MultiNullCheck, 8)
            },
            {
                _linkedMultiNullCheckTestName,
                new TestCaseData(NullCheckTestType.LinkedMultiNullCheck, 0)
            },
            {
                _childNullCheckTestName,
                new TestCaseData(NullCheckTestType.ChildNullCheck, 1)
            },
            {
                _childPrefabNullCheckTestName,
                new TestCaseData(NullCheckTestType.ChildPrefabNullCheck, 1)
            },
            {
                _linkedChildNullCheckTestName,
                new TestCaseData(NullCheckTestType.LinkedChildNullCheck, 0)
            },
            {
                _linkedChildPrefabNullCheckTestName,
                new TestCaseData(NullCheckTestType.LinkedChildPrefabNullCheck, 0)
            },
        };

        #endregion Test Scenerios

        #region Test Case Sources

        private static IEnumerable<TestCaseData> InstantiateGameObjectTests()
        {
            foreach(var scenario in _instantiatedScenarioData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        #endregion Test Case Sources

        #endregion Members

        #region Methods

        [Test]
        [TestCaseSource(nameof(InstantiateGameObjectTests))]
        public void InstantiateGameObject(NullCheckTestType type, int expectedResult)
        {
            // Assemble
            var copy = Object.Instantiate(_testObjects.Get(type));

            // Act
            var result = NullCheckFinder.RetrieveErrors(copy);

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedResult));

            // Cleanup
            Object.Destroy(copy);
        }

        #endregion Methods
    }
}