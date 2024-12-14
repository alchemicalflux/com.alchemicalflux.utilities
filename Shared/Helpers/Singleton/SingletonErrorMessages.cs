/*------------------------------------------------------------------------------
  File:           SingletonErrorMessages.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Contains message constructors for the generic abstract
                    Singleton class. Helps ensure test cases can check for
                    identical messaging contents.
  Copyright:      2024 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2024-12-14 10:05:01 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Helpers
{
    public class SingletonErrorMessages
    {
        #region Methods

        public static string SealedErrMsg(string type)
        {
            return $"{type} must be a sealed class.";
        }

        public static string TooManyConstructorsErrMsg(string type, int length)
        {
            return $"{type} has {length} constructors. It must have exactly one private parameterless constructor.";
        }

        public static string TooManyParametersErrMsg(string type)
        {
            return $"{type}'s constructor must have no parameters.";
        }

        public static string PublicConstructorErrMsg(string type)
        {
            return $"{type}'s constructor must be private.";
        }

        public static string MethodReturnErrMsg(string type, string method)
        {
            return $"{type} has a method '{method}' that could provide a new instance of {type}. This violates the singleton pattern.";
        }

        public static string MethodParameterErrMsg(string type, string method)
        {
            return $"{type} has a parameter for method {method} that could provide a new instance of {type}. This violates the singleton pattern.";
        }

        public static string GetterPropertyErrMsg(string type, string property)
        {
            return $"{type} has a getter for property {property} that could return a new instance of {type}. This violates the singleton pattern.";
        }

        public static string SetterPropertyErrMsg(string type, string property)
        {
            return $"{type} has a setter for property {property} that could return a new instance of {type}. This violates the singleton pattern.";
        }

        public static string FieldErrMsg(string type, string field)
        {
            return $"{type} has a field {field} that could hold a new instance of {type}. This violates the singleton pattern.";
        }

        #endregion Methods
    }
}