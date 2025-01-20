/*------------------------------------------------------------------------------
File:       UnityEnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements the EnumFuncMap with inspector level manipulation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 14:48:23 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    [Serializable]
    public class UnityEnumFuncMap<TEnum, TDelegate> : 
        UnityEnumFuncMapDrawerBase, ISerializationCallbackReceiver
        where TEnum : struct, Enum
        where TDelegate : Delegate
    {
        [SerializeField] private TEnum _curEnum;
        private EnumFuncMap<TEnum, TDelegate> _map;

        public TEnum Enum
        {
            get => _curEnum;
            set
            {
                _curEnum = value;
                if(_map != null) { _map.Enum = _curEnum = value; }
            }
        }

        public TDelegate Func => _map.Func;

        public void AssignFuncs(ICollection<TDelegate> delegates)
        {
            _map = new(delegates, _curEnum);
        }

        public void OnBeforeSerialize()
        {
        }

        public void OnAfterDeserialize()
        {
            Enum = _curEnum;
        }
    }
}