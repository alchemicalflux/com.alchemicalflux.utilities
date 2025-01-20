/*------------------------------------------------------------------------------
File:       EnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a wrapper-style class for mapping enums to functions.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-20 14:29:17 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

namespace AlchemicalFlux.Utilities.Helpers
{
    public class EnumFuncMap<TEnum, TDelegate>
        where TEnum : struct, Enum
        where TDelegate : Delegate
    {
        private readonly Dictionary<TEnum, TDelegate> _map;
        private TEnum _curEnum;
        private TDelegate _curDelegate;

        public TEnum Enum
        {
            get => _curEnum;
            set 
            {
                if(_map == null) { _curEnum = value; }
                else { _curDelegate = _map[_curEnum = value]; }
            }
        }

        public TDelegate Func => _curDelegate;

        public EnumFuncMap(ICollection<TDelegate> delegates) :
            this(delegates, new())
        {
        }

        public EnumFuncMap(ICollection<TDelegate> delegates, TEnum startEnum)
        {
            if(delegates == null)
                throw new ArgumentNullException(nameof(delegates));
            _map = new(delegates.Count);
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
    }
}