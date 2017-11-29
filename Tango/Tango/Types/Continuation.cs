using System;

namespace Tango.Types
{
    /// <summary>
    /// Represents a value of one of two possible types, similar to Either values. 
    /// Instances of <see cref="Continuation{TSuccess, TFail}"/> contains an instance of <typeparamref name="TSuccess"/> or <typeparamref name="TFail"/>.
    /// <para>
    /// A common use of <see cref="Continuation{TSuccess, TFail}"/> is to represents a result of an operation that can be successful or unsuccessful.
    /// This value can be used to implements a Railway Oriented Programming.
    /// </para>
    ///<para>
    /// A <see cref="Continuation{TSuccess, TFail}"/> value is a powerful tool to chaining operations in a sophisticated and idiomatic way dealing
    /// with possible fails without throwing exceptions.
    /// A <see cref="Continuation{TSuccess, TFail}"/> value behaves like an promise of JavaScript, with Then and Catch operations.
    /// </para>
    /// </summary>
    /// <typeparam name="TSuccess">The type of the success value</typeparam>
    /// <typeparam name="TFail">The type of the fail value</typeparam>
    public struct Continuation<TSuccess, TFail>
    {
        private TSuccess Success { get; }
        private TFail Fail { get; }

        /// <summary>
        /// Returns true when the result value is an <see typeparamref="TSuccess"/> value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// Returns true when the result value is an <see typeparamref="TFail"/> value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsFail => !IsSuccess;

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TSuccess, TFail}"/> value with an <see typeparamref="TSuccess"/> value.
        /// </summary>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        public Continuation(TSuccess success)
        {
            Success = success;
            Fail = default(TFail);
            IsSuccess = true;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TSuccess, TFail}"/> value with an <see typeparamref="TFail"/> value.
        /// </summary>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        public Continuation(TFail fail)
        {
            Fail = fail;
            Success = default(TSuccess);
            IsSuccess = false;
        }

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TSuccess, TFail}"/> value with an <see typeparamref="TSuccess"/> value.
        /// </summary>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        public static Continuation<TSuccess, TFail> Return(TSuccess success)
            => new Continuation<TSuccess, TFail>(success);

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TSuccess, TFail}"/> value with an <see typeparamref="TFail"/> value.
        /// </summary>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        public static Continuation<TSuccess, TFail> Return(TFail fail)
            => new Continuation<TSuccess, TFail>(fail);

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TSuccess, TFail}"/> value with an <see typeparamref="TSuccess"/> value.
        /// </summary>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        public static implicit operator Continuation<TSuccess, TFail>(TSuccess success)
            => new Continuation<TSuccess, TFail>(success);

        /// <summary>
        /// Initialize a new instance of <see cref="Continuation{TSuccess, TFail}"/> value with an <see typeparamref="TFail"/> value.
        /// </summary>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        public static implicit operator Continuation<TSuccess, TFail>(TFail fail)
            => new Continuation<TSuccess, TFail>(fail);

        /// <summary>
        /// Creates an <see cref="Option{T}"/> of <see typeparamref="TSuccess"/> value by <see cref="Continuation{TSuccess, TFail}"/> value <see cref="Success"/> property.
        /// </summary>
        /// <param name="continuation">The <see cref="Continuation{TSuccess, TFail}"/> value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="Option{T}.IsSome"/> when the <see cref="Continuation{TSuccess, TFail}"/> value is in <see cref="IsSuccess"/> state.
        /// Otherwise returns <see cref="Option{T}.None"/>
        /// </returns>
        public static implicit operator Option<TSuccess>(Continuation<TSuccess, TFail> continuation)
           => continuation.IsSuccess ?
                continuation.Success
                : default(TSuccess);

        /// <summary>
        /// Creates an <see cref="Option{T}"/> of <see typeparamref="TFail"/> value by <see cref="Continuation{TSuccess, TFail}"/> value <see cref="Fail"/> property.
        /// </summary>
        /// <param name="continuation">The <see cref="Continuation{TSuccess, TFail}"/> value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="Option{T}.IsSome"/> when the <see cref="Continuation{TSuccess, TFail}"/> value is in <see cref="IsFail"/> state.
        /// Otherwise returns <see cref="Option{T}.None"/>
        /// </returns>
        public static implicit operator Option<TFail>(Continuation<TSuccess, TFail> continuation)
            => continuation.IsFail ?
                 continuation.Fail
                : default(TFail);

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <typeparam name="TNewSuccess">The type of the value returned by <paramref name="thenMethod"/>.</typeparam>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TNewSuccess, TFail}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public Continuation<TNewSuccess, TFail> Then<TNewSuccess>(
            Func<TSuccess, Continuation<TNewSuccess, TFail>> thenMethod)
            => IsSuccess ? thenMethod(Success)
                           : Fail;

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TSuccess, TFail}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public Continuation<TSuccess, TFail> Then(
            Func<TSuccess, Continuation<TSuccess, TFail>> thenMethod)
            => Then<TSuccess>(thenMethod);

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied with the <paramref name="parameter"/> as well.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <typeparam name="TNewSuccess">The type of the value returned by <paramref name="thenMethod"/>.</typeparam>
        /// <typeparam name="TParameter">The type of parameter value</typeparam>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <param name="parameter">The parameter to apply to <paramref name="thenMethod"/> with current <see cref="Success"/> property.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TNewSuccess, TFail}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public Continuation<TNewSuccess, TFail> Then<TParameter, TNewSuccess>(
            Func<TParameter, TSuccess, Continuation<TNewSuccess, TFail>> thenMethod,
            TParameter parameter)
            => Then((user) => thenMethod(parameter, user));

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsFail"/> the <paramref name="catchMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Then"/> function.
        /// </summary>
        /// <typeparam name="TNewFail">The type of the value returned by <paramref name="catchMethod"/>.</typeparam>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TSuccess, TNewFail}"/> value when the current value <see cref="IsFail"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public Continuation<TSuccess, TNewFail> Catch<TNewFail>(Func<TFail, Continuation<TSuccess, TNewFail>> catchMethod)
            => IsFail ? catchMethod(Fail)
                        : Success;

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsFail"/> the <paramref name="catchMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Then"/> function.
        /// </summary>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TSuccess, TFail}"/> value when the current value <see cref="IsFail"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public Continuation<TSuccess, TFail> Catch(Func<TFail, Continuation<TSuccess, TFail>> catchMethod)
            => Catch<TFail>(catchMethod);

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsSuccess"/> the <paramref name="thenMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Catch"/> function.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TSuccess, TFail}"/> value when the current value <see cref="IsSuccess"/>.
        /// Otherwise, returns itself.</returns>
        public static Continuation<TSuccess, TFail> operator >
            (Continuation<TSuccess, TFail> first,
                Func<TSuccess, Continuation<TSuccess, TFail>> thenMethod)
            => first.Then(thenMethod);

        /// <summary>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="thenMethod">The function to apply when it is in <see cref="IsSuccess"/> state.</param>
        /// <returns>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </returns>
        public static Continuation<TSuccess, TFail> operator <
            (Continuation<TSuccess, TFail> first,
                Func<TSuccess, Continuation<TSuccess, TFail>> thenMethod)
            => throw new NotSupportedException();

        /// <summary>
        /// This allows a sophisticated and powerful way to apply some method in order to compose an operation with different functions.
        /// When the current <see cref="Continuation{TSuccess, TFail}"/> is <see cref="IsFail"/> the <paramref name="catchMethod"/> is applied.
        /// Otherwise, returns itself until encounter a <see cref="Then"/> function.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Returns a new <see cref="Continuation{TSuccess, TFail}"/> value when the current value <see cref="IsFail"/>.
        /// Otherwise, returns itself.
        /// </returns>
        public static Continuation<TSuccess, TFail> operator >=
            (Continuation<TSuccess, TFail> first,
                Func<TFail, Continuation<TSuccess, TFail>> catchMethod)
            => first.Catch(catchMethod);

        /// <summary>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </summary>
        /// <param name="first">The continuation itself.</param>
        /// <param name="catchMethod">The function to apply when it is in <see cref="IsFail"/> state.</param>
        /// <returns>
        /// Always raises a <see cref="NotSupportedException"/>.
        /// </returns>
        public static Continuation<TSuccess, TFail> operator <=
            (Continuation<TSuccess, TFail> first, Func<TFail,
                Continuation<TSuccess, TFail>> catchMethod)
            => throw new NotSupportedException();
    }
}
