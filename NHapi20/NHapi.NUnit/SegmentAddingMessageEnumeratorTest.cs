using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.Util;
using NHapi.Model.V231.Message;
using NUnit.Framework;

namespace NHapi.NUnit
{
    [TestFixture]
    public class SegmentAddingMessageEnumeratorTest
    {
        private SegmentAddingMessageEnumerator unitUnderTest;
        private IMessage theMessage;

        [SetUp]
        public void Setup()
        {
            string message = @"MSH|^~&|EMR|Sending|Dest|Receiving|20150216152626||ORM^O01|1|P|2.3.1|||AL||||||
PID||1|1||q^q|||F||||||||||||".Replace('\n', '\r');
            PipeParser msgParser = new PipeParser();
            theMessage = msgParser.Parse(message);
            unitUnderTest = new SegmentAddingMessageEnumerator(theMessage, "MSH");
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
        public void MoveNext_Always_ReturnsTrue()
        {
            // setup
            for (int i = 0; i < 5000 && unitUnderTest.MoveNext(); ++i)
            {
            }

            // method under test
            bool rval = unitUnderTest.MoveNext();

            // assertions
            Assert.That(rval, Is.True);
        }

        [Test]
        public void WasCurrentAdded_AfterMoveNextToExpectedSegment_IsFalse()
        {
            // setup
            ORM_O01 orm = theMessage as ORM_O01;
            unitUnderTest = new SegmentAddingMessageEnumerator(orm.MSH, "PID");
            unitUnderTest.MoveNext();

            // method under test
            bool rval = unitUnderTest.MoveNext();

            // assertions
            Assert.That(unitUnderTest.WasCurrentAdded, Is.False);
        }

        [Test]
        public void WasCurrentAdded_AfterMoveNextToUnexpectedSegment_IsTrue()
        {
            // setup
            ORM_O01 orm = theMessage as ORM_O01;
            unitUnderTest = new SegmentAddingMessageEnumerator(orm.GetORDER().BLG, "EVN");
            unitUnderTest.MoveNext();

            // method under test
            bool rval = unitUnderTest.MoveNext();

            // assertions
            Assert.That(unitUnderTest.WasCurrentAdded, Is.True);
        }
    }
}