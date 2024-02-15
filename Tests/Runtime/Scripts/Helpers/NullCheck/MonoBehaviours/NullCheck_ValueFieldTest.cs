/*------------------------------------------------------------------------------
  File:           NullCheck_ValueFieldTest.cs 
  Project:        AlchemicalFlux Utilities
  Description:    NullCheck unit test script for testing NullCheck on value 
                    fields.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-15 04:50:47 
------------------------------------------------------------------------------*/
using System;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers.Tests
{
    public class NullCheck_ValueFieldTest : MonoBehaviour
    {
        #region Definitions

        enum EnumTest { };

        [Serializable]
        public struct StructTest 
        {
            [NullCheck]
            [SerializeField]
            public int Int;

            [NullCheck(IgnorePrefab = true)]
            [SerializeField]
            public int PrefabInt;
        };

        #endregion Definitions

        #region Members

        [NullCheck]
        [SerializeField]
        private EnumTest Enum;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private EnumTest PrefabEnum;

        [NullCheck]
        [SerializeField]
        private bool Bool;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private bool PrefabBool;

        [NullCheck]
        [SerializeField]
        private int Int;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private int PrefabInt;

        [NullCheck]
        [SerializeField]
        private float Float;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private float PrefabFloat;

        [NullCheck]
        [SerializeField]
        private double Double;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private double PrefabDouble;

        [NullCheck]
        [SerializeField]
        private StructTest Struct;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private StructTest PrefabStruct;

        #endregion Members
    }
}