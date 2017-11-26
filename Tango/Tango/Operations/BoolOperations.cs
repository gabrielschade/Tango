using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    public static class BoolOperations
    {
        public static Func<bool, bool> Not
            => (parameter) => !parameter;

        public static Func<bool, bool, bool> And
            => (parameter1, parameter2) => parameter1 && parameter2;

        public static Func<bool, bool> AndWith(bool value)
            => And.PartialApply(value);

        public static Func<bool, bool, bool> Or
            => (parameter1, parameter2) => parameter1 || parameter2;

        public static Func<bool, bool> OrWith(bool value)
            => Or.PartialApply(value);
    }
}
