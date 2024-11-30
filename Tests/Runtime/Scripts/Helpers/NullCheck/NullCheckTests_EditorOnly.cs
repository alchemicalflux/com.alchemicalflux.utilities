/*------------------------------------------------------------------------------
  File:           NullCheckTests_EditorOnly.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for the NullCheck attribute.
  Copyright:      �2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:48:48 
------------------------------------------------------------------------------*/
#if UNITY_EDITOR

using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public partial class NullCheckTests
    {
        #region Members

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
        private static readonly Dictionary<string, TestCaseData> _prefabScenarioData = new()
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

        #endregion Test Scenerios

        #region Test Case Sources

        private static IEnumerable<TestCaseData> PrefabGameObjectTests()
        {
            foreach(var scenario in _prefabScenarioData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        #endregion Test Case Sources

        #endregion Members

        #region Methods

        [Test]
        [TestCaseSource(nameof(PrefabGameObjectTests))]
        public void PrefabGameObject(NullCheckTestType type, int expectedResult)
        {
            // Act
            var result = NullCheckFinder.RetrieveErrors(_testObjects.Get(type));

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedResult));
        }

        #endregion Methods
    }
}

#endif