/*------------------------------------------------------------------------------
  File:           NullCheck_NullCheckTest.cs 
  Project:        AlchemicalFlux Utilities
  Description:    NullCheck unit test script for testing a typical NullCheck.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 04:42:09 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class NullCheck_NullCheckTest : MonoBehaviour
    {
        #region Members

        [NullCheck]
        [SerializeField]
        private GameObject Object;

        #endregion Members
    }
}
