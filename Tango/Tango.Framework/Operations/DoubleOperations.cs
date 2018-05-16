using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    /// <summary>
    /// Basic operations on double values.
    /// </summary>
    public static class DoubleOperations
    {
        /// <summary>
        /// Function to represents addition operation (+) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<double, double, double> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        /// <summary>
        /// Function to represents addition operation (+) between two values,
        /// applying first value as partial application on <see cref="Add"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double> AddWith(double value)
            => Add.PartialApply(value);

        /// <summary>
        /// Function to represents subtraction operation (-) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<double, double, double> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        /// <summary>
        /// Function to represents subtraction operation (-) between two values,
        /// applying first value as partial application on <see cref="Subtract"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double> SubtractWith(double value)
            => Subtract.PartialApply(value);

        /// <summary>
        /// Function to represents multiplication operation (*) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<double, double, double> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        /// <summary>
        /// Function to represents subtraction operation (*) between two values,
        /// applying first value as partial application on <see cref="Multiply"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double> MultiplyWith(double value)
            => Multiply.PartialApply(value);

        /// <summary>
        /// Function to represents division operation (/) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<double, double, double> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        /// <summary>
        /// Function to represents division operation (/) between two values,
        /// applying first value as partial application on <see cref="Divide"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double> DivideWith(double value)
            => Divide.PartialApply(value);

        /// <summary>
        /// Function to represents addition operation (+) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<double, double, double, double> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        /// <summary>
        /// Function to represents addition operation (+) between three values,
        /// applying first value as partial application on <see cref="Add3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double, double> Add3With(double value)
            => Add3.PartialApply(value);

        /// <summary>
        /// Function to represents addition operation (+) between three values,
        /// applying first and second values as partial application on <see cref="Add3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<double, double> Add3With(double value, double value2)
            => Add3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents subtration operation (-) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<double, double, double, double> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        /// <summary>
        /// Function to represents subtration operation (-) between three values,
        /// applying first value as partial application on <see cref="Subtract3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double, double> Subtract3With(double value)
            => Subtract3.PartialApply(value);

        /// <summary>
        /// Function to represents subtration operation (-) between three values,
        /// applying first and second values as partial application on <see cref="Subtract3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<double, double> Subtract3With(double value, double value2)
            => Subtract3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents multiplication operation (*) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<double, double, double, double> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        /// <summary>
        /// Function to represents multiplication operation (*) between three values,
        /// applying first value as partial application on <see cref="Multiply3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double, double> Multiply3With(double value)
            => Multiply3.PartialApply(value);

        /// <summary>
        /// Function to represents multiplication operation (*) between three values,
        /// applying first and second values as partial application on <see cref="Multiply3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<double, double> Multiply3With(double value, double value2)
            => Multiply3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents division operation (/) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<double, double, double, double> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        /// <summary>
        /// Function to represents division operation (/) between three values,
        /// applying first value as partial application on <see cref="Divide3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<double, double, double> Divide3With(double value)
            => Divide3.PartialApply(value);

        /// <summary>
        /// Function to represents division operation (/) between three values,
        /// applying first and second values as partial application on <see cref="Divide3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<double, double> Divide3With(double value, double value2)
            => Divide3.PartialApply(value, value2);
    }
}
