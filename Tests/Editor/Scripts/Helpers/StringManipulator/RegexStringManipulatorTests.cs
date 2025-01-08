/*------------------------------------------------------------------------------
File:       RegexStringManipulatorTests.cs 
Project:    AlchemicalFlux Utilities
Overview:   Unit tests for RegexStringManipulator and its unique cases.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 17:05:53 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    /// <summary>
    /// Implements or overrides any unique test case scenarios for the 
    /// <see cref="RegexStringManipulator"/>. class that are not covered by the 
    /// <see cref="IStringManipulatorTests{}"/>.
    /// </summary>
    public class RegexStringManipulatorTests : IStringManipulatorTests<RegexStringManipulator>
    {
        #region Members

        #region Test Values

        // Escape Characters
        protected static readonly 
            Dictionary<string, string> RegexCharactersReplacements = new()
        {
            { Regex.Escape("\\n"), "\n" },
            { Regex.Escape("\\t"), "\t" },
            { Regex.Escape("\\\\"), "\\" },
        };

        // Special Characters
        protected static readonly 
            Dictionary<string, string> RegexSpecialCharactersReplacements = 
            new()
        {
            { Regex.Escape("("), "[1]" },
            { Regex.Escape(")"), "[2]" },
            { Regex.Escape("{"), "[3]" },
            { Regex.Escape("}"), "[4]" },
        };

        // Regex Replacement values
        protected const string RegexText = 
            "Valid \"version\": \"garbage\" Valid";
        protected static readonly 
            Dictionary<string, string> RegexReplacements = new()
        {
            { "\"version\": \".*\"", "\"version\": \"0.0.0-development\""},
        };
        protected const string RegexResults = 
            "Valid \"version\": \"0.0.0-development\" Valid";

        #endregion Test Values

        #region Test Scenarios

        // Test names
        protected const string RegexReplacementsTestName = 
            "RegexReplacements_ReturnModifiedString";

        protected static readonly 
            Dictionary<string, TestCaseData> RegexReplacementScenariosData = 
            new()
        {
            {
                EscapeCharacterReplacementTestName,
                new(new RegexStringManipulator(), EscapeCharactersText, 
                    RegexCharactersReplacements, EscapeCharactersResult)
            },
            {
                SpecialCharacterReplacementsTestName,
                new(new RegexStringManipulator(), SpecialCharactersText, 
                    RegexSpecialCharactersReplacements, SpecialCharactersResult)
            },
            {
                RegexReplacementsTestName,
                new(new RegexStringManipulator(),
                    RegexText, RegexReplacements, RegexResults)
            },
        };

        #endregion Test Scenarios

        #endregion Members

        #region Methods

        #region Test Case Sources

        public static IEnumerable<TestCaseData> RegexReplacementScenarios()
        {
            var merged = RegexReplacementScenariosData
                .Merge(ReplacementScenariosData);
            foreach(var scenario in merged)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        #endregion Test Case Sources

        #region Unit Tests

        [Test]
        [TestCaseSource(nameof(RegexReplacementScenarios))]
        public override void MultipleReplace_StringReplacementTests(
            IStringManipulator stringManipulator,
            string input,
            Dictionary<string, string> replacements,
            string expectedResult)
        {
            // Act
            string result = stringManipulator.MultipleReplace(input, replacements);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #endregion Unit Tests

        #endregion Methods
    }
}
