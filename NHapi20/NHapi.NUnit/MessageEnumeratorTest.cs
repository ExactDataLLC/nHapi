using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.Util;
using NUnit.Framework;

namespace NHapi.NUnit
{
    [TestFixture]
    public class MessageEnumeratorTest
    {
        private MessageEnumerator unitUnderTest;

        [SetUp]
        public void Setup()
        {
            string message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r";
            PipeParser msgParser = new PipeParser();
            IMessage theMessage = msgParser.Parse(message);
            unitUnderTest = new MessageEnumerator(theMessage, "MSH", false);
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