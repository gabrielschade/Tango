using System;
using Tango.Functional;

namespace Tango.Types
{

    /// <summary>
    /// Represents optional values. 
    /// Instances of <see cref="Option{T}"/> encapsulates the value as a container and only allow access through the method <see cref="Match{TResult}(Func{T, TResult}, Func{TResult})"/>.
    /// </summary>
    /// <typeparam name="T">The type of the <see cref="Option{T}"/> value</typeparam>
    public struct Option<T>
    {
        private readonly T _value;
        
        /// <summary>
        /// Returns true when the value is null or equals to its default value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsNone => _value == null || _value.Equals(default(T));

        /// <summary>
        /// Returns true when the value is not null and is not equals to its default value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsSome => !IsNone;

        /// <summary>
        /// Initialize a new instance of <see cref="Option{T}"/> value.
        /// </summary>
        /// <param name="value">The value.</param>
        public Option(T value)
        {
            _value = value;
        }

        /// <summary>
        /// Creates <see cref="Some(T)"/> if the parameter is not null or equals to its default value. Otherwise creates <see cref="None"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value.</returns>
        public static implicit operator Option<T>(T value)
            => Some(value);

        /// <summary>
        /// Creates <see cref="Some(T)"/> if the parameter is not null or equals to its default value. Otherwise creates <see cref="None"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value.</returns>
        public static Option<T> Some(T value)
            => new Option<T>(value);

        /// <summary>
        /// Creates <see cref="Option{T}"/> with <see cref="None"/> state.
        /// </summary>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="None"/> state.</returns>
        public static Option<T> None()
            => new Option<T>(default(T));

        /// <summary>
        /// This allows a sophisticated way to apply some method for <see cref="Option{T}"/> values without having to check for the existence of a value.
        /// </summary>
        /// <typeparam name="TResult">The type of value returned by functions <paramref name="methodWhenSome"/> and <paramref name="methodWhenNone"/>.</typeparam>
        /// <param name="methodWhenSome">Method that will be invoked when this is in <see cref="IsSome"/> state.</param>
        /// <param name="methodWhenNone">Method that will be invoked when this is in <see cref="IsNone"/> state.</param>
        /// <returns>
        /// returns the result of the method according to the <see cref="Option{T}"/> value.
        /// The returns of <paramref name="methodWhenSome"/> when this is in <see cref="IsSome"/> state, Otherwise the returns of <paramref name="methodWhenNone"/>
        /// </returns>
        public TResult Match<TResult>(
            Func<T, TResult> methodWhenSome, 
            Func<TResult> methodWhenNone)
            => IsSome ?
                methodWhenSome(_value) 
                : methodWhenNone();

        /// <summary>
        /// This allows a sophisticated way to apply some action for <see cref="Option{T}"/> values without having to check for the existence of a value.
        /// </summary>
        /// <param name="actionWhenSome">Action that will be invoked when this is in <see cref="IsSome"/> state.</param>
        /// <param name="actionWhenNone">Action that will be invoked when this is in <see cref="IsNone"/> state.</param>
        public void Match(
            Action<T> actionWhenSome,
            Action actionWhenNone)
            => Match(
                actionWhenSome.ToFunction(),
                actionWhenNone.ToFunction()
                );

        /// <summary>
        /// This allows a sophisticated way to apply some method for two different <see cref="Option{T}"/> values without having to check for the existence of any of them.
        /// </summary>
        /// <typeparam name="T2">The type of second <see cref="Option{T}"/> value.</typeparam>
        /// <typeparam name="TResult">The type of value returned by functions <paramref name="methodWhenSome"/> and <paramref name="methodWhenNone"/>.</typeparam>
        /// <param name="option2">Second <see cref="Option{T}"/> value input.</param>
        /// <param name="methodWhenSome">Method that will be invoked when both this and the <paramref name="option2"/> are in <see cref="IsSome"/> state.</param>
        /// <param name="methodWhenNone">Method that will be invoked when this or the <paramref name="option2"/> are in <see cref="IsNone"/> state.</param>
        /// <returns>
        /// returns the result of the method according to the <see cref="Option{T}"/> values.
        /// The returns of <paramref name="methodWhenSome"/> when this both this and the <paramref name="option2"/> are in <see cref="IsSome"/> state, Otherwise the returns of <paramref name="methodWhenNone"/>
        /// </returns>
        public TResult Match2<T2, TResult>(
            Option<T2> option2,
            Func<T, T2, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
            => IsSome && option2.IsSome ?
                methodWhenSome(_value, option2._value)
                : methodWhenNone();

        /// <summary>
        /// This allows a sophisticated way to apply some action for two different <see cref="Option{T}"/> values without having to check for the existence of any of them.
        /// </summary>
        /// <typeparam name="T2">The type of second <see cref="Option{T}"/> value.</typeparam>
        /// <param name="option2">Second <see cref="Option{T}"/> value input.</param>
        /// <param name="actionWhenSome">Method that will be invoked when both this and the <paramref name="option2"/> are in <see cref="IsSome"/> state.</param>
        /// <param name="actionWhenNone">Method that will be invoked when this or the <paramref name="option2"/> are in <see cref="IsNone"/> state.</param>
        public void Match2<T2>(
            Option<T2> option2,
            Action<T, T2> actionWhenSome,
            Action actionWhenNone)
            => Match2(
                option2,
                actionWhenSome.ToFunction(),
                actionWhenNone.ToFunction()
                );
    }
}
