using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NHapi.Base.Util;
using NUnit.Framework;

namespace NHapi.NUnit
{
    public class FilterEnumeratorTest
    {
        private List<int> theCollection;
        private FilterEnumerator unitUnderTest;

        class EvenNumberPredicate : FilterEnumerator.IPredicate
        {
            public bool Evaluate(object obj)
            {
                int theInteger = (int) obj;
                return theInteger%2 == 0;
            }
        }

        [SetUp]
        public void Setup()
        {
            theCollection = Enumerable.Range(1, 20).ToList();
            unitUnderTest = new FilterEnumerator(theCollection.GetEnumerator(), new EvenNumberPredicate());
        }

        [Test, ExpectedException]
        public void Current_PostConstruction_ThrowsException()
        {
            // setup

            // method under test
            object rval = unitUnderTest.Current;

            // assertions
        }

        [Test]
        public void Current_AfterCurrent_IsSameObject()
        {
            // setup
            unitUnderTest.MoveNext();
            object firstObject = unitUnderTest.Current;

            // method under test
            object secondObject = unitUnderTest.Current;

            // assertions
            Assert.That(firstObject, Is.EqualTo(secondObject));
        }

        [Test]
        public void Current_AfterMoveNextReturnsTrue_IsNotNull()
        {
            // setup
            unitUnderTest.MoveNext();

            // method under test
            object rval = unitUnderTest.Current;

            // assertions
            Assert.That(rval, Is.Not.Null);
        }

        [Test, ExpectedException]
        public void Current_AfterMoveNextReturnsFalse_ThrowsException()
        {
            // setup
            while (unitUnderTest.MoveNext()) { }

            // method under test
            object rval = unitUnderTest.Current;

            // assertions
        }

        [Test, ExpectedException]
        public void Current_AfterReset_ThrowsException()
        {
            // setup
            unitUnderTest.MoveNext();
            unitUnderTest.Reset();

            // method under test
            object rval = unitUnderTest.Current;

            // assertions
        }

        [Test]
        public void Current_AfterMoveNextAfterReset_IsNotNull()
        {
            // setup
            unitUnderTest.MoveNext();
            unitUnderTest.Reset();
            unitUnderTest.MoveNext();

            // method under test
            object rval = unitUnderTest.Current;

            // assertions
            Assert.That(rval, Is.Not.Null);
        }

        [Test]
        public void MoveNext_AfterConstructionWhenSetNotEmpty_ReturnsTrue()
        {
            // setup

            // method under test
            bool rval = unitUnderTest.MoveNext();

            // assertions
            Assert.That(rval, Is.True);
        }

        [Test]
        public void MoveNext_FirstInvocationAndUnderlyingEnumeratorHasCurrent_SkipsThatElement()
        {
            // setup
            theCollection = Enumerable.Range(1, 20).ToList();
            IEnumerator underlyingEnumerator = theCollection.GetEnumerator();
            underlyingEnumerator.MoveNext();
            unitUnderTest = new FilterEnumerator(underlyingEnumerator, new EvenNumberPredicate());

            // method under test
            bool rval = unitUnderTest.MoveNext();

            // assertions
            Assert.That(unitUnderTest.Current, Is.EqualTo(2));
        }

        [Test]
        public void MoveNext_AfterLastObject_ReturnsFalse()
        {
            // setup
            for (int i = 0; i < 5000; i++)
            {
                unitUnderTest.MoveNext();
            }

            // method under test
            bool rval = unitUnderTest.MoveNext();

            // assertions
            Assert.That(rval, Is.False);
        }
 
    }
}