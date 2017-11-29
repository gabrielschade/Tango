using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">Function to curry</param>
        /// <returns>Returns a curried function</returns>
        public static Func<T, Func<T2, TResult>> Curry<T, T2, TResult>(Func<T, T2, TResult> function)
        => parameter
            => parameter2 => function(parameter, parameter2);

        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">Function to curry</param>
        /// <returns>Returns a curried function</returns>
        public static Func<T, Func<T2, Func<T3, TResult>>> Curry<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function)
            => parameter
                => parameter2 =>
                    parameter3 => function(parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <returns>Returns a curried function</returns>
        public static Func<T, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function)
            => parameter
                => parameter2 =>
                    parameter3 =>
                     parameter4 => function(parameter, parameter2, parameter3, parameter4);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Returns a curried action</returns>
        public static Func<T, Action<T2>> Curry<T, T2>(Action<T, T2> action)
            => parameter
                => parameter2 => action(parameter, parameter2);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Returns a curried action</returns>
        public static Func<T, Func<T2, Action<T3>>> Curry<T, T2, T3>(Action<T, T2, T3> action)
            => parameter
                => parameter2
                    => parameter3 => action(parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Returns a curried action</returns>
        public static Func<T, Func<T2, Func<T3, Action<T4>>>> Curry<T, T2, T3, T4>(Action<T, T2, T3, T4> action)
            => parameter
             => parameter2
              => parameter3
               => parameter4 => action(parameter, parameter2, parameter3, parameter4);
    }
}
