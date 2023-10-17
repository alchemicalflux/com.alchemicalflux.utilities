/*------------------------------------------------------------------------------
  File:           IStringManipulator_MultipleReplaceTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for IStringManipulator MultiplerReplace function.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-17 13:43:08 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    /// <summary>
    /// Contains the unit tests regarding the IStringManipulator MultipleReplace.
    /// </summary>
    public class IStringManipulator_MultipleReplaceTests
    {
        #region Members

        #region Test Class Names

        private const string basicManip = "BasicStringManipulator";
        private const string regexManip = "RegexStringManipulator";

        #endregion Test Class Names

        #region Test Values

        // Null and Empty Test values
        private const string defaultText = "default text";

        // Standard Replacement values
        private const string standardText = "Hello {FirstName}, you are from {Country}.";
        private static Dictionary<string, string> standardReplacements = new Dictionary<string, string>()
        {
            { "{FirstName}", "John" },
            { "{Country}", "USA" },
            { "Garbage", "Should Not Replace" },
            { "fasdf", null },
        };
        private const string standardResult = "Hello John, you are from USA.";

        // Case Sensitivty
        private const string lowerCaseText = "hello {firstname}, you are from {country}.";
        private const string lowerCaseResult = "hello {firstname}, you are from {country}.";

        private const string upperCaseText = "HELLO {FIRSTNAME}, YOU ARE FROM {COUNTRY}.";
        private const string upperCaseResult = "HELLO {FIRSTNAME}, YOU ARE FROM {COUNTRY}.";

        // Replace Longest Key First values
        private const string longestKeyFirstText = "The quick brown fox jumps over the lazy dog.";
        private static Dictionary<string, string> longestKeyFirstReplacements = new Dictionary<string, string>()
        {
            { "the lazy", "the energetic" },
            { "the lazy ", "the scared " },

            { "fox jumps", "cat leaps"},
            { "jumps over", "hops across"},
            { "fox jumps over", "squirrel scampers above"}
        };
        private const string longestKeyFirstResult = "The quick brown squirrel scampers above the scared dog.";

        // Replace Escape Characters
        private const string escapeCharactersText = "Replace newline: \\n, tab: \\t, and backslash: \\\\";
        private static Dictionary<string, string> escapeCharactersReplacements = new Dictionary<string, string>()
        {
            // MultipleReplace options, as the underlying code will perform a Regex.Escape for direct replacements.
            { "\\n", "\n" },
            { "\\t", "\t" },
            { "\\\\", "\\" },
            // RegexMultipleReplace options, as the search pattern must catch the Regex.Escape entries while not
            //  breaking the adaptable searching the function provides.
            { Regex.Escape("\\n"), "\n" },
            { Regex.Escape("\\t"), "\t" },
            { Regex.Escape("\\\\"), "\\" },
        };
        private const string escapeCharactersResult = "Replace newline: \n, tab: \t, and backslash: \\";

        // Special Characters
        private const string specialCharactersText = "Replace ( { and } ) with special characters";
        private static Dictionary<string, string> specialCharactersReplacements = new Dictionary<string, string>
        {
            { "(", "[1]" },
            { ")", "[2]" },
            { "{", "[3]" },
            { "}", "[4]" },
        };
        private static Dictionary<string, string> regexSpecialCharactersReplacements = new Dictionary<string, string>
        {
            { Regex.Escape("("), "[1]" },
            { Regex.Escape(")"), "[2]" },
            { Regex.Escape("{"), "[3]" },
            { Regex.Escape("}"), "[4]" },
        };
        private const string specialCharactersResult = "Replace [1] [3] and [4] [2] with special characters";

        // Regex Replacement values
        private const string regexText = "Valid \"version\": \"garbage\" Valid";
        private static Dictionary<string, string> regexReplacements = new Dictionary<string, string>()
        {
            { "\"version\": \".*\"", "\"version\": \"0.0.0-development\""},
        };
        private const string regexResults = "Valid \"version\": \"0.0.0-development\" Valid";

        #endregion Test Values

        #region Test Scenerios

        public static IEnumerable<TestCaseData> InvalidScenerios()
        {
            // Null Initial Text
            yield return new TestCaseData(new BasicStringManipulator(),
                    null,
                    standardReplacements
                ).SetName(basicManip + "_InitialTextIsNull_ThrowArguementNullException");
            yield return new TestCaseData(new RegexStringManipulator(),
                    null,
                    standardReplacements
                ).SetName(regexManip + "_InitialTextIsNull_ThrowArguementNullException");

            // Null Replacements
            yield return new TestCaseData(new BasicStringManipulator(),
                    defaultText,
                    null
                ).SetName(basicManip + "_ReplacementsIsNull_ThrowArguementNullException");
            yield return new TestCaseData(new RegexStringManipulator(),
                    defaultText,
                    null
                ).SetName(regexManip + "_ReplacementsIsNull_ThrowArguementNullException");
        }

        public static IEnumerable<TestCaseData> ReplacementScenerios()
        {
            // Empty Initial String
            yield return new TestCaseData(new BasicStringManipulator(),
                    string.Empty,
                    standardReplacements,
                    string.Empty
                ).SetName(basicManip + "_EmptyInitialString_ReturnEmptyString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    string.Empty,
                    standardReplacements,
                    string.Empty
                ).SetName(regexManip + "_EmptyInitialString_ReturnEmptyString");

            // Empty Replacements
            yield return new TestCaseData(new BasicStringManipulator(),
                    defaultText,
                    new Dictionary<string, string> { },
                    defaultText
                ).SetName(basicManip + "_EmptyReplacements_ReturnSameString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    defaultText,
                    new Dictionary<string, string> { },
                    defaultText
                ).SetName(regexManip + "_EmptyReplacements_ReturnSameString");

            // Replace All Values
            yield return new TestCaseData(new BasicStringManipulator(),
                    standardText,
                    standardReplacements,
                    standardResult
                ).SetName(basicManip + "_SimpleReplacements_ReturnModifiedString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    standardText,
                    standardReplacements,
                    standardResult
                ).SetName(regexManip + "_SimpleReplacements_ReturnModifiedString");

            // Do Not Replace Lower Case Values
            yield return new TestCaseData(new BasicStringManipulator(),
                    lowerCaseText,
                    standardReplacements,
                    lowerCaseResult
                ).SetName(basicManip + "_LowerCaseSensitivity_ReturnSameString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    lowerCaseText,
                    standardReplacements,
                    lowerCaseResult
                ).SetName(regexManip + "_LowerCaseSensitivity_ReturnSameString");

            // Do Not Replace Upper Case Values
            yield return new TestCaseData(new BasicStringManipulator(),
                    upperCaseText,
                    standardReplacements,
                    upperCaseResult
                ).SetName(basicManip + "_UpperCaseSensitivity_ReturnSameString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    upperCaseText,
                    standardReplacements,
                    upperCaseResult
                ).SetName(regexManip + "_UpperCaseSensitivity_ReturnSameString");

            // Replace Longest Keys First
            yield return new TestCaseData(new BasicStringManipulator(),
                    longestKeyFirstText,
                    longestKeyFirstReplacements,
                    longestKeyFirstResult
                ).SetName(basicManip + "_OverlappingReplacements_ReturnStringModifiedByLongestFirst");
            yield return new TestCaseData(new RegexStringManipulator(),
                    longestKeyFirstText,
                    longestKeyFirstReplacements,
                    longestKeyFirstResult
                ).SetName(regexManip + "_OverlappingReplacements_ReturnStringModifiedByLongestFirst");

            // Replace Escape Characters
            yield return new TestCaseData(new BasicStringManipulator(),
                    escapeCharactersText,
                    escapeCharactersReplacements,
                    escapeCharactersResult
                ).SetName(basicManip + "_EscapeCharacterReplacements_ReturnModifiedString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    escapeCharactersText,
                    escapeCharactersReplacements,
                    escapeCharactersResult
                ).SetName(regexManip + "_EscapeCharacterReplacements_ReturnModifiedString");

            // Replace Special Characters
            yield return new TestCaseData(new BasicStringManipulator(),
                    specialCharactersText,
                    specialCharactersReplacements,
                    specialCharactersResult
                ).SetName(basicManip + "_SpecialCharacterReplacements_ReturnModifiedString");
            yield return new TestCaseData(new RegexStringManipulator(),
                    specialCharactersText,
                    regexSpecialCharactersReplacements,
                    specialCharactersResult
                ).SetName(regexManip + "_SpecialCharacterReplacements_ReturnModifiedString");

            // Replace with Regex Formatting
            yield return new TestCaseData(new RegexStringManipulator(),
                    regexText,
                    regexReplacements,
                    regexResults
                ).SetName(regexManip + "_RegexReplacements_ReturnModifiedString");
        }

        #endregion Test Scenerios

        #endregion Members

        #region Methods

        [Test]
        [TestCaseSource(nameof(InvalidScenerios))]
        public void InvalidParameterTests(IStringManipulator stringManipulator, 
            string input, 
            Dictionary<string, string> replacements)
        {
            // Assert
            Assert.That(() => stringManipulator.MultipleReplace(input, replacements), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(nameof(ReplacementScenerios))]
        public void StringReplacementTests(IStringManipulator stringManipulator,
            string input, 
            Dictionary<string, string> replacements, 
            string expectedResult)
        {
            // Act
            string result = stringManipulator.MultipleReplace(input, replacements);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #endregion Methods
    }
}