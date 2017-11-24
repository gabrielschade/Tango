using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tango.Functional
{
    public static class PartialApplication
    {
        public static Func<TResult> PartialApply<T,TResult>(Func<T, TResult> function, T parameter)
            => () => function(parameter);

        public static Func<T2, TResult> PartialApply<T, T2, TResult>(Func<T, T2, TResult> function, T parameter)
            => function.Curry()(parameter);

        public static Func<T2,Func<T3, TResult>> PartialApply<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function, T parameter)
            => function.Curry()(parameter);

        public static Func<T3, TResult> PartialApply<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function, T parameter, T2 parameter2)
            => function.Curry()(parameter)(parameter2);

        public static Func<T2, Func<T3, Func<T4, TResult>>> PartialApply<T, T2, T3,T4, TResult>(Func<T, T2, T3,T4, TResult> function, T parameter)
            => function.Curry()(parameter);

        public static Func<T3, Func<T4, TResult>> PartialApply<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2)
            => function.Curry()(parameter)(parameter2);

        public static Func<T4, TResult> PartialApply<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function, T parameter, T2 parameter2, T3 parameter3)
            => function.Curry()(parameter)(parameter2)(parameter3);


        public static Action PartialApply<T>(Action<T> action, T parameter)
            => () => action(parameter);

        public static Action<T2> PartialApply<T, T2>(Action<T, T2> action, T parameter)
            => action.Curry()(parameter);

        public static Func<T2,Action<T3>> PartialApply<T, T2, T3>(Action<T, T2, T3> action, T parameter)
            => action.Curry()(parameter);

        public static Action<T3> PartialApply<T, T2, T3>(Action<T, T2, T3> action, T parameter, T2 parameter2)
            => action.Curry()(parameter)(parameter2);

        public static Func<T2, Func<T3, Action<T4>>> PartialApply<T, T2, T3,T4>(Action<T, T2, T3, T4> action, T parameter)
            => action.Curry()(parameter);

        public static Func<T3, Action<T4>> PartialApply<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T parameter, T2 parameter2)
            => action.Curry()(parameter)(parameter2);

        public static Action<T4> PartialApply<T, T2, T3, T4>(Action<T, T2, T3, T4> action, T parameter, T2 parameter2, T3 parameter3)
            => action.Curry()(parameter)(parameter2)(parameter3);
    }
}
