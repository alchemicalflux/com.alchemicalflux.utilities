/*------------------------------------------------------------------------------
File:       NullCheckTests_EditorOnly.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the NullCheck attribute.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-22 21:43:06 
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

        private const string _emptyGameObjectPrefabTestName = 
            "EmptyObject_ReturnsNoErrors";
        private const string _emptyScriptPrefabTestName = 
            "EmptyScript_ReturnsNoErrors";
        private const string _valueFieldsPrefabTestName = 
            "ValueFields_ReturnsSixteenErrors";
        private const string _nullCheckPrefabTestName = 
            "NullCheck_ReturnsOneError";
        private const string _prefabNullCheckPrefabTestName = 
            "PrefabNullCheck_ReturnsNoErrors";
        private const string _linkedNullCheckPrefabTestName = 
            "LinkedNullCheck_ReturnsNoErrors";
        private const string _linkedPrefabNullCheckPrefabTestName = 
            "LinkedPrefabNullCheck_ReturnsNoErrors";
        private const string _multiNullCheckPrefabTestName = 
            "MultiNullCheck_ReturnsSixErrors";
        private const string _linkedMultiNullCheckPrefabTestName = 
            "LinkedMultiNullCheck_ReturnsNoErrors";
        private const string _childNullCheckPrefabTestName = 
            "ChildNullCheck_ReturnsOneError";
        private const string _childPrefabNullCheckPrefabTestName =
            "ChildPrefabNullCheck_ReturnsNoErrors";
        private const string _linkedChildNullCheckPrefabTestName = 
            "LinkedChildNullCheck_ReturnsNoErrors";
        private const string _linkedChildPrefabNullCheckPrefabTestName = 
            "LinkedChildPrefabNullCheck_ReturnsNoErrors";

        // Prefab Scenerio Tests
        private static readonly 
            Dictionary<string, TestCaseData> _prefabScenarioData = new()
        {
            {
                _emptyGameObjectPrefabTestName,
                new(NullCheckTestType.EmptyObject, 0)
            },
            {
                _emptyScriptPrefabTestName,
                new(NullCheckTestType.EmptyScript, 0)
            },
            {
                _valueFieldsPrefabTestName,
                new(NullCheckTestType.ValueFields, 16)
            },
            {
                _nullCheckPrefabTestName,
                new(NullCheckTestType.NullCheck, 1)
            },
            {
                _prefabNullCheckPrefabTestName,
                new(NullCheckTestType.PrefabNullCheck, 0)
            },
            {
                _linkedNullCheckPrefabTestName,
                new(NullCheckTestType.LinkedNullCheck, 0)
            },
            {
                _linkedPrefabNullCheckPrefabTestName,
                new(NullCheckTestType.LinkedPrefabNullCheck, 0)
            },
            {
                _multiNullCheckPrefabTestName,
                new(NullCheckTestType.MultiNullCheck, 6)
            },
            {
                _linkedMultiNullCheckPrefabTestName,
                new(NullCheckTestType.LinkedMultiNullCheck, 0)
            },
            {
                _childNullCheckPrefabTestName,
                new(NullCheckTestType.ChildNullCheck, 1)
            },
            {
                _childPrefabNullCheckPrefabTestName,
                new(NullCheckTestType.ChildPrefabNullCheck, 0)
            },
            {
                _linkedChildNullCheckPrefabTestName,
                new(NullCheckTestType.LinkedChildNullCheck, 0)
            },
            {
                _linkedChildPrefabNullCheckPrefabTestName,
                new(NullCheckTestType.LinkedChildPrefabNullCheck, 0)
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