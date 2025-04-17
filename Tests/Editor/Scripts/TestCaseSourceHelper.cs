/*------------------------------------------------------------------------------
File:       TestCaseSourceHelper.cs 
Project:    YourProjectName  # Replace with project name
Overview:   YourOverview  # Replace with overview
Copyright:  2025 YourName/YourCompany. All rights reserved.  # Replace with copyright

Last commit by: alchemicalflux 
Last commit at: 2025-04-16 19:18:32 
------------------------------------------------------------------------------*/
using NUnit.Framework;
using System.Collections.Generic;

public class TestCaseSourceHelper
{
    public Dictionary<string, TestCaseData> ValidTests { get; private set; }

    public TestCaseSourceHelper(Dictionary<string, TestCaseData> testCases)
    {
        ValidTests = new(testCases);
    }

    public void AddOverwrite(Dictionary<string, TestCaseData> testCases)
    {
        foreach(var testCase in testCases)
        {
            ValidTests[testCase.Key] = testCase.Value;
        }
    }

    public IEnumerable<TestCaseData> GetTestCases()
    {
        foreach(var testCase in ValidTests)
        {
            yield return testCase.Value.SetName(testCase.Key);
        }
    }
}
