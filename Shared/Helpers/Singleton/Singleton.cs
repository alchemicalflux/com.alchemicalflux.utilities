/*------------------------------------------------------------------------------
  File:           Singleton.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Provides an implementation of the Singleton design pattern for
                  creating and managing singleton instances of generic types.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-14 10:05:01 
------------------------------------------------------------------------------*/
using System;
using System.Reflection;

namespace AlchemicalFlux.Utilities.Helpers
{
    using SEM = SingletonErrorMessages;

    /// <summary>
    /// A generic Singleton base class that ensures there is only one instance of a type (TType).
    /// This implementation leverages lazy initialization and reflection to enforce singleton behavior.
    /// </summary>
    public abstract class Singleton<TType>
    {
        #region Members

        /// <summary>
        /// Lazy initialization of the singleton instance of TType.
        /// The instance is created only when it is accessed for the first time.
        /// </summary>
        private static readonly Lazy<TType> _lazy;

        #endregion Members

        #region Properties

        /// <summary>
        /// Accessor to the singleton instance of TType.
        /// Accessing this property will initialize the singleton if it hasn't been created yet.
        /// </summary>
        public static TType Get => _lazy.Value;

        #endregion Properties

        #region Methods

        /// <summary>
        /// Static constructor that initializes the lazy instance.
        /// Ensures that the singleton instance is created only once.
        /// </summary>
        static Singleton()
        {
            _lazy = new(() => CreateInstance());
        }

        /// <summary>
        /// Creates an instance of TType, bypassing its constructor to ensure it meets singleton specifications.
        /// Uses reflection to ensure the constructor is private and there is no other public method that can 
        /// create a new instance.
        /// </summary>
        /// <returns>An instance of the TType class.</returns>
        /// <exception cref="InvalidOperationException">
        /// Throws if TType violates singleton requirements (e.g., multiple constructors, public constructor, etc.).
        /// </exception>
        private static TType CreateInstance()
        {
            var type = typeof(TType);
            ValidateClass(type);
            ValidateConstructors(type);
            ValidateMembers(type);
            return (TType)Activator.CreateInstance(type, true);
        }

        #region Type Validation

        private static void ValidateClass(Type type)
        {
            if(!type.IsSealed)
            {
                throw new InvalidOperationException(SEM.SealedErrMsg(type.Name));
            }
        }

        /// <summary>
        /// Validates the constructors of TType to ensure that it has only one private parameterless constructor.
        /// Throws an exception if there are multiple constructors or if the constructor 
        /// is not private or parameterless.
        /// </summary>
        /// <param name="type">The type to validate.</param>
        private static void ValidateConstructors(Type type)
        {
            var constructors =
                type.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            // Ensure there is exactly one constructor, and it must be private and parameterless.
            if(constructors.Length != 1)
            {
                throw new InvalidOperationException(SEM.TooManyConstructorsErrMsg(type.Name, constructors.Length));
            }

            var constructor = constructors[0];
            if(constructor.GetParameters().Length > 0)
            {
                throw new InvalidOperationException(SEM.TooManyParametersErrMsg(type.Name));
            }

            if(!constructor.IsPrivate)
            {
                throw new InvalidOperationException(SEM.PublicConstructorErrMsg(type.Name));
            }
        }

        /// <summary>
        /// Validates the members (methods, properties, and fields) of TType to ensure no public or internal members
        /// can create a new instance of the singleton, violating the singleton pattern.
        /// </summary>
        /// <param name="type">The type to validate.</param>
        private static void ValidateMembers(Type type)
        {
            var members = type
                .GetMembers(BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            UnityEngine.Debug.Log($"{type.Name}");
            foreach(var member in members)
            {
                UnityEngine.Debug.Log($"  {member.Name} => {member.MemberType}");
                ValidateProperty(member as PropertyInfo, type);
                //ValidateMethod(member as MethodInfo, type);
                //ValidateField(member as FieldInfo, type);
            }
        }

        /// <summary>
        /// Validates methods in TType to ensure they do not return a new instance of the singleton or take parameters
        /// that could potentially create a new instance.
        /// </summary>
        /// <param name="method">The method to validate.</param>
        /// <param name="type">The type to which the method belongs.</param>
        private static void ValidateMethod(MethodInfo method, Type type)
        {
            if(method == null) { return; }

            // Ensure no method can return an instance of the singleton or take parameters that could create one.
            if(method.ReturnType == type && method.GetParameters().Length == 0)
            {
                throw new ArgumentException(SEM.MethodReturnErrMsg(type.Name, method.Name));
            }

            foreach(var parameter in method.GetParameters())
            {
                var paramType = parameter.ParameterType;
                if(paramType.IsByRef) { paramType = paramType.GetElementType(); }
                if(paramType != type) { continue; }
                throw new ArgumentException(SEM.MethodParameterErrMsg(type.Name, method.Name));
            }
        }

        /// <summary>
        /// Validates properties of TType to ensure no property getter or setter could potentially create a new instance.
        /// </summary>
        /// <param name="property">The property to validate.</param>
        /// <param name="type">The type to which the property belongs.</param>
        private static void ValidateProperty(PropertyInfo property, Type type)
        {
            if(property == null) { return; }
            UnityEngine.Debug.Log($"Prop: {property.Name} => {type.Name}");
            if(property.PropertyType != type) { return; }

            // Ensure no property getter or setter could return a new instance of the singleton.
            if (property.GetGetMethod() != null)
            {
                throw new ArgumentException(SEM.GetterPropertyErrMsg(type.Name, property.Name));
            }

            if(property.GetSetMethod() != null)
            {
                throw new ArgumentException(SEM.SetterPropertyErrMsg(type.Name, property.Name));
            }
        }

        /// <summary>
        /// Validates fields of TType to ensure no field can hold a reference to a new instance of the singleton.
        /// </summary>
        /// <param name="field">The field to validate.</param>
        /// <param name="type">The type to which the field belongs.</param>
        private static void ValidateField(FieldInfo field, Type type)
        {
            if(field == null || field.FieldType != type) { return; }

            // Ensure no field could hold a reference to a new instance of the singleton.
            throw new ArgumentException(SEM.FieldErrMsg(type.Name, field.Name));
        }

        #endregion Type Validation

        #endregion Methods
    }
}