using System;

namespace Tango.Functional
{

    /// <summary>
    /// Creates an quick and easy way to cast to <see cref="Func{T, TResult}"/> and <see cref="Action"/> delegates
    /// </summary>
    public static class QuickDelegateCast
    {
        /// <summary>
        /// Casts a function as a delegate
        /// </summary>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function to cast.</param>
        /// <returns>
        /// Function as delegate.
        /// </returns>
        public static Func<TResult> F<TResult>(Func<TResult> function)
            => function;

        /// <summary>
        /// Casts a function as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function to cast.</param>
        /// <returns>
        /// Function as delegate.
        /// </returns>
        public static Func<T, TResult> F<T, TResult>(Func<T, TResult> function)
            => function;

        /// <summary>
        /// Casts a function as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function to cast.</param>
        /// <returns>
        /// Function as delegate.
        /// </returns>
        public static Func<T, T2, TResult> F<T, T2, TResult>(Func<T, T2, TResult> function)
            => function;

        /// <summary>
        /// Casts a function as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function to cast.</param>
        /// <returns>
        /// Function as delegate.
        /// </returns>
        public static Func<T, T2, T3, TResult> F<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function)
            => function;

        /// <summary>
        /// Casts a function as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function to cast.</param>
        /// <returns>
        /// Function as delegate.
        /// </returns>
        public static Func<T, T2, T3, T4, TResult> F<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function)
            => function;




        /// <summary>
        /// Casts an action as a delegate
        /// </summary>
        /// <param name="action">The input function to cast.</param>
        /// <returns>
        /// Action as delegate.
        /// </returns>
        public static Action A(Action action)
            => action;

        /// <summary>
        /// Casts an action as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function to cast.</param>
        /// <returns>
        /// Action as delegate.
        /// </returns>
        public static Action<T> A<T>(Action<T> action)
            => action;

        /// <summary>
        /// Casts an action as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function to cast.</param>
        /// <returns>
        /// Action as delegate.
        /// </returns>
        public static Action<T, T2> A<T, T2>(Action<T, T2> action)
            => action;

        /// <summary>
        /// Casts an action as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function to cast.</param>
        /// <returns>
        /// Action as delegate.
        /// </returns>
        public static Action<T, T2, T3> A<T, T2, T3>(Action<T, T2, T3> action)
            => action;

        /// <summary>
        /// Casts an action as a delegate
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T4">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function to cast.</param>
        /// <returns>
        /// Action as delegate.
        /// </returns>
        public static Action<T, T2, T3, T4> A<T, T2, T3, T4>(Action<T, T2, T3, T4> action)
            => action;
    }
}
