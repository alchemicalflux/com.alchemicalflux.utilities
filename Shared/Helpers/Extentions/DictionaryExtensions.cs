/*------------------------------------------------------------------------------
  File:           DictionaryExtensions.cs 
  Project:        AlchemicalFlux Utilities
  Description:    Helper functions to extend the IDictionary class.
  Copyright:      ©2023 AlchemicalFlux. All rights reserved.

  Last commit by: alchemicalflux 
  Last commit at: 2023-10-24 10:51:04 
------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace AlchemicalFlux.Utilities.Helpers
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Merges two dictionaries, preserving unique key/value pairs from the invoking dictionary and including 
        /// non-conflicting pairs from the other dictionary.
        /// </summary>
        /// <typeparam name="T">The specific dictionary type that uses this extension.</typeparam>
        /// <typeparam name="K">The type of keys in the dictionaries.</typeparam>
        /// <typeparam name="V">The type of values in the dictionaries.</typeparam>
        /// <param name="self">The invoking dictionary.</param>
        /// <param name="other">The dictionary to merge with the invoking dictionary.</param>
        /// <returns>An enumerable collection of unique key/value pairs combining both dictionaries.</returns>
        public static IEnumerable<KeyValuePair<K, V>> Merge<T, K, V>(this T self, IDictionary<K, V> other)
            where T : IDictionary<K, V>, new()
        {
            return self.Concat(other)
                .GroupBy(i => i.Key)
                .Select(i => i.First());
        }
    }
}
