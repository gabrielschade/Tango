using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Modules;

namespace Tango.Linq
{
    /// <summary>
    /// Provides extension methods to enum types in order to cast enums to IEnumerables
    /// </summary>
    public class EnumExtensions
    {
        /// <summary>
        /// Cast a <typeparamref name="T"/> type enum to an <see cref="System.Collections.Generic.IEnumerable{T}"/> which each element is an enum option.
        /// </summary>
        /// <typeparam name="T">The type of the enum value</typeparam>
        /// <returns>Returns a collection with all enum option. Returns an empty collection when <typeparamref name="T"/> is not an enum.</returns>
        public static IEnumerable<T> AsEnumerable<T>() where T : struct, IConvertible
            => typeof(T).IsEnum ? Enum.GetValues(typeof(T)).Cast<T>()
                                  : CollectionModule.Empty<T>();

        /// <summary>
        /// Cast a <typeparamref name="T"/> type enum to an <see cref="System.Collections.Generic.IEnumerable{T}"/> which each element is an enum option, except by the zero value option.
        /// </summary>
        /// <typeparam name="T">The type of the enum value</typeparam>
        /// <returns>Returns a collection with all enum option. Returns an empty collection when <typeparamref name="T"/> is not an enum.</returns>
        public static IEnumerable<T> AsEnumerableSkipZero<T>() where T : struct, IConvertible
            => AsEnumerable<T>().Filter(enumItem =>
                Convert.ToInt32(enumItem) > 0);
    }
}
