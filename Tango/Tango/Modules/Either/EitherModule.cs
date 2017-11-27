using System;
using Tango.Types;

namespace Tango.Modules
{
    public static class EitherModule
    {
        public static (Option<TLeft>, Option<TRight>) ToTuple<TLeft, TRight>(Either<TLeft, TRight> either)
            => (either, either);

        public static void Iterate<TLeft, TRight>(Action<TLeft> actionWhenLeft, Action<TRight> actionWhenRight, Either<TLeft, TRight> either)
            => either.Match(actionWhenLeft, actionWhenRight);

        public static bool Exists<TLeft, TRight>(Func<TLeft, bool> predicateWhenLeft, Func<TRight, bool> predicateWhenRight, Either<TLeft, TRight> either)
            => either.Match(predicateWhenLeft, predicateWhenRight);

        public static Either<TLeftResult, TRightResult> Map<TLeft, TRight, TLeftResult, TRightResult>(
            Func<TLeft, TLeftResult> mappingWhenLeft,
            Func<TRight, TRightResult> mappingWhenRight,
            Either<TLeft, TRight> either)
            => either.Match<Either<TLeftResult,TRightResult>>(
                left => mappingWhenLeft(left), 
                right => mappingWhenRight(right));

        public static TState Fold<TLeft, TRight, TState>(Func<TState, TLeft, TState> folderWhenLeft, Func<TState, TRight, TState> folderWhenRight, TState state, Either<TLeft, TRight> either)
            => either.Match(
                left => folderWhenLeft(state, left),
                right => folderWhenRight(state, right));

        public static TState FoldBack<TLeft, TRight, TState>(Func<TLeft, TState, TState> folderWhenLeft, Func<TRight, TState, TState> folderWhenRight, Either<TLeft, TRight> either, TState state)
            => either.Match(
                left => folderWhenLeft(left, state),
                right => folderWhenRight(right, state));
    }
}
