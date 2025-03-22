/*------------------------------------------------------------------------------
File:       UnityEnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements the EnumFuncMap with inspector level manipulation.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-22 14:41:15 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
        /// Gets the current delegate function, falling back to default if null.
        /// </summary>
        public TDelegate Func => _map?.Func ?? DefaultDelegate;

        /// <summary>
        /// Gets the default delegate function.
        /// </summary>
        private static TDelegate DefaultDelegate { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Static constructor to initialize the default delegate.
        /// </summary>
        static UnityEnumFuncMap()
        {
            DefaultDelegate = CreateDefaultDelegate();
        }

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

        /// <summary>
        /// Creates a default delegate that logs a warning message and returns a
        /// default value for the TDelegate type.
        /// </summary>
        /// <returns>The default delegate function.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the delegate type is invalid.
        /// </exception>
        private static TDelegate CreateDefaultDelegate()
        {
            var delegateType = typeof(TDelegate);
            var invokeMethod = delegateType.GetMethod("Invoke");

            if(invokeMethod == null)
            {
                throw new InvalidOperationException("Invalid delegate type.");
            }

            var parameters = invokeMethod.GetParameters();
            var returnType = invokeMethod.ReturnType;

            var dynamicMethod = new DynamicMethod(
                "DefaultDelegate",
                returnType,
                parameters.Select(p => p.ParameterType).ToArray(),
                typeof(TDelegate).Module
            );

            var il = dynamicMethod.GetILGenerator();

            il.Emit(OpCodes.Ldstr,
                $"Accessing default function for {delegateType} EnumFuncMap");
            il.EmitCall(OpCodes.Call, typeof(Debug).GetMethod("LogWarning",
                new[] { typeof(string) }), null);

            if(returnType != typeof(void))
            {
                if(returnType.IsValueType)
                {
                    il.DeclareLocal(returnType);
                    il.Emit(OpCodes.Ldloca_S, 0);
                    il.Emit(OpCodes.Initobj, returnType);
                    il.Emit(OpCodes.Ldloc_0);
                }
                else
                {
                    il.Emit(OpCodes.Ldnull);
                }
            }
            il.Emit(OpCodes.Ret);

            return (TDelegate)dynamicMethod.CreateDelegate(delegateType);
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
