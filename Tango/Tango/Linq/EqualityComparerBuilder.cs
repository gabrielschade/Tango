using System;
using System.Collections.Generic;

namespace Tango.Linq
{
    public class EqualityComparerBuilder<T> : IEqualityComparer<T>
    {
        public Func<T,T,bool> EqualsMethod { get; }
        public Func<T,int> GetHashCodeMethod { get;}

        private EqualityComparerBuilder(Func<T, T, bool> equalsMethod, Func<T, int> getHashCodeMethod)
        {
            EqualsMethod = equalsMethod;
            GetHashCodeMethod = getHashCodeMethod;
        }

        public static EqualityComparerBuilder<T> Create(Func<T, T, bool> equalsMethod, Func<T, int> getHashCodeMethod)
            => new EqualityComparerBuilder<T>(equalsMethod, getHashCodeMethod);

        public bool Equals(T x, T y)
            => EqualsMethod(x, y);

        public int GetHashCode(T obj)
            => GetHashCodeMethod(obj);
    }
}
