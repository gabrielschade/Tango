using System;
using Tango.Functional;

namespace Tango.Types
{

    /// <summary>
    /// Represents optional values. 
    /// Instances of Option encapsulates the value as a container and only allow access through the method Match.
    /// </summary>
    /// <typeparam name="T">Type of the optional value</typeparam>
    public partial struct Option<T>
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
        /// Initialize a new instance of option value.
        /// </summary>
        /// <param name="value">The value.</param>
        public Option(T value)
        {
            _value = value;
        }

        public static implicit operator Option<T>(T value)
            => new Option<T>(value);

        /// <summary>
        /// An method which creates Some(T) if the parameter is not null or equals to its default value. Otherwise creates None.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>New instance of option value.</returns>
        public static Option<T> Some(T value)
            => new Option<T>(value);

        /// <summary>
        /// An method which creates option with None
        /// </summary>
        /// <returns>New instance of option value with None state.</returns>
        public static Option<T> None()
            => new Option<T>(default(T));

        /// <summary>
        /// This allows a sophisticated way to apply some method for option values without having to check for the existence of a value.
        /// </summary>
        /// <typeparam name="TResult">The type of functions returns.</typeparam>
        /// <param name="methodWhenSome">Method that will be invoked when this struct have some value.</param>
        /// <param name="methodWhenNone">Method that will be invoked when this struct have none value</param>
        /// <returns>
        /// returns the result of the method according to the optional value.
        /// The returns of <paramref name="methodWhenSome"/> when this struct have some value, Otherwise the returns of <paramref name="methodWhenNone"/>
        /// </returns>
        public TResult Match<TResult>(
            Func<T, TResult> methodWhenSome, 
            Func<TResult> methodWhenNone)
            => IsSome ?
                methodWhenSome(_value) 
                : methodWhenNone();

        /// <summary>
        /// This allows a sophisticated way to apply some action for option values without having to check for the existence of a value.
        /// </summary>
        /// <param name="actionWhenSome">Action that will be invoked when this struct have some value.</param>
        /// <param name="actionWhenNone">Action that will be invoked when this struct have none value</param>
        public void Match(
            Action<T> actionWhenSome,
            Action actionWhenNone)
            => Match(
                actionWhenSome.ToFunction(),
                actionWhenNone.ToFunction()
                );

        /// <summary>
        /// This allows a sophisticated way to apply some method for two different option values without having to check for the existence of any of them.
        /// </summary>
        /// <typeparam name="T2">The type of second optional value</typeparam>
        /// <typeparam name="TResult">The type of functions returns.</typeparam>
        /// <param name="option2">Second optional value</param>
        /// <param name="methodWhenSome">Method that will be invoked when both this and the <paramref name="option2"/> have some values.</param>
        /// <param name="methodWhenNone">Method that will be invoked when this or the <paramref name="option2"/> have none value</param>
        /// <returns>
        /// returns the result of the method according to the optional values.
        /// The returns of <paramref name="methodWhenSome"/> when this both this and the <paramref name="option2"/> have some value, Otherwise the returns of <paramref name="methodWhenNone"/>
        /// </returns>
        public TResult Match2<T2, TResult>(
            Option<T2> option2,
            Func<T, T2, TResult> methodWhenSome,
            Func<TResult> methodWhenNone)
            => IsSome && option2.IsSome ?
                methodWhenSome(_value, option2._value)
                : methodWhenNone();

        /// <summary>
        /// This allows a sophisticated way to apply some action for two different option values without having to check for the existence of any of them.
        /// </summary>
        /// <typeparam name="T2">The type of second optional value</typeparam>
        /// <param name="option2">Second optional value</param>
        /// <param name="actionWhenSome">Method that will be invoked when both this and the <paramref name="option2"/> have some values.</param>
        /// <param name="actionWhenNone">Method that will be invoked when this or the <paramref name="option2"/> have none value</param>
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
