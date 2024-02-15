/*------------------------------------------------------------------------------
  File:           NullCheck_MultiNullCheckTest.cs 
  Project:        AlchemicalFlux Utilities
  Description:    NullCheck unit test script for testing Multiple NullChecks.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 04:42:09 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class NullCheck_MultiNullCheckTest : MonoBehaviour
    {
        #region Definitions

        [Serializable]
        private class ListType
        {
            [NullCheck]
            [SerializeField]
            private GameObject Object;

            [NullCheck(IgnorePrefab = true)]
            [SerializeField]
            private GameObject Prefab;
        }

        #endregion Definitions

        #region Members

        [NullCheck]
        [SerializeField]
        private GameObject Object;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private GameObject Prefab;

        [SerializeField]
        private List<ListType> List;

        #endregion Members
    }
}
