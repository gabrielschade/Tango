using System;

namespace Tango.Types
{
    public struct Continuation<TSuccess, TFail>
    {
        public TSuccess Success { get; }
        public TFail Fail { get; }
        public bool IsSuccess { get; }
        public bool IsFail => !IsSuccess;

        public Continuation(TSuccess success)
        {
            Success = success;
            Fail = default(TFail);
            IsSuccess = true;
        }
        public Continuation(TFail fail)
        {
            Fail = fail;
            Success = default(TSuccess);
            IsSuccess = false;
        }

        public static implicit operator Continuation<TSuccess, TFail>(TSuccess success)
            => new Continuation<TSuccess, TFail>(success);

        public static implicit operator Continuation<TSuccess, TFail>(TFail fail)
            => new Continuation<TSuccess, TFail>(fail);

        public static implicit operator Option<TSuccess>(Continuation<TSuccess, TFail> continuation)
           => continuation.IsSuccess ?
                continuation.Success
                : default(TSuccess);

        public static implicit operator Option<TFail>(Continuation<TSuccess, TFail> continuation)
            => continuation.IsFail ?
                 continuation.Fail
                : default(TFail);

        public Continuation<TNewSuccess, TFail> Then<TNewSuccess>(
            Func<TSuccess, Continuation<TNewSuccess, TFail>> thenMethod)
            => IsSuccess ? thenMethod(Success)
                           : Fail;

        public Continuation<TSuccess, TFail> Then(
            Func<TSuccess, Continuation<TSuccess, TFail>> thenMethod)
            => Then<TSuccess>(thenMethod);

        public Continuation<TNewSuccess, TFail> Then<TParameter, TNewSuccess>(
            Func<TParameter, TSuccess, Continuation<TNewSuccess, TFail>> thenMethod,
            TParameter parameter)
            => Then((user) => thenMethod(parameter, user));

        public Continuation<TSuccess, TNewFail> Catch<TNewFail>(Func<TFail, Continuation<TSuccess, TNewFail>> catchMethod)
            => IsFail ? catchMethod(Fail)
                        : Success;

        public Continuation<TSuccess, TFail> Catch(Func<TFail, Continuation<TSuccess, TFail>> catchMethod)
            => Catch<TFail>(catchMethod);

        public static Continuation<TSuccess, TFail> operator >
            (Continuation<TSuccess, TFail> first,
                Func<TSuccess, Continuation<TSuccess, TFail>> thenMethod)
            => first.Then(thenMethod);

        public static Continuation<TSuccess, TFail> operator <
            (Continuation<TSuccess, TFail> first,
                Func<TSuccess, Continuation<TSuccess, TFail>> thenMethod)
            => throw new NotSupportedException();

        public static Continuation<TSuccess, TFail> operator >=
            (Continuation<TSuccess, TFail> first,
                Func<TFail, Continuation<TSuccess, TFail>> catchMethod)
            => first.Catch(catchMethod);

        public static Continuation<TSuccess, TFail> operator <=
            (Continuation<TSuccess, TFail> first, Func<TFail,
                Continuation<TSuccess, TFail>> catchMethod)
            => throw new NotSupportedException();
    }
}
