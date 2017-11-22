using System;
using System.Collections.Generic;
using System.Linq;

namespace TangoII.Linq
{
    public class EnumExtensions
    {
        public static IEnumerable<T> AsEnumerable<T>() where T : struct, IConvertible
            => typeof(T).IsEnum ? Enum.GetValues(typeof(T)).Cast<T>()
                                  : Enumerable.Empty<T>();

        public static IEnumerable<T> AsEnumerableSkippingZeroValue<T>() where T : struct, IConvertible
            => AsEnumerable<T>().Where(enumItem =>
                Convert.ToInt32(enumItem) > 0);
    }
}
