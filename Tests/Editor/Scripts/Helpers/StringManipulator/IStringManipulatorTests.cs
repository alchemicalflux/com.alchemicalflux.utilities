/*------------------------------------------------------------------------------
  File:           IStringManipulatorTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests shared by implementors of IStringManipulator.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-29 20:46:10 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    /// <summary>
    /// Contains shared unit tests for implementors of <see cref="TStringManipulator"/>.
    /// </summary>
    [TestFixture]
    public abstract class IStringManipulatorTests<TStringManipulator>
        where TStringManipulator : IStringManipulator, new()
    {
        #region Members

        #region Test Values

        // Null and Empty Test values
        protected const string DefaultText = "default text";

        // Standard Replacement values
        protected const string StandardText = "Hello {FirstName}, you are from {Country}.";
        protected static readonly Dictionary<string, string> StandardReplacements = 
            new Dictionary<string, string>()
            {
                { "{FirstName}", "John" },
                { "{Country}", "USA" },
                { "Garbage", "Should Not Replace" },
                { "fasdf", null },
            };
        protected const string StandardResult = "Hello John, you are from USA.";

        // Case Sensitivty
        protected const string LowerCaseText = "hello {firstname}, you are from {country}.";
        protected const string LowerCaseResult = "hello {firstname}, you are from {country}.";

        protected const string UpperCaseText = "HELLO {FIRSTNAME}, YOU ARE FROM {COUNTRY}.";
        protected const string UpperCaseResult = "HELLO {FIRSTNAME}, YOU ARE FROM {COUNTRY}.";

        // Longest Key First values
        protected const string LongestKeyFirstText = "The quick brown fox jumps over the lazy dog.";
        protected static readonly Dictionary<string, string> LongestKeyFirstReplacements = 
            new Dictionary<string, string>()
            {
                { "the lazy", "the energetic" },
                { "the lazy ", "the scared " },

                { "fox jumps", "cat leaps"},
                { "fox jumps over", "squirrel scampers above"},
                { "jumps over", "hops across"},
            };
        protected const string LongestKeyFirstResult = "The quick brown squirrel scampers above the scared dog.";

        // Escape Characters
        protected const string EscapeCharactersText = "Replace newline: \\n, tab: \\t, and backslash: \\\\";
        protected static readonly Dictionary<string, string> EscapeCharactersReplacements = 
            new Dictionary<string, string>()
            {
                { "\\n", "\n" },
                { "\\t", "\t" },
                { "\\\\", "\\" },
            };
        protected const string EscapeCharactersResult = "Replace newline: \n, tab: \t, and backslash: \\";

        // Special Characters
        protected const string SpecialCharactersText = "Replace ( { and } ) with special characters";
        protected static readonly Dictionary<string, string> SpecialCharactersReplacements =
            new Dictionary<string, string>
            {
                { "(", "[1]" },
                { ")", "[2]" },
                { "{", "[3]" },
                { "}", "[4]" },
            };
        protected const string SpecialCharactersResult = "Replace [1] [3] and [4] [2] with special characters";

        #endregion Test Values

        #region Test Scenarios

        // Invalid Scenario Tests

        protected const string InitialTextNullTestName = "InitialTextIsNull_ThrowArguementNullException";
        protected const string ReplacementsIsNullTestName = "ReplacementsIsNull_ThrowArguementNullException";

        protected static readonly Dictionary<string, TestCaseData> InvalidScenariosData =
            new Dictionary<string, TestCaseData>()
            {
                {
                    InitialTextNullTestName,
                    new TestCaseData(new TStringManipulator(), null, StandardReplacements)
                },
                {
                    ReplacementsIsNullTestName,
                    new TestCaseData(new TStringManipulator(), DefaultText, null)
                }
            };

        // Replacement Scenario Tests

        protected const string EmptyInitialStringTestName = "EmptyInitialString_ReturnEmptyString";
        protected const string EmptyReplacementsTestName = "EmptyReplacements_ReturnSameString";
        protected const string SimpleReplacementsTestName = "SimpleReplacements_ReturnModifiedString";
        protected const string LowerCaseSensitivityTestName = "LowerCaseSensitivity_ReturnSameString";
        protected const string UpperCaseSensitivityTestName = "UpperCaseSensitivity_ReturnSameString";
        protected const string OverlappingReplacementsTestName = "OverlappingReplacements_ReturnStringModifiedByLongestFirst";
        protected const string EscapeCharacterReplacementTestName = "EscapeCharacterReplacements_ReturnModifiedString";
        protected const string SpecialCharacterReplacementsTestName = "SpecialCharacterReplacements_ReturnModifiedString";

        protected static readonly Dictionary<string, TestCaseData> ReplacementScenariosData =
            new Dictionary<string, TestCaseData>()
            {
                {
                    EmptyInitialStringTestName,
                    new TestCaseData(new TStringManipulator(), 
                        string.Empty, StandardReplacements, string.Empty)
                },
                {
                    EmptyReplacementsTestName,
                    new TestCaseData(new TStringManipulator(), 
                        DefaultText, new Dictionary<string, string>(), DefaultText)
                },
                {
                    SimpleReplacementsTestName,
                    new TestCaseData(new TStringManipulator(), 
                        StandardText, StandardReplacements, StandardResult)
                },
                {
                    LowerCaseSensitivityTestName,
                    new TestCaseData(new TStringManipulator(),
                        LowerCaseText, StandardReplacements, LowerCaseResult)
                },
                {
                    UpperCaseSensitivityTestName,
                    new TestCaseData(new TStringManipulator(), 
                        UpperCaseText, StandardReplacements, UpperCaseResult)
                },
                {
                    OverlappingReplacementsTestName,
                    new TestCaseData(new TStringManipulator(), 
                        LongestKeyFirstText, LongestKeyFirstReplacements, LongestKeyFirstResult)
                },
                {
                    EscapeCharacterReplacementTestName,
                    new TestCaseData(new TStringManipulator(), 
                        EscapeCharactersText, EscapeCharactersReplacements, EscapeCharactersResult)
                },
                {
                    SpecialCharacterReplacementsTestName,
                    new TestCaseData(new TStringManipulator(), 
                        SpecialCharactersText, SpecialCharactersReplacements, SpecialCharactersResult)
                },
            };

        #endregion Test Scenarios

        #endregion Members

        #region Methods

        #region Test Case Sources

        public static IEnumerable<TestCaseData> InvalidScenarios()
        {
            foreach(var scenario in InvalidScenariosData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        public static IEnumerable<TestCaseData> ReplacementScenarios()
        {
            foreach(var scenario in ReplacementScenariosData)
            {
                yield return scenario.Value.SetName(scenario.Key);
            }
        }

        #endregion Test Case Sources

        #region Unit Tests

        [Test]
        [TestCaseSource(nameof(InvalidScenarios))]
        public virtual void MultipleReplace_InvalidParameterTests(IStringManipulator stringManipulator,
            string input,
            Dictionary<string, string> replacements)
        {
            // Assert
            Assert.That(() => stringManipulator.MultipleReplace(input, replacements), 
                Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(nameof(ReplacementScenarios))]
        public virtual void MultipleReplace_StringReplacementTests(IStringManipulator stringManipulator,
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