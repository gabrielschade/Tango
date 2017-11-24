using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tango.Functional
{
    public static class Currying
    {

        public static Func<T, Func<T2, TResult>> Curry<T, T2, TResult>(Func<T, T2, TResult> function)
        => parameter
            => parameter2 => function(parameter, parameter2);

        public static Func<T, Func<T2, Func<T3, TResult>>> Curry<T, T2, T3, TResult>(Func<T, T2, T3, TResult> function)
            => parameter
                => parameter2 =>
                    parameter3 => function(parameter, parameter2, parameter3);

        public static Func<T, Func<T2, Func<T3, Func<T4, TResult>>>> Curry<T, T2, T3, T4, TResult>(Func<T, T2, T3, T4, TResult> function)
            => parameter
                => parameter2 =>
                    parameter3 =>
                     parameter4 => function(parameter, parameter2, parameter3, parameter4);

        public static Func<T, Action<T2>> Curry<T, T2>(Action<T, T2> action)
            => parameter
                => parameter2 => action(parameter, parameter2);

        public static Func<T, Func<T2, Action<T3>>> Curry<T, T2, T3>(Action<T, T2, T3> action)
            => parameter
                => parameter2
                    => parameter3 => action(parameter, parameter2, parameter3);

        public static Func<T, Func<T2, Func<T3, Action<T4>>>> Curry<T, T2, T3, T4>(Action<T, T2, T3, T4> action)
            => parameter
             => parameter2
              => parameter3
               => parameter4 => action(parameter, parameter2, parameter3, parameter4);
    }
}
