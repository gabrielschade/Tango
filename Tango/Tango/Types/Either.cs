using System;
namespace Tango.Types
{
    using static Tango.Functional.FunctionExtensions;
    public struct Either<TLeft, TRight>
    {
        public TLeft Left { get; }
        public TRight Right { get; }
        public bool IsLeft { get; }
        public bool IsRight => !IsLeft;

        private Either(TLeft left)
        {
            IsLeft = true;
            Left = left;
            Right = default(TRight);
        }
        private Either(TRight right)
        {
            IsLeft = false;
            Right = right;
            Left = default(TLeft);
            
        }

        public T Match<T>(
            Func<TLeft, T> methodWhenLeft, 
            Func<TRight, T> methodWhenRight)
            => IsLeft ?
                methodWhenLeft(Left) 
                : methodWhenRight(Right);

        public Unit Match(
            Action<TLeft> methodWhenLeft, 
            Action<TRight> methodWhenRight)
            => Match(
                methodWhenLeft.ToFunction(), 
                methodWhenRight.ToFunction()
                );

        public static implicit operator Either<TLeft, TRight>(TLeft left)
            => new Either<TLeft, TRight>(left);

        public static implicit operator Either<TLeft, TRight>(TRight right)
            => new Either<TLeft, TRight>(right);
    }
}
