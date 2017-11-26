using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    public static class DoubleOperations
    {
        public static Func<double, double, double> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        public static Func<double, double> AddWith(double value)
            => Add.PartialApply(value);

        public static Func<double, double, double> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        public static Func<double, double> SubtractWith(double value)
            => Subtract.PartialApply(value);

        public static Func<double, double, double> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        public static Func<double, double> MultiplyWith(double value)
            => Multiply.PartialApply(value);

        public static Func<double, double, double> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        public static Func<double, double> DivideWith(double value)
            => Divide.PartialApply(value);

        public static Func<double, double, double, double> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        public static Func<double, double, double> Add3With(double value)
            => Add3.PartialApply(value);

        public static Func<double, double> Add3With(double value, double value2)
            => Add3.PartialApply(value, value2);

        public static Func<double, double, double, double> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        public static Func<double, double, double> Subtract3With(double value)
            => Subtract3.PartialApply(value);

        public static Func<double, double> Subtract3With(double value, double value2)
            => Subtract3.PartialApply(value, value2);

        public static Func<double, double, double, double> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        public static Func<double, double, double> Multiply3With(double value)
            => Multiply3.PartialApply(value);

        public static Func<double, double> Multiply3With(double value, double value2)
            => Multiply3.PartialApply(value, value2);

        public static Func<double, double, double, double> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        public static Func<double, double, double> Divide3With(double value)
            => Divide3.PartialApply(value);

        public static Func<double, double> Divide3With(double value, double value2)
            => Divide3.PartialApply(value, value2);
    }
}
