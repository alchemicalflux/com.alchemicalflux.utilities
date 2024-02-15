/*------------------------------------------------------------------------------
  File:           NullCheckTestType.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains enumeration used for GameObject accessing during 
                    NullCheck unit tests.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 02:52:43 
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
        LinkedNullCheck,
        LinkedPrefabNullCheck,
        ChildNullCheck,
        ChildPrefabNullCheck,
        LinkedChildNullCheck,
        LinkedChildPrefabNullCheck,
    }
}