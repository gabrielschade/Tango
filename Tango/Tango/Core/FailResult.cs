using System.Collections.Generic;
using System.Linq;

namespace Tango.Core
{
    public class FailResult
    {
        public IEnumerable<string> Errors { get; }
        public bool HasErrors => Errors.Any();

        public FailResult(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public FailResult(string error)
        {
            Errors = new string[1] { error };
        }
    }
}
