using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using Tango.Types;

namespace Tango.Test.Types
{
    [TestClass]
    public class CollectionTests

    {
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

            _100evens = Collection.Initialize(100, evensFunction);
            _1000evens = Collection.Initialize(1000, evensFunction);
            _10000evens = Collection.Initialize(10000, evensFunction);
            _100evens1Odd = Collection.Initialize(100, evensOddFunction);
            _1000evens1Odd = Collection.Initialize(1000, evensOddFunction);
            _10000evens1Odd = Collection.Initialize(10000, evensOddFunction);
            _100values = Collection.Range(1, 100);
            _1000values = Collection.Range(1, 1000);
            _10000values = Collection.Range(1, 10000);
        }

        [TestMethod]
        public void CollectionForAllFalseTest()
        {
            bool result = Collection.ForAll(_isEven, _100evens1Odd);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CollectionForAllTrueTest()
        {
            bool result = Collection.ForAll(_isEven, _100evens);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionSameValuesRangeTest()
        {
            IEnumerable<int> generated = Collection.Range(1, 1);
            int[] expected = { 1 };

            bool result = Collection.ForAll2(_elementsAreEqual, generated, expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionDecreasingRangeTest()
        {
            IEnumerable<int> generated = Collection.Range(10, 1);
            int[] expected = { 10, 9, 8, 7, 6, 5, 4, 3, 2, 1 };

            bool result = Collection.ForAll2(_elementsAreEqual, generated, expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionIncreasingRangeTest()
        {
            IEnumerable<int> generated = Collection.Range(1, 10);
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            bool result = Collection.ForAll2(_elementsAreEqual, generated, expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionAppendTest()
        {
            IEnumerable<int> first = Collection.Range(1, 500);
            IEnumerable<int> second = Collection.Range(501, 1000);
            IEnumerable<int> expected = Collection.Range(1, 1000);

            bool result = Collection.ForAll2(_elementsAreEqual, Collection.Append(first, second), expected);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CollectionChooseTest()
        {
            string _evenValue = "Even";
            Func<int, Option<string>> chooser =
                element => _isEven(element) ? _evenValue : null;

            IEnumerable<string> results =
                Collection.Choose(chooser, _1000values);

            Assert.IsTrue(
                results.Count() == 500
                && Collection.ForAll(element => element == _evenValue, results)
                );
        }

        [TestMethod]
        public void CollectionChooseToEmptyTest()
        {
            Func<int, Option<string>> chooser =
                element => element == 10000 ? "Even" : null;

            IEnumerable<string> results =
                Collection.Choose(chooser, _1000values);

            Assert.AreEqual(0, results.Count());
        }

        [TestMethod]
        public void CollectionChunkTest()
        {
            var chunks = Collection.ChunkBySize(10, _10000evens);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectionChunkArgumentExceptionWith0Test()
        {
            var chunks = Collection.ChunkBySize(0, _10000evens);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CollectionChunkArgumentExceptionWithNegativeTest()
        {
            var chunks = Collection.ChunkBySize(-10, _10000evens);
            Assert.AreEqual(chunks.Count(), 10000 / 10);
        }

        [TestMethod]
        public void CollectionChunkAndConcat100Test()
        {
            var chunks = Collection.ChunkBySize(10, _100evens);
            var concat = Collection.Concat(chunks);
            Assert.AreEqual(concat.Count(), _100evens.Count());
        }

        [TestMethod]
        public void CollectionChunkAndConcat1000Test()
        {
            var chunks = Collection.ChunkBySize(10, _1000evens);
            var concat = Collection.Concat(chunks);
            Assert.AreEqual(concat.Count(), _1000evens.Count());
        }

        [TestMethod]
        public void CollectionChunkAndConcat10000Test()
        {
            var chunks = Collection.ChunkBySize(10, _10000evens);
            var concat = Collection.Concat(chunks);
            Assert.AreEqual(concat.Count(), _10000evens.Count());
        }

        [TestMethod]
        public void CollectionCollectTest()
        {
            int expected = 5050;
            var results =
                Collection.Collect(element => Enumerable.Range(1, element), _100values);

            Assert.AreEqual(expected, results.Count());
        }

        [TestMethod]
        public void CollectionCompareWithEqualListsTest()
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
        public void CollectionCompareWithFirstListBiggerTest()
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
        public void CollectionCompareWithFirstListSmallerTest()
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
        public void CollectionCountByTest()
        {
            var result =
                Collection.CountBy(_isEven, _10000values);

            int expected = 10000;
            var headAndTailEnd = Collection.HeadAndTailEnd(result);
            int oddsCount = headAndTailEnd.Item1.Item2;
            int evensCount = headAndTailEnd.Item2.Item2;

            Assert.AreEqual(expected, evensCount + oddsCount);
        }

        [TestMethod]
        public void CollectionDistinctTest()
        {
            var results =
                Collection.Collect(element => Enumerable.Range(1, element), _100values);

            var distinctedResult =
            Collection.Distinct(_elementsAreEqual, element => element, results);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, distinctedResult, Enumerable.Range(1, 100)));
        }

        [TestMethod]
        public void CollectionEmptyTest()
        {
            int expected = 0;
            var results = Collection.Empty<int>();

            Assert.AreEqual(expected, results.Count());
        }

        [TestMethod]
        public void CollectionExists2FirstTrueTest()
        {
            bool expected = true;
            bool result =
            Collection.Exists2(_elementsAreEqual, _10000evens, _10000evens);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionExists2TrueTest()
        {
            bool expected = true;
            bool result =
            Collection.Exists2(_elementsAreEqual, Collection.Range(0, 100000), Collection.Range(100000, 0));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionExists2FalseTest()
        {
            bool expected = false;
            bool result =
            Collection.Exists2(_elementsAreEqual, Collection.Range(0, 100000), Collection.Range(1, 100001));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFindIndexTest()
        {
            int expected = 99;
            int index =
                Collection.FindIndex(element => element == 100, _10000evens);

            Assert.AreEqual(expected, index);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionFindIndexExceptionTest()
        {
            int index =
                Collection.FindIndex(element => element == -100, _10000evens);
        }

        [TestMethod]
        public void CollectionFoldTest()
        {
            int expected = 65;
            int result =
                Collection.Fold((accumulator, element) => accumulator + element, 10, Collection.Range(0, 10));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFold2Test()
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
        public void CollectionFoldBackTest()
        {
            int expected = -5;
            int result =
                Collection.FoldBack((accumulator, element) => accumulator - element, Collection.Range(0, 10), 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionFoldBack2Test()
        {
            int expected = -9;
            int result =
                Collection.FoldBack2(
                    (accumulator, element1, element2) =>
                        accumulator - Math.Max(element1, element2),
                    Collection.Range(1, 10), Collection.Range(10, 1), 10);

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionHeadTest()
        {
            int expected = 1;
            int result = Collection.Head(Collection.Range(1, 10));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionHeadExceptionTest()
        {
            Collection.Head(Collection.Empty<int>());
        }

        [TestMethod]
        public void CollectionHeadAndTailTest()
        {
            (int, int) expected = (1, 10);
            (int, int) result = Collection.HeadAndTailEnd(Collection.Range(1, 10));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionHeadAndTail10000ElementsTest()
        {
            (int, int) expected = (1, 10000);
            (int, int) result = Collection.HeadAndTailEnd(Collection.Range(1, 10000));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void CollectionHeadAndTailExceptionTest()
        {
            Collection.Head(Collection.Empty<int>());
        }

        [TestMethod]
        public void CollectionGenerateTest()
        {
            IEnumerable<int> generated = Collection.Generate(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            int[] expected = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionGenerate2Test()
        {
            IEnumerable<int> generated = Collection.Generate(1, 10, 5, 3, 12, 5, 7, 9);
            int[] expected = { 1, 10, 5, 3, 12, 5, 7, 9 };

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionGenerateAndRangeTest()
        {
            IEnumerable<int> generated = Collection.Generate(1, 2, 3, 4, 5, 6, 7, 8, 9, 10);
            IEnumerable<int> rangeGenerated = Collection.Range(1, 10);

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, rangeGenerated, generated));
        }

        [TestMethod]
        public void CollectionInitializeTest()
        {
            IEnumerable<int> generated = Collection.Initialize(11, (index) => index * 2);
            IEnumerable<int> expected = Collection.Map(n => n * 2, Collection.Range(0, 10));

            Assert.IsTrue(Collection.ForAll2(_elementsAreEqual, expected, generated));
        }

        [TestMethod]
        public void CollectionIterateTest()
        {
            int expected = 55;
            int result = 0;
            Collection.Iterate(element => result += element, Collection.Range(0,10));

            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CollectionIterate2Test()
        {
            int expected = 110;
            int result = 0;
            Collection.Iterate2( (element1, element2) => result += element1 + element2, Collection.Range(0, 10), Collection.Range(10, 0));

            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void CollectionMapTest()
        {
            Collection.Map(e => e * 2,_10000values).Count();
        }

        [TestMethod]
        public void CollectionSelectTest()
        {
            _10000values.Select(e => e * 2).Count();
        }
    }
}
