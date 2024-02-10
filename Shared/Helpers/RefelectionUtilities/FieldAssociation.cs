/*------------------------------------------------------------------------------
  File:           FieldAssociation.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Defines a class representing a field assignment within a 
                    MonoBehaviour.
  Copyright:      ©2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-02-10 11:12:07 
------------------------------------------------------------------------------*/
using System.Reflection;
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Represents a field assignment within a MonoBehaviour.
    /// </summary>
    public class FieldAssociation
    {
        #region Members

        /// <summary>Output format for the string representation of the FieldAssociation.</summary>
        private const string _output = "[" + nameof(FieldAssociation) + ": Field = {0}, FullName = {1}]";

        #endregion Members

        #region Properties

        /// <summary>Gets or sets the FieldInfo object representing the field.</summary>
        public FieldInfo FieldInfo { get; set; }

        /// <summary>Gets or sets the GameObject associated with the field.</summary>
        public GameObject GameObject { get; set; }

        /// <summary>Gets or sets the MonoBehaviour associated with the field.</summary>
        public MonoBehaviour SourceMonoBehaviour { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initializes a new instance of the FieldAssociation class.
        /// </summary>
        /// <param name="fieldInfo">The FieldInfo object representing the field.</param>
        /// <param name="sourceMB">The MonoBehaviour associated with the field.</param>
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
                var currentParent = GameObject.transform.parent;
                var fullName = GameObject.name;
                while (currentParent != null)
                {
                    fullName = currentParent.gameObject.name + "/" + fullName;
                    currentParent = currentParent.transform.parent;
                }

                return fullName;
            }
        }

        /// <summary>
        /// Returns a string representation of the FieldAssociation object.
        /// </summary>
        /// <returns>String containing the name of the field and its full GameObject hierarchy.</returns>
        public override string ToString()
        {
            return string.Format(_output, FieldInfo.Name, FullName);
        }

        #endregion Methods
    }
}
