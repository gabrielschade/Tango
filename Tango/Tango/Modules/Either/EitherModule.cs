using System;
using Tango.Types;

namespace Tango.Modules
{
    /// <summary>
    /// Basic operations on <see cref="Either{TLeft, TRight}"/> values.
    /// </summary>
    public static class EitherModule
    {
        /// <summary>Applies the given function to <see cref="Either{TLeft, TRight}"/> value when <see cref="Either{TLeft, TRight}.IsLeft"/>.</summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="action">The action to apply to left value of input <paramref name="either"/>.</param>
        /// <param name="either">the input either.</param>
        public static void IterateLeft<TLeft, TRight>(Action<TLeft> action, Either<TLeft, TRight> either)
            => Iterate(action, right => new Unit(), either);

        /// <summary>Applies the given function to <see cref="Either{TLeft, TRight}"/> value when <see cref="Either{TLeft, TRight}.IsRight"/>.</summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="action">The action to apply to right value of input <paramref name="either"/>.</param>
        /// <param name="either">the input either.</param>
        public static void IterateRight<TLeft, TRight>(Action<TRight> action, Either<TLeft, TRight> either)
            => Iterate(left => new Unit(), action, either);

        /// <summary>Applies the given functions to <see cref="Either{TLeft, TRight}"/> value depends on its state.</summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="actionWhenLeft">The action to apply to left value of input <paramref name="either"/>.</param>
        /// <param name="actionWhenRight">The action to apply to right value of input <paramref name="either"/>.</param>
        /// <param name="either">the input either.</param>
        public static void Iterate<TLeft, TRight>(Action<TLeft> actionWhenLeft, Action<TRight> actionWhenRight, Either<TLeft, TRight> either)
            => either.Match(actionWhenLeft, actionWhenRight);

        /// <summary>
        ///  Returns true when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/> and given <paramref name="predicate"/> function applied to <paramref name="either"/> return true.
        ///  Otherwise, returns false.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="predicate">A function that evaluates whether the left value contained in the option is valid or not.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns true if the given predicate functions return true when applied to either value.
        /// </returns>
        public static bool ExistsLeft<TLeft, TRight>(Func<TLeft, bool> predicate, Either<TLeft, TRight> either)
            => Exists(predicate, right => false, either);

        /// <summary>
        ///  Returns true when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsRight"/> and given <paramref name="predicate"/> function applied to <paramref name="either"/> return true.
        ///  Otherwise, returns false.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="predicate">A function that evaluates whether the right value contained in the option is valid or not.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns true if the given predicate functions return true when applied to either value.
        /// </returns>
        public static bool ExistsRight<TLeft, TRight>(Func<TRight, bool> predicate, Either<TLeft, TRight> either)
            => Exists(left => false, predicate, either);

