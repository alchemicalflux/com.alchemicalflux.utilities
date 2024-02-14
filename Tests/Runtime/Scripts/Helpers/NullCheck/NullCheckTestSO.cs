/*------------------------------------------------------------------------------
  File:           NullCheckTestSO.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Scriptable Object used for NullCheck unit testing. Contains
                    references for GameObjecct prefabs that will be tested and
                    a function the unit testing can use for access.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-13 18:35:48 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    /// <summary>
    /// Scriptable object used to hold references for NullCheck unit testing.
    /// </summary>
    [CreateAssetMenu(menuName = "AlchemicalFlux/Tests/NullCheckTestSO")]
    public class NullCheckTestSO : ScriptableObject
    {
        public GameObject EmptyObjectTest;
        public GameObject EmptyScriptTest;
        public GameObject NullCheckTest;
        public GameObject PrefabNullCheckTest;

        public GameObject Get(NullCheckTestType type)
        {
            switch (type)
            {
                case NullCheckTestType.EmptyObject: return EmptyObjectTest;
                case NullCheckTestType.EmptyScript: return EmptyScriptTest;
                case NullCheckTestType.NullCheck: return NullCheckTest;
                case NullCheckTestType.PrefabNullCheck: return PrefabNullCheckTest;
            }
            return null;
        }
    }
}