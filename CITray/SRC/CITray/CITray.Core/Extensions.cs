using System;
using System.Collections.Generic;

namespace CITray
{
    /// <summary>
    /// Misc extensions
    /// </summary>
    internal static class Extensions
    {
        /// <summary>
        /// Applies the specified action to all the items in a sequence.
        /// </summary>
        /// <typeparam name="T">Type of the sequence's items.</typeparam>
        /// <param name="sequence">The sequence.</param>
        /// <param name="action">The action.</param>
        public static void Do<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            foreach (var item in sequence) action(item);
        }
    }
}
