using System;

namespace CITray
{
    internal static class TypeExtensions
    {
        /// <summary>
        /// Determines whether the specified type is a descendant of <paramref name="parentType"/>.
        /// </summary>
        /// <param name="type">The type to test.</param>
        /// <param name="parentType">Type of the supposed parent.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is a descendant of <paramref name="parentType"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsA(this Type type, Type parentType)
        {
            if (parentType == null) return type == null;
            return parentType.IsAssignableFrom(type);
        }

        /// <summary>
        /// Determines whether the specified type is a descendant of <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Type of the supposed parent.</typeparam>
        /// <param name="type">The type to test.</param>
        /// <returns>
        /// 	<c>true</c> if the specified type is a descendant of <typeparamref name="T"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public static bool IsA<T>(this Type type)
        {
            return IsA(type, typeof(T));
        }
    }
}
