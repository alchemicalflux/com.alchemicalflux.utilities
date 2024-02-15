/*------------------------------------------------------------------------------
  File:           NullCheck_MultiNullCheckTest.cs 
  Project:        AlchemicalFlux Utilities
  Description:    NullCheck unit test script for testing Multiple NullChecks.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 08:00:02 
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
            private GameObject _object;

            [NullCheck(IgnorePrefab = true)]
            [SerializeField]
            private GameObject _prefab;
        }

        #endregion Definitions

        #region Members

        [NullCheck]
        [SerializeField]
        private GameObject _object;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private GameObject _prefab;

        [SerializeField]
        private List<ListType> _list;

        #endregion Members
    }
}
