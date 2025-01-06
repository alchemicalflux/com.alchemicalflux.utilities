/*------------------------------------------------------------------------------
File:       NullCheckTestSO.cs 
Project:    AlchemicalFlux Utilities
Overview:   Scriptable Object used for NullCheck unit testing. Contains
            references for GameObjecct prefabs that will be tested and a 
            function the unit testing can use for access.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
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
            return type switch
            {
                NullCheckTestType.EmptyObject => EmptyObjectTest,
                NullCheckTestType.EmptyScript => EmptyScriptTest,
                NullCheckTestType.ValueFields => ValueFieldsTest,
                NullCheckTestType.NullCheck => NullCheckTest,
                NullCheckTestType.PrefabNullCheck => PrefabNullCheckTest,
                NullCheckTestType.LinkedNullCheck => LinkedNullCheckTest,
                NullCheckTestType.LinkedPrefabNullCheck => LinkedPrefabNullCheckTest,
                NullCheckTestType.ChildNullCheck => ChildNullCheckTest,
                NullCheckTestType.ChildPrefabNullCheck => ChildPrefabNullCheckTest,
                NullCheckTestType.LinkedChildNullCheck => LinkedChildNullCheckTest,
                NullCheckTestType.LinkedChildPrefabNullCheck => LinkedChildPrefabNullCheckTest,
                NullCheckTestType.MultiNullCheck => MultiNullCheckTest,
                NullCheckTestType.LinkedMultiNullCheck => LinkedMultiNullCheckTest,
                _ => null,
            };
        }

        #endregion Methods
    }
}