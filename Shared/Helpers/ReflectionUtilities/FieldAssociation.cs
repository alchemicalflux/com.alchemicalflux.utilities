/*------------------------------------------------------------------------------
File:       FieldAssociation.cs 
Project:    AlchemicalFlux Utilities
Overview:   Defines a class representing a field assignment within a 
            MonoBehaviour.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-07-23 21:43:22 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Represents a field assignment within a MonoBehaviour.
    /// </summary>
    public class FieldAssociation
    {
        #region Properties

        /// <summary>
        /// Gets or sets the FieldInfo object representing the field.
        /// </summary>
        public FieldInfo FieldInfo { get; set; }

        /// <summary>
        /// Gets or sets the GameObject associated with the field.
        /// </summary>
        public GameObject GameObject { get; set; }

        /// <summary>
        /// Gets or sets the MonoBehaviour associated with the field.
        /// </summary>
        public MonoBehaviour SourceMonoBehaviour { get; set; }

        /// <summary>
        /// Used to build object heirarchy path, only if necessary.
        /// </summary>
        private Stack<string> NameStack { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the FieldAssociation class.
        /// </summary>
        /// <param name="fieldInfo">
        /// The FieldInfo object representing the field.
        /// </param>
        /// <param name="sourceMB">
        /// The MonoBehaviour associated with the field.
        /// </param>
        public FieldAssociation(FieldInfo fieldInfo, MonoBehaviour sourceMB)
        {
            FieldInfo = fieldInfo;
            SourceMonoBehaviour = sourceMB;
            GameObject = sourceMB.gameObject;
        }

        /// <summary>
        /// Gets the full name of the GameObject hierarchy containing the field.
        /// </summary>
        public string FullName
        {
            get
            {
                NameStack ??= new(); // Only initialize once.
                var length = GameObject.name.Length;
                for(var parent = GameObject.transform.parent; 
                    parent != null; parent = parent.transform.parent) 
                {
                    NameStack.Push(parent.name);
                    length += parent.name.Length;
                    ++length; // Account for '/' symbol.
                }

                var builder = new StringBuilder(length + NameStack.Count);
                for(; NameStack.Count > 0; ) 
                { 
                    builder.Append(NameStack.Pop()).Append('/'); 
                }
                builder.Append(GameObject.name).Append('/')
                    .Append(SourceMonoBehaviour.GetType().Name);
                return builder.ToString();
            }
        }

        /// <summary>
        /// Returns a string representation of the FieldAssociation object.
        /// </summary>
        /// <returns>
        /// String containing the name of the field and its full GameObject 
        /// hierarchy.
        /// </returns>
        public override string ToString()
        {
            return $"[{nameof(FieldAssociation)}: Field = {FieldInfo.Name}, " +
                $"FullName = {FullName}]";
        }

        #endregion Methods
    }
}
