using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tango.Functional
{
    public static class Operations
    {
        public static Func<int, int, int> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        public static Func<int, int, int> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        public static Func<int, int, int> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        public static Func<int, int, int> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        public static Func<int, int, int, int> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        public static Func<int, int, int, int> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        public static Func<int, int, int, int> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        public static Func<int, int, int, int> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;


        public static Func<double, double, double> AddDouble
            => (parameter1, parameter2) => parameter1 + parameter2;

        public static Func<double, double, double> SubtractDouble
            => (parameter1, parameter2) => parameter1 - parameter2;

        public static Func<double, double, double> MultiplyDouble
            => (parameter1, parameter2) => parameter1 * parameter2;

        public static Func<double, double, double> DivideDouble
            => (parameter1, parameter2) => parameter1 / parameter2;

        public static Func<double, double, double, double> Add3Double
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        public static Func<double, double, double, double> Subtract3Double
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        public static Func<double, double, double, double> Multiply3Double
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        public static Func<double, double, double, double> Divide3Double
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        public static Func<decimal, decimal, decimal> AddDecimal
            => (parameter1, parameter2) => parameter1 + parameter2;

        public static Func<decimal, decimal, decimal> SubtractDecimal
            => (parameter1, parameter2) => parameter1 - parameter2;

        public static Func<decimal, decimal, decimal> MultiplyDecimal
            => (parameter1, parameter2) => parameter1 * parameter2;

        public static Func<decimal, decimal, decimal> DivideDecimal
            => (parameter1, parameter2) => parameter1 / parameter2;

        public static Func<decimal, decimal, decimal, decimal> Add3Decimal
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        public static Func<decimal, decimal, decimal, decimal> Subtract3Decimal
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        public static Func<decimal, decimal, decimal, decimal> Multiply3Decimal
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        public static Func<decimal, decimal, decimal, decimal> Divide3Decimal
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        public static Func<bool, bool, bool> And
            => (parameter1, parameter2) => parameter1 && parameter2;

        public static Func<bool, bool, bool> Or
            => (parameter1, parameter2) => parameter1 || parameter2;

        public static Func<string, string, string> Concat
            => (parameter1, parameter2) => string.Concat(parameter1, parameter2);
    }
}
