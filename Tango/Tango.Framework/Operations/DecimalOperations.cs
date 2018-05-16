using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    /// <summary>
    /// Basic operations on decimal values.
    /// </summary>
    public static class DecimalOperations
    {
        /// <summary>
        /// Function to represents addition operation (+) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<decimal, decimal, decimal> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        /// <summary>
        /// Function to represents addition operation (+) between two values,
        /// applying first value as partial application on <see cref="Add"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal> AddWith(decimal value)
            => Add.PartialApply(value);

        /// <summary>
        /// Function to represents subtraction operation (-) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<decimal, decimal, decimal> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        /// <summary>
        /// Function to represents subtraction operation (-) between two values,
        /// applying first value as partial application on <see cref="Subtract"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal> SubtractWith(decimal value)
            => Subtract.PartialApply(value);

        /// <summary>
        /// Function to represents multiplication operation (*) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<decimal, decimal, decimal> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        /// <summary>
        /// Function to represents subtraction operation (*) between two values,
        /// applying first value as partial application on <see cref="Multiply"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal> MultiplyWith(decimal value)
            => Multiply.PartialApply(value);

        /// <summary>
        /// Function to represents division operation (/) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<decimal, decimal, decimal> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        /// <summary>
        /// Function to represents division operation (/) between two values,
        /// applying first value as partial application on <see cref="Divide"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal> DivideWith(decimal value)
            => Divide.PartialApply(value);

        /// <summary>
        /// Function to represents addition operation (+) between three values.
        /// Ideal to use in Fold2 function.
        /// </summary>
        public static Func<decimal, decimal, decimal, decimal> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        /// <summary>
        /// Function to represents addition operation (+) between three values,
        /// applying first value as partial application on <see cref="Add3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal, decimal> Add3With(decimal value)
            => Add3.PartialApply(value);

        /// <summary>
        /// Function to represents addition operation (+) between three values,
        /// applying first and second values as partial application on <see cref="Add3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second value.</param>
        public static Func<decimal, decimal> Add3With(decimal value, decimal value2)
            => Add3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents subtration operation (-) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<decimal, decimal, decimal, decimal> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        /// <summary>
        /// Function to represents subtration operation (-) between three values,
        /// applying first value as partial application on <see cref="Subtract3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal, decimal> Subtract3With(decimal value)
            => Subtract3.PartialApply(value);

        /// <summary>
        /// Function to represents subtration operation (-) between three values,
        /// applying first and second values as partial application on <see cref="Subtract3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<decimal, decimal> Subtract3With(decimal value, decimal value2)
            => Subtract3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents multiplication operation (*) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<decimal, decimal, decimal, decimal> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        /// <summary>
        /// Function to represents multiplication operation (*) between three values,
        /// applying first value as partial application on <see cref="Multiply3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal, decimal> Multiply3With(decimal value)
            => Multiply3.PartialApply(value);

        /// <summary>
        /// Function to represents multiplication operation (*) between three values,
        /// applying first and second values as partial application on <see cref="Multiply3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<decimal, decimal> Multiply3With(decimal value, decimal value2)
            => Multiply3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents division operation (/) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<decimal, decimal, decimal, decimal> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        /// <summary>
        /// Function to represents division operation (/) between three values,
        /// applying first value as partial application on <see cref="Divide3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<decimal, decimal, decimal> Divide3With(decimal value)
            => Divide3.PartialApply(value);

        /// <summary>
        /// Function to represents division operation (/) between three values,
        /// applying first and second values as partial application on <see cref="Divide3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<decimal, decimal> Divide3With(decimal value, decimal value2)
            => Divide3.PartialApply(value, value2);
    }
}
