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

        public static void Iterate<TLeft, TRight>(this Either<TLeft, TRight> either,Action<TLeft> actionWhenLeft, Action<TRight> actionWhenRight)
            => EitherModule.Iterate(actionWhenLeft,actionWhenRight,either);

        public static bool Exists<TLeft, TRight>(this Either<TLeft, TRight> either,Func<TLeft, bool> predicateWhenLeft, Func<TRight, bool> predicateWhenRight)
            => EitherModule.Exists(predicateWhenLeft, predicateWhenRight, either);

        public static Either<TLeftResult, TRightResult> Map<TLeft, TRight, TLeftResult, TRightResult>(
            this Either<TLeft, TRight> either,
            Func<TLeft, TLeftResult> mappingWhenLeft,
            Func<TRight, TRightResult> mappingWhenRight)
            => EitherModule.Map(mappingWhenLeft, mappingWhenRight, either);

        public static TState Fold<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TLeft, TState> folderWhenLeft, Func<TState, TRight, TState> folderWhenRight)
            => EitherModule.Fold(folderWhenLeft, folderWhenRight, state, either);

        public static TState FoldBack<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TLeft, TState, TState> folderWhenLeft, Func<TRight, TState, TState> folderWhenRight, TState state)
            => EitherModule.FoldBack(folderWhenLeft, folderWhenRight, either, state);
    }
}
