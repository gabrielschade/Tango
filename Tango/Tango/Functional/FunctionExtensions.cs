using System;
using Tango.Types;

namespace Tango.Functional
{
    /// <summary>
    /// Provides extention methods to cast, curry and partial apply functions and actions.
    /// </summary>
    public static class FunctionExtensions
    {
        /// <summary>
        /// Casts an action to a function that returns a new instance of <see cref="Unit"/>.
        /// </summary>
        /// <param name="action">The input action.</param>
        /// <returns>Returns a new function that internally invokes the action and returns an <see cref="Unit"/> type value.</returns>
        public static Func<Unit> ToFunction(this Action action)
            => () =>
            {
                action();
                return new Unit();
            };

        /// <summary>
        /// Casts an action to a function that returns a new instance of <see cref="Unit"/>.
        /// </summary>
        /// <param name="action">The input action.</param>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <returns>Returns a new function that internally invokes the action and returns an <see cref="Unit"/> type value.</returns>
        public static Func<T, Unit> ToFunction<T>(this Action<T> action)
            => parameter =>
            {
                action(parameter);
                return new Unit();
            };

        /// <summary>
        /// Casts an action to a function that returns a new instance of <see cref="Unit"/>.
        /// </summary>
        /// <param name="action">The input action.</param>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <returns>Returns a new function that internally invokes the action and returns an <see cref="Unit"/> type value.</returns>
        public static Func<T, T2, Unit> ToFunction<T, T2>(this Action<T, T2> action)
            => (parameter1, parameter2) =>
            {
                action(parameter1, parameter2);
                return new Unit();
            };


        /// <summary>
        /// Casts an action to a function that returns a new instance of <see cref="Unit"/>.
        /// </summary>
        /// <param name="action">The input action.</param>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <returns>Returns a new function that internally invokes the action and returns an <see cref="Unit"/> type value.</returns>
        public static Func<T, T2, T3, Unit> ToFunction<T, T2, T3>(this Action<T, T2, T3> action)
            => (parameter1, parameter2, parameter3) =>
            {
                action(parameter1, parameter2, parameter3);
                return new Unit();
            };


        /// <summary>
        /// Casts an action to a function that returns a new instance of <see cref="Unit"/>.
        /// </summary>
        /// <param name="action">The input action.</param>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <returns>Returns a new function that internally invokes the action and returns an <see cref="Unit"/> type value.</returns>
        public static Func<T, T2, T3, T4, Unit> ToFunction<T, T2, T3, T4>(this Action<T, T2, T3, T4> action)
            => (parameter1, parameter2, parameter3, parameter4) =>
            {
                action(parameter1, parameter2, parameter3, parameter4);
                return new Unit();
            };

        /// <summary>
        /// Casts a function that returns <see cref="Unit"/> to an action.
        /// </summary>
        /// <param name="function">The input action.</param>
        /// <returns>Returns a new action that internally invokes the <paramref name="function"/>.</returns>
        public static Action ToAction(this Func<Unit> function)
            => () => function();

        /// <summary>
        /// Casts a function that returns <see cref="Unit"/> to an action.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <param name="function">The input action.</param>
        /// <returns>Returns a new action that internally invokes the <paramref name="function"/>.</returns>
        public static Action<T> ToAction<T>(this Func<T, Unit> function)
            => parameter => function(parameter);

        /// <summary>
        /// Casts a function that returns <see cref="Unit"/> to an action.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <param name="function">The input action.</param>
        /// <returns>Returns a new action that internally invokes the <paramref name="function"/>.</returns>
        public static Action<T, T2> ToAction<T, T2>(this Func<T, T2, Unit> function)
            => (parameter1, parameter2) => function(parameter1, parameter2);


        /// <summary>
        /// Casts a function that returns <see cref="Unit"/> to an action.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <param name="function">The input action.</param>
        /// <returns>Returns a new action that internally invokes the <paramref name="function"/>.</returns>
        public static Action<T, T2, T3> ToAction<T, T2, T3>(this Func<T, T2, T3, Unit> function)
            => (parameter1, parameter2, parameter3) => function(parameter1, parameter2, parameter3);


