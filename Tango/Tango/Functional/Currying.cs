using System;

namespace Tango.Functional
{
    /// <summary>
    /// The Currying static class contains various methods in order to curry a function in a series of smallers functions
    /// </summary>
    public static class Currying
    {
        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">Function to curry</param>
        /// <returns>Returns a curried function</returns>
        public static Func<T, Func<T2, TResult>> Curry<T, T2, TResult>(Func<T, T2, TResult> function)
        => parameter
            => parameter2 => function(parameter, parameter2);

        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">Function to curry</param>
        /// <returns>Returns a curried function</returns>
        public static Func<T, Func<T2, Func<T3, TResult>>> Curry<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function)
            => parameter
                => parameter2 =>
                    parameter3 => function(parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <returns>Returns a curried function</returns>
        public static Func<T, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function)
            => parameter
                => parameter2 =>
                    parameter3 =>
                     parameter4 => function(parameter, parameter2, parameter3, parameter4);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Returns a curried action</returns>
        public static Func<T, Action<T2>> Curry<T, T2>(Action<T, T2> action)
            => parameter
                => parameter2 => action(parameter, parameter2);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Returns a curried action</returns>
        public static Func<T, Func<T2, Action<T3>>> Curry<T, T2, T3>(Action<T, T2, T3> action)
            => parameter
                => parameter2
                    => parameter3 => action(parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Returns a curried action</returns>
        public static Func<T, Func<T2, Func<T3, Action<T4>>>> Curry<T, T2, T3, T4>(Action<T, T2, T3, T4> action)
            => parameter
             => parameter2
              => parameter3
               => parameter4 => action(parameter, parameter2, parameter3, parameter4);
    }
}
