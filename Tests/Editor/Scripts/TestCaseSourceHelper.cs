/*------------------------------------------------------------------------------
File:       TestCaseSourceHelper.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a helper class for managing and manipulating test case
            data used in NUnit tests. This includes adding, overwriting, and
            retrieving test cases with custom names.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-24 19:30:32 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities
{
    /// <summary>
    /// Provides a helper class for managing and manipulating test case data
    /// used in NUnit tests.
    /// </summary>
    /// <remarks>
    /// This class includes methods for adding, overwriting, and retrieving
    /// test cases with custom names.
    /// </remarks>
    /// <summary>
    /// A helper class for managing and manipulating NUnit test case data.
    /// </summary>
    public class TestCaseSourceHelper
    {
        /// <summary>
        /// A dictionary to store test cases with their associated names.
        /// </summary>
        private readonly Dictionary<string, TestCaseData> _testCases;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseSourceHelper"/>
        /// class with an empty collection of test cases.
        /// </summary>
        public TestCaseSourceHelper()
        {
            _testCases = new();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestCaseSourceHelper"/>
        /// class with a predefined collection of test cases.
        /// </summary>
        /// <param name="testCases">
        /// A dictionary of test cases to initialize with.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="testCases"/> is null.
        /// </exception>
        public TestCaseSourceHelper(Dictionary<string, TestCaseData> testCases)
        {
            _testCases = testCases ??
                throw new ArgumentNullException(nameof(testCases));
        }

        /// <summary>
        /// Overwrites test cases in the current collection. Will add new test 
        /// cases if not present, or update existing ones.
        /// </summary>
        /// <param name="testCases">
        /// A dictionary of test cases that will update the current collection.
        /// </param>
        /// <returns>
        /// The current instance of <see cref="TestCaseSourceHelper"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="testCases"/> is null.
        /// </exception>
        public TestCaseSourceHelper Overwrite(
            Dictionary<string, TestCaseData> testCases)
        {
            if(testCases == null)
            {
                throw new ArgumentNullException(nameof(testCases));
            }

            foreach(var testCase in testCases)
            {
                _testCases[testCase.Key] = testCase.Value;
            }
            return this;
        }

        /// <summary>
        /// Removes test cases from the current collection based on their keys.
        /// </summary>
        /// <param name="testCaseKeys">
        /// A collection of keys identifying the test cases to remove.
        /// </param>
        /// <returns>
        /// The current instance of <see cref="TestCaseSourceHelper"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="testCaseKeys"/> is null.
        /// </exception>
        public TestCaseSourceHelper Remove(IEnumerable<string> testCaseKeys)
        {
            if(testCaseKeys == null)
            {
                throw new ArgumentNullException(nameof(testCaseKeys));
            }

            foreach(var key in testCaseKeys)
            {
                _testCases.Remove(key);
            }
            return this;
        }

        /// <summary>
        /// Retrieves all test cases in the current collection as an enumerable.
        /// Each test case is assigned a custom name based on its key.
        /// </summary>
        /// <returns>
        /// An enumerable of <see cref="TestCaseData"/> with custom names.
        /// </returns>
        public IEnumerable<TestCaseData> GetTestCases()
        {
            if(_testCases.Count == 0)
            {
                yield return new TestCaseData(0f)
                    .SetName("Placeholder")
                    .SetDescription(
                        "This is a placeholder test case because no cases " +
                        "are currently possible.");
                yield break;
            }

            foreach(var testCase in _testCases)
            {
                yield return testCase.Value.SetName(testCase.Key);
            }
        }

        /// <summary>
        /// Ignores the test execution if no test cases are available.
        /// </summary>
        /// <exception cref="IgnoreException">
        /// Thrown to indicate that the test should be ignored.
        /// </exception>
        public void IgnoreIfNoTestCases()
        {
            if(_testCases.Count == 0)
            {
                Assert.Pass("No invalid progress test cases available.");
            }
        }

        /// <summary>
        /// Checks if a test case with the specified key exists.
        /// </summary>
        /// <param name="key">The key of the test case to check.</param>
        /// <returns>True if the test case exists; otherwise, false.</returns>
        public bool ContainsTestCase(string key)
        {
            return _testCases.ContainsKey(key);
        }
    }
}