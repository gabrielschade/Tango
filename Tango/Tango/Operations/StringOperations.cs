using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    /// <summary>
    /// Basic operations on string values.
    /// </summary>
    public class StringOperations
    {
        /// <summary>
        /// Function to represents concatenation operation <see cref="string.Concat(object)"/> between two values.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<string, string, string> Concat
            => (parameter1, parameter2) => string.Concat(parameter1, parameter2);

        /// <summary>
        /// Function to represents concatenation operation <see cref="string.Concat(object)"/> between two values,
        /// applying first value as partial application on <see cref="Concat"/> method.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<string, string> ConcatWith(string value)
            => Concat.PartialApply(value);

        /// <summary>
        /// Function to represents concatenation operation <see cref="string.Concat(object)"/> between three values.
        /// Ideal to use in Fold2 functions.
        /// </summary>
        public static Func<string, string, string, string> Concat3
            => (parameter1, parameter2, parameter3) => string.Concat(parameter1, parameter2, parameter3);

        /// <summary>
        /// Function to represents concatenation operation <see cref="string.Concat(object)"/> between three values,
        /// applying first value as partial application on <see cref="Concat3"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<string, string,string> Concat3With(string value)
            => Concat3.PartialApply(value);

        /// <summary>
        /// Function to represents concatenation operation <see cref="string.Concat(object)"/> between three values,
        /// applying first and second values as partial application on <see cref="Concat3"/> method.
        /// </summary>
        /// <param name="value">The first input value.</param>
        /// <param name="value2">The second input value.</param>
        public static Func<string, string> Concat3With(string value, string value2)
            => Concat3.PartialApply(value, value2);
    }
}
