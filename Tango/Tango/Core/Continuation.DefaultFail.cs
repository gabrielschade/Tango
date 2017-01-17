using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tango.Core
{
    public class Continuation<TSuccess> : Continuation<TSuccess, FailResult>
    {
        public Continuation(TSuccess success)
            :base(success)
        {}

        public Continuation(FailResult fail)
            :base(fail)
        {}
    }
}
