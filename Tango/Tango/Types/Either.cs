using System;
namespace Tango.Types
{
    using static Tango.Functional.FunctionExtensions;

    /// <summary>
    /// Represents a value of one of two possible types 
    /// Instances of <see cref="Either{TLeft, TRight}"/> contains an instance of <typeparamref name="TLeft"/> or <typeparamref name="TRight"/>.
    ///<para>
    /// A common use of <see cref="Either{TLeft, TRight}"/> is as an alternative to <see cref="Option{T}"/> for dealing
    /// with possible missing values. But in this case, <see cref="Option{T}.None"/> is replaced
    /// with a <typeparamref name="TRight"/> which can contain real and useful information.
    /// Convention dictates that <typeparamref name="TLeft"/> is used for success types and <typeparamref name="TRight"/> is used for fail.
    /// </para>
    /// </summary>
    /// <typeparam name="TLeft">The type of the left value</typeparam>
    /// <typeparam name="TRight">The type of the right value</typeparam>
    public struct Either<TLeft, TRight>
    {
        private TLeft Left { get; }
        private TRight Right { get; }

        /// <summary>
        /// Returns true when the value is an <see typeparamref="TLeft"/> value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsLeft { get; }

        /// <summary>
        /// Returns true when the value is an <see typeparamref="TRight"/> value.
        /// Otherwise, returns false.
        /// </summary>
        public bool IsRight => !IsLeft;

        /// <summary>
        /// Initialize a new instance of <see cref="Either{TLeft, TRight}"/> value with an <see typeparamref="TLeft"/> value.
        /// </summary>
        /// <param name="left">The input <see typeparamref="TLeft"/> value.</param>
        public Either(TLeft left)
        {
            IsLeft = true;
            Left = left;
            Right = default(TRight);
        }
        /// <summary>
        /// Initialize a new instance of <see cref="Either{TLeft, TRight}"/> value with an <see typeparamref="TRight"/> value.
        /// </summary>
        /// <param name="right">The input <see typeparamref="TRight"/> value.</param>
        public Either(TRight right)
        {
            IsLeft = false;
            Right = right;
            Left = default(TLeft);

        }

        /// <summary>
        /// Creates an <see typeparamref="TLeft"/> value.
        /// </summary>
        /// <param name="left">The input left value.</param>
        /// <returns>New instance of <see cref="Either{TLeft, TRight}"/> value with <see cref="IsLeft"/> state.</returns>
        public static implicit operator Either<TLeft, TRight>(TLeft left)
            => new Either<TLeft, TRight>(left);

        /// <summary>
        /// Creates an <see typeparamref="TRight"/> value.
        /// </summary>
        /// <param name="right">The input right value.</param>
        /// <returns>New instance of <see cref="Either{TLeft, TRight}"/> value with <see cref="IsRight"/> state.</returns>
        public static implicit operator Either<TLeft, TRight>(TRight right)
            => new Either<TLeft, TRight>(right);

        /// <summary>
        /// Creates an <see cref="Option{T}"/> of <see typeparamref="TLeft"/> value by <see cref="Either{TLeft, TRight}"/> value <see cref="Left"/> property.
        /// </summary>
        /// <param name="either">The input <see cref="Either{TLeft, TRight}"/> value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="Option{T}.IsSome"/> when the <see cref="Either{TLeft, TRight}"/> value is in <see cref="IsLeft"/> state.
        /// Otherwise returns <see cref="Option{T}.None"/>
        /// </returns>
        public static implicit operator Option<TLeft>(Either<TLeft, TRight> either)
           => either.IsLeft ? either.Left : Option<TLeft>.None();

        /// <summary>
        /// Creates an <see cref="Option{T}"/> of <see typeparamref="TRight"/> value by <see cref="Either{TLeft, TRight}"/> value <see cref="Right"/> property.
        /// </summary>
        /// <param name="either">The input <see cref="Either{TLeft, TRight}"/> value.</param>
        /// <returns>New instance of <see cref="Option{T}"/> value with <see cref="Option{T}.IsSome"/> when the <see cref="Either{TLeft, TRight}"/> value is in <see cref="IsRight"/> state.
        /// Otherwise returns <see cref="Option{T}.None"/>
        /// </returns>
        public static implicit operator Option<TRight>(Either<TLeft, TRight> either)
           => either.IsRight ? either.Right : Option<TRight>.None();

