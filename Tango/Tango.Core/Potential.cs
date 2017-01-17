using System;
using System.Collections.Generic;
using System.Linq;

namespace Tango.Core
{
    public delegate bool PotencialRawToTypeConvert<T>(string raw, out T value);
    public class Potential<T>
    {
        public T Value { get;}
        public string Raw { get; }
        public IEnumerable<string> Errors { get; }
        public bool HasError => Errors.Any();
        public static Potential<T> Map(T value, PotencialRawToTypeConvert<T> convertMethod)
            => new Potential<T>(value.ToString(), convertMethod);

        public Potential(string rawValue, PotencialRawToTypeConvert<T> convertMethod)
        {
            T value = default(T);
            bool formatError = convertMethod(rawValue, out value);
            Errors = formatError ? new string[1] { new FormatException().Message } 
                                   : Enumerable.Empty<string>();

            Value = value;
            Raw = rawValue;
        }
    }
}
