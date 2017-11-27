using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Functional;
using Tango.Types;

namespace Tango.Modules
{
    public static class OptionModule
    {
        public static Option<T> OfNullable<T>(T? nullableValue)
            where T : struct
            => nullableValue.Value;

        public static T? ToNullable<T>(Option<T> option)
            where T : struct
            => option.Match(value => new T?(value), () => null);

        public static IEnumerable<T> AsEnumerable<T>(Option<T> option)
            => option.Match(
                LazyAsEnumerable,
                () => CollectionModule.Empty<T>());

        public static T[] ToArray<T>(Option<T> option)
            => option.Match(
                value => LazyAsEnumerable(value).ToArray(),
                () => new T[0]);

        public static List<T> ToList<T>(Option<T> option)
            => option.Match(
                value => LazyAsEnumerable(value).ToList(),
                () => new List<T>());

        public static void Iterate<T>(Action<T> action, Option<T> option)
            => option.Match(
                value => action.ToFunction()(value),
                () => new Unit());

        public static int Count<T>(Option<T> option)
            => option.Match(
                value => 1,
                () => 0);

        public static Option<T> Filter<T>(Func<T, bool> predicate, Option<T> option)
            => option.Match(
                value => predicate(value) ? option : Option<T>.None(),
                () => Option<T>.None());

        public static bool Exists<T>(Func<T, bool> predicate, Option<T> option)
            => option.Match(
                value => predicate(value),
                () => false);

        public static Option<TResult> Map<T, TResult>(Func<T, TResult> mapping, Option<T> option)
            => option.Match(
                value => Option<TResult>.Some(mapping(value)),
                () => Option<TResult>.None());

        public static Option<TResult> Apply<T, TResult>(Option<Func<T, TResult>> applying, Option<T> option)
            => option.Match2(
                applying,
                (value, function) => Option<TResult>.Some(function(value)),
                () => Option<TResult>.None());

        public static Option<TResult> Bind<T, TResult>(Func<T, Option<TResult>> binder, Option<T> option)
            => option.Match(
                value => binder(value),
                () => Option<TResult>.None());

        public static TState Fold<T, TState>(Func<TState, T, TState> folder, TState state, Option<T> option)
            => option.Match(
                value => folder(state, value),
                () => state);

        public static TState FoldBack<T, TState>(Func<T, TState, TState> folder, Option<T> option, TState state)
            => option.Match(
                value => folder(value, state),
                () => state);

        private static IEnumerable<T> LazyAsEnumerable<T>(T value)
        {
            yield return value;
        }
    }
}
