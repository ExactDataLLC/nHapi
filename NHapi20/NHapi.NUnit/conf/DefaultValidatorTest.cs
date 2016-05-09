using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NHapi.Base;
using NHapi.Base.Conf.Parser;
using NHapi.Base.Conf.Spec;
using NHapi.Base.Conf.Spec.Message;
using NHapi.Base.Conf.Store;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V24.Message;
using NUnit.Framework;

namespace NHapi.Base.Conf.Check
{

	/// <summary>
	/// JUnit tests for DefaultValidator
	/// @author Bryan Tripp
	/// </summary>
	[TestFixture]
	public class DefaultValidatorTest 
	{

		private RuntimeProfile profile;
		private ACK msg;

        [SetUp]
		public virtual void setUp()
		{
            string profileString = File.ReadAllText("resources/conf/parser/example_ack.xml");
			
			//System.out.println(profileString);
			ProfileParser parser = new ProfileParser(false);
			profile = parser.parse(profileString);

            Uri specURL = new Uri("resources/conf/store/sampleTables.xml", UriKind.Relative);
			ICodeStore store = new ProfileCodeStore(specURL);
			ProfileStoreFactory.addCodeStore(store, ".*ConfSig.*"); //need qualifier to avoid collision with ProfileStoreFactpryTest

			string message = "MSH|^~\\&|||||||ACK^A01|1|D|2.4|||||CAN|wrong|F^^HL70001^x^^HL78888|\r"; //note HL7888 doesn't exist
            msg = (ACK) new PipeParser().Parse(message);
		}

        [Test]
		public virtual void testRequiredSegment()
        {
            string profileString = File.ReadAllText("resources/conf/parser/ADT_A01_reqsft.xml");

            //System.out.println(profileString);
			ProfileParser parser = new ProfileParser(false);
			RuntimeProfile prof = parser.parse(profileString);

			DefaultValidator v = new DefaultValidator();

			// This message has an unsupported second component in MSH-7 
			string message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r";
			
            PipeParser msgParser = new PipeParser();
            IMessage msg = msgParser.Parse(message);
			
			HL7Exception[] problems = v.Validate(msg, prof.Message);
            string toString = string.Join(", ", problems.Select(p => p.ToString()));

			Assert.True(problems.Length > 0);
            Assert.True(toString.Contains("SFT must have at least 1"), toString);

		}

        [Test]
        public virtual void testNotSupportedSegment()
		{

            string profileString = File.ReadAllText("resources/conf/parser/ADT_A01_segnotsup.xml");

			//System.out.println(profileString);
			ProfileParser parser = new ProfileParser(false);
			RuntimeProfile prof = parser.parse(profileString);

			DefaultValidator v = new DefaultValidator();

			string message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r";
            PipeParser msgParser = new PipeParser();
            IMessage msg = msgParser.Parse(message);

			HL7Exception[] problems = v.Validate(msg, prof.Message);
            string toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123";
            msg = msgParser.Parse(message);

            problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 1, toString);
            Assert.True(toString.Contains("SFT"), toString);

		}

        [Test]
        public virtual void testNotSupportedField()
		{

            string profileString = File.ReadAllText("resources/conf/parser/ADT_A01_fieldnotsup.xml");
			
			//System.out.println(profileString);
			ProfileParser parser = new ProfileParser(false);
			RuntimeProfile prof = parser.parse(profileString);

			DefaultValidator v = new DefaultValidator();

			string message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r";
            PipeParser msgParser = new PipeParser();
            IMessage msg = msgParser.Parse(message);

            HL7Exception[] problems = v.Validate(msg, prof.Message);
            string toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123";
            msg = msgParser.Parse(message);

            problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123|sssss";
            msg = msgParser.Parse(message);

            problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 1, toString);
            Assert.True(toString.Contains("Field 2"), toString);

		}

        [Test]
        public virtual void testNotSupportedComponent()
		{

            string profileString = File.ReadAllText("resources/conf/parser/ADT_A01_compnotsup.xml");			
			
			//System.out.println(profileString);
			ProfileParser parser = new ProfileParser(false);
			RuntimeProfile prof = parser.parse(profileString);

			DefaultValidator v = new DefaultValidator();

			string message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r";
            PipeParser msgParser = new PipeParser();
            IMessage msg = msgParser.Parse(message);

			HL7Exception[] problems = v.Validate(msg, prof.Message);
            string toString = toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123|sssss";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123^1111|sssss";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 1, toString);
            Assert.True(toString.Contains("organization name type code"), toString);

		}

        [Test]
        public virtual void testNotSupportedSubComponent()
		{

            string profileString = File.ReadAllText("resources/conf/parser/ADT_A01_subcompnotsup.xml");			
			//System.out.println(profileString);
			ProfileParser parser = new ProfileParser(false);
			RuntimeProfile prof = parser.parse(profileString);

			DefaultValidator v = new DefaultValidator();

			string message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r";
            PipeParser msgParser = new PipeParser();
            IMessage msg = msgParser.Parse(message);

			HL7Exception[] problems = v.Validate(msg, prof.Message);
            string toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123|sssss";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|123^1111|sssss";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length == 0, toString);

			message = "MSH|^~\\&|^QueryServices||||20021011161756-0500||ADT^A01^ADT_A01|1|D|2.5\r" + "SFT|1^2^3^4^5^6&aaa|sssss";
            msg = msgParser.Parse(message);

			problems = v.Validate(msg, prof.Message);
            toString = string.Join(", ", problems.Select(p => p.ToString()));

            Assert.True(problems.Length > 0, toString);
            Assert.True(toString.Contains("universal ID"), toString);

		}

        [Test]
        public virtual void testTableValidation()
		{
			DefaultValidator v = new DefaultValidator();
			Seg mshProfile = (Seg) profile.Message.getChild(1);
			string profileID = "{ConfSig(1) CCO(1) 2.4(7) static-profile(1) ADT(3) A01(1) null(0) ADT_A01(4) HL7 2.4(1) Sender(1)}";

			IList<HL7Exception> e = v.testField(msg.MSH.CountryCode, mshProfile.GetField(17), false, profileID);
			printExceptions(e);
			Assert.AreEqual(0, e.Count);

			//should return an exception saying that the code "wrong" is not found 
            e = v.testField(msg.MSH.GetCharacterSet(0), mshProfile.GetField(18), false, profileID);
			Assert.AreEqual(1, e.Count);
			Assert.True(e[0].Message.Contains("wrong"));

            e = v.testField(msg.MSH.PrincipalLanguageOfMessage, mshProfile.GetField(19), false, profileID);

			printExceptions(e);

			//HEY!  this will fail if something unrelated goes wrong ... check output of the above to see what it is
			Assert.AreEqual(0, e.Count);
		}

		public virtual void printExceptions(IList<HL7Exception> exceptions)
		{
			foreach (Exception e in exceptions)
			{
//JAVA TO C# CONVERTER WARNING: The .NET Type.FullName property will not always yield results identical to the Java Class.getName method:
				Console.WriteLine(e.GetType().FullName + ": " + e.Message);
			}
		}

	}

}