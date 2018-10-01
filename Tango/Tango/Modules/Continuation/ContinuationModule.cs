using Tango.Types;

namespace Tango.Modules
{
    /// <summary>
    /// Basic operations on <see cref="Continuation{TFail,TSuccess}"/> values. 
    /// </summary>
    public static class ContinuationModule
    {
        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail,TSuccess}" /> value with a <see typeparamref = "TSuccess" /> value and a <see cref="Unit"/> as Fail value.
        /// </summary>
        /// <typeparam name="TSuccess">Type of Success value</typeparam>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> in Success state.
        /// </returns>
        public static Continuation<Unit, TSuccess> Resolve<TSuccess>(TSuccess success)
        => Resolve<Unit, TSuccess>(success);

        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail,TSuccess}" /> value with a <see typeparamref = "TSuccess" /> value and a <see typeparamref="TFail"/> as Fail value.
        /// </summary>
        /// <typeparam name="TSuccess">Type of Success value</typeparam>
        /// <typeparam name="TFail">Type of Fail value</typeparam>
        /// <param name="success">The input <see typeparamref="TSuccess"/> value.</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> in Success state.
        /// </returns>
        public static Continuation<TFail, TSuccess> Resolve<TFail, TSuccess>(TSuccess success)
        => success;

        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail,TSuccess}" /> value with a <see typeparamref = "TFail" /> value and a <see cref="Unit"/> as Success value.
        /// </summary>
        /// <typeparam name="TFail">Type of Fail value</typeparam>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> in Fail state.
        /// </returns>
        public static Continuation<TFail, Unit> Reject<TFail>(TFail fail)
        => Reject<TFail, Unit>(fail);

        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail,TSuccess}" /> value with a <see typeparamref = "TFail" /> value and a <see typeparamref="TSuccess"/> as Success value.
        /// </summary>
        /// <typeparam name="TSuccess">Type of Success value</typeparam>
        /// <typeparam name="TFail">Type of Fail value</typeparam>
        /// <param name="fail">The input <see typeparamref="TFail"/> value.</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> in Fail state.
        /// </returns>
        public static Continuation<TFail, TSuccess> Reject<TFail, TSuccess>(TFail fail)
        => fail;

        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail, TSuccess}" /> value according to the <see cref="Continuation{TFail, TSuccess}"/>parameters.
        /// <para>
        /// The new <see cref="Continuation{TFail, TSuccess}"/> will be in Success state only when all parameters are also in this state.
        /// </para>
        /// Otherwise, the new <see cref="Continuation{TFail, TSuccess}"/> will be in Fail state.
        /// </summary>
        /// <typeparam name="TFail1">Fail value type of the first <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess1">Success value type of the first <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TFail2">Fail value type of the second <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess2">Success value type of second <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <param name="continuation1">First <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <param name="continuation2">Second <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> based on parameters.
        /// </returns>
        public static Continuation<(Option<TFail1>, Option<TFail2>), (TSuccess1, TSuccess2)> All<TFail1, TSuccess1, TFail2, TSuccess2>(
            Continuation<TFail1, TSuccess1> continuation1,
            Continuation<TFail2, TSuccess2> continuation2
            )
        => continuation1.IsSuccess && continuation2.IsSuccess ?
                Resolve<(Option<TFail1>, Option<TFail2>), (TSuccess1, TSuccess2)>((continuation1.Success, continuation2.Success))
                : (continuation1.Fail, continuation2.Fail);

        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail, TSuccess}" /> value according to the <see cref="Continuation{TFail, TSuccess}"/>parameters.
        /// <para>
        /// The new <see cref="Continuation{TFail, TSuccess}"/> will be in Success state only when all parameters are also in this state.
        /// </para>
        /// Otherwise, the new <see cref="Continuation{TFail, TSuccess}"/> will be in Fail state.
        /// </summary>
        /// <typeparam name="TFail1">Fail value type of the first <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess1">Success value type of the first <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TFail2">Fail value type of the second <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess2">Success value type of second <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TFail3">Fail value type of the third <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess3">Success value type of third <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <param name="continuation1">First <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <param name="continuation2">Second <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <param name="continuation3">Third <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> based on parameters.
        /// </returns>
        public static Continuation<(Option<TFail1>, Option<TFail2>, Option<TFail3>), (TSuccess1, TSuccess2, TSuccess3)> All<TFail1, TSuccess1, TFail2, TSuccess2, TFail3, TSuccess3>(
            Continuation<TFail1, TSuccess1> continuation1,
            Continuation<TFail2, TSuccess2> continuation2,
            Continuation<TFail3, TSuccess3> continuation3
            )
        => continuation1.IsSuccess && continuation2.IsSuccess && continuation3.IsSuccess ?
                Resolve<(Option<TFail1>, Option<TFail2>, Option<TFail3>), (TSuccess1, TSuccess2, TSuccess3)>((continuation1.Success, continuation2.Success, continuation3.Success))
                : (continuation1.Fail, continuation2.Fail, continuation3.Fail);

        /// <summary>
        /// Initialize a new instance of <see cref= "Continuation{TFail, TSuccess}" /> value according to the <see cref="Continuation{TFail, TSuccess}"/>parameters.
        /// <para>
        /// The new <see cref="Continuation{TFail, TSuccess}"/> will be in Success state only when all parameters are also in this state.
        /// </para>
        /// Otherwise, the new <see cref="Continuation{TFail, TSuccess}"/> will be in Fail state.
        /// </summary>
        /// <typeparam name="TFail1">Fail value type of the first <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess1">Success value type of the first <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TFail2">Fail value type of the second <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess2">Success value type of second <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TFail3">Fail value type of the third <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess3">Success value type of third <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TFail4">Fail value type of the fourth <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <typeparam name="TSuccess4">Success value type of fourth <see cref="Continuation{TFail, TSuccess}"/></typeparam>
        /// <param name="continuation1">First <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <param name="continuation2">Second <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <param name="continuation3">Third <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <param name="continuation4">Fourth <see cref="Continuation{TFail, TSuccess}"/> value</param>
        /// <returns>
        /// A new instance of <see cref= "Continuation{TFail,TSuccess}" /> based on parameters.
        /// </returns>
        public static Continuation<(Option<TFail1>, Option<TFail2>, Option<TFail3>, Option<TFail4>), (TSuccess1, TSuccess2, TSuccess3, TSuccess4)> All<TFail1, TSuccess1, TFail2, TSuccess2, TFail3, TSuccess3, TFail4, TSuccess4>(
            Continuation<TFail1, TSuccess1> continuation1,
            Continuation<TFail2, TSuccess2> continuation2,
            Continuation<TFail3, TSuccess3> continuation3,
            Continuation<TFail4, TSuccess4> continuation4
            )
        => continuation1.IsSuccess && continuation2.IsSuccess && continuation3.IsSuccess && continuation4.IsSuccess ?
                Resolve<(Option<TFail1>, Option<TFail2>, Option<TFail3>, Option<TFail4>), (TSuccess1, TSuccess2, TSuccess3, TSuccess4)>((continuation1.Success, continuation2.Success, continuation3.Success, continuation4.Success))
                : (continuation1.Fail, continuation2.Fail, continuation3.Fail, continuation4.Fail);
    }
}