        /// <summary>
        ///  Returns true if the given predicate functions return true when applied to either value.
        ///  Otherwise, returns false.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="predicateWhenLeft">A function that evaluates whether the left value contained in the option is valid or not.</param>
        /// <param name="predicateWhenRight">A function that evaluates whether the right value contained in the option is valid or not.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns true if the given predicate functions return true when applied to either value.
        /// </returns>
        public static bool Exists<TLeft, TRight>(Func<TLeft, bool> predicateWhenLeft, Func<TRight, bool> predicateWhenRight, Either<TLeft, TRight> either)
            => either.Match(predicateWhenLeft, predicateWhenRight);

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given <paramref name="mapping"/> function when <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise, returns the input <paramref name="either"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TLeftResult">The type of the left value returned by mapping functions.</typeparam>
        /// <param name="mapping">The function to transform either value when <see cref="Either{TLeft, TRight}.IsLeft"/>.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given mapping functions.
        /// </returns>
        public static Either<TLeftResult, TRight> MapLeft<TLeft, TRight, TLeftResult>(
            Func<TLeft, TLeftResult> mapping,
            Either<TLeft, TRight> either)
            => Map(mapping, right => right, either);

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given <paramref name="mapping"/> function when <see cref="Either{TLeft, TRight}.IsRight"/>.
        /// Otherwise, returns the input <paramref name="either"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TRightResult">The type of the right value returned by mapping functions</typeparam>
        /// <param name="mapping">The function to transform either value when <see cref="Either{TLeft, TRight}.IsRight"/>.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given mapping functions.
        /// </returns>
        public static Either<TLeft, TRightResult> MapRight<TLeft, TRight, TRightResult>(
            Func<TRight, TRightResult> mapping,
            Either<TLeft, TRight> either)
            => Map(left => left, mapping, either);

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given mapping functions.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TLeftResult">The type of the left value returned by mapping functions.</typeparam>
        /// <typeparam name="TRightResult">The type of the right value returned by mapping functions</typeparam>
        /// <param name="mappingWhenLeft">The function to transform either value when <see cref="Either{TLeft, TRight}.IsLeft"/>.</param>
        /// <param name="mappingWhenRight">The function to transform either value when <see cref="Either{TLeft, TRight}.IsRight"/>.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given mapping functions.
        /// </returns>
        public static Either<TLeftResult, TRightResult> Map<TLeft, TRight, TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> mappingWhenLeft,
            Func<TRight, TRightResult> mappingWhenRight,
            Either<TLeft, TRight> either)
            => either.Match<Either<TLeftResult, TRightResult>>(
                left => mappingWhenLeft(left),
                right => mappingWhenRight(right));

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <paramref name="state"/> and <see cref="Either{TLeft, TRight}"/> value when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise, returns the <paramref name="state"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given folder functions
        /// to <paramref name="state"/> and <see cref="Either{TLeft, TRight}"/> value <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise, returns the input <paramref name="either"/>.
        /// </returns>
        public static TState FoldLeft<TLeft, TRight, TState>(Func<TState, TLeft, TState> folder, TState state, Either<TLeft, TRight> either)
            => Fold(folder, (_state, right) => _state, state, either);

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <paramref name="state"/> and <see cref="Either{TLeft, TRight}"/> value when <paramref name="either"/><see cref="Either{TLeft, TRight}.IsRight"/>.
        /// Otherwise, returns the <paramref name="state"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given folder functions
        /// to <paramref name="state"/> and <see cref="Either{TLeft, TRight}"/> value <see cref="Either{TLeft, TRight}.IsRight"/>.
        /// Otherwise, returns the input <paramref name="either"/>.
        /// </returns>
        public static TState FoldRight<TLeft, TRight, TState>(Func<TState, TRight, TState> folder, TState state, Either<TLeft, TRight> either)
            => Fold((_state, left) => _state, folder, state, either);

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given folder functions
        /// to <paramref name="state"/> and <see cref="Either{TLeft, TRight}"/> value.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folderWhenLeft">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="folderWhenRight">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given folder functions
        /// to <paramref name="state"/> and <see cref="Either{TLeft, TRight}"/> value.
        /// </returns>
        public static TState Fold<TLeft, TRight, TState>(Func<TState, TLeft, TState> folderWhenLeft, Func<TState, TRight, TState> folderWhenRight, TState state, Either<TLeft, TRight> either)
            => either.Match(
                left => folderWhenLeft(state, left),
                right => folderWhenRight(state, right));

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <see cref="Either{TLeft, TRight}"/> value and <paramref name="state"/> when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise, returns the <paramref name="state"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <see cref="Either{TLeft, TRight}"/> value and <paramref name="state"/> when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise, returns the input <paramref name="either"/>.
        /// </returns>
        public static TState FoldBackLeft<TLeft, TRight, TState>(Func<TLeft, TState, TState> folder, Either<TLeft, TRight> either, TState state)
            => FoldBack(folder, (right, _state) => _state, either, state);

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <see cref="Either{TLeft, TRight}"/> value and <paramref name="state"/> when the <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsRight"/>.
        /// Otherwise, returns the <paramref name="state"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given <paramref name="folder"/> function
        /// to <see cref="Either{TLeft, TRight}"/> value and <paramref name="state"/> when the <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsRight"/>.
        /// Otherwise, returns the input <paramref name="either"/>.
        /// </returns>
        public static TState FoldBackRight<TLeft, TRight, TState>(Func<TRight, TState, TState> folder, Either<TLeft, TRight> either, TState state)
            => FoldBack((left, _state) => _state, folder, either, state);

