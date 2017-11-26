using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    public static class DecimalOperations
    {
        public static Func<decimal, decimal, decimal> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        public static Func<decimal, decimal> AddWith(decimal value)
            => Add.PartialApply(value);

        public static Func<decimal, decimal, decimal> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        public static Func<decimal, decimal> SubtractWith(decimal value)
            => Subtract.PartialApply(value);

        public static Func<decimal, decimal, decimal> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        public static Func<decimal, decimal> MultiplyWith(decimal value)
            => Multiply.PartialApply(value);

        public static Func<decimal, decimal, decimal> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        public static Func<decimal, decimal> DivideWith(decimal value)
            => Divide.PartialApply(value);

        public static Func<decimal, decimal, decimal, decimal> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        public static Func<decimal, decimal, decimal> Add3With(decimal value)
            => Add3.PartialApply(value);

        public static Func<decimal, decimal> Add3With(decimal value, decimal value2)
            => Add3.PartialApply(value, value2);

        public static Func<decimal, decimal, decimal, decimal> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        public static Func<decimal, decimal, decimal> Subtract3With(decimal value)
            => Subtract3.PartialApply(value);

        public static Func<decimal, decimal> Subtract3With(decimal value, decimal value2)
            => Subtract3.PartialApply(value, value2);

        public static Func<decimal, decimal, decimal, decimal> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        public static Func<decimal, decimal, decimal> Multiply3With(decimal value)
            => Multiply3.PartialApply(value);

        public static Func<decimal, decimal> Multiply3With(decimal value, decimal value2)
            => Multiply3.PartialApply(value, value2);

        public static Func<decimal, decimal, decimal, decimal> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        public static Func<decimal, decimal, decimal> Divide3With(decimal value)
            => Divide3.PartialApply(value);

        public static Func<decimal, decimal> Divide3With(decimal value, decimal value2)
            => Divide3.PartialApply(value, value2);
    }
}
