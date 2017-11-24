using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tango.Types.Extensions;

namespace Tango.Exceptions
{
    public enum ExceptionMessages
    {
        [Description("The input \"{0}\" must be positive")]
        MustBePositive
    }

    public static class ExceptionMessagesMethods
    {
        public static string GetMessage(this ExceptionMessages message, params string[] args)
            => string.Format(message.GetDescription(), args);
        
    }
}
