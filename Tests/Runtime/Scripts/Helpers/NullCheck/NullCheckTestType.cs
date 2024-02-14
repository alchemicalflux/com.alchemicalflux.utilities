/*------------------------------------------------------------------------------
  File:           NullCheckTestType.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains enumeration used for GameObject accessing during 
                    NullCheck unit tests.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-13 18:35:48 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    /// <summary>
    /// Enumeration used for GameObject accessing during NullCheck unit tests.
    /// </summary>
    public enum NullCheckTestType
    {
        EmptyObject,
        EmptyScript,
        NullCheck,
        PrefabNullCheck,
    }
}