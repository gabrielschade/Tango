using Tango.Types;

namespace Tango.Modules
{
    /// <summary>
    /// Basic operations on <see cref="Option{T}"/> as extension methods.
    /// The original operations are in <see cref="ContinuationModule"/>.
    /// </summary>
    public static class ContinuationLinqExtensions
    {
        /// <summary>
        /// Creates a new instance of <see cref= "Continuation{TFail,TSuccess}" /> by invoking the <see cref="Continuation.Resolve{TFail, TSuccess}(TSuccess)"/> method.
        /// </summary>
        /// <typeparam name="TSuccess">Type of Success value</typeparam>
        /// <typeparam name="TFail">Type of Fail value</typeparam>
        /// <param name="value">The input <see typeparamref="TSuccess"/> value.</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> in Success state.
        /// </returns>
        public static Continuation<TFail, TSuccess> AsContinuation<TFail, TSuccess>(this TSuccess value)
            => ContinuationModule.Resolve<TFail, TSuccess>(value);

        /// <summary>
        /// Creates a new instance of <see cref= "Continuation{TFail,TSuccess}" /> by invoking the <see cref="Continuation.Reject{TFail}(TFail)"/> method.
        /// </summary>
        /// <typeparam name="TSuccess">Type of Success value</typeparam>
        /// <typeparam name="TFail">Type of Fail value</typeparam>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> in Fail state.
        /// </returns>
        public static Continuation<TFail, TSuccess> AsContinuation<TFail, TSuccess>(this TFail value)
            => ContinuationModule.Reject<TFail, TSuccess>(value);
    }
}
