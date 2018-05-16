using System;
using Tango.Functional;

namespace Tango.CommonOperations
{
    /// <summary>
    /// Basic operations on boolean values.
    /// </summary>
    public static class BoolOperations
    {
        /// <summary>
        /// Function to represents the operator !.
        /// </summary>
        public static Func<bool, bool> Not
            => (parameter) => !parameter;

        /// <summary>
        /// Function to represents the and operation between two booleans.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<bool, bool, bool> And
            => (parameter1, parameter2) => parameter1 && parameter2;

        /// <summary>
        /// Function to represents the and operation between two booleans, 
        /// applying first value as partial application on <see cref="And"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<bool, bool> AndWith(bool value)
            => And.PartialApply(value);

        /// <summary>
        /// Function to represents the or operation between two booleans.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        public static Func<bool, bool, bool> Or
            => (parameter1, parameter2) => parameter1 || parameter2;

        /// <summary>
        /// Function to represents the or operation between two booleans, 
        /// applying first value as partial application on <see cref="Or"/> method.
        /// Ideal to use in Reduce and Fold functions.
        /// </summary>
        /// <param name="value">The input value.</param>
        public static Func<bool, bool> OrWith(bool value)
            => Or.PartialApply(value);
    }
}
