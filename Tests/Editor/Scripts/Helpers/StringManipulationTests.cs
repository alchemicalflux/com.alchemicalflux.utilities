/*------------------------------------------------------------------------------
  File:           StringManipulationTests.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Unit tests for String Manipulation functions.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-01 22:20:10 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class StringManipulationTests
    {
        [Test]
        public void MultipleReplace_ReplacesMultipleKeysWithValues_ReturnsReplacedString()
        {
            // Arrange
            string input = "Hello [FirstName], you are from [Country].";
            var replacements = new Dictionary<string, string>
            {
                { "[FirstName]", "John" },
                { "[Country]", "USA" }
            };

            // Act
            string result = StringManipulation.MultipleReplace(input, replacements);

            // Assert
            Assert.AreEqual("Hello John, you are from USA.", result);
        }

        [Test]
        public void MultipleReplace_NoMatchingKeys_ReturnsUnchangedString()
        {
            // Arrange
            string input = "Hello [FirstName], you are from [Country].";
            var replacements = new Dictionary<string, string>
            {
                { "[Name]", "John" },
                { "[Nation]", "USA" }
            };

            // Act
            string result = StringManipulation.MultipleReplace(input, replacements);

            // Assert
            Assert.AreEqual(input, result);
        }

        [Test]
        public void RegexMultipleReplace_ReplacesMultipleKeysWithValues_ReturnsReplacedString()
        {
            // Arrange
            string input = "Hello {FirstName}, you are from {Country}.";
            var replacements = new Dictionary<string, string>
            {
                { "{FirstName}", "John" },
                { "{Country}", "USA" }
            };

            // Act
            string result = StringManipulation.RegexMultipleReplace(input, replacements);

            // Assert
            Assert.AreEqual("Hello John, you are from USA.", result);
        }

        [Test]
        public void RegexMultipleReplace_UsingRegexPattern_ReturnsReplacedString()
        {
            // Arrange
            string input = "Hello {FirstName}, you are from {Country}.";
            var replacements = new Dictionary<string, string>
            {
                { @"\{FirstName\}", "John" },
                { @"\{Country\}", "USA" }
            };

            // Act
            string result = StringManipulation.RegexMultipleReplace(input, replacements);

            // Assert
            Assert.AreEqual("Hello John, you are from USA.", result);
        }

        [Test]
        public void RegexMultipleReplace_NoMatchingKeys_ReturnsUnchangedString()
        {
            // Arrange
            string input = "Hello {FirstName}, you are from {Country}.";
            var replacements = new Dictionary<string, string>
            {
                { "{Name}", "John" },
                { "{Nation}", "USA" }
            };

            // Act
            string result = StringManipulation.RegexMultipleReplace(input, replacements);

            // Assert
            Assert.AreEqual(input, result);
        }

        [Test]
        public void RegexMultipleReplace_OverlappingKeys_ReturnLongestReplacementString()
        {
            // Arrange
            string input = "The quick brown fox jumps over the lazy dog.";
            var replacements = new Dictionary<string, string>
            {
                { "fox jumps", "cat leaps"},
                { "jumps over", "hops across"},
                { "fox jumps over", "squirrel scampers above"}
            };

            // Act
            string result = StringManipulation.RegexMultipleReplace(input, replacements);

            // Assert
            Assert.AreEqual("The quick brown squirrel scampers above the lazy dog.", result);
        }
    }
}