/*------------------------------------------------------------------------------
File:       UnityEnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements the EnumFuncMap with inspector level manipulation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-19 21:35:50 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Implements the EnumFuncMap with inspector level manipulation.
    /// </summary>
    /// <typeparam name="TEnum">The enum type to be mapped.</typeparam>
    /// <typeparam name="TDelegate">The delegate type to be mapped.</typeparam>
    [Serializable]
    public class UnityEnumFuncMap<TEnum, TDelegate> :
        IUnityEnumFuncMapDrawerBase, ISerializationCallbackReceiver
        where TEnum : struct, Enum
        where TDelegate : Delegate
    {
        #region Fields

        [SerializeField] private TEnum _curEnum;
        private EnumFuncMap<TEnum, TDelegate> _map;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the current enum value.
        /// </summary>
        public TEnum Enum
        {
            get => _curEnum;
            set
            {
                _curEnum = value;
                if(_map != null) { _map.Enum = value; }
            }
        }

        /// <summary>
        /// Gets the current delegate function.
        /// </summary>
        public TDelegate Func => _map?.Func ??
            EnumFuncMap<TEnum, TDelegate>.DefaultDelegate;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Assigns the functions to the enum values.
        /// </summary>
        /// <param name="delegates">
        /// The collection of delegates to be assigned.
        /// </param>
        public void AssignFuncs(ICollection<TDelegate> delegates)
        {
            _map = new(delegates, _curEnum);
        }

        #endregion Methods

        #region ISerializationCallbackReceiver Implementation

        /// <summary>
        /// Called before the object is serialized.
        /// </summary>
        public void OnBeforeSerialize()
        {
            // No specific actions needed before serialization in this case.
        }

        /// <summary>
        /// Called after the object is deserialized.
        /// </summary>
        public void OnAfterDeserialize()
        {
            Enum = _curEnum;
        }

        #endregion ISerializationCallbackReceiver Implementation
    }
}
