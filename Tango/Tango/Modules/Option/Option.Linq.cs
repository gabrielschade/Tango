using System;
using System.Collections.Generic;
using Tango.Modules;
using Tango.Types;

namespace Tango.Linq
{
    public static class OptionLinqExtensions
    {

        public static T? ToNullable<T>(this Option<T> option)
            where T : struct
            => OptionModule.ToNullable(option);

        public static IEnumerable<T> AsEnumerable<T>(this Option<T> option)
            => OptionModule.AsEnumerable(option);

        public static T[] ToArray<T>(this Option<T> option)
            => OptionModule.ToArray(option);

        public static List<T> ToList<T>(this Option<T> option)
            => OptionModule.ToList(option);

        public static void Iterate<T>(this Option<T> option,Action<T> action)
            => OptionModule.Iterate(action, option);

        public static int Count<T>(this Option<T> option)
            => OptionModule.Count(option);

        public static Option<T> Filter<T>(this Option<T> option, Func<T, bool> predicate)
            => OptionModule.Filter(predicate, option);

        public static bool Exists<T>(this Option<T> option, Func<T, bool> predicate)
            => OptionModule.Exists(predicate, option);

        public static Option<TResult> Map<T, TResult>(this Option<T> option, Func<T, TResult> mapping)
            => OptionModule.Map(mapping, option);

        public static Option<TResult> Apply<T, TResult>(this Option<T> option, Option<Func<T, TResult>> applying)
            => OptionModule.Apply(applying, option);

        public static Option<TResult> Bind<T, TResult>(this Option<T> option, Func<T, Option<TResult>> binder)
            => OptionModule.Bind(binder, option);

        public static TState Fold<T, TState>(this Option<T> option, TState state, Func<TState, T, TState> folder)
            => OptionModule.Fold(folder, state, option);

        public static TState FoldBack<T, TState>(this Option<T> option, Func<T, TState, TState> folder, TState state)
            => OptionModule.FoldBack(folder, option, state);
    }
}
