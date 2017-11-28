using System;
using Tango.Types;

namespace Tango.Modules
{
    public static class EitherModule
    {
        public static (Option<TLeft> Left, Option<TRight> Right) ToTuple<TLeft, TRight>(Either<TLeft, TRight> either)
            => (Left: either, Right: either);

        public static void IterateLeft<TLeft, TRight>(Action<TLeft> action, Either<TLeft, TRight> either)
            => Iterate(action, right => new Unit(), either);

        public static void IterateRight<TLeft, TRight>(Action<TRight> action, Either<TLeft, TRight> either)
            => Iterate(left => new Unit(), action, either);

        public static void Iterate<TLeft, TRight>(Action<TLeft> actionWhenLeft, Action<TRight> actionWhenRight, Either<TLeft, TRight> either)
            => either.Match(actionWhenLeft, actionWhenRight);

        public static bool ExistsLeft<TLeft, TRight>(Func<TLeft, bool> predicate, Either<TLeft, TRight> either)
            => Exists(predicate, right => false, either);

        public static bool ExistsRight<TLeft, TRight>(Func<TRight, bool> predicate, Either<TLeft, TRight> either)
            => Exists(left => false, predicate, either);

        public static bool Exists<TLeft, TRight>(Func<TLeft, bool> predicateWhenLeft, Func<TRight, bool> predicateWhenRight, Either<TLeft, TRight> either)
            => either.Match(predicateWhenLeft, predicateWhenRight);

        public static Either<TLeftResult, TRight> MapLeft<TLeft, TRight, TLeftResult>(
            Func<TLeft, TLeftResult> mapping,
            Either<TLeft, TRight> either)
            => Map(mapping, right => right, either);

        public static Either<TLeft, TRightResult> MapRight<TLeft, TRight, TRightResult>(
            Func<TRight, TRightResult> mapping,
            Either<TLeft, TRight> either)
            => Map(left => left, mapping, either);

        public static Either<TLeftResult, TRightResult> Map<TLeft, TRight, TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> mappingWhenLeft,
            Func<TRight, TRightResult> mappingWhenRight,
            Either<TLeft, TRight> either)
            => either.Match<Either<TLeftResult, TRightResult>>(
                left => mappingWhenLeft(left),
                right => mappingWhenRight(right));

        public static TState FoldLeft<TLeft, TRight, TState>(Func<TState, TLeft, TState> folder, TState state, Either<TLeft, TRight> either)
            => Fold(folder, (_state, right) => _state, state, either);

        public static TState FoldRight<TLeft, TRight, TState>(Func<TState, TRight, TState> folder, TState state, Either<TLeft, TRight> either)
            => Fold((_state, left) => _state, folder, state, either);

        public static TState Fold<TLeft, TRight, TState>(Func<TState, TLeft, TState> folderWhenLeft, Func<TState, TRight, TState> folderWhenRight, TState state, Either<TLeft, TRight> either)
            => either.Match(
                left => folderWhenLeft(state, left),
                right => folderWhenRight(state, right));

        public static TState FoldBackLeft<TLeft, TRight, TState>(Func<TLeft, TState, TState> folder, Either<TLeft, TRight> either, TState state)
            => FoldBack(folder, (right, _state) => _state, either, state);

        public static TState FoldBackRight<TLeft, TRight, TState>(Func<TRight, TState, TState> folder, Either<TLeft, TRight> either, TState state)
            => FoldBack((left, _state) => _state, folder, either, state);

        public static TState FoldBack<TLeft, TRight, TState>(Func<TLeft, TState, TState> folderWhenLeft, Func<TRight, TState, TState> folderWhenRight, Either<TLeft, TRight> either, TState state)
            => either.Match(
                left => folderWhenLeft(left, state),
                right => folderWhenRight(right, state));

        public static Either<TRight, TLeft> Swap<TLeft, TRight>(Either<TLeft, TRight> either)
            => either.Match<Either<TRight, TLeft>>(
                left => left,
                right => right);

        public static Option<TLeft> ToOptionLeft<TLeft, TRight>(Either<TLeft, TRight> either)
            => either.Match(
                left => left,
                right => Option<TLeft>.None()
                );

        public static Option<TRight> ToOptionRight<TLeft, TRight>(Either<TLeft, TRight> either)
            => either.Match(
                left => Option<TRight>.None(),
                right => right);
    }
}
