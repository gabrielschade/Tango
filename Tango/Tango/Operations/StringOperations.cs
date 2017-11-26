using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    public class StringOperations
    {
        public static Func<string, string, string> Concat
            => (parameter1, parameter2) => string.Concat(parameter1, parameter2);

        public static Func<string, string> ConcatWith(string value)
            => Concat.PartialApply(value);
    }
}
