/*------------------------------------------------------------------------------
  File:           NullCheckTestSO.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Scriptable Object used for NullCheck unit testing. Contains
                    references for GameObjecct prefabs that will be tested and
                    a function the unit testing can use for access.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 08:00:02 
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
        #region Members

        public GameObject EmptyObjectTest;
        public GameObject EmptyScriptTest;
        public GameObject ValueFieldsTest;
        public GameObject NullCheckTest;
        public GameObject PrefabNullCheckTest;
        public GameObject LinkedNullCheckTest;
        public GameObject LinkedPrefabNullCheckTest;
        public GameObject ChildNullCheckTest;
        public GameObject ChildPrefabNullCheckTest;
        public GameObject LinkedChildNullCheckTest;
        public GameObject LinkedChildPrefabNullCheckTest;
        public GameObject MultiNullCheckTest;
        public GameObject LinkedMultiNullCheckTest;

        #endregion Members

        #region Methods

        /// <summary>
        /// Resturns the apporiate prefab reference based on the requested type.
        /// </summary>
        /// <param name="type">Requesting game object type.</param>
        /// <returns>Reference to a GameObject.</returns>
        public GameObject Get(NullCheckTestType type)
        {
            switch (type)
            {
                case NullCheckTestType.EmptyObject: return EmptyObjectTest;
                case NullCheckTestType.EmptyScript: return EmptyScriptTest;
                case NullCheckTestType.ValueFields: return ValueFieldsTest;
                case NullCheckTestType.NullCheck: return NullCheckTest;
                case NullCheckTestType.PrefabNullCheck: return PrefabNullCheckTest;
                case NullCheckTestType.LinkedNullCheck: return LinkedNullCheckTest;
                case NullCheckTestType.LinkedPrefabNullCheck: return LinkedPrefabNullCheckTest;
                case NullCheckTestType.ChildNullCheck: return ChildNullCheckTest;
                case NullCheckTestType.ChildPrefabNullCheck: return ChildPrefabNullCheckTest;
                case NullCheckTestType.LinkedChildNullCheck: return LinkedChildNullCheckTest;
                case NullCheckTestType.LinkedChildPrefabNullCheck: return LinkedChildPrefabNullCheckTest;
                case NullCheckTestType.MultiNullCheck: return MultiNullCheckTest;
                case NullCheckTestType.LinkedMultiNullCheck: return LinkedMultiNullCheckTest;
            }
            return null;
        }

        #endregion Methods
    }
}