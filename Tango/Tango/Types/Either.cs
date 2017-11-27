using System;
namespace Tango.Types
{
    using static Tango.Functional.FunctionExtensions;
    public struct Either<TLeft, TRight>
    {
        private TLeft Left { get; }
        private TRight Right { get; }
        public bool IsLeft { get; }
        public bool IsRight => !IsLeft;

        public Either(TLeft left)
        {
            IsLeft = true;
            Left = left;
            Right = default(TRight);
        }
        public Either(TRight right)
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

        public T Match2<TLeft2, TRight2, T>(
            Either<TLeft2, TRight2> either2,
            Func<TLeft, TLeft2, T> methodWhenBothLeft,
            Func<TLeft, TRight2, T> methodWhenLeftRight,
            Func<TRight, TLeft2, T> methodWhenRightLeft,
            Func<TRight, TRight2, T> methodWhenBothRight)
            => IsLeft && either2.IsLeft ? methodWhenBothLeft(Left, either2.Left)
                : IsLeft && either2.IsRight ? methodWhenLeftRight(Left, either2.Right)
                : IsRight && either2.IsLeft ? methodWhenRightLeft(Right, either2.Left)
                : methodWhenBothRight(Right, either2.Right);

        public Unit Match(
            Action<TLeft> methodWhenLeft, 
            Action<TRight> methodWhenRight)
            => Match(
                methodWhenLeft.ToFunction(), 
                methodWhenRight.ToFunction()
                );

        public Unit Match2<TLeft2, TRight2>(
            Either<TLeft2, TRight2> either2,
            Action<TLeft, TLeft2> methodWhenBothLeft,
            Action<TLeft, TRight2> methodWhenLeftRight,
            Action<TRight, TLeft2> methodWhenRightLeft,
            Action<TRight, TRight2> methodWhenBothRight)
            => Match2(
                either2,
                methodWhenBothLeft.ToFunction(),
                methodWhenLeftRight.ToFunction(),
                methodWhenRightLeft.ToFunction(),
                methodWhenBothRight.ToFunction());

        public static implicit operator Either<TLeft, TRight>(TLeft left)
            => new Either<TLeft, TRight>(left);

        public static implicit operator Either<TLeft, TRight>(TRight right)
            => new Either<TLeft, TRight>(right);

        public static implicit operator Option<TLeft>(Either<TLeft, TRight> either)
           => either.IsLeft ? either.Left : Option<TLeft>.None();

        public static implicit operator Option<TRight>(Either<TLeft, TRight> either)
           => either.IsRight ? either.Right : Option<TRight>.None();
    }
}
