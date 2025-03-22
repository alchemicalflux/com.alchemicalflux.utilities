/*------------------------------------------------------------------------------
File:       EnumFuncMap.cs 
Project:    AlchemicalFlux Utilities
Overview:   Implements a wrapper-style class for mapping enums to functions.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-22 14:41:15 
------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;

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

        private readonly Dictionary<TEnum, TDelegate> _map;
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
            set { _curDelegate = _map[_curEnum = value]; }
        }

        /// <summary>
        /// Gets the current delegate function.
        /// </summary>
        public TDelegate Func => _curDelegate;

        #endregion Properties

        #region Methods

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
            _map = new();
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
        private void AssignFuncs(ICollection<TDelegate> delegates)
        {
            var enumValues = System.Enum.GetValues(typeof(TEnum));
            if(enumValues.Length != delegates.Count)
            {
                throw new ArgumentException(
                    $"Number of delegates ({delegates.Count}) must match " +
                    $"number of enum values ({enumValues.Length}).",
                    nameof(delegates));
            }

            var enumIter = enumValues.GetEnumerator();
            var delegateIter = delegates.GetEnumerator();
            while(enumIter.MoveNext() && delegateIter.MoveNext())
            {
                if(delegateIter.Current == null)
                {
                    throw new ArgumentNullException(
                        $"Delegate for enum value {enumIter.Current} is null.");
                }
                _map.Add((TEnum)enumIter.Current, delegateIter.Current);
            }
        }

        #endregion Methods
    }
}
