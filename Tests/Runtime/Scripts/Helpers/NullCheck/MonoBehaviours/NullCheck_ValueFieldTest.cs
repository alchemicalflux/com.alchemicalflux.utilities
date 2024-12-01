/*------------------------------------------------------------------------------
  File:           NullCheck_ValueFieldTest.cs 
  Project:        AlchemicalFlux Utilities
  Description:    NullCheck unit test script for testing NullCheck on value 
                    fields.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-11-30 22:23:47 
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
            private int _int;

            [NullCheck(IgnorePrefab = true)]
            [SerializeField]
            private int _prefabInt;
        };

        #endregion Definitions

        #region Members

        [NullCheck]
        [SerializeField]
        private EnumTest _enum;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private EnumTest _prefabEnum;

        [NullCheck]
        [SerializeField]
        private bool _bool;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private bool _prefabBool;

        [NullCheck]
        [SerializeField]
        private int _int;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private int _prefabInt;

        [NullCheck]
        [SerializeField]
        private float _float;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private float _prefabFloat;

        [NullCheck]
        [SerializeField]
        private double _double;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private double _prefabDouble;

        [NullCheck]
        [SerializeField]
        private StructTest _struct;

        [NullCheck(IgnorePrefab = true)]
        [SerializeField]
        private StructTest _prefabStruct;

        #endregion Members
    }
}