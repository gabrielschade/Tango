using System;
using System.Collections.Generic;
using Tango.Modules;
using Tango.Types;

namespace Tango.Linq
{
    /// <summary>
    /// Basic operations on <see cref="IEnumerable{T}"/> as extension methods.
    /// The original operations are in <see cref="CollectionModule"/>.
    /// </summary>
    public static class CollectionLinqExtensions
    {
        /// <summary>Returns a new collection that contains the elements of the first collection
        /// followed by elements of the second.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The first input collection.</param>
        /// <param name="second">The second input collection.</param>
        /// <returns>The resulting collection.</returns>
        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, IEnumerable<T> second)
            => CollectionModule.Append(source, second);

        /// <summary>Applies the given function to each element of the collection. Returns
        /// the collection comprised of the results <typeparamref name="TResult"/> for each element where
        /// the function returns <see cref="Option{TResult}.Some(TResult)"/></summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of option value returned by <paramref name="chooser"/>.</typeparam>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection comprising the values selected from the chooser function.</returns>
        public static IEnumerable<TResult> Choose<T, TResult>(this IEnumerable<T> source, Func<T, Option<TResult>> chooser)
            => CollectionModule.Choose(chooser, source);

        /// <summary>Divides the input collection into chunks of size at most <paramref name="chunkSize"/>.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="chunkSize">The maximum size of each chunk.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection divided into chunks.</returns>
        /// <exception cref="ArgumentException">Thrown when <paramref name="chunkSize"/> is not positive.</exception>
        public static IEnumerable<IEnumerable<T>> ChunkBySize<T>(this IEnumerable<T> source, int chunkSize)
            => CollectionModule.ChunkBySize(chunkSize, source);

        /// <summary>Projects each element of a sequence to an <see cref="IEnumerable{T}"/> and flattens the resulting sequences into one collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of the elements of the sequence returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform each input element into a subcollection to be concatenated.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>An <see cref="IEnumerable{TResult}"/> whose elements are the result of invoking the one-to-many transform function on each element of the input sequence.</returns>
        public static IEnumerable<TResult> Collect<T, TResult>(this IEnumerable<T> source, Func<T, IEnumerable<TResult>> mapping)
            => CollectionModule.Collect(mapping, source);

        /// <summary>Compares two collections using the given comparison function, element by element.
        /// Returns the first non-zero result from the comparison function.  If the end of a collection
        /// is reached it returns a -1 if the first collection is shorter and a 1 if the second collection
        /// is shorter.
        /// </summary>
        /// <remarks>
        /// if one collections is longer 
        /// than other then the loop runs only run until the smallest collection length.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.
        /// </remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="comparer">A function that takes an element from each collection and returns an int.
        /// If it evaluates to a non-zero value iteration is stopped and that value is returned.</param>
        /// <param name="source">The first input collection.</param>
        /// <param name="second">The second input collection.</param>
        ///
        /// <returns>The first non-zero value from the comparison function.</returns>
        public static int CompareWith<T>(this IEnumerable<T> source, IEnumerable<T> second, Func<T, T, int> comparer)
            => CollectionModule.CompareWith(comparer, source, second);