        /// <summary>
        /// Casts a function that returns <see cref="Unit"/> to an action.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <param name="function">The input action.</param>
        /// <returns>Returns a new action that internally invokes the <paramref name="function"/>.</returns>
        public static Action<T, T2, T3, T4> ToAction<T, T2, T3, T4>(this Func<T, T2, T3, T4, Unit> function)
            => (parameter1, parameter2, parameter3, parameter4) =>
                function(parameter1, parameter2, parameter3, parameter4);





        /// <summary>
        /// Creates a new curried method of <paramref name="function"/> 
        /// by using the <see cref="Currying.Curry{T, T2, TResult}(Func{T, T2, TResult})"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">Function to curry</param>
        /// <returns>Curried function</returns>
        public static Func<T, Func<T2, TResult>> Curry<T, T2, TResult>(this Func<T, T2, TResult> function)
            => Currying.Curry(function);

        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>
        /// by using the <see cref="Currying.Curry{T, T2,T3, TResult}(Func{T, T2,T3, TResult})"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">Function to curry</param>
        /// <returns>Curried function</returns>
        public static Func<T, Func<T2, Func<T3, TResult>>> Curry<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function)
            => Currying.Curry(function);

        /// <summary>
        /// Creates a new curried method of <paramref name="function"/>
        /// by using the <see cref="Currying.Curry{T, T2, T3, T4, TResult}(Func{T, T2,T3, T4, TResult})"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">Function to curry</param>
        public static Func<T, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function)
            => Currying.Curry(function);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>
        /// by using the <see cref="Currying.Curry{T, T2, TResult}(Action{T, T2, TResult})"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Curried action</returns>
        public static Func<T, Action<T2>> Curry<T, T2>(this Action<T, T2> action)
            => Currying.Curry(action);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>
        /// by using the <see cref="Currying.Curry{T, T2,T3, TResult}(Action{T, T2,T3, TResult})"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Curried action</returns>
        public static Func<T, Func<T2, Action<T3>>> Curry<T, T2, T3>(this Action<T, T2, T3> action)
            => Currying.Curry(action);

        /// <summary>
        /// Creates a new curried action of <paramref name="action"/>
        /// by using the <see cref="Currying.Curry{T, T2, T3, T4}(Action{T, T2, T3, T4})"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <param name="action">Action to curry</param>
        /// <returns>Curried action</returns>
        public static Func<T, Func<T2, Func<T3, Action<T4>>>> Curry<T, T2, T3, T4>(this Action<T, T2, T3, T4> action)
            => Currying.Curry(action);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T, TResult}(Func{T, TResult}, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, TResult>(this Func<T, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,TResult}(Func{T,T2, TResult}, T,T2)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, T2, TResult>(this Func<T, T2, TResult> function, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(function, parameter, parameter2);

        /// <summary>
        /// Creates a new single parameter function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2, TResult}(Func{T,T2, TResult}, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T2, TResult> PartialApply<T, T2, TResult>(this Func<T, T2, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3, TResult}(Func{T,T2,T3, TResult}, T,T2,T3)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function, T parameter, T2 parameter2, T3 parameter3)
            => PartialApplication.PartialApply(function, parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new single parameter function by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3, TResult}(Func{T,T2,T3, TResult}, T,T2)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T3, TResult> PartialApply<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(function, parameter, parameter2);

        /// <summary>
        /// Creates a new two parameters function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3, TResult}(Func{T,T2,T3, TResult}, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function</returns>
        public static Func<T2, T3, TResult> PartialApply<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        /// <summary>
        /// Creates a new non parameter function by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/>, <paramref name="parameter3"/> and <paramref name="parameter4"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4, TResult}(Func{T,T2,T3,T4, TResult}, T,T2,T3,T4)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <param name="parameter4">The fourth input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2, T3 parameter3, T4 parameter4)
            => PartialApplication.PartialApply(function, parameter, parameter2, parameter3, parameter4);

        /// <summary>
        /// Creates a new single parameter function by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4, TResult}(Func{T,T2,T3,T4, TResult}, T,T2,T3)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T4, TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2, T3 parameter3)
            => PartialApplication.PartialApply(function, parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new two parameters function by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4, TResult}(Func{T,T2,T3,T4, TResult}, T,T2)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T3, T4, TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(function, parameter, parameter2);

        /// <summary>
        /// Creates a new three parameters function by partial applies the <paramref name="parameter"/> into the <paramref name="function"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4, TResult}(Func{T,T2,T3,T4, TResult}, T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="function"/> parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="function"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="function"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="function"/> fourth parameter.</typeparam>
        /// <typeparam name="TResult">Type of <paramref name="function"/> return.</typeparam>
        /// <param name="function">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <returns>Returns a partial applied function.</returns>
        public static Func<T2, T3, T4, TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);


        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T}(Action{T},T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T>(this Action<T> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2}(Action{T,T2},T,T2)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T, T2>(this Action<T, T2> action, T parameter, T2 parameter2)
             => PartialApplication.PartialApply(action, parameter, parameter2);

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2}(Action{T,T2},T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T2> PartialApply<T, T2>(this Action<T, T2> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3}(Action{T,T2,T3},T,T2,T3)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T, T2, T3>(this Action<T, T2, T3> action, T parameter, T2 parameter2, T3 parameter3)
            => PartialApplication.PartialApply(action, parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new single parameter action by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3}(Action{T,T2,T3},T,T2)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T3> PartialApply<T, T2, T3>(this Action<T, T2, T3> action, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(action, parameter, parameter2);

        /// <summary>
        /// Creates a new two parameters action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3}(Action{T,T2,T3},T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T2, T3> PartialApply<T, T2, T3>(this Action<T, T2, T3> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        /// <summary>
        /// Creates a new non parameter action by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/>, <paramref name="parameter3"/> and <paramref name="parameter4"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4}(Action{T,T2,T3,T4},T,T2,T3,T4)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <param name="parameter4">The fourth input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter, T2 parameter2, T3 parameter3, T4 parameter4)
            => PartialApplication.PartialApply(action, parameter, parameter2, parameter3, parameter4);

        /// <summary>
        /// Creates a new single parameter action by partial applies the <paramref name="parameter"/>, <paramref name="parameter2"/> and <paramref name="parameter3"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4}(Action{T,T2,T3,T4},T,T2,T3)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <param name="parameter3">The third input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T4> PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter, T2 parameter2, T3 parameter3)
            => PartialApplication.PartialApply(action, parameter, parameter2, parameter3);

        /// <summary>
        /// Creates a new two parameters action by partial applies the <paramref name="parameter"/> and <paramref name="parameter2"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4}(Action{T,T2,T3,T4},T,T2)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <param name="parameter2">The second input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T3, T4> PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(action, parameter, parameter2);

        /// <summary>
        /// Creates a new three parameter action by partial applies the <paramref name="parameter"/> into the <paramref name="action"/>
        /// by using the <see cref="PartialApplication.PartialApply{T,T2,T3,T4}(Action{T,T2,T3,T4},T)"/> method.
        /// </summary>
        /// <typeparam name="T">Type of <paramref name="action"/> first parameter.</typeparam>
        /// <typeparam name="T2">Type of <paramref name="action"/> second parameter.</typeparam>
        /// <typeparam name="T3">Type of <paramref name="action"/> third parameter.</typeparam>
        /// <typeparam name="T4">Type of <paramref name="action"/> fourth parameter.</typeparam>
        /// <param name="action">The input function.</param>
        /// <param name="parameter">The first input parameter to partial apply.</param>
        /// <returns>Returns a partial applied action.</returns>
        public static Action<T2, T3, T4> PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        

        
    }
}
