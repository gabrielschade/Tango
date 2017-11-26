using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Types;

namespace Tango.Test.Types
{
    [TestClass]
    public class CollectionTests

    {
        IEnumerable<int> _0to5values;
        IEnumerable<int> _0to10values;
        IEnumerable<int> _10to0values;
        IEnumerable<int> _5to0values;
        IEnumerable<int> _100evens;
        IEnumerable<int> _100evens1Odd;
        IEnumerable<int> _1000evens;
        IEnumerable<int> _1000evens1Odd;
        IEnumerable<int> _10000evens;
        IEnumerable<int> _10000evens1Odd;
        IEnumerable<int> _100values;
        IEnumerable<int> _1000values;
        IEnumerable<int> _10000values;
        Func<int, bool> _isEven = element => element % 2 == 0;
        Func<int, bool> _isOdd = element => element % 2 == 1;
        Func<int, int, bool> _elementsAreEqual = (element1, element2) => element1 == element2;

        [TestInitialize]
        public void Setup()
        {
            Func<int, int> evensFunction = (index) => index % 2 == 0 ? index : index + 1;
            Func<int, int> evensOddFunction = (index) => index % 2 == 0 || index == 1 ? index : index + 1;

            _100evens = Collection.Initialize(100, evensFunction).ToArray();
            _1000evens = Collection.Initialize(1000, evensFunction).ToArray();
            _10000evens = Collection.Initialize(10000, evensFunction).ToArray();
            _100evens1Odd = Collection.Initialize(100, evensOddFunction).ToArray();
            _1000evens1Odd = Collection.Initialize(1000, evensOddFunction).ToArray();
            _10000evens1Odd = Collection.Initialize(10000, evensOddFunction).ToArray();
            _100values = Collection.Range(1, 100).ToArray();
            _1000values = Collection.Range(1, 1000).ToArray();
            _10000values = Collection.Range(1, 10000).ToArray();
            _0to5values = Collection.Range(0, 5).ToArray();
            _0to10values = Collection.Range(0, 10).ToArray();
            _10to0values = _0to10values.Reverse();
            _5to0values = _0to5values.Reverse();
        }

