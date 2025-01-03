/*------------------------------------------------------------------------------
  File:           NullCheck_PrefabNullCheckTest.cs 
  Project:        AlchemicalFlux Utilities
  Description:    NullCheck unit test script for testing IgnorePrefab parameter.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-30 22:23:47 
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
