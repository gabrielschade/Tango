namespace Tango.Core.Extensions
{
    public static class ContinuationExtensions
    {
        public static Continuation<TSuccess, TFail> MapToContinuation<TSuccess, TFail>(this TSuccess value)
            where TFail : FailResult
            => new Continuation<TSuccess, TFail>(value);
    }
}
