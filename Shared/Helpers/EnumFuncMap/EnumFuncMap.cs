/*------------------------------------------------------------------------------
File:       EnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a wrapper-style class for mapping enums to functions.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-19 21:35:50 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// A wrapper-style class for mapping enums to functions.
    /// </summary>
    /// <typeparam name="TEnum">The enum type to be mapped.</typeparam>
    /// <typeparam name="TDelegate">The delegate type to be mapped.</typeparam>
    public class EnumFuncMap<TEnum, TDelegate>
        where TEnum : struct, Enum
        where TDelegate : Delegate
    {
        #region Fields

        private readonly Dictionary<TEnum, TDelegate> _map = new();
        private TEnum _curEnum;
        private TDelegate _curDelegate;

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
                if(_map.Count == 0) { _curEnum = value; }
                else { _curDelegate = _map[_curEnum = value]; }
            }
        }

        /// <summary>
        /// Gets the current delegate function.
        /// </summary>
        public TDelegate Func => _curDelegate ?? DefaultDelegate;

        /// <summary>
        /// Gets the default delegate function.
        /// </summary>
        public static TDelegate DefaultDelegate { get; }

        #endregion Properties

        #region Methods

        static EnumFuncMap()
        {
            DefaultDelegate = GetDefaultDelegate();
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="EnumFuncMap{TEnum, TDelegate}"/> class.
        /// </summary>
        /// <param name="delegates">
        /// The collection of delegates to be mapped.
        /// </param>
        public EnumFuncMap(ICollection<TDelegate> delegates) :
            this(delegates, default)
        {
        }

        /// <summary>
        /// Initializes a new instance of the 
        /// <see cref="EnumFuncMap{TEnum, TDelegate}"/> class.
        /// </summary>
        /// <param name="delegates">
        /// The collection of delegates to be mapped.
        /// </param>
        /// <param name="startEnum">The initial enum value.</param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when the delegates parameter is null.
        /// </exception>
        public EnumFuncMap(ICollection<TDelegate> delegates, TEnum startEnum)
        {
            if(delegates == null)
            {
                throw new ArgumentNullException(nameof(delegates));
            }
            AssignFuncs(delegates);
            Enum = startEnum;
        }

        /// <summary>
        /// Assigns the functions to the enum values.
        /// </summary>
        /// <param name="delegates">
        /// The collection of delegates to be assigned.
        /// </param>
        /// <exception cref="ArgumentException">
        /// Thrown when the number of delegates does not match the number of 
        /// enum values.
        /// </exception>
        public void AssignFuncs(ICollection<TDelegate> delegates)
        {
            var enumValues = System.Enum.GetValues(typeof(TEnum));

            if(enumValues.Length != delegates.Count)
            {
                throw new ArgumentException(
                    $"Number of delegates ({delegates.Count}) must match " +
                    $"number of enum values ({enumValues.Length}).",
                    nameof(delegates));
            }

            _map.Clear();

            var enumIter = enumValues.GetEnumerator();
            var delegateIter = delegates.GetEnumerator();

            while(enumIter.MoveNext() && delegateIter.MoveNext())
            {
                _map.Add((TEnum)enumIter.Current, delegateIter.Current);
            }
        }

        /// <summary>
        /// Creates a default delegate that logs a warning message and returns a
        /// default value for for the TDelegate type.
        /// </summary>
        /// <returns>The default delegate function.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the delegate type is invalid.
        /// </exception>
        private static TDelegate GetDefaultDelegate()
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
    }
}
