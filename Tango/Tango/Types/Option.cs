using System;
using Tango.Functional;

namespace Tango.Types
{
    public partial struct Option<T>
    {
        private readonly T _value;
        public bool IsNone => _value == null || _value.Equals(default(T));
        public bool IsSome => !IsNone;

        public Option(T value)
        {
            _value = value;
        }

        public static implicit operator Option<T>(T value)
            => new Option<T>(value);

        public static Option<T> Some(T value)
            => new Option<T>();

        public static Option<T> None()
            => new Option<T>(default(T));

        public TResult Match<TResult>(
            Func<T, TResult> methodWhenSome, 
            Func<TResult> methodWhenNone)
            => IsSome ?
                methodWhenSome(_value) 
                : methodWhenNone();

        public Unit Match(
            Action<T> methodWhenSome,
            Action methodWhenNone)
            => Match(
                methodWhenSome.ToFunction(),
                methodWhenNone.ToFunction()
                );

        public TResult Match2<T2, TResult>(
            Option<T2> option2,
            Func<T, T2, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
            => IsSome && option2.IsSome ?
                methodWhenSome(_value, option2._value)
                : methodWhenNone();

        public Unit Match2<T2>(
            Option<T2> option2,
            Action<T, T2> methodWhenSome,
            Action methodWhenNone)
            => Match2(
                option2,
                methodWhenSome.ToFunction(),
                methodWhenNone.ToFunction()
                );
    }
}
