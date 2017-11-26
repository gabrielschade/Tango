using System;
using Tango.Types;

namespace Tango.Functional
{
    public static class FunctionExtensions
    {
        public static Func<T, Unit> ToFunction<T>(this Action<T> action)
            => parameter =>
            {
                action(parameter);
                return new Unit();
            };

        public static Func<T, T2, Unit> ToFunction<T, T2>(this Action<T, T2> action)
            => (parameter1, parameter2) => 
            {
                action(parameter1, parameter2);
                return new Unit();
            };

        public static Func<T, T2, T3, Unit> ToFunction<T, T2, T3>(this Action<T, T2, T3> action)
            => (parameter1, parameter2, parameter3) =>
            {
                action(parameter1, parameter2, parameter3);
                return new Unit();
            };

        public static Func<T, T2, T3, T4, Unit> ToFunction<T, T2, T3, T4>(this Action<T, T2, T3, T4> action)
            => (parameter1, parameter2, parameter3, parameter4) =>
            {
                action(parameter1, parameter2, parameter3, parameter4);
                return new Unit();
            };

        public static Func<T, Func<T2, TResult>> Curry<T, T2, TResult>(this Func<T, T2, TResult> function)
            => Currying.Curry(function);

        public static Func<T, Func<T2, Func<T3, TResult>>> Curry<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function)
            => Currying.Curry(function);

        public static Func<T, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function)
            => Currying.Curry(function);

        public static Func<T, Action<T2>> Curry<T, T2>(this Action<T, T2> action)
            => Currying.Curry(action);

        public static Func<T, Func<T2, Action<T3>>> Curry<T, T2, T3>(this Action<T, T2, T3> action)
            => Currying.Curry(action);

        public static Func<T, Func<T2, Func<T3, Action<T4>>>> Curry<T, T2, T3, T4>(this Action<T, T2, T3, T4> action)
            => Currying.Curry(action);

        public static Func<TResult> PartialApply<T, TResult>(this Func<T, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        public static Func<T2, TResult> PartialApply<T, T2, TResult>(this Func<T, T2, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        public static Func<T2, T3, TResult> PartialApply<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        public static Func<T3, TResult> PartialApply<T, T2, T3, TResult>(this Func<T, T2, T3, TResult> function, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(function, parameter, parameter2);

        public static Func<T2, T3, T4, TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter)
            => PartialApplication.PartialApply(function, parameter);

        public static Func<T3, T4, TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(function, parameter, parameter2);

        public static Func<T4, TResult> PartialApply<T, T2, T3, T4, TResult>(this Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2, T3 parameter3)
            => PartialApplication.PartialApply(function, parameter, parameter2, parameter3);


        public static Action PartialApply<T>(this Action<T> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        public static Action<T2> PartialApply<T, T2>(this Action<T, T2> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        public static Action<T2, T3> PartialApply<T, T2, T3>(this Action<T, T2, T3> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        public static Action<T3> PartialApply<T, T2, T3>(this Action<T, T2, T3> action, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(action, parameter, parameter2);

        public static Action<T2, T3, T4> PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter)
            => PartialApplication.PartialApply(action, parameter);

        public static Action<T3, T4> PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter, T2 parameter2)
            => PartialApplication.PartialApply(action, parameter, parameter2);

        public static Action<T4> PartialApply<T, T2, T3, T4>(this Action<T, T2, T3, T4> action, T parameter, T2 parameter2, T3 parameter3)
            => PartialApplication.PartialApply(action, parameter, parameter2, parameter3);
    }
}