        /// <summary>
        /// This allows a sophisticated way to apply some method for <see cref="Either{TLeft, TRight}"/> values without having to check for the existence of a left or right value.
        /// </summary>
        /// <typeparam name="T">The type of value returned by functions <paramref name="methodWhenLeft"/> and <paramref name="methodWhenRight"/>.</typeparam>
        /// <param name="methodWhenLeft">Method that will be invoked when this is in <see cref="IsLeft"/> state.</param>
        /// <param name="methodWhenRight">Method that will be invoked when this is in <see cref="IsRight"/> state.</param>
        /// <returns>
        /// returns the result of the method according to the <see cref="Either{TLeft, TRight}"/> value.
        /// The returns of <paramref name="methodWhenLeft"/> when this is in <see cref="IsLeft"/> state, Otherwise the returns of <paramref name="methodWhenRight"/>
        /// </returns>
        public T Match<T>(
            Func<TLeft, T> methodWhenLeft,
            Func<TRight, T> methodWhenRight)
            => IsLeft ?
                methodWhenLeft(Left)
                : methodWhenRight(Right);

        /// <summary>
        /// This allows a sophisticated way to apply some action for <see cref="Either{TLeft, TRight}"/> values without having to check for the existence of a left or right value.
        /// </summary>
        /// <param name="methodWhenLeft">Method that will be invoked when this is in <see cref="IsLeft"/> state.</param>
        /// <param name="methodWhenRight">Method that will be invoked when this is in <see cref="IsRight"/> state.</param>
        public Unit Match(
            Action<TLeft> methodWhenLeft,
            Action<TRight> methodWhenRight)
            => Match(
                methodWhenLeft.ToFunction(),
                methodWhenRight.ToFunction()
                );

        /// <summary>
        /// This allows a sophisticated way to apply some method for two different <see cref="Either{TLeft, TRight}"/> values without having to check for the existence of a left or right values of any of them.
        /// </summary>
        /// <typeparam name="TLeft2">The type of <see cref="Left"/> value of second <see cref="Either{TLeft, TRight}"/> value.</typeparam>
        /// <typeparam name="TRight2">The type of <see cref="Right"/> value of second <see cref="Either{TLeft, TRight}"/> value.</typeparam>
        /// <typeparam name="T">The type of value returned by all functions of the parameters.</typeparam>
        /// <param name="either2">Second <see cref="Either{TLeft, TRight}"/> value input.</param>
        /// <param name="methodWhenBothLeft">Method that will be invoked when both this and the <paramref name="either2"/> are in <see cref="IsLeft"/> state.</param>
        /// <param name="methodWhenLeftRight">Method that will be invoked when this is in <see cref="IsLeft"/> state the <paramref name="either2"/> is in <see cref="IsRight"/> state.</param>
        /// <param name="methodWhenRightLeft">Method that will be invoked when this is in <see cref="IsRight"/> state the <paramref name="either2"/> is in <see cref="IsLeft"/> state.</param>
        /// <param name="methodWhenBothRight">Method that will be invoked when both this and the <paramref name="either2"/> are in <see cref="IsRight"/> state.</param>
        /// <returns>
        /// returns the result of the method according to the <see cref="Either{TLeft, TRight}"/> values.
        /// </returns>
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

        /// <summary>
        /// This allows a sophisticated way to apply some method for two different <see cref="Either{TLeft, TRight}"/> values without having to check for the existence of a left or right values of any of them.
        /// </summary>
        /// <typeparam name="TLeft2">The type of <see cref="Left"/> value of second <see cref="Either{TLeft, TRight}"/> value.</typeparam>
        /// <typeparam name="TRight2">The type of <see cref="Right"/> value of second <see cref="Either{TLeft, TRight}"/> value.</typeparam>
        /// <param name="either2">Second <see cref="Either{TLeft, TRight}"/> value input.</param>
        /// <param name="methodWhenBothLeft">Method that will be invoked when both this and the <paramref name="either2"/> are in <see cref="IsLeft"/> state.</param>
        /// <param name="methodWhenLeftRight">Method that will be invoked when this is in <see cref="IsLeft"/> state the <paramref name="either2"/> is in <see cref="IsRight"/> state.</param>
        /// <param name="methodWhenRightLeft">Method that will be invoked when this is in <see cref="IsRight"/> state the <paramref name="either2"/> is in <see cref="IsLeft"/> state.</param>
        /// <param name="methodWhenBothRight">Method that will be invoked when both this and the <paramref name="either2"/> are in <see cref="IsRight"/> state.</param>
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

        
    }
}
