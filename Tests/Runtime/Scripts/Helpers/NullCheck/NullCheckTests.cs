/*------------------------------------------------------------------------------
  File:           NullCheckTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for the NullCheck attribute.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 08:00:02 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class NullCheckTests
    {
        #region Members

        /// <summary>Handle used to access GameObjects for testing purposes.</summary>
        private NullCheckTestSO _testObjects;

        #region Test Scenerios

        private const string _emptyGameObjectPrefabTestName = "EmptyObject_ReturnsNoErrors";
        private const string _emptyScriptPrefabTestName = "EmptyScript_ReturnsNoErrors";
        private const string _valueFieldsPrefabTestName = "ValueFields_ReturnsTwelveErrors";
        private const string _nullCheckPrefabTestName = "NullCheck_ReturnsOneError";
        private const string _prefabNullCheckPrefabTestName = "PrefabNullCheck_ReturnsNoErrors";
        private const string _linkedNullCheckPrefabTestName = "LinkedNullCheck_ReturnsNoErrors";
        private const string _linkedPrefabNullCheckPrefabTestName = "LinkedPrefabNullCheck_ReturnsNoErrors";
        private const string _multiNullCheckPrefabTestName = "MultiNullCheck_ReturnsFourErrors";
        private const string _linkedMultiNullCheckPrefabTestName = "LinkedMultiNullCheck_ReturnsNoErrors";
        private const string _childNullCheckPrefabTestName = "ChildNullCheck_ReturnsOneError";
        private const string _childPrefabNullCheckPrefabTestName = "ChildPrefabNullCheck_ReturnsNoErrors";
        private const string _linkedChildNullCheckPrefabTestName = "LinkedChildNullCheck_ReturnsNoErrors";
        private const string _linkedChildPrefabNullCheckPrefabTestName = "LinkedChildPrefabNullCheck_ReturnsNoErrors";

        // Prefab Scenerio Tests
        private static readonly Dictionary<string, TestCaseData> _prefabScenarioData =
             new Dictionary<string, TestCaseData>()
             {
                {
                    _emptyGameObjectPrefabTestName,
                    new TestCaseData(NullCheckTestType.EmptyObject, 0)
                },
                {
                    _emptyScriptPrefabTestName,
                    new TestCaseData(NullCheckTestType.EmptyScript, 0)
                },
                {
                    _valueFieldsPrefabTestName,
                    new TestCaseData(NullCheckTestType.ValueFields, 12)
                },
                {
                    _nullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.NullCheck, 1)
                },
                {
                    _prefabNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.PrefabNullCheck, 0)
                },
                {
                    _linkedNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.LinkedNullCheck, 0)
                },
                {
                    _linkedPrefabNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.LinkedPrefabNullCheck, 0)
                },
                {
                    _multiNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.MultiNullCheck, 4)
                },
                {
                    _linkedMultiNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.LinkedMultiNullCheck, 0)
                },
                {
                    _childNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.ChildNullCheck, 1)
                },
                {
                    _childPrefabNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.ChildPrefabNullCheck, 0)
                },
                {
                    _linkedChildNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.LinkedChildNullCheck, 0)
                },
                {
                    _linkedChildPrefabNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.LinkedChildPrefabNullCheck, 0)
                },
             };

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

        private static readonly Dictionary<string, TestCaseData> _instantiatedScenarioData =
            new Dictionary<string, TestCaseData>()
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

        private static IEnumerable<TestCaseData> PrefabGameObjectTests()
        {
            foreach (var scenario in _prefabScenarioData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        private static IEnumerable<TestCaseData> InstantiateGameObjectTests()
        {
            foreach (var scenario in _instantiatedScenarioData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        #endregion Test Case Sources

        #endregion Members

        #region Methods

        [SetUp]
        public void Setup()
        {
            // Arrange
            _testObjects = Resources.Load<NullCheckTestSO>("Helpers/NullCheck/NullCheckTestSO");
        }

        [Test]
        [TestCaseSource(nameof(PrefabGameObjectTests))]
        public void PrefabGameObject(NullCheckTestType type, int expectedResult)
        {
            // Act
            var result = NullCheckFinder.RetrieveErrors(_testObjects.Get(type));

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedResult));
        }

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