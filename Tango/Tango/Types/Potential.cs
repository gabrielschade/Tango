using System;
using System.Collections.Generic;
using System.Linq;

namespace Tango.Types
{
    public delegate bool RawToTypeConvert<T>(string raw, out T value);
    public struct Potential<T>
    {
        public T Value { get; }
        public string Raw { get; }
        public IEnumerable<string> Errors { get; }
        public bool HasError => Errors.Any();

        public static Potential<T> Map(T value, RawToTypeConvert<T> convertMethod)
            => new Potential<T>(value.ToString(), convertMethod);

        public Potential(string rawValue, RawToTypeConvert<T> convertMethod)
        {
            bool formatError = convertMethod(rawValue, out T value);
            Errors = formatError ? new string[1] { new FormatException().Message }
                                   : Enumerable.Empty<string>();

            Value = value;
            Raw = rawValue;
        }
    }
}
