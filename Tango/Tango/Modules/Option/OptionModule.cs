using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Functional;
using Tango.Types;

namespace Tango.Modules
{
    /// <summary>
    /// Basic operations on <see cref="Option{T}"/> values.
    /// </summary>
    public static class OptionModule
    {
        /// <summary>
        /// Convert the a <see cref="Nullable{T}"/> value to <see cref="Option{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="nullableValue">The input nullable.</param>
        /// <returns>New instance of <see cref="Option{T}"/>, with <see cref="Option{T}.Some(T)"/> <typeparamref name="T"/> when <paramref name="nullableValue"/> has value. 
        /// Otherwise creates an option with <see cref="Option{T}.None"/>.
        /// </returns>
        public static Option<T> OfNullable<T>(T? nullableValue)
            where T : struct
            => nullableValue ?? Option<T>.None();

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to a <see cref="Nullable{T}"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">The input option.</param>
        /// <returns><see cref="Nullable{T}"/> value that contains the option value when <see cref="Option{T}.IsSome"/>. Otherwise returns a nullable with null value.</returns>
        public static T? ToNullable<T>(Option<T> option)
            where T : struct
            => option.Match(value => new T?(value), () => null);

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to an <see cref="IEnumerable{T}"/> of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> with a single element when <see cref="Option{T}.IsSome"/>. Otherwise returns an <see cref="CollectionModule.Empty{T}"/>.</returns>
        public static IEnumerable<T> AsEnumerable<T>(Option<T> option)
            => option.Match(
                LazyAsEnumerable,
                () => CollectionModule.Empty<T>());

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to an array of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns an array with a single element when <see cref="Option{T}.IsSome"/>. Otherwise returns an empty array.</returns>
        public static T[] ToArray<T>(Option<T> option)
            => option.Match(
                value => LazyAsEnumerable(value).ToArray(),
                () => new T[0]);

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to a list of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns a list with a single element when <see cref="Option{T}.IsSome"/>. Otherwise returns an empty list.</returns>
        public static List<T> ToList<T>(Option<T> option)
            => option.Match(
                value => LazyAsEnumerable(value).ToList(),
                () => new List<T>());

        /// <summary>Applies the given function to <see cref="Option{T}"/> value when <see cref="Option{T}.IsSome"/>.</summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="action">The function to apply to value from the option.</param>
        /// <param name="option">the input option.</param>
        public static void Iterate<T>(Action<T> action, Option<T> option)
            => option.Match(
                value => action.ToFunction()(value),
                () => new Unit());

        /// <summary>
        /// Evaluates a <see cref="Option{T}.Match{TResult}(Func{T, TResult}, Func{TResult})"/> that returns one when <see cref="Option{T}.IsSome"/>, otherwise returns 0
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">The input option.</param>
        /// <returns>Returns one if the option <see cref="Option{T}.IsSome"/>, otherwise returns zero.</returns>
        public static int Count<T>(Option<T> option)
            => option.Match(
                value => 1,
                () => 0);

        /// <summary>
        /// Returns the <paramref name="option"/> if it <see cref="Option{T}.IsSome"/> and the <paramref name="predicate"/> return true when applied it. 
        /// Otherwise, returns an <see cref="Option{T}.None()"/>.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="predicate">A function that evaluates whether the value contained in the option should remain, or be filtered out.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns <paramref name="option"/> if the option <see cref="Option{T}.IsSome"/> and the <paramref name="predicate"/> function returns true.
        /// Otherwise, returns an <see cref="Option{T}.None()"/>.
        /// </returns>
        public static Option<T> Filter<T>(Func<T, bool> predicate, Option<T> option)
            => option.Match(
                value => predicate(value) ? option : Option<T>.None(),
                () => Option<T>.None());

