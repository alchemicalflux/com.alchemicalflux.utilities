/*------------------------------------------------------------------------------
File:       ScriptableObjectWrapper.cs 
Project:    AlchemicalFlux Utilities
Overview:   Generically wraps a scriptable object class with a reference to the
            generic type. Useful for abstracting/decoupling asset referencing
            for unit testing.
Copyright:  2023-2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-01-05 16:56:47 
------------------------------------------------------------------------------*/
using UnityEngine;

namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Wraps a reference to a type in a scriptable object.
    /// </summary>
    /// <typeparam name="T">Type that will be referenced.</typeparam>
    public class ScriptableObjectWrapper<T> : ScriptableObject
    {
        public T value;
    }
}
