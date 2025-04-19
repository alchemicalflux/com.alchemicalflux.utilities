/*------------------------------------------------------------------------------
File:       TestCaseSourceHelper.cs 
Project:    AlchemicalFlux Utilities
Overview:   Provides a helper class for managing and manipulating test case
            data used in NUnit tests. This includes adding, overwriting, and
            retrieving test cases with custom names.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-04-18 18:44:06 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;

/// <summary>
/// A helper class for managing and manipulating NUnit test case data.
/// </summary>
public class TestCaseSourceHelper
{
    /// <summary>
    /// A dictionary to store test cases with their associated names.
    /// </summary>
    private Dictionary<string, TestCaseData> _testCases;

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
    public TestCaseSourceHelper(Dictionary<string, TestCaseData> testCases)
    {
        _testCases = new(testCases);
    }

    /// <summary>
    /// Overwrites test cases in the current collection. Will add new test cases
    /// if not present, or update existing ones.
    /// </summary>
    /// <param name="testCases">
    /// A dictionary of test cases that will update the current collection.
    /// </param>
    /// <returns>
    /// The current instance of <see cref="TestCaseSourceHelper"/>.
    /// </returns>
    public TestCaseSourceHelper Overwrite(Dictionary<string, 
        TestCaseData> testCases)
    {
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
    public TestCaseSourceHelper Remove(IEnumerable<string> testCaseKeys)
    {
        foreach(var key in testCaseKeys) { _testCases.Remove(key); }
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
        foreach(var testCase in _testCases)
        {
            yield return testCase.Value.SetName(testCase.Key);
        }
    }
}
