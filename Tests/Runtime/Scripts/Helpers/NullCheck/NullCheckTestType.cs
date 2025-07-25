/*------------------------------------------------------------------------------
File:       NullCheckTestType.cs 
Project:    AlchemicalFlux Utilities
Overview:   Contains enumeration used for GameObject accessing during NullCheck 
            unit tests.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    #region Definitions

    /// <summary>
    /// Enumeration used for GameObject accessing during NullCheck unit tests.
    /// </summary>
    public enum NullCheckTestType
    {
        EmptyObject,
        EmptyScript,
        ValueFields,
        NullCheck,
        PrefabNullCheck,
        LinkedNullCheck,
        LinkedPrefabNullCheck,
        ChildNullCheck,
        ChildPrefabNullCheck,
        LinkedChildNullCheck,
        LinkedChildPrefabNullCheck,
        MultiNullCheck,
        LinkedMultiNullCheck,
    }

    #endregion Definitions
}