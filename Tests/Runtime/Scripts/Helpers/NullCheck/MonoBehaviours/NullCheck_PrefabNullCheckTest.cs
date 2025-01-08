/*------------------------------------------------------------------------------
File:       NullCheck_PrefabNullCheckTest.cs 
Project:    AlchemicalFlux Utilities
Overview:   NullCheck unit test script for testing IgnorePrefab parameter.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class NullCheck_PrefabNullCheckTest : MonoBehaviour
    {
        #region Members

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private GameObject _object;

        #endregion Members
    }
}
