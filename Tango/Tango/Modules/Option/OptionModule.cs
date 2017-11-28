using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Functional;
using Tango.Types;

namespace Tango.Modules
{
    /// <summary>
    /// Basic operations on Option values.
    /// </summary>
    public static class OptionModule
    {
        /// <summary>
        /// Convert the a Nullable value to option.
        /// </summary>
        /// <typeparam name="T">Type of the nullable value.</typeparam>
        /// <param name="nullableValue">The input nullable.</param>
        /// <returns>New instance of option, with some <typeparamref name="T"/> when <paramref name="nullableValue"/> has value. 
        /// Otherwise creates an option with none.
        /// </returns>
        public static Option<T> OfNullable<T>(T? nullableValue)
            where T : struct
            => nullableValue ?? Option<T>.None();

        /// <summary>
        /// Convert the option to a Nullable value.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="option">The input option.</param>
        /// <returns>Nullable value that contains the option value when some. Otherwise returns a nullable with null value.</returns>
        public static T? ToNullable<T>(Option<T> option)
            where T : struct
            => option.Match(value => new T?(value), () => null);

        /// <summary>
        /// Convert the option to an IEnumerable of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns an IEnumerable with a single element when some. Otherwise returns an empty IEnumerable.</returns>
        public static IEnumerable<T> AsEnumerable<T>(Option<T> option)
            => option.Match(
                LazyAsEnumerable,
                () => CollectionModule.Empty<T>());

        /// <summary>
        /// Convert the option to an array of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns an array with a single element when some. Otherwise returns an empty array.</returns>
        public static T[] ToArray<T>(Option<T> option)
            => option.Match(
                value => LazyAsEnumerable(value).ToArray(),
                () => new T[0]);

        /// <summary>
        /// Convert the option to a list of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns a list with a single element when some. Otherwise returns an empty list.</returns>
        public static List<T> ToList<T>(Option<T> option)
            => option.Match(
                value => LazyAsEnumerable(value).ToList(),
                () => new List<T>());

        /// <summary>Applies the given function to option value when some.</summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="action">The function to apply to value from the option.</param>
        /// <param name="option">the input option.</param>
        public static void Iterate<T>(Action<T> action, Option<T> option)
            => option.Match(
                value => action.ToFunction()(value),
                () => new Unit());

        /// <summary>
        /// Evaluates a Match that returns one when some, otherwise returns 0
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="option">The input option.</param>
        /// <returns>A one if the option is some, a zero otherwise.</returns>
        public static int Count<T>(Option<T> option)
            => option.Match(
                value => 1,
                () => 0);

        /// <summary>
        /// Returns this option if it is some and the predicate return true when applied this value. 
        /// Otherwise, returns am option with none.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="predicate">A function that evaluates whether the value contained in the option should remain, or be filtered out.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns the same option value if the option is some and the <paramref name="predicate"/> function returns true.
        /// Otherwise, returns an option with none.
        /// </returns>
        public static Option<T> Filter<T>(Func<T, bool> predicate, Option<T> option)
            => option.Match(
                value => predicate(value) ? option : Option<T>.None(),
                () => Option<T>.None());

        /// <summary>
        ///  Returns true if this option is some and the predicate returns true when applied to this value.
        ///  Otherwise, returns false.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <param name="predicate">A function that evaluates whether the value contained in the option is valid or not.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns true if the option is some and the <paramref name="predicate"/> function returns true.
        /// Otherwise, returns false.
        /// </returns>
        public static bool Exists<T>(Func<T, bool> predicate, Option<T> option)
            => option.Match(
                value => predicate(value),
                () => false);

        /// <summary>
        /// Creates a new option whose value is the result of applying the given function <paramref name="mapping"/>
        /// to the some value of the option input.
        /// </summary>
        /// <typeparam name="T">Type of the option value.</typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="mapping"></param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns a some containing the result of applying mapping to this option value when it is some.
        /// Otherwise returns none.
        /// </returns>
        public static Option<TResult> Map<T, TResult>(Func<T, TResult> mapping, Option<T> option)
            => option.Match(
                value => Option<TResult>.Some(mapping(value)),
                () => Option<TResult>.None());

        public static Option<TResult> Apply<T, TResult>(Option<Func<T, TResult>> applying, Option<T> option)
            => option.Match2(
                applying,
                (value, function) => Option<TResult>.Some(function(value)),
                () => Option<TResult>.None());

        public static Option<TResult> Bind<T, TResult>(Func<T, Option<TResult>> binder, Option<T> option)
            => option.Match(
                value => binder(value),
                () => Option<TResult>.None());

        public static TState Fold<T, TState>(Func<TState, T, TState> folder, TState state, Option<T> option)
            => option.Match(
                value => folder(state, value),
                () => state);

        public static TState FoldBack<T, TState>(Func<T, TState, TState> folder, Option<T> option, TState state)
            => option.Match(
                value => folder(value, state),
                () => state);

        private static IEnumerable<T> LazyAsEnumerable<T>(T value)
        {
            yield return value;
        }
    }
}
