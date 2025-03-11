/*------------------------------------------------------------------------------
File:       IOnUpdateEvent.cs 
Project:    AlchemicalFlux Utilities
Overview:   Interface for handling update events with a generic type.
Copyright:  2025 AlchemicalFlux. All rights reserved.

Last commit by: alchemicalflux 
Last commit at: 2025-03-11 04:42:53 
------------------------------------------------------------------------------*/
using System;

namespace AlchemicalFlux.Utilities.Events
{
    /// <summary>
    /// Interface for handling update events with a generic type.
    /// </summary>
    /// <typeparam name="TType">The type of the value being updated.</typeparam>
    public interface IOnUpdateEvent<TType>
    {
        /// <summary>
        /// Adds an update listener.
        /// </summary>
        /// <param name="action">The action to be called on update.</param>
        void AddOnUpdateListener(Action<TType> action);

        /// <summary>
        /// Removes an update listener.
        /// </summary>
        /// <param name="action">The action to be removed.</param>
        void RemoveOnUpdateListener(Action<TType> action);
    }
}
