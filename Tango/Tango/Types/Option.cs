using System;

namespace Tango.Types
{
    public struct Option<T>
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

        public static Option<T> None()
            => new Option<T>(default(T));

        public TResult Match<TResult>(
            Func<T, TResult> methodWhenSome, 
            Func<TResult> methodWhenNone)
            => IsSome ?
                methodWhenSome(_value) 
                : methodWhenNone();

    }
}