        /// <summary>Applies a key-generating function to each element of a collection and returns a collection yielding unique
        /// keys and their number of occurrences in the original collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TKey">The type of key generated by projection function.</typeparam>
        /// <param name="projection">A function transforming each item of the input collection into a key to be
        /// compared against the others.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The result collection is a tuple with key and the quantity of elements with this same key.</returns>
        public static IEnumerable<(TKey Key, int Count)> CountBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> projection)
            => CollectionModule.CountBy(projection, source);

        /// <summary>Returns a new collection that contains the elements of each the collection in order.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="sources">The input sequence of collections.</param>
        /// <returns>The resulting concatenated collection.</returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<IEnumerable<T>> sources)
            => CollectionModule.Concat(sources);

        /// <summary>Returns a collection that contains no duplicate entries according to <paramref name="comparer"/> and <paramref name="hashCodeGetter"/> functions.
        /// If an element occurs multiple times in the collection then the later occurrences are discarded.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="comparer">A function comparing each element of the input collection against the others.</param>
        /// <param name="hashCodeGetter">A function to generate the hash code of each elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The result collection.</returns>
        public static IEnumerable<T> Distinct<T>(this IEnumerable<T> source, Func<T, T, bool> comparer, Func<T, int> hashCodeGetter)
            => CollectionModule.Distinct(comparer, hashCodeGetter, source);

        /// <summary>Tests if any element of the collection satisfies the given <paramref name="predicate"/>.</summary>
        /// <remarks>The <paramref name="predicate"/> is applied to the elements of the input <paramref name="source"/>. If any application 
        /// returns true then the overall result is true and no further elements are tested. 
        /// Otherwise, false is returned.
        /// </remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>True if any element satisfies the predicate, otherwise, false.</returns>
        public static bool Exists<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => CollectionModule.Exists(predicate, source);

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
        /// <param name="source">The first input collection.</param>
        /// <param name="second">The second input collection.</param>
        /// <returns>True if any pair of elements satisfy the predicate.</returns>
        public static bool Exists2<T>(this IEnumerable<T> source, IEnumerable<T> second, Func<T, T, bool> predicate)
            => CollectionModule.Exists2(predicate, source, second);

        /// <summary>Returns a new collection containing only the elements of the collection
        /// for which the given predicate returns true.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>A collection containing only the elements that satisfy the <paramref name="predicate"/>.</returns>
        public static IEnumerable<T> Filter<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => CollectionModule.Filter(predicate, source);

        /// <summary>Returns the index of the first element in the collection
        /// that satisfies the given <paramref name="predicate"/>.
        /// Raises <c>InvalidOperationException</c> if no such element exists.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown if the <paramref name="predicate"/> evaluates to false for all the
        /// elements of the collection.</exception>
        /// <returns>The index of the first element that satisfies the predicate.</returns>
        public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => CollectionModule.FindIndex(predicate, source);

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
        public static TState Fold<T, TState>(this IEnumerable<T> source, TState state, Func<TState, T, TState> folder)
            => CollectionModule.Fold(folder, state, source);

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
        /// <param name="second">The second input collection.</param>
        /// <returns>The final state value.</returns>
        public static TState Fold2<T, T2, TState>(this IEnumerable<T> source, IEnumerable<T2> second, TState state, Func<TState, T, T2, TState> folder)
            => CollectionModule.Fold2(folder, state, source, second);

        /// <summary>Applies a function to each element of the collection, starting from the end, threading an accumulator argument
        /// through the computation. If the input function is <c>f</c> and the elements are <c>i0...iN</c> then 
        /// computes <c>f i0 (...(f iN s))</c>.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TState">The type of initial and final states</typeparam>
        /// <param name="folder">The function to update the state given the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The state object after the folding function is applied to each element of the collection.</returns>
        public static TState FoldBack<T, TState>(this IEnumerable<T> source, Func<T, TState, TState> folder, TState state)
            => CollectionModule.FoldBack(folder, source, state);

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
        /// <param name="second">The second input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The final state value.</returns>
        public static TState FoldBack2<T, T2, TState>(this IEnumerable<T> source, IEnumerable<T2> second, Func<T, T2, TState, TState> folder, TState state)
            => CollectionModule.FoldBack2(folder, source,second, state);

        /// <summary>Tests if all elements of the collection satisfy the given <paramref name="predicate"/>.</summary>
        /// <remarks>The <paramref name="predicate"/> is applied to the elements of the input collection. If any application 
        /// returns false then the overall result is false and no further elements are tested. 
        /// Otherwise, true is returned.
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.</remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>True if all of the elements satisfy the predicate.</returns>
        public static bool ForAll<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => CollectionModule.ForAll(predicate, source);

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
        /// <param name="second">The second input collection.</param>
        /// <returns>True if all of the pairs of elements satisfy the predicate.</returns>
        public static bool ForAll2<T, T2>(this IEnumerable<T> source, IEnumerable<T2> second, Func<T, T2, bool> predicate)
            => CollectionModule.ForAll2(predicate, source, second);

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
        /// <param name="second">The second input collection.</param>
        /// <param name="third">The third input collection.</param>
        /// <returns>True if all of the triples of elements satisfy the predicate.</returns>
        public static bool ForAll3<T, T2, T3>(this IEnumerable<T> source, IEnumerable<T2> second, IEnumerable<T3> third, Func<T, T2,T3, bool> predicate)
            => CollectionModule.ForAll3(predicate, source, second, third);

        /// <summary>Returns the first element of the collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>The first element of the collection.</returns>
        public static T Head<T>(this IEnumerable<T> source)
            => CollectionModule.Head(source);

        /// <summary>Returns the first and the last element of the collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <exception cref="InvalidOperationException">Thrown when the collection is empty.</exception>
        /// <returns>A Tuple with first (Head) and last (TailEnd) element of the <paramref name="source"/>.</returns>
        public static (T Head, T TailEnd) HeadAndTailEnd<T>(this IEnumerable<T> source)
            => CollectionModule.HeadAndTailEnd(source);

        /// <summary>Applies the given function to each element of the collection.</summary>
        /// <remarks>This function causes <see cref="IEnumerable{T}"/> evaluation.</remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="action">The function to apply to elements from the input collection.</param>
        /// <param name="source">The input collection.</param>
        public static void Iterate<T>(this IEnumerable<T> source, Action<T> action)
            => CollectionModule.Iterate(action, source);

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
        /// <param name="second">The second input collection.</param>
        public static void Iterate2<T, T2>(this IEnumerable<T> source, IEnumerable<T2> second, Action<T, T2> action)
            => CollectionModule.Iterate2(action, source, second);

        /// <summary>Applies the given function to each element of the collection. The integer passed to the
        /// function indicates the index of element.</summary>
        /// <remarks>
        /// This function causes <see cref="IEnumerable{T}"/> evaluation.</remarks>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="action">The function to apply to the elements of the collection along with their index.</param>
        /// <param name="source">The input collection.</param>
        public static void IterateIndexed<T>(this IEnumerable<T> source, Action<int, T> action)
            => CollectionModule.IterateIndexed(action, source);

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
        /// <param name="second">The second input collection.</param>
        public static void IterateIndexed2<T, T2>(this IEnumerable<T> source, IEnumerable<T2> second, Action<int, T, T2> action)
            => CollectionModule.IterateIndexed2(action, source, second);

        /// <summary>Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to each of the elements of the collection.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform elements from the input collection.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> Map<T, TResult>(this IEnumerable<T> source, Func<T, TResult> mapping)
            => CollectionModule.Map(mapping, source);

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
        /// <param name="second">The second input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> Map2<T, T2, TResult>(this IEnumerable<T> source, IEnumerable<T2> second, Func<T, T2, TResult> mapping)
            => CollectionModule.Map2(mapping, source, second);

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
        /// <param name="second">The second input collection.</param>
        /// <param name="third">The third input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> Map3<T, T2, T3, TResult>(this IEnumerable<T> source, IEnumerable<T2> second, IEnumerable<T3> third, Func<T, T2, T3, TResult> mapping)
            => CollectionModule.Map3(mapping, source, second, third);

        /// <summary>Builds a new collection whose elements are the results of applying the given <paramref name="mapping"/> function
        /// to each of the elements of the collection. The integer index passed to the
        /// function indicates the index (from 0) of element being transformed.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="TResult">The type of value returned by <paramref name="mapping"/>.</typeparam>
        /// <param name="mapping">The function to transform elements and their indices.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> MapIndexed<T, TResult>(this IEnumerable<T> source, Func<int, T, TResult> mapping)
            => CollectionModule.MapIndexed(mapping, source);

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
        /// <param name="second">The second input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> MapIndexed2<T, T2, TResult>(this IEnumerable<T> source, IEnumerable<T2> second, Func<int, T, T2, TResult> mapping)
            => CollectionModule.MapIndexed2(mapping, source, second);

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
        /// <param name="second">The second input collection.</param>
        /// <param name="third">The third input collection.</param>
        /// <returns>The collection of transformed elements.</returns>
        public static IEnumerable<TResult> MapIndexed3<T, T2, T3, TResult>(this IEnumerable<T> source, IEnumerable<T2> second, IEnumerable<T3> third, Func<int, T, T2, T3, TResult> mapping)
            => CollectionModule.MapIndexed3(mapping, source, second, third);

        /// <summary>Splits the collection into two collections, containing the 
        /// elements for which the given <paramref name="predicate"/> returns true and false
        /// respectively. Element order is preserved in both of the created collections.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>A collection containing the elements for which the predicate evaluated to true and a collection
        /// containing the elements for which the predicate evaluated to false, respectively.</returns>
        public static (IEnumerable<T> Trues, IEnumerable<T> Falses) Partition<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => CollectionModule.Partition(predicate, source);

        /// <summary>Returns a collection with all elements permuted according to the
        /// specified <paramref name="indexMap"/> permutation.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="indexMap">The function to map input indices to output indices.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The permuted collection.</returns>
        public static IEnumerable<T> Permute<T>(this IEnumerable<T> source, Func<int, int> indexMap)
            => CollectionModule.Permute(indexMap, source);

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
        public static T2 Pick<T, T2>(this IEnumerable<T> source, Func<T, Option<T2>> chooser)
            => CollectionModule.Pick(chooser, source);

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
        public static T Reduce<T>(this IEnumerable<T> source, Func<T, T, T> reduction)
            => CollectionModule.Reduce(reduction, source);

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
        public static T ReduceBack<T>(this IEnumerable<T> source, Func<T, T, T> reduction)
            => CollectionModule.ReduceBack(reduction, source);

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
        public static IEnumerable<TState> Scan<T, TState>(this IEnumerable<T> source, TState state, Func<TState, T, TState> folder)
            => CollectionModule.Scan(folder, state, source);

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
        /// <param name="second">The second input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The collection of states.</returns>
        public static IEnumerable<TState> Scan2<T, T2, TState>(this IEnumerable<T> source, IEnumerable<T2> second, TState state, Func<TState, T, T2, TState> folder)
            => CollectionModule.Scan2(folder, state, source, second);

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
        public static IEnumerable<TState> ScanBack<T, TState>(this IEnumerable<T> source, Func<T, TState, TState> folder, TState state)
            => CollectionModule.ScanBack(folder, source, state);

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
        /// <param name="second">The second input collection.</param>
        /// <param name="state">The initial state.</param>
        /// <returns>The collection of states.</returns>
        public static IEnumerable<TState> ScanBack2<T, T2, TState>(this IEnumerable<T> source, IEnumerable<T2> second, Func<T, T2, TState,TState> folder, TState state)
            => CollectionModule.ScanBack2(folder, source, second, state);

        /// <summary>Returns the collection without <see cref="Head{T}(IEnumerable{T})"/> element.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="source">The collection collection.</param>
        /// <exception cref="ArgumentNullException">Thrown when the collection is empty.</exception>
        /// <returns>The collection after removing the first element.</returns>
        public static IEnumerable<T> Tail<T>(this IEnumerable<T> source)
            => CollectionModule.Tail(source);

        /// <summary>Returns the first element for which the given <paramref name="predicate"/> function returns <c>true.</c>.
        /// Return <see cref="Option{T}.None"/> if no such element exists.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <param name="predicate">The function to test the input elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The first element for which the predicate returns true, or <see cref="Option{T}.None"/> if
        /// every element evaluates to false.</returns>
        public static Option<T> TryFind<T>(this IEnumerable<T> source, Func<T, bool> predicate)
            => CollectionModule.TryFind(predicate, source);

        /// <summary>Applies the given function to successive elements, returning <see cref="Option{T}.Some(T)"/> the first
        /// result where function returns <see cref="Option{T}.Some(T)"/> for some x. If no such element 
        /// exists then return <see cref="Option{T}.None"/>.</summary>
        /// <typeparam name="T">The type of elements of source.</typeparam>
        /// <typeparam name="T2">The type of option value returned by <paramref name="chooser"/>.</typeparam>
        /// <param name="chooser">The function to generate options from the elements.</param>
        /// <param name="source">The input collection.</param>
        /// <returns>The first resulting <see cref="Option{T}.Some(T)"/> value or <see cref="Option{T}.None"/>.</returns>
        public static Option<T2> TryPick<T, T2>(this IEnumerable<T> source, Func<T, Option<T2>> chooser)
            => CollectionModule.TryPick(chooser, source);

        /// <summary>Splits a collection of pairs into two collections.</summary>
        /// <typeparam name="T">The type of Item1 property of elements of source.</typeparam>
        /// <typeparam name="T2">The type of Item2 property of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <returns>A Tuple of two collections of splitted elements.</returns>
        public static (IEnumerable<T>, IEnumerable<T2>) Unzip<T, T2>(this IEnumerable<(T, T2)> source)
            => CollectionModule.Unzip(source);

        /// <summary>Splits a collection of triples into three collections.</summary>
        /// <typeparam name="T">The type of Item1 property of elements of source.</typeparam>
        /// <typeparam name="T2">The type of Item2 property of elements of source.</typeparam>
        /// <typeparam name="T3">The type of Item3 property of elements of source.</typeparam>
        /// <param name="source">The input collection.</param>
        /// <returns>A Tuple of three collections of splitted elements.</returns>
        public static (IEnumerable<T>, IEnumerable<T2>, IEnumerable<T3>) Unzip3<T, T2, T3>(this IEnumerable<(T, T2, T3)> source)
            => CollectionModule.Unzip3(source);

        /// <summary>Combines two collections into a collection of pairs.</summary>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <param name="source">The first input collection.</param>
        /// <param name="second">The second input collection.</param>
        /// <returns>A single collection containing pairs of matching elements from the input collection.</returns>
        public static IEnumerable<(T, T2)> Zip<T, T2>(this IEnumerable<T> source, IEnumerable<T2> second)
            => CollectionModule.Zip(source, second);

        /// <summary>Combines three collections into a collections of triples.</summary>
        /// <typeparam name="T">The type of elements of first source.</typeparam>
        /// <typeparam name="T2">The type of elements of second source.</typeparam>
        /// <typeparam name="T3">The type of elements of third source.</typeparam>
        /// <param name="source">The first input collection.</param>
        /// <param name="second">The second input collection.</param>
        /// <param name="third">The third input collection.</param>
        /// <returns>A single collection containing triples of matching elements from the input collections.</returns>
        public static IEnumerable<(T, T2, T3)> Zip3<T, T2, T3>(this IEnumerable<T> source, IEnumerable<T2> second, IEnumerable<T3> third)
            => CollectionModule.Zip3(source, second, third);

        internal static IEnumerable<TResult> LazyLoop<T, TResult>(
            this IEnumerable<T> source
            , Func<int, T, TResult> function)
            => source.LazyLoop(function, () => true);
        internal static IEnumerable<TResult> LazyLoop<T, TResult>(
            this IEnumerable<T> source,
            Func<int, T, TResult> function,
            Func<bool> conditionToContinue)
        {
            int currentIndex = 0;
            using (IEnumerator<T> enumeratorSource = source.GetEnumerator())
            {
                while (enumeratorSource.MoveNext() && conditionToContinue())
                {
                    yield return function(currentIndex, enumeratorSource.Current);
                    currentIndex++;
                }
            }
        }

        internal static IEnumerable<TResult> LazyLoop2<T, T2, TResult>(
            this IEnumerable<T> source,
            IEnumerable<T2> source2,
            Func<int, T, T2, TResult> function)
            => source.LazyLoop2(source2,function, () => true);

        internal static IEnumerable<TResult> LazyLoop2<T, T2, TResult>(
        this IEnumerable<T> source,
        IEnumerable<T2> source2,
        Func<int, T, T2, TResult> function,
        Func<bool> conditionToContinue)
        {
            int currentIndex = 0;
            using (IEnumerator<T> enumeratorSource = source.GetEnumerator())
            {
                using (IEnumerator<T2> enumeratorSource2 = source2.GetEnumerator())
                {
                    while (enumeratorSource.MoveNext() && enumeratorSource2.MoveNext() && conditionToContinue())
                    {
                        yield return function(currentIndex, enumeratorSource.Current, enumeratorSource2.Current);
                        currentIndex++;
                    }
                }
            }
        }
        internal static IEnumerable<TResult> LazyLoop3<T, T2, T3, TResult>(
            this IEnumerable<T> source,
            IEnumerable<T2> source2,
            IEnumerable<T3> source3,
            Func<int, T, T2, T3, TResult> function)
            => source.LazyLoop3(source2, source3, function, () => true);

        internal static IEnumerable<TResult> LazyLoop3<T, T2, T3, TResult>(
        this IEnumerable<T> source,
        IEnumerable<T2> source2,
        IEnumerable<T3> source3,
        Func<int, T, T2, T3, TResult> function,
        Func<bool> conditionToContinue)
        {
            int currentIndex = 0;
            using (IEnumerator<T> enumeratorSource = source.GetEnumerator())
            {
                using (IEnumerator<T2> enumeratorSource2 = source2.GetEnumerator())
                {
                    using (IEnumerator<T3> enumeratorSource3 = source3.GetEnumerator())
                    {
                        while (enumeratorSource.MoveNext() && enumeratorSource2.MoveNext() && enumeratorSource3.MoveNext() && conditionToContinue())
                        {
                            yield return function(currentIndex, enumeratorSource.Current, enumeratorSource2.Current, enumeratorSource3.Current);
                            currentIndex++;
                        }
                    }
                }
            }
        }
    }

}

