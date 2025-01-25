/*------------------------------------------------------------------------------
File:       EnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a wrapper-style class for mapping enums to functions.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-25 01:42:32 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class EnumFuncMap<TEnum, TDelegate>
        where TEnum : struct, Enum
        where TDelegate : Delegate
    {
        private readonly Dictionary<TEnum, TDelegate> _map = new();
        private TEnum _curEnum;
        private TDelegate _curDelegate;

        public TEnum Enum
        {
            get => _curEnum;
            set 
            {
                if(_map.Count == 0) { _curEnum = value; }
                else { _curDelegate = _map[_curEnum = value]; }
            }
        }

        public TDelegate Func => _curDelegate ?? DefaultDelegate;

        public static TDelegate DefaultDelegate { get; }

        static EnumFuncMap()
        {
            DefaultDelegate = GetDefaultDelegate();
        }

        public EnumFuncMap(ICollection<TDelegate> delegates) :
            this(delegates, new())
        {
        }

        public EnumFuncMap(ICollection<TDelegate> delegates, TEnum startEnum)
        {
            if(delegates == null)
                throw new ArgumentNullException(nameof(delegates));
            AssignFuncs(delegates);
            Enum = startEnum;
        }

        public void AssignFuncs(ICollection<TDelegate> delegates)
        {
            var enumValues = System.Enum.GetValues(typeof(TEnum));

            if(enumValues.Length != delegates.Count)
                throw new ArgumentException(
                    $"Number of delegates ({delegates.Count}) must match " +
                    $"number of enum values ({enumValues.Length}).",
                    nameof(delegates));

            _map.Clear();

            var enumIter = enumValues.GetEnumerator();
            var delegateIter = delegates.GetEnumerator();

            while(enumIter.MoveNext() && delegateIter.MoveNext())
            {
                _map.Add((TEnum)enumIter.Current, delegateIter.Current);
            }
        }

        private static TDelegate GetDefaultDelegate()
        {
            var delegateType = typeof(TDelegate);
            var invokeMethod = delegateType.GetMethod("Invoke");

            if(invokeMethod == null)
            {
                throw new InvalidOperationException("Invalid delegate type");
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
    }
}