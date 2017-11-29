using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    /// <summary>
    /// Basic operations on int values.
    /// </summary>
    public static class IntegerOperations
    {
        /// <summary>
        /// Function to represents addition operation (+) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<int, int, int> Add
            => (parameter1, parameter2) => parameter1 + parameter2;

        /// <summary>
        /// Function to represents addition operation (+) between two values,
        /// applying first value as partial application on <see cref="Add"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int> AddWith(int value)
            => Add.PartialApply(value);

        /// <summary>
        /// Function to represents subtraction operation (-) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<int, int, int> Subtract
            => (parameter1, parameter2) => parameter1 - parameter2;

        /// <summary>
        /// Function to represents subtraction operation (-) between two values,
        /// applying first value as partial application on <see cref="Subtract"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int> SubtractWith(int value)
            => Subtract.PartialApply(value);

        /// <summary>
        /// Function to represents multiplication operation (*) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<int, int, int> Multiply
            => (parameter1, parameter2) => parameter1 * parameter2;

        /// <summary>
        /// Function to represents subtraction operation (*) between two values,
        /// applying first value as partial application on <see cref="Multiply"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int> MultiplyWith(int value)
            => Multiply.PartialApply(value);

        /// <summary>
        /// Function to represents division operation (/) between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<int, int, int> Divide
            => (parameter1, parameter2) => parameter1 / parameter2;

        /// <summary>
        /// Function to represents division operation (/) between two values,
        /// applying first value as partial application on <see cref="Divide"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int> DivideWith(int value)
            => Divide.PartialApply(value);

        /// <summary>
        /// Function to represents addition operation (+) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<int, int, int, int> Add3
            => (parameter1, parameter2, parameter3) => parameter1 + parameter2 + parameter3;

        /// <summary>
        /// Function to represents addition operation (+) between three values,
        /// applying first value as partial application on <see cref="Add3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int, int> Add3With(int value)
            => Add3.PartialApply(value);

        /// <summary>
        /// Function to represents addition operation (+) between three values,
        /// applying first and second values as partial application on <see cref="Add3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<int, int> Add3With(int value, int value2)
            => Add3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents subtration operation (-) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<int, int, int, int> Subtract3
            => (parameter1, parameter2, parameter3) => parameter1 - parameter2 - parameter3;

        /// <summary>
        /// Function to represents subtration operation (-) between three values,
        /// applying first value as partial application on <see cref="Subtract3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int, int> Subtract3With(int value)
            => Subtract3.PartialApply(value);

        /// <summary>
        /// Function to represents subtration operation (-) between three values,
        /// applying first and second values as partial application on <see cref="Subtract3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<int, int> Subtract3With(int value, int value2)
            => Subtract3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents multiplication operation (*) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<int, int, int, int> Multiply3
            => (parameter1, parameter2, parameter3) => parameter1 * parameter2 * parameter3;

        /// <summary>
        /// Function to represents multiplication operation (*) between three values,
        /// applying first value as partial application on <see cref="Multiply3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int, int> Multiply3With(int value)
            => Multiply3.PartialApply(value);

        /// <summary>
        /// Function to represents multiplication operation (*) between three values,
        /// applying first and second values as partial application on <see cref="Multiply3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<int, int> Multiply3With(int value, int value2)
            => Multiply3.PartialApply(value, value2);

        /// <summary>
        /// Function to represents division operation (/) between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<int, int, int, int> Divide3
            => (parameter1, parameter2, parameter3) => parameter1 / parameter2 / parameter3;

        /// <summary>
        /// Function to represents division operation (/) between three values,
        /// applying first value as partial application on <see cref="Divide3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<int, int, int> Divide3With(int value)
            => Divide3.PartialApply(value);

        /// <summary>
        /// Function to represents division operation (/) between three values,
        /// applying first and second values as partial application on <see cref="Divide3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<int, int> Divide3With(int value, int value2)
            => Divide3.PartialApply(value, value2);
    }
}
