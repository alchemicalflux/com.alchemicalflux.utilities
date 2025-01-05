/*------------------------------------------------------------------------------
  File:           ISingleton.cs 
  Project:        AlchemicalFlux Utilities
  Description:    An interface for generic singletons. Useful for mocking 
                    purposes when unit testing classes reliante on a singleton.
  Copyright:      2025 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2025-01-05 09:28:23 
------------------------------------------------------------------------------*/
namespace AlchemicalFlux.Utilities.Helpers
{
    /// <summary>
    /// Interface for generic singleton.
    /// Useful for mocking purposes when unit testing classes reliante on a singleton.
    /// </summary>
    /// <typeparam name="TType">Underlying type provided by the Singleton.</typeparam>
    public interface ISingleton<TType>
    {
        /// <summary>Returns an unique instance to TType.</summary>
        public static TType Get;
    }
}
