using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Exceptions;
using Tango.Functional;
using Tango.Linq;
using Tango.Types;

namespace Tango.Modules
{
    /// <summary>
    /// Basic operations on <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class CollectionModule
    {
        /// <summary>Returns a new collection that contains the elements of the first collection
        /// followed by elements of the second.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source1">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>The resulting collection.</returns>
        public static IEnumerable<T> Append<T>(IEnumerable<T> source1, IEnumerable<T> source2)
            => source1.Concat(source2);


        /// <summary>Applies the given function to each element of the collection. Returns
        /// the collection comprised of the results <typeparamref name="TResult"/> for each element where
        /// the function returns <see cref="Option{TResult}.Some(TResult)"/></summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of option value returned by <paramref name="chooser"/>.</typeparam>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection comprising the values selected from the chooser function.</returns>
        public static IEnumerable<TResult> Choose<T, TResult>(Func<T, Option<TResult>> chooser, IEnumerable<T> source)
            => source.Map(chooser)
                   .Filter(optionResult => optionResult.IsSome)
                   .Map(optionResult => optionResult.Match(
                        some => some,
                        () => default(TResult)));

        /// <summary>Divides the input collection into chunks of size at most <paramref name="chunkSize"/>.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="chunkSize">The maximum size of each chunk.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection divided into chunks.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="chunkSize"/> is not positive.</exception>
        public static IEnumerable<IEnumerable<T>> ChunkBySize<T>(int chunkSize, IEnumerable<T> source)
        {
            if (chunkSize <= 0)
                throw new ArgumentException(ExceptionMessages.MustBePositive.GetMessage(nameof(chunkSize)));

            return source.MapIndexed((index, element) => new { Index = index, Value = element })
                       .GroupBy(element => element.Index / chunkSize)
                       .Map(groupedElement => groupedElement.Map(element => element.Value));
        }


        /// <summary>Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform each input element into a subcollection to be concatenated.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>An <see cref="IEnumerable{TResult}"/> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        public static IEnumerable<TResult> Collect<T, TResult>(Func<T, IEnumerable<TResult>> mapping, IEnumerable<T> source)
            => source.SelectMany(mapping);

        /// <summary>Compares two collections using the given comparison function, element by element.
        /// Returns the first non-zero result from the comparison function.  
        /// </summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="comparer">A function that takes an element from each collection and returns an int.
        /// If it evaluates to a non-zero value iteration is stopped and that value is returned.</param>
        /// <param name="source1">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        ///
        /// <returns>The first non-zero value from the comparison function.</returns>
        public static int CompareWith<T>(Func<T, T, int> comparer, IEnumerable<T> source1, IEnumerable<T> source2)
        {
            int result = 0;
            Map2((element1, element2) =>
               {
                   result = comparer(element1, element2);
                   return result;
               }, source1, source2, () => result == 0).Count();
            return result;
        }

        /// <summary>Applies a key-generating function to each element of a collection and returns a collection yielding unique
        /// keys and their number of occurrences in the original collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TKey">The type of key generated by projection function.</typeparam>
        /// <param name="projection">A function transforming each item of the input collection into a key to be
        /// compared against the others.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The result collection is a tuple with key and the quantity of elements with this same key.</returns>
        public static IEnumerable<(TKey Key, int Count)> CountBy<T, TKey>(Func<T, TKey> projection, IEnumerable<T> source)
            => source.Map(element => new { Key = projection(element), Value = element })
                       .GroupBy(element => element.Key)
                       .Map(groupedElements => (Key: groupedElements.Key, Count: groupedElements.Count()));

        /// <summary>Returns a new collection that contains the elements of each the collection in order.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="sources">The input sequence of collections.</param>
        /// <returns>The resulting concatenated collection.</returns>
        public static IEnumerable<T> Concat<T>(params IEnumerable<T>[] sources)
        => sources.Reduce((first, second) => first.Append(second));

        /// <summary>Returns a new collection that contains the elements of each the collection in order.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="sources">The input sequence of collections.</param>
        /// <returns>The resulting concatenated collection.</returns>
        public static IEnumerable<T> Concat<T>(IEnumerable<IEnumerable<T>> sources)
            => sources.Reduce((first, second) => first.Append(second));

        /// <summary>Returns a collection that contains no duplicate entries according to <paramref name="comparer"/> and <paramref name="hashCodeGetter"/> functions.
        /// If an element occurs multiple times in the collection then the later occurrences are discarded.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="comparer">A function comparing each element of the input collection against the others.</param>
        /// <param name="hashCodeGetter">A function to generate the hash code of each elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The result collection.</returns>
        public static IEnumerable<T> Distinct<T>(Func<T, T, bool> comparer, Func<T, int> hashCodeGetter, IEnumerable<T> source)
            => source.Distinct(EqualityComparerBuilder<T>.Create(comparer, hashCodeGetter));

        /// <summary>Returns an empty collection of the given type.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <returns>An empty collection of the given type.</returns>
        public static IEnumerable<T> Empty<T>()
            => Enumerable.Empty<T>();

        /// <summary>Tests if any element of the collection satisfies the given <paramref name="predicate"/>.</summary>
        /// <remarks>The <paramref name="predicate"/> is applied to the elements of the input <paramref name="source"/>. If any application 
        /// returns true then the overall result is true and no further elements are tested. 
        /// Otherwise, false is returned.
        /// </remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>True if any element satisfies the predicate, otherwise, false.</returns>
        public static bool Exists<T>(Func<T, bool> predicate, IEnumerable<T> source)
            => source.Any(predicate);

        /// <summary>Tests if any pair of corresponding elements of the collections satisfies the given <paramref name="predicate"/>.</summary>
        /// <remarks>The <paramref name="predicate"/> is applied to matching elements in the two collections up to the lesser of the 
        /// two lengths of the collections. If any application returns true then the overall result is 
        /// true and no further elements are tested.
        /// Otherwise, false is returned.
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source1">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>True if any pair of elements satisfy the predicate.</returns>
        public static bool Exists2<T>(Func<T, T, bool> predicate, IEnumerable<T> source1, IEnumerable<T> source2)
        {
            bool result = false;
            Map2(
                (element1, element2) =>
                {
                    result |= predicate(element1, element2);
                    return result;
                }
                , source1, source2, () => !result).Count();
            return result;
        }

        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns true.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>A collection containing only the elements that satisfy the <paramref name="predicate"/>.</returns>
        public static IEnumerable<T> Filter<T>(Func<T, bool> predicate, IEnumerable<T> source)
            => source.Where(predicate);

        /// <summary>Returns the index of the first element in the collection
        /// that satisfies the given <paramref name="predicate"/>.
        /// Raises <c>InvalidOperationException</c> if no such element exists.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown if the <paramref name="predicate"/> evaluates to false for all the
        /// elements of the collection.</exception>
        /// <returns>The index of the first element that satisfies the predicate.</returns>
        public static int FindIndex<T>(Func<T, bool> predicate, IEnumerable<T> source)
         => source.MapIndexed((currentIndex, element) => (currentIndex, predicate(element)))
                  .First(element => element.Item2)
                  .Item1;

        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. Take the second argument, and apply the function to it
        /// and the first element of the collection. Then feed this result into the function along
        /// with the second element and so on.<para>
        /// If the input function is <c>f</c> and the elements are <c>i0...iN</c> then 
        /// computes <c>f (... (f s i0) i1 ...) iN</c>.</para></summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The final state value.</returns>
        public static TState Fold<T, TState>(Func<TState, T, TState> folder, TState state, IEnumerable<T> source)
            => Scan(folder, state, source).Last();

        /// <summary>Applies a function to corresponding elements of two collections, threading an accumulator argument
        /// through the computation.
        /// <para>
        /// If the input function is <c>f</c> and the elements are <c>i0...iN</c> and <c>j0...jN</c>
        /// then computes <c>f (... (f s i0 j0)...) iN jN</c>.</para>
        /// </summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.
        /// </remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>The final state value.</returns>
        public static TState Fold2<T, T2, TState>(Func<TState, T, T2, TState> folder, TState state, IEnumerable<T> source, IEnumerable<T2> source2)
            => Scan2(folder, state, source, source2).Last();

        /// <summary>Applies a function to each element of the collection, starting from the end, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then 
        /// computes <c>f i0 (...(f iN s))</c>.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The state object after the folding function is applied to each element of the collection.</returns>
        public static TState FoldBack<T, TState>(Func<T, TState, TState> folder, IEnumerable<T> source, TState state)
        => source.Reverse()
                 .Fold(state, (accumulator, element) => folder(element, accumulator));

        /// <summary>Applies a function to corresponding elements of two collections, threading an accumulator argument
        /// through the computation.
        /// If the input function is <c>f</c> and the elements are <c>i0...iN</c> and <c>j0...jN</c>
        /// then computes <c>f i0 j0 (...(f iN jN s))</c>.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than the other then the loop runs only run until the smallest collection length.
        /// </remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The final state value.</returns>
        public static TState FoldBack2<T, T2, TState>(Func<T, T2, TState, TState> folder, IEnumerable<T> source, IEnumerable<T2> source2, TState state)
            => source.Reverse()
                     .Fold2(source2.Reverse(),
                            state,
                            (accumulator, element1, element2) => folder(element1, element2, accumulator));

        /// <summary>Tests if all elements of the collection satisfy the given <paramref name="predicate"/>.</summary>
        /// <remarks>The <paramref name="predicate"/> is applied to the elements of the input collection. If any application 
        /// returns false then the overall result is false and no further elements are tested. 
        /// Otherwise, true is returned.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.</remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>True if all of the elements satisfy the predicate.</returns>
        public static bool ForAll<T>(Func<T, bool> predicate, IEnumerable<T> source)
        {
            bool result = true;
            Map((element) =>
            {
                result = predicate(element);
                return result;
            }, source, () => result).Count();
            return result;
        }


        /// <summary>Tests if all corresponding elements of the collection satisfy the given <paramref name="predicate"/> pairwise.</summary>
        ///
        /// <remarks>The predicate is applied to matching elements in the two collections up to the lesser of the 
        /// two lengths of the collections. If any application returns false then the overall result is 
        /// false and no further elements are tested.
        /// Otherwise, true is returned.
        /// if one collections is longer 
        /// than the other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>True if all of the pairs of elements satisfy the predicate.</returns>
        public static bool ForAll2<T, T2>(Func<T, T2, bool> predicate, IEnumerable<T> source, IEnumerable<T2> source2)
        {
            bool result = true;
            Map2((element1, element2) =>
            {
                result = predicate(element1, element2);
                return result;
            }, source, source2, () => result).Count();
            return result;
        }

        /// <summary>Tests if all corresponding elements of the collection satisfy the given <paramref name="predicate"/>.</summary>
        ///
        /// <remarks>The <paramref name="predicate"/> is applied to matching elements in the three collections up to the lesser of the 
        /// three lengths of the collections. If any application returns false then the overall result is 
        /// false and no further elements are tested.
        /// Otherwise, true is returned.
        /// if one collections is longer 
        /// than the other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="T3">The type of elements of third source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="source3">The third input collection.</param>
        /// <returns>True if all of the triples of elements satisfy the predicate.</returns>
        public static bool ForAll3<T, T2, T3>(Func<T, T2, T3, bool> predicate, IEnumerable<T> source, IEnumerable<T2> source2, IEnumerable<T3> source3)
        {
            bool result = true;
            Map3((element1, element2, element3) =>
            {
                result = predicate(element1, element2, element3);
                return result;
            }, source, source2, source3, () => result).Count();
            return result;
        }
        /// <summary>Returns the first element of the collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>The first element of the collection.</returns>
        public static T Head<T>(IEnumerable<T> source)
            => source.First();

        /// <summary>Returns the first and the last element of the collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>A Tuple with first (Head) and last (TailEnd) element of the <paramref name="source"/>.</returns>
        public static (T Head, T TailEnd) HeadAndTailEnd<T>(IEnumerable<T> source)
            => (Head: source.First(), TailEnd: source.Last());

        /// <summary>Creates a collection of integers with all values between <paramref name="first"/> and <paramref name="second"/> parameters.</summary>
        /// <param name="first">First value of the Range.</param>
        /// <param name="second">Second value of the Range.</param>
        /// <returns>The collection of generated elements.</returns>
        public static IEnumerable<int> Range(int first, int second)
        {
            IEnumerable<int> Generate(
                int start,
                int end,
                Func<int, bool> condition,
                Func<int, int> step)
            {
                for (int value = start; condition(value); value = step(value))
                    yield return value;
            };

            return first < second ?
                 Generate(first, second, (value) => value <= second, (value) => value + 1)
                 : Generate(first, second, (value) => value >= second, (value) => value - 1);
        }

        /// <summary>Creates a collection by by the given values</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="values">Elements of the collection.</param>
        /// <returns>The collection of generated elements.</returns>
        public static IEnumerable<T> Generate<T>(params T[] values)
        {
            foreach (T value in values)
                yield return value;
        }

        /// <summary>Creates a collection by calling the given <paramref name="initializer"/> on each index.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="length">The length of the collection to generate.</param>
        /// <param name="initializer">The function to generate an element from an index.</param>
        /// <returns>The collection of generated elements.</returns>
        public static IEnumerable<T> Initialize<T>(int length, Func<int, T> initializer)
            => Replicate(length, default(T))
                .MapIndexed((index, element) => initializer(index));


        /// <summary>Applies the given function to each element of the collection.</summary>
        /// <remarks>This function causes <see cref="IEnumerable{T}"/> evaluation.</remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="action">The function to apply to elements from the input collection.</param>
        /// <param name="source">The input collection.</param>
        public static void Iterate<T>(Action<T> action, IEnumerable<T> source)
            => IterateIndexed(
                (_, element) => action(element),
                source);

        /// <summary>Applies the given function to two collections simultaneously.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <param name="action">The function to apply to pairs of elements from the input collections.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        public static void Iterate2<T, T2>(Action<T, T2> action, IEnumerable<T> source, IEnumerable<T2> source2)
            => IterateIndexed2(
                (_, element1, element2) => action(element1, element2),
                source,
                source2);

        /// <summary>Applies the given function to each element of the collection. The integer passed to the
        /// function indicates the index of element.</summary>
        /// <remarks>
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.</remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="action">The function to apply to the elements of the collection along with their index.</param>
        /// <param name="source">The input collection.</param>
        public static void IterateIndexed<T>(Action<int, T> action, IEnumerable<T> source)
            => source.LazyLoop(action.ToFunction(), () => true).Count();

        private static void IterateIndexed<T>(Action<int, T> action, IEnumerable<T> source, Func<bool> conditionToBreak)
            => source.LazyLoop(action.ToFunction(), conditionToBreak).Count();

        /// <summary>Applies the given function to two collections simultaneously. The integer passed to the
        /// function indicates the index of element.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than the other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <param name="action">The function to apply to a pair of elements from the input collection along with their index.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        public static void IterateIndexed2<T, T2>(Action<int, T, T2> action, IEnumerable<T> source, IEnumerable<T2> source2)
            => source.LazyLoop2(source2, action.ToFunction(), () => true).Count();

        private static void IterateIndexed2<T, T2>(Action<int, T, T2> action, IEnumerable<T> source, IEnumerable<T2> source2, Func<bool> conditionToBreak)
            => source.LazyLoop2(source2, action.ToFunction(), conditionToBreak).Count();

        /// <summary>Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to each of the elements of the collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform elements from the input collection.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> Map<T, TResult>(Func<T, TResult> mapping, IEnumerable<T> source)
            => source.LazyLoop((_, element) => mapping(element));

        private static IEnumerable<TResult> Map<T, TResult>(Func<T, TResult> mapping, IEnumerable<T> source, Func<bool> conditionToBreak)
            => source.LazyLoop((_, element) => mapping(element), conditionToBreak);

        /// <summary>Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to the corresponding elements of the two collections pairwise.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.
        /// </remarks>
        /// <typeparam name="T">The type of elements first of source.</typeparam>
        /// <typeparam name="T2">The type of elements second of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform pairs of elements from the input collection.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> Map2<T, T2, TResult>(Func<T, T2, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2)
            => source.LazyLoop2(source2, (_, element1, element2) => mapping(element1, element2));

        private static IEnumerable<TResult> Map2<T, T2, TResult>(Func<T, T2, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2, Func<bool> conditionToBreak)
            => source.LazyLoop2(source2, (_, element1, element2) => mapping(element1, element2), conditionToBreak);

        /// <summary>Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to the corresponding elements of the three collections simultaneously.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than others then the loop runs only run until the smallest collection length.
        /// </remarks>
        /// <typeparam name="T">The type of elements first of source.</typeparam>
        /// <typeparam name="T2">The type of elements second of source.</typeparam>
        /// <typeparam name="T3">The type of elements third of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform triples of elements from the input collections.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="source3">The third input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> Map3<T, T2, T3, TResult>(Func<T, T2, T3, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2, IEnumerable<T3> source3)
            => source.LazyLoop3(source2, source3,
                (_, element1, element2, element3) => mapping(element1, element2, element3)
                             );

        private static IEnumerable<TResult> Map3<T, T2, T3, TResult>(Func<T, T2, T3, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2, IEnumerable<T3> source3, Func<bool> conditionToBreak)
            => source.LazyLoop3(source2, source3,
                (_, element1, element2, element3) => mapping(element1, element2, element3),
                              conditionToBreak);

        /// <summary>Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to each of the elements of the collection. The integer index passed to the
        /// function indicates the index (from 0) of element being transformed.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform elements and their indices.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> MapIndexed<T, TResult>(Func<int, T, TResult> mapping, IEnumerable<T> source)
            => source.LazyLoop(mapping);

        private static IEnumerable<TResult> MapIndexed<T, TResult>(Func<int, T, TResult> mapping, IEnumerable<T> source, Func<bool> conditionToContinue)
            => source.LazyLoop(mapping, conditionToContinue);

        /// <summary>
        /// Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to each pair of the elements of the collections. The integer index passed to the
        /// function indicates the index (from 0) of element being transformed.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than the other then the loop runs only run until the smallest collection length.
        /// </remarks>
        /// <typeparam name="T">The type of elements first of source.</typeparam>
        /// <typeparam name="T2">The type of elements second of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform pairs of elements from the two collections and their index.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> MapIndexed2<T, T2, TResult>(Func<int, T, T2, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2)
            => source.LazyLoop2(source2, mapping);

        private static IEnumerable<TResult> MapIndexed2<T, T2, TResult>(Func<int, T, T2, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2, Func<bool> conditionToContinue)
            => source.LazyLoop2(source2, mapping, conditionToContinue);

        /// <summary>
        /// Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to each triples of the elements of the collections. The integer index passed to the
        /// function indicates the index (from 0) of element being transformed.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than others then the loop runs only run until the smallest collection length.</remarks>
        /// <typeparam name="T">The type of elements first of source.</typeparam>
        /// <typeparam name="T2">The type of elements second of source.</typeparam>
        /// <typeparam name="T3">The type of elements third of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform trio of elements from the three collections and their index.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="source3">The third input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> MapIndexed3<T, T2, T3, TResult>(Func<int, T, T2, T3, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2, IEnumerable<T3> source3)
            => source.LazyLoop3(source2, source3, mapping, () => true);

        private static IEnumerable<TResult> MapIndexed3<T, T2, T3, TResult>(Func<int, T, T2, T3, TResult> mapping, IEnumerable<T> source, IEnumerable<T2> source2, IEnumerable<T3> source3, Func<bool> conditionToContinue)
            => source.LazyLoop3(source2, source3, mapping, conditionToContinue);

        /// <summary>Splits the collection into two collections, containing the 
        /// elements for which the given <paramref name="predicate"/> returns true and false
        /// respectively. Element order is preserved in both of the created collections.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>A collection containing the elements for which the predicate evaluated to true and a collection
        /// containing the elements for which the predicate evaluated to false, respectively.</returns>
        public static (IEnumerable<T> Trues, IEnumerable<T> Falses) Partition<T>(Func<T, bool> predicate, IEnumerable<T> source)
            => source.Map(element => new { Key = predicate(element), Value = element })
                         .GroupBy(element => element.Key)
                         .OrderByDescending(element => element.Key)
                         .Map(groupedElements => groupedElements.Select(element => element.Value))
                         .HeadAndTailEnd();

        /// <summary>Returns a collection with all elements permuted according to the
        /// specified <paramref name="indexMap"/> permutation.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="indexMap">The function to map input indices to output indices.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The permuted collection.</returns>
        public static IEnumerable<T> Permute<T>(Func<int, int> indexMap, IEnumerable<T> source)
        => source.MapIndexed((index, element) => new { Key = indexMap(index), Value = element })
                   .OrderBy(element => element.Key)
                   .Map(groupedElement => groupedElement.Value);

        /// <summary>
        /// Applies the given function to successive elements, returning the first
        /// result where function returns <see cref="Option{T}.Some(T)"/> for some <typeparamref name="T2"/>. If no such
        /// element exists then raise <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="T2">The type of option value returned by <paramref name="chooser"/>.</typeparam>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>The first resulting value.</returns>
        public static T2 Pick<T, T2>(Func<T, Option<T2>> chooser, IEnumerable<T> source)
            => source.TryPick(chooser)
                     .Match(
                        value => value,
                        () => throw new InvalidOperationException());

        /// <summary>
        /// Apply a function to each element of the collection, threading an accumulator argument
        /// through the computation. Apply the function to the first two elements of the collection.
        /// Raises <see cref="InvalidOperationException"/> if <paramref name="source"/> is empty
        /// </summary>
        /// <remarks>
        /// Then feed this result into the function along with the third element and so on. 
        /// If the input function is f and the elements are <c>i0...iN</c> then computes 
        /// <c>f (... (f i0 i1) i2 ...) iN</c>.
        /// </remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="reduction">The function to reduce two collection elements to a single element.</param>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>The final reduced value.</returns>
        public static T Reduce<T>(Func<T, T, T> reduction, IEnumerable<T> source)
            => source.Aggregate(reduction);

        /// <summary>Applies a function to each element of the collection, starting from the end, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then computes 
        /// <c>f i0 (...(f iN-1 iN))</c>.
        /// Raises <see cref="InvalidOperationException"/> if <paramref name="source"/> is empty
        /// </summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="reduction">A function that takes in the next-to-last element of the collection and the
        /// current accumulated result to produce the next accumulated result.</param>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>The final reduced value.</returns>
        public static T ReduceBack<T>(Func<T, T, T> reduction, IEnumerable<T> source)
            => source.Reverse()
                     .Reduce(reduction);

        /// <summary>Creates a collection by replicating the given initial value.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="count">The number of elements to replicate.</param>
        /// <param name="initial">The value to replicate</param>
        /// <returns>The generated collection.</returns>
        public static IEnumerable<T> Replicate<T>(int count, T initial)
            => Enumerable.Repeat(initial, count);

        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. Take the second argument, and apply the function to it
        /// and the first element of the collection. Then feed this result into the function along
        /// with the second element and so on. Returns the collection of intermediate results and the final result.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="state">The initial state.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection of states.</returns>
        public static IEnumerable<TState> Scan<T, TState>(Func<TState, T, TState> folder, TState state, IEnumerable<T> source)
        {
            TState accumulator = state;
            return source.LazyLoop(
                (index, element) =>
                {
                    accumulator = index == 0 ? state : accumulator;
                    accumulator = folder(accumulator, element);
                    return accumulator;
                });
        }

        /// <summary>
        /// Applies a function to each pair of elements of the collections, threading an accumulator argument
        /// through the computation. Take the second argument, and apply the function to it
        /// and the first pair of elements of the collections. Then feed this result into the function along
        /// with the second pair of elements and so on. Returns the collection of intermediate results and the final result.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.</remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The collection of states.</returns>
        public static IEnumerable<TState> Scan2<T, T2, TState>(Func<TState, T, T2, TState> folder, TState state, IEnumerable<T> source, IEnumerable<T2> source2)
        {
            TState accumulator = state;
            return source.LazyLoop2(
                source2,
                (index, element1, element2) =>
                {
                    accumulator = accumulator = index == 0 ? state : accumulator;
                    accumulator = folder(accumulator, element1, element2);
                    return accumulator;
                });
        }

        /// <summary>Applies a function to each element of the collection, threading an accumulator argument
        /// through the computation. Take the third argument, and apply the function to it
        /// and the last element of the collection. Then feed this result into the function along
        /// with the previous element and so on. Returns the collection of intermediate results and the final result.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The collection of states.</returns>
        public static IEnumerable<TState> ScanBack<T, TState>(Func<T, TState, TState> folder, IEnumerable<T> source, TState state)
        {
            TState accumulator = state;
            return source.Reverse().LazyLoop(
                (index, element) =>
                {
                    accumulator = index == 0 ? state : accumulator;
                    accumulator = folder(element, accumulator);
                    return accumulator;
                }).Reverse();
        }

        /// <summary>
        /// Applies a function to each pair of elements of the collections, threading an accumulator argument
        /// through the computation. Take the third argument, and apply the function to it
        /// and the last pair of elements of the collections. Then feed this result into the function along
        /// with the previous pair of elements and so on. Returns the collection of intermediate results and the final result.</summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.</remarks>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The collection of states.</returns>
        public static IEnumerable<TState> ScanBack2<T, T2, TState>(Func<T, T2, TState, TState> folder, IEnumerable<T> source, IEnumerable<T2> source2, TState state)
        {
            TState accumulator = state;
            return source.Reverse().LazyLoop2(
                source2.Reverse(),
                (index, element1, element2) =>
                {
                    accumulator = index == 0 ? state: accumulator;
                    accumulator = folder(element1, element2, accumulator);
                    return accumulator;
                })
                .Reverse();
        }

        /// <summary>Returns the collection without <see cref="Head{T}(IEnumerable{T})"/> element.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The collection collection.</param>
        /// <exception cref="ArgumentNullException">Thrown when the collection is empty.</exception>
        /// <returns>The collection after removing the first element.</returns>
        public static IEnumerable<T> Tail<T>(IEnumerable<T> source)
            => source.Skip(1);

        /// <summary>Returns the first element for which the given <paramref name="predicate"/> function returns <c>true.</c>.
        /// Return <see cref="Option{T}.None"/> if no such element exists.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The first element for which the predicate returns true, or <see cref="Option{T}.None"/> if
        /// every element evaluates to false.</returns>
        public static Option<T> TryFind<T>(Func<T, bool> predicate, IEnumerable<T> source)
            => source.FirstOrDefault(predicate);

        /// <summary>Applies the given function to successive elements, returning <see cref="Option{T}.Some(T)"/> the first
        /// result where function returns <see cref="Option{T}.Some(T)"/> for some x. If no such element 
        /// exists then return <see cref="Option{T}.None"/>.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="T2">The type of option value returned by <paramref name="chooser"/>.</typeparam>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The first resulting <see cref="Option{T}.Some(T)"/> value or <see cref="Option{T}.None"/>.</returns>
        public static Option<T2> TryPick<T, T2>(Func<T, Option<T2>> chooser, IEnumerable<T> source)
            => source.Map(chooser)
                     .FirstOrDefault(element => element.IsSome);

        /// <summary>Splits a collection of pairs into two collections.</summary>
        /// <typeparam name="T">The type of Item1 property of elements of source.</typeparam>
        /// <typeparam name="T2">The type of Item2 property of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <returns>A Tuple of two collections of splitted elements.</returns>
        public static (IEnumerable<T>, IEnumerable<T2>) Unzip<T, T2>(IEnumerable<(T, T2)> source)
            => (source.Map(e => e.Item1),
                source.Map(e => e.Item2));

        /// <summary>Splits a collection of triples into three collections.</summary>
        /// <typeparam name="T">The type of Item1 property of elements of source.</typeparam>
        /// <typeparam name="T2">The type of Item2 property of elements of source.</typeparam>
        /// <typeparam name="T3">The type of Item3 property of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <returns>A Tuple of three collections of splitted elements.</returns>
        public static (IEnumerable<T>, IEnumerable<T2>, IEnumerable<T3>) Unzip3<T, T2, T3>(IEnumerable<(T, T2, T3)> source)
             => (source.Map(e => e.Item1),
                 source.Map(e => e.Item2),
                 source.Map(e => e.Item3));

        /// <summary>Combines two collections into a collection of pairs.</summary>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <returns>A single collection containing pairs of matching elements from the input collection.</returns>
        public static IEnumerable<(T, T2)> Zip<T, T2>(IEnumerable<T> source, IEnumerable<T2> source2)
            => source.LazyLoop2(
                source2,
                (_, element1, element2) => (element1, element2));


        /// <summary>Combines three collections into a collections of triples.</summary>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="T3">The type of elements of third source.</typeparam>
        /// <param name="source">The first input collection.</param>
        /// <param name="source2">The second input collection.</param>
        /// <param name="source3">The third input collection.</param>
        /// <returns>A single collection containing triples of matching elements from the input collections.</returns>
        public static IEnumerable<(T, T2, T3)> Zip3<T, T2, T3>(IEnumerable<T> source, IEnumerable<T2> source2, IEnumerable<T3> source3)
        => source.LazyLoop3(source2, source3,
            (_, element1, element2, element3) => (element1, element2, element3));

    }

}

