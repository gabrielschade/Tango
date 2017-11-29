using System;
using System.Collections.Generic;
using Tango.Modules;
using Tango.Types;

namespace Tango.Linq
{
    /// <summary>
    /// Basic operations on <see cref="Option{T}"/> as extension methods.
    /// The original operations are in <see cref="OptionModule"/>.
    /// </summary>
    public static class OptionLinqExtensions
    {

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to a <see cref="Nullable{T}"/> value.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">The input option.</param>
        /// <returns><see cref="Nullable{T}"/> value that contains the option value when <see cref="Option{T}.IsSome"/>. Otherwise returns a nullable with null value.</returns>
        public static T? ToNullable<T>(this Option<T> option)
            where T : struct
            => OptionModule.ToNullable(option);

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to an <see cref="IEnumerable{T}"/> of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns an <see cref="IEnumerable{T}"/> with a single element when <see cref="Option{T}.IsSome"/>. Otherwise returns an <see cref="CollectionModule.Empty{T}"/>.</returns>
        public static IEnumerable<T> AsEnumerable<T>(this Option<T> option)
            => OptionModule.AsEnumerable(option);

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to an array of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns an array with a single element when <see cref="Option{T}.IsSome"/>. Otherwise returns an empty array.</returns>
        public static T[] ToArray<T>(this Option<T> option)
            => OptionModule.ToArray(option);

        /// <summary>
        /// Convert the <see cref="Option{T}"/> to a list of length 0 or 1.
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">the input option.</param>
        /// <returns>Returns a list with a single element when <see cref="Option{T}.IsSome"/>. Otherwise returns an empty list.</returns>
        public static List<T> ToList<T>(this Option<T> option)
            => OptionModule.ToList(option);

        /// <summary>Applies the given function to <see cref="Option{T}"/> value when <see cref="Option{T}.IsSome"/>.</summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="action">The function to apply to value from the option.</param>
        /// <param name="option">the input option.</param>
        public static void Iterate<T>(this Option<T> option,Action<T> action)
            => OptionModule.Iterate(action, option);

        /// <summary>
        /// Evaluates a <see cref="Option{T}.Match{TResult}(Func{T, TResult}, Func{TResult})"/> that returns one when <see cref="Option{T}.IsSome"/>, otherwise returns 0
        /// </summary>
        /// <typeparam name="T">The type of the option value.</typeparam>
        /// <param name="option">The input option.</param>
        /// <returns>Returns one if the option <see cref="Option{T}.IsSome"/>, otherwise returns zero.</returns>
        public static int Count<T>(this Option<T> option)
            => OptionModule.Count(option);

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
        public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate)
            => OptionModule.Filter(predicate, option);

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
        public static bool Exists<T>(this Option<T> option, Func<T, bool> predicate)
            => OptionModule.Exists(predicate, option);

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
        public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> mapping)
            => OptionModule.Map(mapping, option);

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
        public static Option<TResult> Apply<T, TResult>(this Option<T> option, Option<Func<T, TResult>> applying)
            => OptionModule.Apply(applying, option);

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
        public static Option<TResult> Bind<T, TResult>(this Option<T> option, Func<T, Option<TResult>> binder)
            => OptionModule.Bind(binder, option);

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
        public static TState Fold<T, TState>(this Option<T> option, TState state, Func<TState, T, TState> folder)
            => OptionModule.Fold(folder, state, option);

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
        public static TState FoldBack<T, TState>(this Option<T> option, Func<T, TState, TState> folder, TState state)
            => OptionModule.FoldBack(folder, option, state);
    }
}
