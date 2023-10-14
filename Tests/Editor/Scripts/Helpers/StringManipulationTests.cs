/*------------------------------------------------------------------------------
  File:           StringManipulationTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for String Manipulation functions.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-12 01:13:52 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class StringManipulationTests
    {
        #region Members

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
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    null,
                    standardReplacements
                ).SetName("MultipleReplace_InitialTextIsNull_ThrowArguementNullException");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    null,
                    standardReplacements
                ).SetName("RegexMultipleReplace_InitialTextIsNull_ThrowArguementNullException");

            // Null Replacements
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    defaultText,
                    null
                ).SetName("MultipleReplace_ReplacementsIsNull_ThrowArguementNullException");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    defaultText,
                    null
                ).SetName("RegexMultipleReplace_ReplacementsIsNull_ThrowArguementNullException");
        }

        public static IEnumerable<TestCaseData> ReplacementScenerios()
        {
            // Empty Initial String
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    string.Empty,
                    standardReplacements,
                    string.Empty
                ).SetName("MultipleReplace_EmptyInitialString_ReturnEmptyString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    string.Empty,
                    standardReplacements,
                    string.Empty
                ).SetName("RegexMultipleReplace_EmptyInitialString_ReturnEmptyString");

            // Empty Replacements
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    defaultText,
                    new Dictionary<string, string> { },
                    defaultText
                ).SetName("MultipleReplace_EmptyReplacements_ReturnSameString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    defaultText,
                    new Dictionary<string, string> { },
                    defaultText
                ).SetName("RegexMultipleReplace_EmptyReplacements_ReturnSameString");

            // Replace All Values
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    standardText,
                    standardReplacements,
                    standardResult
                ).SetName("MultipleReplace_SimpleReplacements_ReturnModifiedString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    standardText,
                    standardReplacements,
                    standardResult
                ).SetName("RegexMultipleReplace_SimpleReplacements_ReturnModifiedString");

            // Lower Case Values
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    lowerCaseText,
                    standardReplacements,
                    lowerCaseResult
                ).SetName("MultipleReplace_LowerCaseSensitivity_ReturnSameString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    lowerCaseText,
                    standardReplacements,
                    lowerCaseResult
                ).SetName("RegexMultipleReplace_LowerCaseSensitivity_ReturnSameString");

            // Upper Case Values
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    upperCaseText,
                    standardReplacements,
                    upperCaseResult
                ).SetName("MultipleReplace_UpperCaseSensitivity_ReturnSameString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    upperCaseText,
                    standardReplacements,
                    upperCaseResult
                ).SetName("RegexMultipleReplace_UpperCaseSensitivity_ReturnSameString");

            // Replace Longest Keys First
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    longestKeyFirstText,
                    longestKeyFirstReplacements,
                    longestKeyFirstResult
                ).SetName("MultipleReplace_OverlappingReplacements_ReturnStringModifiedByLongestFirst");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    longestKeyFirstText,
                    longestKeyFirstReplacements,
                    longestKeyFirstResult
                ).SetName("RegexMultipleReplace_OverlappingReplacements_ReturnStringModifiedByLongestFirst");

            // Replace Escape Characters
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    escapeCharactersText,
                    escapeCharactersReplacements,
                    escapeCharactersResult
                ).SetName("MultipleReplace_EscapeCharacterReplacements_ReturnModifiedString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    escapeCharactersText,
                    escapeCharactersReplacements,
                    escapeCharactersResult
                ).SetName("RegexMultipleReplace_EscapeCharacterReplacements_ReturnModifiedString");

            // Special Characters
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.MultipleReplace,
                    specialCharactersText,
                    specialCharactersReplacements,
                    specialCharactersResult
                ).SetName("MultipleReplace_SpecialCharacterReplacements_ReturnModifiedString");
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    specialCharactersText,
                    regexSpecialCharactersReplacements,
                    specialCharactersResult
                ).SetName("RegexMultipleReplace_SpecialCharacterReplacements_ReturnModifiedString");

            // Regex Replacement Test
            yield return new TestCaseData(
                    (Func<string, Dictionary<string, string>, string>)StringManipulation.RegexMultipleReplace,
                    regexText,
                    regexReplacements,
                    regexResults
                ).SetName("RegexMultipleReplace_RegexReplacements_ReturnModifiedString");
        }

        #endregion Test Scenerios

        #endregion Members

        #region Methods

        [Test]
        [TestCaseSource(nameof(InvalidScenerios))]
        public void InvalidParameterTests(Func<string, 
            Dictionary<string, string>, string> stringReplaceMethod, string input, 
            Dictionary<string, string> replacements)
        {
            // Assert
            Assert.That(() => stringReplaceMethod(input, replacements), Throws.ArgumentNullException);
        }

        [Test]
        [TestCaseSource(nameof(ReplacementScenerios))]
        public void StringReplacementTests(Func<string, 
            Dictionary<string, string>, string> stringReplaceMethod, string input, 
            Dictionary<string, string> replacements, string expectedResult)
        {
            // Act
            string result = stringReplaceMethod(input, replacements);

            // Assert
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        #endregion Methods
    }
}