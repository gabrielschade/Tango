using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using Tango.Types;
using Tango.Linq;
using Tango.Modules;

namespace Tango.Test.Modules.Collection
{
    [TestClass]
    public class CollectionLinqTests

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

            _100evens = CollectionModule.Initialize(100, evensFunction).ToArray();
            _1000evens = CollectionModule.Initialize(1000, evensFunction).ToArray();
            _10000evens = CollectionModule.Initialize(10000, evensFunction).ToArray();
            _100evens1Odd = CollectionModule.Initialize(100, evensOddFunction).ToArray();
            _1000evens1Odd = CollectionModule.Initialize(1000, evensOddFunction).ToArray();
            _10000evens1Odd = CollectionModule.Initialize(10000, evensOddFunction).ToArray();
            _100values = CollectionModule.Range(1, 100).ToArray();
            _1000values = CollectionModule.Range(1, 1000).ToArray();
            _10000values = CollectionModule.Range(1, 10000).ToArray();
            _0to5values = CollectionModule.Range(0, 5).ToArray();
            _0to10values = CollectionModule.Range(0, 10).ToArray();
            _10to0values = _0to10values.Reverse();
            _5to0values = _0to5values.Reverse();
        }

        [TestMethod]
        public void LinqCollectionForAllFalse()
        {
            bool result = _100evens1Odd.ForAll(_isEven);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LinqCollectionForAllTrue()
        {
            bool result = _100evens.ForAll(_isEven);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LinqCollectionSameValuesRange()
        {
            IEnumerable<int> generated = CollectionModule.Range(1, 1);
            int[] expected = { 1 };

            bool result = generated.ForAll2(expected, _elementsAreEqual);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LinqCollectionDecreasingRange()
        {
            IEnumerable<int> generated = CollectionModule.Range(10, 1);
            int[] expected = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            bool result = generated.ForAll2(expected, _elementsAreEqual);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LinqCollectionIncreasingRange()
        {
            IEnumerable<int> generated = CollectionModule.Range(1, 10);
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            bool result = generated.ForAll2(expected, _elementsAreEqual);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LinqCollectionAppend()
        {
            IEnumerable<int> first = CollectionModule.Range(1, 500);
            IEnumerable<int> second = CollectionModule.Range(501, 1000);
            IEnumerable<int> expected = CollectionModule.Range(1, 1000);

            bool result = expected.ForAll2(first.Append(second), _elementsAreEqual);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void LinqCollectionChoose()
        {
            string _evenValue = "Even";
            Func<int, Option<string>> chooser =
                element => _isEven(element) ? _evenValue : null;

            IEnumerable<string> result =
                _1000values.Choose(chooser);

            Assert.IsTrue(
                result.Count() == 500
                && result.ForAll(element => element == _evenValue)
                );
        }

        [TestMethod]
        public void LinqCollectionChooseToEmpty()
        {
            Func<int, Option<string>> chooser =
                element => element == 10000 ? "Even" : null;

            IEnumerable<string> result =
                _1000values.Choose(chooser);

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void LinqCollectionChunk()
        {
            IEnumerable<IEnumerable<int>> chunks = _10000evens.ChunkBySize(10);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LinqCollectionChunkArgumentExceptionWith0()
        {
            IEnumerable<IEnumerable<int>> chunks = _10000evens.ChunkBySize(0);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LinqCollectionChunkArgumentExceptionWithNegative()
        {
            IEnumerable<IEnumerable<int>> chunks = _10000evens.ChunkBySize(-10);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        public void LinqCollectionChunkAndConcat100()
        {
            IEnumerable<IEnumerable<int>> chunks =
                _100evens.ChunkBySize(10);

            IEnumerable<int> concat = chunks.Concat();
            Assert.AreEqual(concat.Count(), _100evens.Count());
        }

        [TestMethod]
        public void LinqCollectionChunkAndConcat1000()
        {
            IEnumerable<IEnumerable<int>> chunks =
                _1000evens.ChunkBySize(10);

            IEnumerable<int> concat = chunks.Concat();
            Assert.AreEqual(concat.Count(), _1000evens.Count());
        }

        [TestMethod]
        public void LinqCollectionChunkAndConcat10000()
        {
            IEnumerable<IEnumerable<int>> chunks =
                _10000evens.ChunkBySize(10);
            IEnumerable<int> concat = chunks.Concat();
            Assert.AreEqual(concat.Count(), _10000evens.Count());
        }

        [TestMethod]
        public void LinqCollectionCollect()
        {
            int expected = 5050;
            IEnumerable<int> result =
                _100values.Collect(element => CollectionModule.Range(1, element));

            Assert.AreEqual(expected, result.Count());
        }

        [TestMethod]
        public void LinqCollectionCompareWithEqualLists()
        {
            int expected = 0;
            int result =
            _1000values.CompareWith<int, int>(_1000values,
                (element1, element2) => element1 > element2 ? 1
                                        : element1 < element2 ? -1
                                        : 0
                );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionCompareWithFirstListBigger()
        {
            int expected = 1;
            int result =
            _1000values.CompareWith<int, int>(_1000evens,
                (element1, element2) => element1 > element2 ? 1
                                        : element1 < element2 ? -1
                                        : 0
                 );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionCompareWithFirstListSmaller()
        {
            int expected = -1;
            int result =
            _1000evens.CompareWith<int, int>(_1000values,
                (element1, element2) => element1 > element2 ? 1
                                        : element1 < element2 ? -1
                                        : 0
                );

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionCountBy()
        {
            IEnumerable<(bool Key, int Count)> result =
                _10000values.CountBy(_isEven);

            int expected = 10000;
            var headAndTailEnd = result.HeadAndTailEnd();
            int oddsCount = headAndTailEnd.Head.Count;
            int evensCount = headAndTailEnd.TailEnd.Count;

            Assert.AreEqual(expected, evensCount + oddsCount);
        }

        [TestMethod]
        public void LinqCollectionDistinct()
        {
            IEnumerable<int> result =
                _100values.Collect(element => CollectionModule.Range(1, element));

            IEnumerable<int> distinctedResult =
                result.Distinct(_elementsAreEqual, element => element);

            Assert.IsTrue(distinctedResult.ForAll2(CollectionModule.Range(1, 100), _elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionExists2FirstTrue()
        {
            bool expected = true;
            bool result =
                _10000evens.Exists2(_10000evens, _elementsAreEqual);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionExists2True()
        {
            bool expected = true;
            bool result =
            CollectionModule.Range(0, 100000).Exists2(CollectionModule.Range(100000, 0), _elementsAreEqual);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionExists2False()
        {
            bool expected = false;
            bool result =
            CollectionModule.Range(0, 100000).Exists2(CollectionModule.Range(1, 100001), _elementsAreEqual);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionFindIndex()
        {
            int expected = 99;
            int index =
                _10000evens.FindIndex(element => element == 100);

            Assert.AreEqual(expected, index);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LinqCollectionFindIndexException()
        {
            int index =
                _10000evens.FindIndex(element => element == -100);
        }

        [TestMethod]
        public void LinqCollectionFold()
        {
            int expected = 65;
            int result =
                _0to10values.Fold(10, (accumulator, element) => accumulator + element);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionFold2()
        {
            int expected = 20;
            int result =
                CollectionModule.Range(1, 3).Fold2(
                    CollectionModule.Range(3, 1),
                    12,
                    (accumulator, element1, element2) =>
                        accumulator + Math.Max(element1, element2));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionFoldBack()
        {
            int expected = -5;
            int result =
                _0to10values.FoldBack((accumulator, element) => accumulator - element, 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionFoldBack2()
        {
            int expected = -10;
            int result =
                _0to10values.FoldBack2(_10to0values,
                    (accumulator, element1, element2) =>
                        accumulator - Math.Max(element1, element2), 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionHead()
        {
            int expected = 0;
            int result = _0to10values.Head();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LinqCollectionHeadException()
        {
            var empty = CollectionModule.Empty<int>();
            empty.Head();
        }

        [TestMethod]
        public void LinqCollectionHeadAndTail()
        {
            (int Head, int TailEnd) expected = (0, 10);
            (int Head, int TailEnd) result =
                _0to10values.HeadAndTailEnd();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionHeadAndTail10000Elements()
        {
            (int Head, int TailEnd) expected = (1, 10000);
            (int Head, int TailEnd) result =
                _10000values.HeadAndTailEnd();

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LinqCollectionHeadAndTailException()
        {
            var empty = CollectionModule.Empty<int>();
            empty.Head();
        }

        [TestMethod]
        public void LinqCollectionIterate()
        {
            int expected = 55;
            int result = 0;
            _0to10values.Iterate(element => result += element);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void LinqCollectionIterate2()
        {
            int expected = 110;
            int result = 0;
            _0to10values.Iterate2(_10to0values,
                (element1, element2) => result += element1 + element2);

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void LinqCollectionMap()
        {
            IEnumerable<int> generated =
                CollectionModule.Generate(1, 2, 3).Map(element => element * 2);

            IEnumerable<int> expected =
                CollectionModule.Generate(2, 4, 6);

            Assert.IsTrue(generated.ForAll2(expected, _elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionMap2()
        {
            IEnumerable<int> generated =
                CollectionModule.Generate(1, 2, 3).Map2(
                    CollectionModule.Generate(3, 2, 1),
                    (element1, element2) => Math.Max(element1, element2) * 2
                );

            IEnumerable<int> expected =
                CollectionModule.Generate(6, 4, 6);

            Assert.IsTrue(generated.ForAll2(expected, _elementsAreEqual));
        }
        [TestMethod]
        public void LinqCollectionMap3()
        {
            IEnumerable<int> generated =
                CollectionModule.Generate(1, 2, 3).Map3(
                    CollectionModule.Generate(3, 2, 1),
                    CollectionModule.Generate(3, 3, 3),
                    (element1, element2, element3) =>
                        Math.Max(Math.Max(element1, element2), element3) * 2
                );

            IEnumerable<int> expected =
                CollectionModule.Generate(6, 6, 6);

            Assert.IsTrue(expected.ForAll2(generated, _elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionMapIndexed()
        {
            IEnumerable<int> generated =
                CollectionModule.Generate(1, 2, 3).MapIndexed(
                    (index, element) => index + element * 2);

            IEnumerable<int> expected =
                CollectionModule.Generate(2, 5, 8);

            Assert.IsTrue(generated.ForAll2(expected,_elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionMapIndexed2()
        {
            IEnumerable<int> generated =
                CollectionModule.Generate(1, 2, 3).MapIndexed2(
                     CollectionModule.Generate(3, 2, 1),
                    (index, element1, element2) => 
                        index + Math.Max(element1, element2) * 2);

            IEnumerable<int> expected =
                CollectionModule.Generate(6, 5, 8);

            Assert.IsTrue(generated.ForAll2(expected,_elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionMapIndexed3()
        {
            IEnumerable<int> generated =
                CollectionModule.Generate(1, 2, 3).MapIndexed3(
                    CollectionModule.Generate(3, 2, 1),
                    CollectionModule.Generate(3, 3, 3),
                    (index, element1, element2, element3) => 
                        index + Math.Max(Math.Max(element1, element2), element3) * 2);

            IEnumerable<int> expected =
                CollectionModule.Generate(6, 7, 8);

            Assert.IsTrue(expected.ForAll2(generated, _elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionPartition()
        {
            var (evens, odds) =
                CollectionModule.Range(0, 1000).Partition(element => element % 2 == 0);

            IEnumerable<int> expectedEvens =
                CollectionModule.Initialize(500, index => index * 2);

            IEnumerable<int> expectedOdds =
                CollectionModule.Initialize(500, index => index * 2 + 1);


            Assert.IsTrue(
                evens.ForAll2(expectedEvens, _elementsAreEqual)
                && odds.ForAll2(expectedOdds, _elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionPermute()
        {
            IEnumerable<int> original =
                _0to5values;

            IEnumerable<int> permuted =
                original.Permute(index => (index + 3) % 6);

            IEnumerable<int> expected =
                CollectionModule.Generate(3, 4, 5, 0, 1, 2);

            Assert.IsTrue(permuted.ForAll2(expected,_elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionPick()
        {
            int expected = 400;
            int value =
                _1000values.Pick(
                    element => element == 400 ?
                                element
                                : Option<int>.None());

            Assert.AreEqual(expected, value);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LinqCollectionPickInvalidOperationException()
        {
            _1000values.Pick(
                    element => element == 4000 ?
                                element
                                : Option<int>.None());
        }

        [TestMethod]
        public void LinqCollectionScan()
        {
            IEnumerable<int> expected =
                CollectionModule.Generate(10, 11, 13, 16, 20, 25, 31, 38, 46, 55, 65);

            IEnumerable<int> result =
                _0to10values.Scan(10, (accumulator, element) => accumulator + element);

            Assert.IsTrue(result.ForAll2(expected, _elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionScanBack()
        {
            IEnumerable<int> expected =
                CollectionModule.Generate(65, 65, 64, 62, 59, 55, 50, 44, 37, 29, 20);

            IEnumerable<int> result =
                _0to10values.ScanBack((accumulator, element) => accumulator + element, 10);

            Assert.IsTrue(result.ForAll2(expected, _elementsAreEqual));
        }


        [TestMethod]
        public void LinqCollectionScan2()
        {
            IEnumerable<int> expected =
                CollectionModule.Generate(15, 20, 25, 30, 35, 40);

            IEnumerable<int> result =
                CollectionModule.Range(0, 5).Scan2(
                    CollectionModule.Range(5, 0), 10,
                    (accumulator, element1, element2) => accumulator + element1 + element2);

            Assert.IsTrue(result.ForAll2(expected,_elementsAreEqual));
        }

        [TestMethod]
        public void LinqCollectionScanBack2()
        {
            IEnumerable<int> expected =
                CollectionModule.Generate(40, 35, 30, 25, 20, 15);

            IEnumerable<int> result =
                CollectionModule.ScanBack2(
                    (accumulator, element1, element2) => accumulator + element1 + element2,
                    _0to5values,
                    _5to0values
                    , 10);

            Assert.IsTrue(CollectionModule.ForAll2(_elementsAreEqual, expected, result));
        }

        [TestMethod]
        public void LinqCollectionTail()
        {
            IEnumerable<int> expected =
                CollectionModule.Range(1, 5);

            IEnumerable<int> result =
                _0to5values.Tail();

            Assert.IsTrue(
                result.ForAll2(expected, _elementsAreEqual)
                && expected.Count() == result.Count());
        }

        [TestMethod]
        public void LinqCollectionTryFind()
        {
            Option<int> result =
                _10000values.TryFind(element => element == 400);

            int someResult = result.Match(value => value, () => 0);

            Assert.IsTrue(result.IsSome && someResult == 400);
        }

        [TestMethod]
        public void LinqCollectionTryFindNone()
        {
            Option<int> result =
             _1000values.TryFind(element => element == 4000);

            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void LinqCollectionTryPick()
        {
            Option<int> result =
                CollectionModule.Generate(11, 15, 19, 22, 20)
                    .TryPick(element =>
                        element % 2 == 0 ?
                        element
                        : Option<int>.None());

            int someResult = result.Match(value => value, () => 0);

            Assert.IsTrue(result.IsSome && someResult == 22);
        }

        [TestMethod]
        public void LinqCollectionTryPickNone()
        {
            Option<int> result =
                CollectionModule.Generate(11, 15, 19, 23, 29)
                    .TryPick(element =>
                        element % 2 == 0 ?
                        element
                        : Option<int>.None());
            Assert.IsTrue(result.IsNone);
        }

        [TestMethod]
        public void LinqCollectionUnzip()
        {
            IEnumerable<(int, bool)> numbersAndBools =
                CollectionModule.Generate((10, true), (2, false), (33, true), (9, false), (42, true));

            var (numbers, booleans) =
                numbersAndBools.Unzip();

            IEnumerable<int> expectedNumbers =
                CollectionModule.Generate(10, 2, 33, 9, 42);

            IEnumerable<bool> expectedBooleans =
                CollectionModule.Generate(true, false, true, false, true);

            Assert.IsTrue(
                numbers.ForAll2(expectedNumbers, _elementsAreEqual)
                && booleans.ForAll2(expectedBooleans,
                    (element1, element2) => element1 == element2));
        }

        [TestMethod]
        public void LinqCollectionUnzip3()
        {
            IEnumerable<(int, bool, int)> numbersBoolsAndRange =
                CollectionModule.Generate(
                    (10, true, 1),
                    (2, false, 2),
                    (33, true, 3),
                    (9, false, 4),
                    (42, true, 5));

            var (numbers, booleans, numbers2) =
                numbersBoolsAndRange.Unzip3();

            IEnumerable<int> expectedNumbers =
                CollectionModule.Generate(10, 2, 33, 9, 42);

            IEnumerable<bool> expectedBooleans =
                CollectionModule.Generate(true, false, true, false, true);

            Assert.IsTrue(
                numbers.ForAll2(expectedNumbers,_elementsAreEqual)
                && booleans.ForAll2(expectedBooleans,
                    (element1, element2) => element1 == element2)
                && numbers2.ForAll2(CollectionModule.Range(1, 5), _elementsAreEqual )
                    );
        }

        [TestMethod]
        public void LinqCollectionZip()
        {
            IEnumerable<(int, int)> pairs =
                _0to10values.Zip(_10to0values);

            Assert.IsTrue(
                pairs.ForAll3(
                    _0to10values, _10to0values,
                    (element1, element2, element3) => 
                        element1.Item1 == element2 && element1.Item2 == element3));
        }

        [TestMethod]
        public void LinqCollectionZip3()
        {
            IEnumerable<(int, int, int)> triples =
                CollectionModule.Zip3(_0to10values, _10to0values, _100values);

            Assert.IsTrue(
                CollectionModule.ForAll2(
                    (element1, element2) => element1.Item1 == element2, triples, _0to10values)
                && CollectionModule.ForAll2(
                    (element1, element2) => element1.Item2 == element2, triples, _10to0values)
                && CollectionModule.ForAll2(
                    (element1, element2) => element1.Item3 == element2, triples, _100values)
                    );
        }

    }
}
