using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    public static class IntegerOperations
    {
        public static Func<int, int, int> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        public static Func<int, int> AddWith(int value)
            => Add.PartialApply(value);

        public static Func<int, int, int> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        public static Func<int, int> SubtractWith(int value)
            => Subtract.PartialApply(value);

        public static Func<int, int, int> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        public static Func<int, int> MultiplyWith(int value)
            => Multiply.PartialApply(value);

        public static Func<int, int, int> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        public static Func<int, int> DivideWith(int value)
            => Divide.PartialApply(value);

        public static Func<int, int, int, int> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        public static Func<int, int, int> Add3With(int value)
            => Add3.PartialApply(value);

        public static Func<int, int> Add3With(int value, int value2)
            => Add3.PartialApply(value, value2);

        public static Func<int, int, int, int> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        public static Func<int, int, int> Subtract3With(int value)
            => Subtract3.PartialApply(value);

        public static Func<int, int> Subtract3With(int value, int value2)
            => Subtract3.PartialApply(value, value2);

        public static Func<int, int, int, int> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        public static Func<int, int, int> Multiply3With(int value)
            => Multiply3.PartialApply(value);

        public static Func<int, int> Multiply3With(int value, int value2)
            => Multiply3.PartialApply(value, value2);

        public static Func<int, int, int, int> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        public static Func<int, int, int> Divide3With(int value)
            => Divide3.PartialApply(value);

        public static Func<int, int> Divide3With(int value, int value2)
            => Divide3.PartialApply(value, value2);
    }
}
