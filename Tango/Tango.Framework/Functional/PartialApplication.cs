using System;

namespace Tango.Functional
{
    /// <summary>
    /// The PartialApplication static class contains various methods in order to partial apply parameters to a function and return new functions with less parameters.
    /// </summary>
    public static class PartialApplication
    {
        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, TResult>(Func<T, TResult> function, T parameter)
            => () => function(parameter);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, T2, TResult>(Func<T, T2, TResult> function, T parameter, T2 parameter2)
            => () => function.Curry()(parameter)(parameter2);

        /// <summary>
        /// Creates a new single parameter function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T2, TResult> PartialApply<T, T2, TResult>(Func<T, T2, TResult> function, T parameter)
            => function.Curry()(parameter);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function, T parameter, T2 parameter2, T3 parameter3)
            => () => function.Curry()(parameter)(parameter2)(parameter3);

        /// <summary>
        /// Creates a new single parameter function by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T3, TResult> PartialApply<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function, T parameter, T2 parameter2)
            => function.Curry()(parameter)(parameter2);

        /// <summary>
        /// Creates a new two parameters function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function</returns>
        public static Func<T2, T3, TResult> PartialApply<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function, T parameter)
            => (parameter2, parameter3) => function.Curry()(parameter)(parameter2)(parameter3);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/>, <paramref name="parameter3"/> and <paramref name="parameter4"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <param name="parameter4">The fourth input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2, T3 parameter3, T4 parameter4)
            => () => function.Curry()(parameter)(parameter2)(parameter3)(parameter4);

        /// <summary>
        /// Creates a new single parameter function by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T4, TResult> PartialApply<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2, T3 parameter3)
            => function.Curry()(parameter)(parameter2)(parameter3);

        /// <summary>
        /// Creates a new two parameters function by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T3, T4, TResult> PartialApply<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2)
            => (parameter3, parameter4) => function.Curry()(parameter)(parameter2)(parameter3)(parameter4);

        /// <summary>
        /// Creates a new three parameters function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="function"/>.</typeparam>
        /// <typeparam name="TResult">The type of the value returned by <paramref name="function"/>.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T2, T3, T4, TResult> PartialApply<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function, T parameter)
            => (parameter2, parameter3, parameter4) => function.Curry()(parameter)(parameter2)(parameter3)(parameter4);

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T>(Action<T> action, T parameter)
            => PartialApply(action.ToFunction(), parameter).ToAction();

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T, T2>(Action<T, T2> action, T parameter, T2 parameter2)
            => PartialApply(action.ToFunction(), parameter, parameter2).ToAction();

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T2> PartialApply<T, T2>(Action<T, T2> action, T parameter)
            => PartialApply(action.ToFunction(), parameter).ToAction();

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T, T2, T3>(Action<T, T2, T3> action, T parameter, T2 parameter2, T3 parameter3)
            => PartialApply(action.ToFunction(), parameter, parameter2, parameter3).ToAction();

        /// <summary>
        /// Creates a new single parameter action by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T3> PartialApply<T, T2, T3>(Action<T, T2, T3> action, T parameter, T2 parameter2)
            => PartialApply(action.ToFunction(), parameter, parameter2).ToAction();

        /// <summary>
        /// Creates a new two parameters action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T2, T3> PartialApply<T, T2, T3>(Action<T, T2, T3> action, T parameter)
            => PartialApply(action.ToFunction(), parameter).ToAction();

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/>, <paramref name="parameter3"/> and <paramref name="parameter4"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <param name="parameter4">The fourth input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T parameter, T2 parameter2, T3 parameter3, T4 parameter4)
            => PartialApply(action.ToFunction(), parameter, parameter2, parameter3, parameter4).ToAction();

        /// <summary>
        /// Creates a new single parameter action by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T4> PartialApply<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T parameter, T2 parameter2, T3 parameter3)
            => PartialApply(action.ToFunction(), parameter, parameter2, parameter3).ToAction();

        /// <summary>
        /// Creates a new two parameters action by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T3, T4> PartialApply<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T parameter, T2 parameter2)
            => PartialApply(action.ToFunction(), parameter, parameter2).ToAction();

        /// <summary>
        /// Creates a new three parameter action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>.
        /// </summary>
        /// <typeparam name="T">The type of the first parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T2">The type of the second parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T3">The type of the third parameter of <paramref name="action"/>.</typeparam>
        /// <typeparam name="T4">The type of the fourth parameter of <paramref name="action"/>.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T2, T3, T4> PartialApply<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T parameter)
            => PartialApply(action.ToFunction(), parameter).ToAction();
    }
}
