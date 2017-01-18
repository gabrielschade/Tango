namespace Tango.Core.Extensions
{
    public static class ContinuationExtensions
    {
        public static Continuation<TSuccess, TFail> MapToContinuation<TSuccess, TFail>(this TSuccess value)
            where TFail : FailResult
            => new Continuation<TSuccess, TFail>(value);

        public static Continuation<TSuccess> MapToContinuation<TSuccess>(this TSuccess value)
            => new Continuation<TSuccess>(value);
    }

    public static class ContinuationFail
    {
        public static FailResult Ignore()
            => default(FailResult);
        public static TFail Ignore<TFail>()
            where TFail : FailResult
            => default(TFail);
    }

    public static class ContinuationSuccess
    {
        public static TSuccess Ignore<TSuccess>()
            => default(TSuccess);
    }
}
