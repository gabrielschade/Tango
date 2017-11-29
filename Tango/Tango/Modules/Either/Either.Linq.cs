using System;
using Tango.Modules;
using Tango.Types;

namespace Tango.Linq
{
    /// <summary>
    /// Basic operations on <see cref="Either{TLeft, TRight}"/> as extension methods.
    /// The original operations are in <see cref="EitherModule"/>.
    /// </summary>
    public static class EitherLinqExtensions
    {
        /// <summary>Applies the given function to <see cref="Either{TLeft, TRight}"/> value when <see cref="Either{TLeft, TRight}.IsLeft"/>.</summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="action">The action to apply to left value of input <paramref name="either"/>.</param>
        /// <param name="either">the input either.</param>
        public static void IterateLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> action)
            => EitherModule.IterateLeft(action, either);

        /// <summary>Applies the given function to <see cref="Either{TLeft, TRight}"/> value when <see cref="Either{TLeft, TRight}.IsRight"/>.</summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="action">The action to apply to right value of input <paramref name="either"/>.</param>
        /// <param name="either">the input either.</param>
        public static void IterateRight<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TRight> action)
            => EitherModule.IterateRight(action, either);

        /// <summary>Applies the given functions to <see cref="Either{TLeft, TRight}"/> value depends on its state.</summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="actionWhenLeft">The action to apply to left value of input <paramref name="either"/>.</param>
        /// <param name="actionWhenRight">The action to apply to right value of input <paramref name="either"/>.</param>
        /// <param name="either">the input either.</param>
        public static void Iterate<TLeft, TRight>(this Either<TLeft, TRight> either, Action<TLeft> actionWhenLeft,Action<TRight> actionWhenRight)
            => EitherModule.Iterate(actionWhenLeft, actionWhenRight, either);

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
        public static bool ExistsLeft<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, bool> predicate)
            => EitherModule.ExistsLeft(predicate, either);

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
        public static bool ExistsRight<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TRight, bool> predicate)
            => EitherModule.ExistsRight(predicate, either);

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
        public static bool Exists<TLeft, TRight>(this Either<TLeft, TRight> either, Func<TLeft, bool> predicateWhenLeft, Func<TRight, bool> predicateWhenRight)
            => EitherModule.Exists(predicateWhenLeft, predicateWhenRight, either);

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given <paramref name="mapping"/> function when <see cref="Either{TLeft, TRight}.IsLeft"/>.
        /// Otherwise, returns itself.
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
            this Either<TLeft, TRight> either,
            Func<TLeft, TLeftResult> mapping)
            => EitherModule.MapLeft(mapping, either);

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> whose value is the result of applying the given <paramref name="mapping"/> function when <see cref="Either{TLeft, TRight}.IsRight"/>.
        /// Otherwise, returns itself.
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
            this Either<TLeft, TRight> either,
            Func<TRight, TRightResult> mapping)
            => EitherModule.MapRight(mapping, either);

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
            this Either<TLeft, TRight> either,
            Func<TLeft, TLeftResult> mappingWhenLeft,
            Func<TRight, TRightResult> mappingWhenRight)
            => EitherModule.Map(mappingWhenLeft, mappingWhenRight, either);

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
        public static TState FoldLeft<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TLeft, TState> folder)
            => EitherModule.FoldLeft(folder, state, either);

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
        public static TState FoldRight<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TRight, TState> folder)
            => EitherModule.FoldRight(folder, state, either);

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
        public static TState Fold<TLeft, TRight, TState>(this Either<TLeft, TRight> either, TState state, Func<TState, TLeft, TState> folderWhenLeft, Func<TState, TRight, TState> folderWhenRight)
            => EitherModule.Fold(folderWhenLeft, folderWhenRight, state, either);

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
        public static TState FoldBackLeft<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TLeft, TState, TState> folder, TState state)
            => EitherModule.FoldBackLeft(folder, either, state);

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
        public static TState FoldBackRight<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TRight, TState, TState> folder, TState state)
            => EitherModule.FoldBackRight(folder, either, state);

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
        public static TState FoldBack<TLeft, TRight, TState>(this Either<TLeft, TRight> either, Func<TLeft, TState, TState> folderWhenLeft, Func<TRight, TState, TState> folderWhenRight, TState state)
            => EitherModule.FoldBack(folderWhenLeft, folderWhenRight, either, state);

        /// <summary>
        /// Creates a new <see cref="Either{TLeft, TRight}"/> value by swapping <see cref="Either{TLeft, TRight}.Left"/> and <see cref="Either{TLeft, TRight}.Right"/> values.
        /// </summary>
        /// <typeparam name="TLeft">The type of the left value.</typeparam>
        /// <typeparam name="TRight">The type of the right value.</typeparam>
        /// <param name="either">the input either.</param>
        /// <returns>
        /// Returns a new <see cref="Either{TLeft, TRight}"/> value by swapping <see cref="Either{TLeft, TRight}.Left"/> and <see cref="Either{TLeft, TRight}.Right"/> values.
        /// </returns>
        public static Either<TRight, TLeft> Swap<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.Swap(either);

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
        public static (Option<TLeft>, Option<TRight>) ToTuple<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.ToTuple(either);

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
        public static Option<TLeft> ToOptionLeft<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.ToOptionLeft(either);

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
        public static Option<TRight> ToOptionRight<TLeft, TRight>(this Either<TLeft, TRight> either)
            => EitherModule.ToOptionRight(either);
    }
}