        [TestMethod]
        public void CollectionForAllFalse()
        {
            bool result = Collection.ForAll(_isEven, _100evens1Odd);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollectionForAllTrue()
        {
            bool result = Collection.ForAll(_isEven, _100evens);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionForAll2False()
        {
            bool result = Collection.ForAll2(_elementsAreEqual, _100evens1Odd, _100evens);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollectionForAll2True()
        {
            bool result = Collection.ForAll2(_elementsAreEqual, _100evens, _100evens);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionForAll3False()
        {
            bool result = Collection.ForAll3( 
                (element1, element2, element3) => element1 == element2 && element1 == element3, 
                _100evens1Odd, _100evens, _10000evens);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollectionForAll3True()
        {
            bool result = Collection.ForAll3(
               (element1, element2, element3) => element1 == element2 && element1 == element3,
               _100evens, _100evens, _100evens);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionSameValuesRange()
        {
            IEnumerable<int> generated = Collection.Range(1, 1);
            int[] expected = { 1 };

            bool result = Collection.ForAll2(_elementsAreEqual, generated, expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionDecreasingRange()
        {
            IEnumerable<int> generated = Collection.Range(10, 1);
            int[] expected = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            bool result = Collection.ForAll2(_elementsAreEqual, generated, expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionIncreasingRange()
        {
            IEnumerable<int> generated = Collection.Range(1, 10);
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            bool result = Collection.ForAll2(_elementsAreEqual, generated, expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionAppend()
        {
            IEnumerable<int> first = Collection.Range(1, 500);
            IEnumerable<int> second = Collection.Range(501, 1000);
            IEnumerable<int> expected = Collection.Range(1, 1000);

            bool result = Collection.ForAll2(_elementsAreEqual, Collection.Append(first, second), expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionChoose()
        {
            string _evenValue = "Even";
            Func<int, Option<string>> chooser =
                element => _isEven(element) ? _evenValue : null;

            IEnumerable<string> result =
                Collection.Choose(chooser, _1000values);

            Assert.IsTrue(
                result.Count() == 500
                && Collection.ForAll(element => element == _evenValue, result)
                );
        }

        [TestMethod]
        public void CollectionChooseToEmpty()
        {
            Func<int, Option<string>> chooser =
                element => element == 10000 ? "Even" : null;

            IEnumerable<string> result =
                Collection.Choose(chooser, _1000values);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void CollectionChunk()
        {
            IEnumerable<IEnumerable<int>> chunks = Collection.ChunkBySize(10, _10000evens);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectionChunkArgumentExceptionWith0()
        {
            IEnumerable<IEnumerable<int>> chunks = Collection.ChunkBySize(0, _10000evens);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectionChunkArgumentExceptionWithNegative()
        {
            IEnumerable<IEnumerable<int>> chunks = Collection.ChunkBySize(-10, _10000evens);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        public void CollectionChunkAndConcat100()
        {
            IEnumerable<IEnumerable<int>> chunks =
                Collection.ChunkBySize(10, _100evens);

            IEnumerable<int> concat = Collection.Concat(chunks);
            Assert.AreEqual(concat.Count(), _100evens.Count());
        }

        [TestMethod]
        public void CollectionChunkAndConcat1000()
        {
            IEnumerable<IEnumerable<int>> chunks =
                Collection.ChunkBySize(10, _1000evens);

            IEnumerable<int> concat = Collection.Concat(chunks);
            Assert.AreEqual(concat.Count(), _1000evens.Count());
        }

        [TestMethod]
        public void CollectionChunkAndConcat10000()
        {
            IEnumerable<IEnumerable<int>> chunks =
                Collection.ChunkBySize(10, _10000evens);
            IEnumerable<int> concat = Collection.Concat(chunks);
            Assert.AreEqual(concat.Count(), _10000evens.Count());
        }

        [TestMethod]
        public void CollectionCollect()
        {
            int expected = 5050;
            IEnumerable<int> result =
                Collection.Collect(element => Collection.Range(1, element), _100values);

            Assert.AreEqual(expected, result.Count());
        }

        [TestMethod]
        public void CollectionCompareWithEqualLists()
        {
            int expected = 0;
            int result =
            Collection.CompareWith(
                (element1, element2) => element1 > element2 ? 1
                                        : element1 < element2 ? -1
                                        : 0
                , _1000values, _1000values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionCompareWithFirstListBigger()
        {
            int expected = 1;
            int result =
            Collection.CompareWith(
                (element1, element2) => element1 > element2 ? 1
                                        : element1 < element2 ? -1
                                        : 0
                , _1000values, _1000evens);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionCompareWithFirstListSmaller()
        {
            int expected = -1;
            int result =
            Collection.CompareWith(
                (element1, element2) => element1 > element2 ? 1
                                        : element1 < element2 ? -1
                                        : 0
                , _1000evens, _1000values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionCountBy()
        {
            IEnumerable<(bool Key, int Count)> result =
                Collection.CountBy(_isEven, _10000values);

            int expected = 10000;
            var headAndTailEnd = Collection.HeadAndTailEnd(result);

            int oddsCount = headAndTailEnd.Head.Count;
            int evensCount = headAndTailEnd.TailEnd.Count;
            
            Assert.AreEqual(expected, evensCount + oddsCount);
        }

        [TestMethod]
        public void CollectionDistinct()
        {
            IEnumerable<int> result =
                Collection.Collect(element => Collection.Range(1, element), _100values);

            IEnumerable<int> distinctedResult =
            Collection.Distinct(_elementsAreEqual, element => element, result);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, distinctedResult, Collection.Range(1, 100)));
        }

        [TestMethod]
        public void CollectionEmpty()
        {
            int expected = 0;
            IEnumerable<int> result = Collection.Empty<int>();

            Assert.AreEqual(expected, result.Count());
        }

        [TestMethod]
        public void CollectionExists2FirstTrue()
        {
            bool expected = true;
            bool result =
            Collection.Exists2(_elementsAreEqual, _10000evens, _10000evens);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionExists2True()
        {
            bool expected = true;
            bool result =
            Collection.Exists2(_elementsAreEqual, Collection.Range(0, 100000), Collection.Range(100000, 0));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionExists2False()
        {
            bool expected = false;
            bool result =
            Collection.Exists2(_elementsAreEqual, Collection.Range(0, 100000), Collection.Range(1, 100001));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFindIndex()
        {
            int expected = 99;
            int index =
                Collection.FindIndex(element => element == 100, _10000evens);

            Assert.AreEqual(expected, index);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionFindIndexException()
        {
            int index =
                Collection.FindIndex(element => element == -100, _10000evens);
        }

        [TestMethod]
        public void CollectionFold()
        {
            int expected = 65;
            int result =
                Collection.Fold((accumulator, element) => accumulator + element, 10, _0to10values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFold2()
        {
            int expected = 20;
            int result =
                Collection.Fold2(
                    (accumulator, element1, element2) =>
                        accumulator + Math.Max(element1, element2),
                    12, Collection.Range(1, 3), Collection.Range(3, 1));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFoldBack()
        {
            int expected = -5;
            int result =
                Collection.FoldBack((accumulator, element) => accumulator - element, _0to10values, 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFoldBack2()
        {
            int expected = -10;
            int result =
                Collection.FoldBack2(
                    (accumulator, element1, element2) =>
                        accumulator - Math.Max(element1, element2),
                    _0to10values, _10to0values, 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionHead()
        {
            int expected = 0;
            int result = Collection.Head(_0to10values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionHeadException()
        {
            Collection.Head(Collection.Empty<int>());
        }

        [TestMethod]
        public void CollectionHeadAndTail()
        {
            (int, int) expected = (0, 10);
            (int, int) result = Collection.HeadAndTailEnd(_0to10values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionHeadAndTail10000Elements()
        {
            (int, int) expected = (1, 10000);
            (int, int) result = Collection.HeadAndTailEnd(_10000values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionHeadAndTailException()
        {
            Collection.HeadAndTailEnd(Collection.Empty<int>());
        }

        [TestMethod]
        public void CollectionGenerate()
        {
            IEnumerable<int> generated = Collection.Generate(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionGenerate2()
        {
            IEnumerable<int> generated = Collection.Generate(1, 10, 5, 3, 12, 5, 7, 9);
            int[] expected = { 1, 10, 5, 3, 12, 5, 7, 9 };

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionGenerateAndRange()
        {
            IEnumerable<int> generated = Collection.Generate(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            IEnumerable<int> rangeGenerated = Collection.Range(1, 10);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, rangeGenerated, generated));
        }

        [TestMethod]
        public void CollectionInitialize()
        {
            IEnumerable<int> generated = Collection.Initialize(11, (index) => index * 2);
            IEnumerable<int> expected = Collection.Map(n => n * 2, _0to10values);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionIterate()
        {
            int expected = 55;
            int result = 0;
            Collection.Iterate(element => result += element, _0to10values);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionIterate2()
        {
            int expected = 110;
            int result = 0;
            Collection.Iterate2((element1, element2) => result += element1 + element2, _0to10values, _10to0values);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void CollectionMap()
        {
            IEnumerable<int> generated =
                Collection.Map(element => element * 2, Collection.Generate(1, 2, 3));

            IEnumerable<int> expected =
                Collection.Generate(2, 4, 6);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionMap2()
        {
            IEnumerable<int> generated =
                Collection.Map2(
                    (element1, element2) => Math.Max(element1, element2) * 2,
                Collection.Generate(1, 2, 3),
                Collection.Generate(3, 2, 1));

            IEnumerable<int> expected =
                Collection.Generate(6, 4, 6);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }
        [TestMethod]
        public void CollectionMap3()
        {
            IEnumerable<int> generated =
                Collection.Map3(
                    (element1, element2, element3) => Math.Max(Math.Max(element1, element2), element3) * 2,
                Collection.Generate(1, 2, 3),
                Collection.Generate(3, 2, 1),
                Collection.Generate(3, 3, 3));

            IEnumerable<int> expected =
                Collection.Generate(6, 6, 6);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionMapIndexed()
        {
            IEnumerable<int> generated =
                Collection.MapIndexed(
                    (index, element) => index + element * 2,
                    Collection.Generate(1, 2, 3));

            IEnumerable<int> expected =
                Collection.Generate(2, 5, 8);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionMapIndexed2()
        {
            IEnumerable<int> generated =
                Collection.MapIndexed2(
                    (index, element1, element2) => index + Math.Max(element1, element2) * 2,
                Collection.Generate(1, 2, 3),
                Collection.Generate(3, 2, 1));

            IEnumerable<int> expected =
                Collection.Generate(6, 5, 8);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionMapIndexed3()
        {
            IEnumerable<int> generated =
                Collection.MapIndexed3(
                    (index, element1, element2, element3) => index + Math.Max(Math.Max(element1, element2), element3) * 2,
                Collection.Generate(1, 2, 3),
                Collection.Generate(3, 2, 1),
                Collection.Generate(3, 3, 3)).ToArray();

            IEnumerable<int> expected =
                Collection.Generate(6, 7, 8);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionPartition()
        {
            var (evens, odds) =
                Collection.Partition(element => element % 2 == 0, Collection.Range(0, 1000));

            IEnumerable<int> expectedEvens =
                Collection.Initialize(500, index => index * 2);

            IEnumerable<int> expectedOdds =
                Collection.Initialize(500, index => index * 2 + 1);


            Assert.IsTrue(
                Collection.ForAll2(_elementsAreEqual, expectedEvens, evens)
                && Collection.ForAll2(_elementsAreEqual, expectedOdds, odds));
        }

        [TestMethod]
        public void CollectionPermute()
        {
            IEnumerable<int> original =
                _0to5values;

            IEnumerable<int> permuted =
                Collection.Permute(index => (index + 3) % 6, original);

            IEnumerable<int> expected =
                Collection.Generate(3, 4, 5, 0, 1, 2);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, permuted));
        }

        [TestMethod]
        public void CollectionPick()
        {
            int expected = 400;
            int value =
                Collection.Pick(
                    element => element == 400 ?
                                element
                                : Option<int>.None()
                , _1000values);

            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionPickInvalidOperationException()
        {
            Collection.Pick(
                    element => element == 4000 ?
                                element
                                : Option<int>.None()
                , _1000values);
        }

        [TestMethod]
        public void CollectionScan()
        {
            IEnumerable<int> expected =
                Collection.Generate(10, 11, 13, 16, 20, 25, 31, 38, 46, 55, 65);

            IEnumerable<int> result =
                Collection.Scan((accumulator, element) => accumulator + element, 10, _0to10values);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, result));
        }

        [TestMethod]
        public void CollectionScanBack()
        {
            IEnumerable<int> expected =
                Collection.Generate(65, 65, 64, 62, 59, 55, 50, 44, 37, 29, 20);

            IEnumerable<int> result =
                Collection.ScanBack((accumulator, element) => accumulator + element, _0to10values, 10);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, result));
        }


        [TestMethod]
        public void CollectionScan2()
        {
            IEnumerable<int> expected =
                Collection.Generate(15, 20, 25, 30, 35, 40);

            IEnumerable<int> result =
                Collection.Scan2(
                    (accumulator, element1, element2) => accumulator + element1 + element2, 10,
                    Collection.Range(0, 5),
                    Collection.Range(5, 0));

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, result));
        }

        [TestMethod]
        public void CollectionScanBack2()
        {
            IEnumerable<int> expected =
                Collection.Generate(40, 35, 30, 25, 20, 15);

            IEnumerable<int> result =
                Collection.ScanBack2(
                    (accumulator, element1, element2) => accumulator + element1 + element2,
                    _0to5values,
                    _5to0values
                    , 10);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, result));
        }

        [TestMethod]
        public void CollectionTail()
        {
            IEnumerable<int> expected =
                Collection.Range(1, 5);

            IEnumerable<int> result =
                Collection.Tail(_0to5values);

            Assert.IsTrue(
                Collection.ForAll2(_elementsAreEqual, expected, result)
                && expected.Count() == result.Count());
        }

        [TestMethod]
        public void CollectionTryFind()
        {
            Option<int> result =
            Collection.TryFind(element => element == 400, _10000values);

            int someResult = result.Match(value => value, () => 0);

            Assert.IsTrue(result.IsSome && someResult == 400);
        }

        [TestMethod]
        public void CollectionTryFindNone()
        {
            Option<int> result =
            Collection.TryFind(element => element == 4000, _1000values);

            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void CollectionTryPick()
        {
            Option<int> result =
                Collection.TryPick(element =>
                    element % 2 == 0 ?
                    element
                    : Option<int>.None(), 
                Collection.Generate(11, 15, 19, 22, 20));

            int someResult = result.Match(value => value, () => 0);

            Assert.IsTrue(result.IsSome && someResult == 22);
        }

        [TestMethod]
        public void CollectionTryPickNone()
        {
            Option<int> result =
                Collection.TryPick(element =>
                        element % 2 == 0 ?
                        element
                        : Option<int>.None(), 
                    Collection.Generate(11, 15, 19, 23, 29));
            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void CollectionUnzip()
        {
            IEnumerable<(int, bool)> numbersAndBools =
                Collection.Generate((10, true), (2, false), (33, true), (9, false), (42, true));

            var (numbers,booleans) =
                Collection.Unzip(numbersAndBools);

            IEnumerable<int> expectedNumbers =
                Collection.Generate(10, 2, 33, 9, 42);

            IEnumerable<bool> expectedBooleans =
                Collection.Generate(true, false, true, false, true);

            Assert.IsTrue(
                Collection.ForAll2(_elementsAreEqual, expectedNumbers, numbers)
                && Collection.ForAll2( 
                    (element1, element2) => element1 == element2, 
                    expectedBooleans, booleans) );
        }

        [TestMethod]
        public void CollectionUnzip3()
        {
            IEnumerable<(int, bool,int)> numbersBoolsAndRange =
                Collection.Generate(
                    (10, true,1), 
                    (2, false,2), 
                    (33, true,3), 
                    (9, false,4), 
                    (42, true,5));

            var (numbers, booleans, numbers2) =
                Collection.Unzip3(numbersBoolsAndRange);

            IEnumerable<int> expectedNumbers =
                Collection.Generate(10, 2, 33, 9, 42);

            IEnumerable<bool> expectedBooleans =
                Collection.Generate(true, false, true, false, true);

            Assert.IsTrue(
                Collection.ForAll2(_elementsAreEqual, expectedNumbers, numbers)
                && Collection.ForAll2(
                    (element1, element2) => element1 == element2,
                    expectedBooleans, booleans)
                && Collection.ForAll2(_elementsAreEqual, Collection.Range(1,5), numbers2)
                    );
        }

        [TestMethod]
        public void CollectionZip()
        {
            IEnumerable<(int,int)> pairs =
                Collection.Zip(_0to10values, _10to0values);

            Assert.IsTrue(
                Collection.ForAll3( 
                    (element1, element2, element3) => 
                        element1.Item1 == element2
                        && element1.Item2 == element3
                        , pairs, _0to10values, _10to0values));
        }

        [TestMethod]
        public void CollectionZip3()
        {
            IEnumerable<(int, int,int)> triples =
                Collection.Zip3(_0to10values, _10to0values, _100values);

            Assert.IsTrue(
                Collection.ForAll2(
                    (element1, element2) => element1.Item1 == element2, triples, _0to10values)
                && Collection.ForAll2(
                    (element1, element2) => element1.Item2 == element2, triples, _10to0values)
                && Collection.ForAll2(
                    (element1, element2) => element1.Item3 == element2, triples, _100values)
                    );
        }

    }
}
