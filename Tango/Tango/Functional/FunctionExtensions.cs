using System;
using Tango.Types;

namespace Tango.Functional
{
    public static class FunctionExtensions
    {
        public static Func<T, Unit> ToFunction<T>(this Action<T> action)
            => parameter => {
                action(parameter);
                return new Unit();
            };
    }
}
