/*------------------------------------------------------------------------------
File:       ReflectionUtility.cs 
Project:    AlchemicalFlux Utilities
Overview:   Utility functions to help with the Reflection process.
Copyright:  2024-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Provides utility methods for reflection operations.
    /// </summary>
    public static class ReflectionUtility
    {
        #region Methods

        /// <summary>
        /// Retrieves fields with a specified attribute type from the given object.
        /// </summary>
        /// <typeparam name="T">The type of attribute to search for.</typeparam>
        /// <param name="source">The object to inspect.</param>
        /// <param name="reflectionFlags">Optional BindingFlags to control the scope of reflection.</param>
        /// <param name="attributeIgnoreCheck">Optional function to determine whether a field with the specified 
        ///     attribute should be ignored.</param>
        /// <returns>A list of tuples containing the fields and their corresponding attribute instances.</returns>
        public static List<(FieldInfo field, T attribute)> GetFieldsWithAttribute<T>(
            object source,
            BindingFlags reflectionFlags = BindingFlags.Default,
            Func<FieldInfo, object, T, bool> attributeIgnoreCheck = null)
            where T : Attribute
        {
            // Default shouldIgnore function to return false if not provided.
            attributeIgnoreCheck ??= (_, _, _) => false;

            var typeOfT = typeof(T);
            var enumerable = typeof(IEnumerable);
            var processing = new Stack<object>();
            processing.Push(source);

            var fieldsWithAttributes = new List<(FieldInfo field, T attribute)>();
            while(processing.Count > 0)
            {
                // Process an object, adding it to the outcome if it has the requested attribute.
                // The attributeIgnoreCheck functor can filter out objects from the final outcome.
                // If the object is an enumerable container, add its contents to be processed.
                var obj = processing.Pop();
                var fields = GetFields(obj.GetType(), reflectionFlags);
                foreach(var field in fields)
                {
                    ProcessField(obj, field);
                }
            }
            return fieldsWithAttributes;

            #region Local Helpers

            // Helper method to process a field
            void ProcessField(object obj, FieldInfo field)
            {
                if(enumerable.IsAssignableFrom(field.FieldType))
                {
                    AddEnumerableChildrenForProcessing(obj, field);
                }   
                else
                {
                    ProcessNonEnumerableField(obj, field);
                }
            }

            // Helper method to process enumerable fields
            void AddEnumerableChildrenForProcessing(object obj, FieldInfo field)
            {
                var group = (IEnumerable)field.GetValue(obj);
                foreach(var iter in group)
                {
                    processing.Push(iter);
                }
            }

            // Helper method to process non-enumerable fields
            void ProcessNonEnumerableField(object obj, FieldInfo field)
            {
                var attribute = (T)Attribute.GetCustomAttribute(field, typeOfT);
                if(attribute == null || attributeIgnoreCheck(field, obj, attribute)) { return; }

                fieldsWithAttributes.Add((field, attribute));
            }

            #endregion Local Helpers
        }

        /// <summary>
        /// Retrieves the fields of the specified type from the given class type.
        /// </summary>
        /// <param name="type">The class type to retrieve fields from.</param>
        /// <param name="flags">Optional BindingFlags to control the scope of reflection.</param>
        /// <returns>An array of FieldInfo objects representing the fields of the class.</returns>
        private static FieldInfo[] GetFields(Type type, BindingFlags flags)
        {
            return flags == BindingFlags.Default ? type.GetFields() : type.GetFields(flags);
        }

        #endregion Methods
    }
}