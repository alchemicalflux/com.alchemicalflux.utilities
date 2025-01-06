/*------------------------------------------------------------------------------
File:       DictionaryExtensionsTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for the DictionaryExtensions.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
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
        private static readonly Dictionary<string, int> _oneElement = new()
        {
            { "A", 1 },
        };

        // No Conflict values
        private static readonly Dictionary<string, int> _noConflictA = new()
        {
            { "A1", 1 },
            { "A2", 2 },
        };

        private static readonly Dictionary<string, int> _noConflictB = new()
        {
            { "B1", 1 },
            { "B2", 2 },
        };

        private static readonly 
            Dictionary<string, int> _noConflictResult = new()
        {
            { "A1", 1 },
            { "A2", 2 },
            { "B1", 1 },
            { "B2", 2 },
        };

        // Conflict values
        private static readonly Dictionary<string, int> _conflictA = new()
        {
            { "A", 1 },
            { "B", 1 },
        };

        private static readonly Dictionary<string, int> _conflictB = new()
        {
            { "B", 2 },
            { "C", 1 },
        };

        private static readonly Dictionary<string, int> _conflictResult = new()
        {
            { "A", 1 },
            { "B", 1 },
            { "C", 1 },
        };

        private static readonly 
            Dictionary<string, int> _conflictReversedResult = new()
        {
            { "A", 1 },
            { "B", 2 },
            { "C", 1 },
        };

        #endregion Test Values

        #region Test Scenarios

        // Invalid Scenario Tests

        private const string _selfIsNullTestName = 
            "SelfIsNull_ThrowArgumentNullException";
        private const string _otherIsNullTestName = 
            "OtherIsNull_ThrowArgumentNullException";

        private static readonly 
            Dictionary<string, TestCaseData> _invalidScenariosData = new()
        {
            {
                _selfIsNullTestName,
                new(null, new Dictionary<string, int>())
            },
            {
                _otherIsNullTestName,
                new(new Dictionary<string, int>(), null)
            },
        };

        // Merging Scenario Tests
        private const string _selfIsEmptyTestName = 
            "SelfIsEmpty_ResultMatchesOther";
        private const string _otherIsEmptyTestName = 
            "OtherIsEmpty_ResultMatchesSelf";
        private const string _noConflictKeysTestName = 
            "NoConflictKeys_ResultHasAllElements";
        private const string _dictionariesWithConflictKeysTestName = 
            "DictionariesWithConflictKeys_ResultContainsSelfKeys";
        private const string _reverseDictionaryOrderTestName = 
            "ReverseDictionaryOrder_ConflictKeyHasDifferentValue";

        private static readonly 
            Dictionary<string, TestCaseData> _mergingScenariosData = new()
        {
            {
                _selfIsEmptyTestName,
                new(new Dictionary<string, int>(), _oneElement, _oneElement)
            },
            {
                _otherIsEmptyTestName,
                new(_oneElement, new Dictionary<string, int>(), _oneElement)
            },
            {
                _noConflictKeysTestName,
                new(_noConflictA, _noConflictB, _noConflictResult)
            },
            {
                _dictionariesWithConflictKeysTestName,
                new(_conflictA, _conflictB, _conflictResult)
            },
            {
                _reverseDictionaryOrderTestName,
                new(_conflictB, _conflictA, _conflictReversedResult)
            },
        };

        #endregion Test Scenarios

        #endregion Members

        #region Methods

        #region Test Case Sources

        private static IEnumerable<TestCaseData> InvalidParameterScenarios()
        {
            foreach(var scenario in _invalidScenariosData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        private static IEnumerable<TestCaseData> MergingScenarios()
        {
            foreach(var scenario in _mergingScenariosData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        #endregion Test Case Sources

        #region Unit Tests

        [Test]
        [TestCaseSource(nameof(InvalidParameterScenarios))]
        public void Merge_InvalidParameterTests(Dictionary<string, int> self,
            Dictionary<string, int> other)
        {
            // Act and Assert - Functor must be called due to expected exception
            // throw check.
            Assert.That(() => self.Merge(other), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(nameof(MergingScenarios))]
        public void Merge_MergingTests(Dictionary<string, int> self,
            Dictionary<string, int> other, 
            IEnumerable<KeyValuePair<string,int>> expectedResults)
        {
            // Act
            var result = self.Merge(other);

            // Assert
            Assert.That(result, Is.EquivalentTo(expectedResults));
        }

        #endregion Unit Tests

        #endregion Methods
    }
}
