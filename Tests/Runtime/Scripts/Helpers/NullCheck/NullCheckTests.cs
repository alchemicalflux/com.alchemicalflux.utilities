/*------------------------------------------------------------------------------
  File:           NullCheckTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for the NullCheck attribute.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-13 18:35:48 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class NullCheckTests
    {
        #region Members

        private NullCheckTestSO testObjects;

        #region Test Scenerios

        private const string _emptyGameObjectPrefabTestName = "EmptyObject_ReturnsNoErrors";
        private const string _emptyScriptPrefabTestName = "EmptyScript_ReturnsNoErrors";
        private const string _nullCheckPrefabTestName = "NullCheck_ReturnsOneError";
        private const string _prefabNullCheckPrefabTestName = "PrefabNullCheck_ReturnsNoErrors";

        // Prefab Scenerio Tests
        private static readonly Dictionary<string, TestCaseData> PrefabScenarioData =
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
                    _nullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.NullCheck, 1)
                },
                {
                    _prefabNullCheckPrefabTestName,
                    new TestCaseData(NullCheckTestType.PrefabNullCheck, 0)
                },
             };

        // Instantiate Scenario Tests
        private const string _emptyGameObjectTestName = "EmptyObject_ReturnsNoErrors";
        private const string _emptyScriptTestName = "EmptyScript_ReturnsNoErrors";
        private const string _nullCheckTestName = "NullCheck_ReturnsOneError";
        private const string _prefabNullCheckTestName = "PrefabNullCheck_ReturnsOneError";

        private static readonly Dictionary<string, TestCaseData> InstantiatedScenarioData =
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
                    _nullCheckTestName,
                    new TestCaseData(NullCheckTestType.NullCheck, 1)
                },
                {
                    _prefabNullCheckTestName,
                    new TestCaseData(NullCheckTestType.PrefabNullCheck, 1)
                },
            };

        #endregion Test Scenerios

        #region Test Case Sources

        private static IEnumerable<TestCaseData> PrefabGameObjectTests()
        {
            foreach (var scenario in PrefabScenarioData)
            {
                yield return SetTestName(scenario.Value, scenario.Key);
            }
        }

        private static IEnumerable<TestCaseData> InstantiateGameObjectTests()
        {
            foreach (var scenario in InstantiatedScenarioData)
            {
                yield return SetTestName(scenario.Value, scenario.Key);
            }
        }

        #endregion Test Case Sources

        #endregion Members

        #region Methods

        [SetUp]
        public void Setup()
        {
            // Arrange
            testObjects = Resources.Load<NullCheckTestSO>("Helpers/NullCheck/NullCheckTestSO");
        }

        [Test]
        [TestCaseSource(nameof(PrefabGameObjectTests))]
        public void PrefabGameObject(NullCheckTestType type, int expectedResult)
        {
            // Act
            var result = NullCheckFinder.RetrieveErrors(testObjects.Get(type));

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedResult));
        }

        [Test]
        [TestCaseSource(nameof(InstantiateGameObjectTests))]
        public void InstantiateGameObject(NullCheckTestType type, int expectedResult)
        {
            // Assemble
            var copy = Object.Instantiate(testObjects.Get(type));

            // Act
            var result = NullCheckFinder.RetrieveErrors(copy);

            // Assert
            Assert.That(result.Count, Is.EqualTo(expectedResult));

            // Cleanup
            Object.Destroy(copy);
        }

        #region Helpers

        /// <summary>
        /// A central location for the setting of the name for generated tests.
        /// </summary>
        /// <param name="data">Data whose name will be set.</param>
        /// <param name="name">Value that will be displayed in the test suite.</param>
        /// <returns>Reference to the passed in data.</returns>
        private static TestCaseData SetTestName(TestCaseData data, string name)
        {
            return data.SetName(name);
        }

        #endregion Helpers

        #endregion Methods
    }
}