using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Types;

namespace Tango.Linq.Extensions
{
    public static class OptionExtensions
    {
        public static Option<T> FirstOrNone<T>(this IEnumerable<T> source)
            => source.FirstOrDefault();

        public static Option<T> FirstOrNone<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => source.FirstOrDefault(predicate);
    }
}
