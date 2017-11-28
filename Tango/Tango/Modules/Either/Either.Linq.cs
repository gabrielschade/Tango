using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tango.Modules;
using Tango.Types;

namespace Tango.Linq
{
    public static class EitherLinqExtensions
    {
        public static (Option<TLeft>, Option<TRight>) ToTuple<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.ToTuple(either);

        public static void IterateLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> action)
            => EitherModule.IterateLeft(action, either);

        public static void IterateRight<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TRight> action)
            => EitherModule.IterateRight(action, either);

        public static void Iterate<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> actionWhenLeft,Action<TRight> actionWhenRight)
            => EitherModule.Iterate(actionWhenLeft, actionWhenRight, either);

        public static bool ExistsLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, bool> predicate)
            => EitherModule.ExistsLeft(predicate, either);

        public static bool ExistsRight<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, bool> predicate)
            => EitherModule.ExistsRight(predicate, either);

        public static bool Exists<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, bool> predicateWhenLeft, Func<TRight, bool> predicateWhenRight)
            => EitherModule.Exists(predicateWhenLeft, predicateWhenRight, either);

        public static Either<TLeftResult, TRight> MapLeft<TLeft, TRight, TLeftResult>(
            this Either<TLeft, TRight> either,
            Func<TLeft, TLeftResult> mapping)
            => EitherModule.MapLeft(mapping, either);

        public static Either<TLeft, TRightResult> MapRight<TLeft, TRight, TRightResult>(
            this Either<TLeft, TRight> either,
            Func<TRight, TRightResult> mapping)
            => EitherModule.MapRight(mapping, either);

        public static Either<TLeftResult, TRightResult> Map<TLeft, TRight, TLeftResult, TRightResult>(
            this Either<TLeft, TRight> either,
            Func<TLeft, TLeftResult> mappingWhenLeft,
            Func<TRight, TRightResult> mappingWhenRight)
            => EitherModule.Map(mappingWhenLeft, mappingWhenRight, either);

        public static TState FoldLeft<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TLeft, TState> folder)
            => EitherModule.FoldLeft(folder, state, either);

        public static TState FoldRight<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TRight, TState> folder)
            => EitherModule.FoldRight(folder, state, either);

        public static TState Fold<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TLeft, TState> folderWhenLeft, Func<TState, TRight, TState> folderWhenRight)
            => EitherModule.Fold(folderWhenLeft, folderWhenRight, state, either);

        public static TState FoldBackLeft<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TLeft, TState, TState> folder, TState state)
            => EitherModule.FoldBackLeft(folder, either, state);

        public static TState FoldBackRight<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TRight, TState, TState> folder, TState state)
            => EitherModule.FoldBackRight(folder, either, state);

        public static TState FoldBack<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TLeft, TState, TState> folderWhenLeft, Func<TRight, TState, TState> folderWhenRight, TState state)
            => EitherModule.FoldBack(folderWhenLeft, folderWhenRight, either, state);

        public static Either<TRight, TLeft> Swap<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.Swap(either);

        public static Option<TLeft> ToOptionLeft<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.ToOptionLeft(either);

        public static Option<TRight> ToOptionRight<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.ToOptionRight(either);
    }
}
