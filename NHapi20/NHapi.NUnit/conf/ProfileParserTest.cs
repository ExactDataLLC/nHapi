using System;
using System.IO;
using System.Text;
using NHapi.Base.Conf.Spec;
using NHapi.Base.Conf.Spec.Message;
using NUnit.Framework;

namespace NHapi.Base.Conf.Parser
{

	/// <summary>
	/// JUnit tests for conformance profile parser
	/// 
	/// @author bryan
	/// </summary>
	[TestFixture]
	public class ProfileParserTest
	{
        [SetUp]
		public virtual void setUp()
		{
			// System.out.println(profileString);
		}

        [Test]
        public virtual void testParse()
		{
			string profileString = File.ReadAllText("resources/conf/parser/example_ack.xml");

			ProfileParser pp = new ProfileParser(true);
			RuntimeProfile rp = pp.Parse(profileString);
			Assert.AreEqual("2.4", rp.HL7Version);
			StaticDef p = rp.Message;

			Assert.AreEqual("ACK", p.MsgType);
			Assert.AreEqual("ACK", p.MsgStructID);
			Assert.True(p.EventDesc.IndexOf("general") > 1);

			Seg sp = (Seg) p.getChild(1);
			Assert.AreEqual("MSH", sp.Name);
			Assert.AreEqual("Message Header", sp.LongName);
			Assert.AreEqual("R", sp.Usage);
			Assert.AreEqual(1, sp.Min);
			Assert.AreEqual(1, sp.Max);
			Field fieldSep = sp.GetField(1);
			Assert.AreEqual("Field Separator", fieldSep.Name);
			Assert.AreEqual("R", fieldSep.Usage);
			Assert.AreEqual(1, fieldSep.Min);
			Assert.AreEqual(1, fieldSep.Max);
			Assert.AreEqual("ST", fieldSep.Datatype);
			Assert.AreEqual(1, fieldSep.Length);
			Assert.AreEqual(1, fieldSep.ItemNo);
			Assert.AreEqual("2.16.9.1", fieldSep.Reference);
            Field VID = sp.GetField(12);
			Component vid = VID.getComponent(1);
			Assert.AreEqual("version ID", vid.Name);
			Assert.AreEqual("O", vid.Usage);
			Assert.AreEqual("ID", vid.Datatype);
			Assert.AreEqual(3, vid.Length);
			Assert.AreEqual("0104", vid.Table);
			Assert.AreEqual(3, VID.Components);
			Assert.AreEqual(6, VID.getComponent(2).SubComponents);
			SubComponent name = VID.getComponent(2).getSubComponent(3);
			Assert.AreEqual("name of coding system", name.Name);
			Assert.AreEqual("O", name.Usage);
			Assert.AreEqual("IS", name.Datatype);
			Assert.AreEqual(3, name.Length);
			Assert.AreEqual("0396-X", name.Table);

			sp = (Seg) p.getChild(2);
			Assert.AreEqual("MSA", sp.Name);

			sp = (Seg) p.getChild(3);
			Assert.AreEqual("ERR", sp.Name);

		}

	}

}