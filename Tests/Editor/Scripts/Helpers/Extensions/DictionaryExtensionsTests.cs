/*------------------------------------------------------------------------------
  File:           DictionaryExtensionsTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for the DictionaryExtensions.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-24 12:35:55 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    /// <summary>
    /// Stores extensions for the <see cref="IDictionary{,}"/> class.
    /// </summary>
    public class DictionaryExtensionsTests
    {
        #region Members

        #region Test Values

        // Simple value

        private static Dictionary<string, int> _oneElement =
            new Dictionary<string, int>()
            {
                { "A", 1 },
            };

        // No Conflict values
        private static Dictionary<string, int> _noConflictA = new Dictionary<string, int>()
        {
            { "A1", 1 },
            { "A2", 2 },
        };

        private static Dictionary<string, int> _noConflictB = new Dictionary<string, int>()
        {
            { "B1", 1 },
            { "B2", 2 },
        };

        private static Dictionary<string, int> _noConflictResult = new Dictionary<string, int>()
        {
            { "A1", 1 },
            { "A2", 2 },
            { "B1", 1 },
            { "B2", 2 },
        };

        // Conflict values
        private static Dictionary<string, int> _conflictA = new Dictionary<string, int>()
        {
            { "A", 1 },
            { "B", 1 },
        };

        private static readonly Dictionary<string, int> _conflictB = new Dictionary<string, int>()
        {
            { "B", 2 },
            { "C", 1 },
        };

        private static readonly Dictionary<string, int> _conflictResult = new Dictionary<string, int>()
        {
            { "A", 1 },
            { "B", 1 },
            { "C", 1 },
        };

        private static readonly Dictionary<string, int> _conflictReversedResult = new Dictionary<string, int>()
        {
            { "A", 1 },
            { "B", 2 },
            { "C", 1 },
        };

        #endregion Test Values

        #region Test Scenarios

        // Invalid Scenario Tests

        private const string _selfIsNullTestName = "SelfIsNull_ThrowArgumentNullException";
        private const string _otherIsNullTestName = "OtherIsNull_ThrowArgumentNullException";

        private static readonly Dictionary<string, TestCaseData> _invalidScenariosData =
            new Dictionary<string, TestCaseData>()
            {
                {
                    _selfIsNullTestName,
                    new TestCaseData(null, new Dictionary<string, int>())
                },
                {
                    _otherIsNullTestName,
                    new TestCaseData(new Dictionary<string, int>(), null)
                },
            };

        // Merging Scenario Tests
        private const string _selfIsEmptyTestName = "SelfIsEmpty_ResultMatchesOther";
        private const string _otherIsEmptyTestName = "OtherIsEmpty_ResultMatchesSelf";
        private const string _noConflictKeysTestName = "NoConflictKeys_ResultHasAllElements";
        private const string _dictionariesWithConflictKeysTestName = "DictionariesWithConflictKeys_ResultContainsSelfKeys";
        private const string _reverseDictionaryOrderTestName = "ReverseDictionaryOrder_ConflictKeyHasDifferentValue";

        private static readonly Dictionary<string, TestCaseData> _mergingScenariosData =
            new Dictionary<string, TestCaseData>()
            {
                {
                    _selfIsEmptyTestName,
                    new TestCaseData(new Dictionary<string, int>(), _oneElement, _oneElement)
                },
                {
                    _otherIsEmptyTestName,
                    new TestCaseData(_oneElement, new Dictionary<string, int>(), _oneElement)
                },
                {
                    _noConflictKeysTestName,
                    new TestCaseData(_noConflictA, _noConflictB, _noConflictResult)
                },
                {
                    _dictionariesWithConflictKeysTestName,
                    new TestCaseData(_conflictA, _conflictB, _conflictResult)
                },
                {
                    _reverseDictionaryOrderTestName,
                    new TestCaseData(_conflictB, _conflictA, _conflictReversedResult)
                },
            };

        #endregion Test Scenarios

        #endregion Members

        #region Methods

        #region Test Case Sources

        private static IEnumerable<TestCaseData> InvalidParameterScenarios()
        {
            foreach (var scenario in _invalidScenariosData)
            {
                yield return SetTestName(scenario.Value, scenario.Key);
            }
        }

        private static IEnumerable<TestCaseData> MergingScenarios()
        {
            foreach (var scenario in _mergingScenariosData)
            {
                yield return SetTestName(scenario.Value, scenario.Key);
            }
        }

        #endregion Test Case Sources

        #region Unit Tests

        [Test]
        [TestCaseSource(nameof(InvalidParameterScenarios))]
        public void Merge_InvalidParameterTests(Dictionary<string, int> self,
            Dictionary<string, int> other)
        {
            // Assert
            Assert.That(() => self.Merge(other), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(nameof(MergingScenarios))]
        public void Merge_MergingTests(Dictionary<string, int> self,
            Dictionary<string, int> other, IEnumerable<KeyValuePair<string,int>> expectedResults)
        {
            // Act
            var result = self.Merge(other);

            // Assert
            Assert.That(result, Is.EquivalentTo(expectedResults));
        }

        #endregion Unit Tests

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