        /// <summary>
        ///  Returns true if the <paramref name="option"/> <see cref="Option{T}.IsSome"/> and the <paramref name="predicate"/> return true when applied it.
        ///  Otherwise, returns false.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="predicate">A function that evaluates whether the value contained in the option is valid or not.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        ///  Returns true if the <paramref name="option"/> <see cref="Option{T}.IsSome"/> and the <paramref name="predicate"/> return true when applied it.
        ///  Otherwise, returns false.
        /// </returns>
        public static bool Exists<T>(Func<T, bool> predicate, Option<T> option)
            => option.Match(
                value => predicate(value),
                () => false);

        /// <summary>
        /// Creates a new <see cref="Option{T}"/> whose value is the result of applying the given <paramref name="mapping"/> function
        /// to <see cref="Option{T}.Some(T)"/> value. Otherwise returns an <see cref="Option{T}.None"/>.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="mapping"/> function.</typeparam>
        /// <param name="mapping">The function to transform option value from the input option.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns a new <see cref="Option{T}"/> whose value is the result of applying the given <paramref name="mapping"/> function
        /// is the <paramref name="option"/> <see cref="Option{T}.IsSome"/>. Otherwise returns an <see cref="Option{T}.None"/>.
        /// </returns>
        public static Option<TResult> Map<T, TResult>(Func<T, TResult> mapping, Option<T> option)
            => option.Match(
                value => Option<TResult>.Some(mapping(value)),
                () => Option<TResult>.None());

        /// <summary>
        /// Creates a new <see cref="Option{T}"/> whose value is the result of applying the given <paramref name="applying"/> function
        /// to <see cref="Option{T}.Some(T)"/> value when both <paramref name="applying"/> and <paramref name="option"/> are <see cref="Option{T}.IsSome"/>. 
        /// Otherwise returns an <see cref="Option{T}.None"/>.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="applying"/> function.</typeparam>
        /// <param name="applying">The option function to transform option value from the input option.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns a new <see cref="Option{T}"/> whose value is the result of applying the given <paramref name="applying"/> function
        /// to <see cref="Option{T}.Some(T)"/> value when both <paramref name="applying"/> and <paramref name="option"/> are <see cref="Option{T}.IsSome"/>. 
        /// Otherwise returns an <see cref="Option{T}.None"/>.
        /// </returns>
        public static Option<TResult> Apply<T, TResult>(Option<Func<T, TResult>> applying, Option<T> option)
            => option.Match2(
                applying,
                (value, function) => Option<TResult>.Some(function(value)),
                () => Option<TResult>.None());

        /// <summary>
        /// Creates a new <see cref="Option{T}"/> whose value is the result of applying the given <paramref name="binder"/> function
        /// to <paramref name="option"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <typeparam name="TResult">The type of the option value returned by <paramref name="binder"/> function.</typeparam>
        /// <param name="binder">The function to transform option value from the input option to a new <see cref="Option{T}"/>.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns a new <see cref="Option{T}"/> whose value is the result of applying the given <paramref name="binder"/> function
        /// to <paramref name="option"/> value.
        /// </returns>
        public static Option<TResult> Bind<T, TResult>(Func<T, Option<TResult>> binder, Option<T> option)
            => option.Match(
                value => binder(value),
                () => Option<TResult>.None());

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <paramref name="state"/> and <see cref="Option{T}.Some(T)"/> option value. 
        /// Otherwise returns the <paramref name="state"/> itself.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to transform option value from the input option to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <paramref name="state"/> and <see cref="Option{T}.Some(T)"/> option value. 
        /// Otherwise returns the <paramref name="state"/> itself.
        /// </returns>
        public static TState Fold<T, TState>(Func<TState, T, TState> folder, TState state, Option<T> option)
            => option.Match(
                value => folder(state, value),
                () => state);

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <see cref="Option{T}.Some(T)"/> option value and <paramref name="state"/>. 
        /// Otherwise returns the <paramref name="state"/> itself.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to transform option value from the input option to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="option">The input option.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <see cref="Option{T}.Some(T)"/> option value and <paramref name="state"/>. 
        /// Otherwise returns the <paramref name="state"/> itself.
        /// </returns>
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