        /// <summary>
        /// Creates a new <typeparamref name="TState"/> value by applying the given folder functions
        /// to <see cref="Either{TLeft, TRight}"/> value and <paramref name="state"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folderWhenLeft">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="folderWhenRight">The function to transform either value from the input <paramref name="either"/> to a new state value.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <typeparamref name="TState"/> value by applying the given folder functions
        /// to <see cref="Either{TLeft, TRight}"/> value and <paramref name="state"/>
        /// </returns>
        public static TState FoldBack<TLeft, TRight, TState>(Func<TLeft, TState, TState> folderWhenLeft, Func<TRight, TState, TState> folderWhenRight, Either<TLeft, TRight> either, TState state)
            => either.Match(
                left => folderWhenLeft(left, state),
                right => folderWhenRight(right, state));

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> value by swapping <see cref="Either{TLeft, TRight}.Left"/> and <see cref="Either{TLeft, TRight}.Right"/> values.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <see cref="Either{TLeft, TRight}"/> value by swapping <see cref="Either{TLeft, TRight}.Left"/> and <see cref="Either{TLeft, TRight}.Right"/> values.
        /// </returns>
        public static Either<TRight, TLeft> Swap<TLeft, TRight>(Either<TLeft, TRight> either)
            => either.Match<Either<TRight, TLeft>>(
                left => left,
                right => right);

        /// <summary>
        /// Convert the <see cref="Either{TLeft, TRight}"/> to a <see cref="Tuple{T1,T2}"/> where T1 and T2 are <see cref="Option{T}"/> value of <typeparamref name="TLeft"/> and <typeparamref name="TRight"/> respectively.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="either">The input either.</param>
        /// <returns>
        /// Returns a <see cref="Tuple{T1,T2}"/> where T1 and T2 are <see cref="Option{T}"/> value of <typeparamref name="TLeft"/> and <typeparamref name="TRight"/> respectively.
        /// The Tuple left value (Item1) is <see cref="Option{T}.Some(T)"/> when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/>, otherwise is <see cref="Option{T}.None"/>
        /// The Tuple right value (Item2) is <see cref="Option{T}.Some(T)"/> when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsRight"/>, otherwise is <see cref="Option{T}.None"/>
        /// </returns>
        public static (Option<TLeft> Left, Option<TRight> Right) ToTuple<TLeft, TRight>(Either<TLeft, TRight> either)
            => (Left: either, Right: either);

        /// <summary>
        /// Convert the <see cref="Either{TLeft, TRight}"/> to a <see cref="Option{T}"/> where T is <typeparamref name="TLeft"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="either">The input either.</param>
        /// <returns>
        /// Returns an <see cref="Option{T}"/> where T is <typeparamref name="TLeft"/>. 
        /// The option value state is <see cref="Option{T}.Some(T)"/> when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsLeft"/>, otherwise is <see cref="Option{T}.None"/>
        /// </returns>
        public static Option<TLeft> ToOptionLeft<TLeft, TRight>(Either<TLeft, TRight> either)
            => either.Match(
                left => left,
                right => Option<TLeft>.None()
                );

        /// <summary>
        /// Convert the <see cref="Either{TLeft, TRight}"/> to a <see cref="Option{T}"/> where T is <typeparamref name="TRight"/>.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="either">The input either.</param>
        /// <returns>
        /// Returns an <see cref="Option{T}"/> where T is <typeparamref name="TRight"/>. 
        /// The option value state is <see cref="Option{T}.Some(T)"/> when <paramref name="either"/> <see cref="Either{TLeft, TRight}.IsRight"/>, otherwise is <see cref="Option{T}.None"/>
        /// </returns>
        public static Option<TRight> ToOptionRight<TLeft, TRight>(Either<TLeft, TRight> either)
            => either.Match(
                left => Option<TRight>.None(),
                right => right);
    }
}
