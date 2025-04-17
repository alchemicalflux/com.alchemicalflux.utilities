/*------------------------------------------------------------------------------
File:       IInterpolatorTests.cs 
Project:    YourProjectName  # Replace with project name
Overview:   YourOverview  # Replace with overview
Copyright:  2025 YourName/YourCompany. All rights reserved.  # Replace with copyright

Last commit by: alchemicalflux 
Last commit at: 2025-04-16 19:18:32 
------------------------------------------------------------------------------*/

public interface IInterpolatorTests<TType>
{
    public void InterpolatorTests_ValidProgress_ReturnsExpectedValue(
        float progress, TType expectedValue);
}
