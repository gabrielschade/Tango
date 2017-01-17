using System;

namespace Tango.Core
{
    public class Continuation<TSuccess, TFail>
        where TFail : FailResult
    {
        public TSuccess SuccessResult { get; }
        public TFail FailResult { get; }
        public bool IsSuccess { get; }
        public bool IsFail => !IsSuccess;

        public Continuation(TSuccess success)
        {
            SuccessResult = success;
            FailResult = default(TFail);
            IsSuccess = true;
        }
        public Continuation(TFail fail)
        {
            FailResult = fail;
            SuccessResult = default(TSuccess);
            IsSuccess = false;
        }

        public Continuation(FailResult fail)
        {
            FailResult = fail as TFail;
            SuccessResult = default(TSuccess);
            IsSuccess = false;
        }

        public static implicit operator TSuccess(Continuation<TSuccess, TFail> continuation)
            => continuation.SuccessResult;

        public static implicit operator TFail(Continuation<TSuccess, TFail> continuation)
            => continuation.FailResult;

        public static implicit operator Continuation<TSuccess, TFail>(TSuccess success)
            => new Continuation<TSuccess, TFail>(success);

        public static implicit operator Continuation<TSuccess, TFail>(TFail fail)
            => new Continuation<TSuccess, TFail>(fail);

        public Continuation<TSuccess, TFail> Bypass(Func<TSuccess, TFail> bypass)
            => Then(BypassExecute(bypass));

        public Continuation<TSuccess, TFail> Then(Func<TSuccess, Continuation<TSuccess, TFail>> toContinue, bool safeMode = true)
            => Then<TSuccess>(toContinue, safeMode);

        public Continuation<TNewSuccess, TFail> Then<TNewSuccess>(Func<TSuccess, Continuation<TNewSuccess, TFail>> toContinue, bool safeMode = true)
            => IsSuccess ? Execute(() => toContinue(SuccessResult))
                           : new Continuation<TNewSuccess, TFail>(FailResult);

        public TSuccess Fail(Func<TFail, Continuation<TSuccess, TFail>> toError)
            => IsFail ? toError(FailResult).SuccessResult
                            : SuccessResult;

        private Func<TSuccess, Continuation<TSuccess, TFail>> BypassExecute(Func<TSuccess, TFail> bypass)
            => (result) =>
            {
                TFail fail = bypass(result);
                return fail == null || fail.Equals(default(TFail)) ? 
                       this : new Continuation<TSuccess, TFail>(fail);
            };


        private Continuation<TNewSuccess, TFail> Execute<TNewSuccess>(Func<Continuation<TNewSuccess, TFail>> toContinue, bool safeMode = true)
            => safeMode ? SafeExecute(toContinue)
                              : toContinue();



        private Continuation<TNewSuccess, TFail> SafeExecute<TNewSuccess>(Func<Continuation<TNewSuccess, TFail>> toContinue)
        {
            Continuation<TNewSuccess, TFail> result;
            try
            {
                result = toContinue();
            }
            catch (Exception exception)
            {
                result = new Continuation<TNewSuccess, TFail>(new FailResult(exception.Message));
            }

            return result;
        }
    }
}
