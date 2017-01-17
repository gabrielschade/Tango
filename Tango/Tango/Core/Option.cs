using System;

namespace Tango.Core
{
    public class Option<T>
    {
        public T Value { private get; set; }
        public bool IsSome => Value.Equals(default(T));
        public bool IsNone => !IsSome;
        public static Option<T> Map(T value)
            => new Option<T>() { Value = value };

        public static implicit operator Option<T>(T value)
            => Map(value);

        public TResult Match<TResult>(Func<TResult> none, Func<T, TResult> some)
            => IsSome ? some(Value) : none();



    }
}
