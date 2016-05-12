using System;
using System.Linq;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Model.V251.Datatype;
using NHapi.Model.V251.Message;
using NHapi.Model.V251.Segment;
using NUnit.Framework;

namespace NHapi.NUnit
{
	internal class PipeParsingFixture251
	{
		public string GetMessage()
		{
			return @"MSH|^~\&|XPress Arrival||||200610120839||ORU^R01|EBzH1711114101206|P|2.5.1|||AL|||ASCII
PID|1||1711114||Appt^Test||19720501||||||||||||001020006
ORC|||||F
OBR|1|||ehipack^eHippa Acknowlegment|||200610120839|||||||||00002^eProvider^Electronic|||||||||F
OBX|1|FT|||This\.br\is\.br\A Test~MoreText~SomeMoreText||||||F
OBX|2|FT|||This\.br\is\.br\A Test~MoreText~SomeMoreText||||||F
OBX|3|FT|||This\.br\is\.br\A Test~MoreText~SomeMoreText||||||F".
                Replace('\n', '\r');
		}

		[Test]
		public void TestOBR5RepeatingValuesMessage_DataTypesAndRepetitions()
		{
			var parser = new PipeParser();
			var oru = new ORU_R01();
			oru = (ORU_R01) parser.Parse(GetMessage());

			int expectedObservationCount = 3;
			int parsedObservations = oru.GetPATIENT_RESULT(0).GetORDER_OBSERVATION(0).OBSERVATIONRepetitionsUsed;
			bool parsedCorrectNumberOfObservations = parsedObservations == expectedObservationCount;
			Assert.IsTrue(parsedCorrectNumberOfObservations,
				string.Format("Expected 3 OBX repetitions used for this segment, found {0}", parsedObservations));

			foreach (var obs in oru.GetPATIENT_RESULT(0).GetORDER_OBSERVATION(0).GetOBSERVATION().OBX.GetObservationValue())
			{
				Assert.IsTrue(obs.Data is FT);
			}
		}

		[TestCase(
			@"MSH|^~\&|XPress Arrival||||200610120839||ORU^R01|EBzH1711114101206|P|2.5.1|||AL|||ASCII
PID|1||1711114||Appt^Test||19720501||||||||||||001020006
ORC|||||F
OBR|1|||ehipack^eHippa Acknowlegment|||200610120839|||||||||00002^eProvider^Electronic|||||||||F
OBX|1|DT|||DTValue||||||F
OBX|2|ST|||STValue||||||F
OBX|3|TM|||TMValue||||||F"
		//OBX|4|ID|||IDValue||||||F //Doesn't work
		//OBX|5|IS|||ISValue||||||F //Doesn't work
		)]
		public void Test_251DataTypesParseCorrectly(string message)
		{
			var parser = new PipeParser();
			var oru = new ORU_R01();
			oru = (ORU_R01)parser.Parse(message.Replace('\n', '\r'));

			int expectedObservationCount = 3;
			int parsedObservations = oru.GetPATIENT_RESULT(0).GetORDER_OBSERVATION(0).OBSERVATIONRepetitionsUsed;
			bool parsedCorrectNumberOfObservations = parsedObservations == expectedObservationCount;
			Assert.IsTrue(parsedCorrectNumberOfObservations,
				string.Format("Expected {1} OBX repetitions used for this segment, found {0}", parsedObservations, expectedObservationCount));

			int index = 0;
			var obs = oru.GetPATIENT_RESULT(0).GetORDER_OBSERVATION(0).GetOBSERVATION(index).OBX.GetObservationValue().FirstOrDefault();
			Assert.IsTrue(obs.Data is DT);
			index++;
			obs = oru.GetPATIENT_RESULT(0).GetORDER_OBSERVATION(0).GetOBSERVATION(index).OBX.GetObservationValue().FirstOrDefault();
			Assert.IsTrue(obs.Data is ST);
			index++;
			obs = oru.GetPATIENT_RESULT(0).GetORDER_OBSERVATION(0).GetOBSERVATION(index).OBX.GetObservationValue().FirstOrDefault();
			Assert.IsTrue(obs.Data is TM);
		}


		[Test]
		public void TestAdtA04AndA01MessageStructure()
		{
			var result = PipeParser.GetMessageStructureForEvent("ADT_A04", "2.5");
			bool isSame = string.Compare("ADT_A01", result, StringComparison.InvariantCultureIgnoreCase) == 0;
			Assert.IsTrue(isSame, "ADT_A04 returns ADT_A01");

			result = PipeParser.GetMessageStructureForEvent("ADT_A13", "2.5");
			isSame = string.Compare("ADT_A01", result, StringComparison.InvariantCultureIgnoreCase) == 0;
			Assert.IsTrue(isSame, "ADT_A13 returns ADT_A01");

			result = PipeParser.GetMessageStructureForEvent("ADT_A08", "2.5");
			isSame = string.Compare("ADT_A01", result, StringComparison.InvariantCultureIgnoreCase) == 0;
			Assert.IsTrue(isSame, "ADT_A08 returns ADT_A01");

			result = PipeParser.GetMessageStructureForEvent("ADT_A01", "2.5");
			isSame = string.Compare("ADT_A01", result, StringComparison.InvariantCultureIgnoreCase) == 0;
			Assert.IsTrue(isSame, "ADT_A01 returns ADT_A01");
		}

	    [Test]
	    public void Parse_RdeO11WithAL1_Al1IsInPatientGroup()
	    {
	        // setup
            PipeParser parser = new PipeParser();
	        string encodedRdeO11 =
	            @"MSH|^~\&|EXACTDATA||ALL|ALL|20101217181500||RDE^O11^RDE_O11|EF010000000071000000_4|T^T|2.5.1
PID|1||7010566270^^^^DODID~EM010000008000000^^^^MR||ZZEDConner^Dean^J^^Mr.||19810611|MALE||WHITE^^HL70005|^^^^^USA^H||||ENGLISH^^HL70296|SINGLE^^HL70002||EF010000000071000000||||NOTHISPANICORLATINO^^HL70189|||||ACTIVEDUTY^^HL70172
PV1|1|OUTPATIENT|^^^MBCC^^AMBLOC||||1853210951^Pennington^Zachary|||MEDICINEGENERAL|||||||1853210951^Pennington^Zachary|||||||||||||||||||01|||||ACTIVE|||20101217181500|20101217184500||||||V
PV2|||V70.0^General medical examination^I9C|||||||||||||||||||||||||||||||||||||||||N
AL1|1|DRUG^^HL70127|70618^Penicillin^RXNORM|MODERATE^^HL70128|Dry Mouth|20100928
ORC|NW|101217-833651^ExD|||ORDERED
RXE|^^^20101217184500|54569-4888-0^Oseltamivir|1||75 mg||^po bid x 5d||N|10||0||||||||||||||||||||20101217184500
RXR|^Oral^HL70162".Replace('\n', '\r');

	        // method under test
	        RDE_O11 rdeO11 = parser.Parse(encodedRdeO11) as RDE_O11;

	        // assertions
            Assert.That(rdeO11.PATIENT.AL1RepetitionsUsed, Is.EqualTo(1));
	    }

        [Test]
        public void Parse_RdeO11WithoutOrderDetailGroup_RxrIsInOrderGroup()
        {
            // setup
            PipeParser parser = new PipeParser();
            string encodedRdeO11 =
                @"MSH|^~\&|EXACTDATA||ALL|ALL|20101217181500||RDE^O11^RDE_O11|EF010000000071000000_4|T^T|2.5.1
PID|1||7010566270^^^^DODID~EM010000008000000^^^^MR||ZZEDConner^Dean^J^^Mr.||19810611|MALE||WHITE^^HL70005|^^^^^USA^H||||ENGLISH^^HL70296|SINGLE^^HL70002||EF010000000071000000||||NOTHISPANICORLATINO^^HL70189|||||ACTIVEDUTY^^HL70172
PV1|1|OUTPATIENT|^^^MBCC^^AMBLOC||||1853210951^Pennington^Zachary|||MEDICINEGENERAL|||||||1853210951^Pennington^Zachary|||||||||||||||||||01|||||ACTIVE|||20101217181500|20101217184500||||||V
PV2|||V70.0^General medical examination^I9C|||||||||||||||||||||||||||||||||||||||||N
AL1|1|DRUG^^HL70127|70618^Penicillin^RXNORM|MODERATE^^HL70128|Dry Mouth|20100928
ORC|NW|101217-833651^ExD|||ORDERED
RXE|^^^20101217184500|54569-4888-0^Oseltamivir|1||75 mg||^po bid x 5d||N|10||0||||||||||||||||||||20101217184500
RXR|^Oral^HL70162".Replace('\n', '\r');

            // method under test
            RDE_O11 rdeO11 = parser.Parse(encodedRdeO11) as RDE_O11;

            // assertions
            Assert.That(rdeO11.ORDERRepetitionsUsed, Is.EqualTo(1));
            Assert.That(rdeO11.GetORDER().RXRRepetitionsUsed, Is.EqualTo(1));
        }

	    [Test]
	    public void Parse_AdtA08WithExtraOrcBeforeGT1_Gt1IsPresent()
	    {
            // setup
            PipeParser parser = new PipeParser();
            string encodedAdtA08 =
                @"MSH|^~\&|EXACTDATA||ALL|ALL|20110103144500||ADT^A08^ADT_A01|EF010000000001000000_1|T^T|2.5.1
EVN|A08|20110103144500|20110103144500|||20110103144500|NMMC BETHESDA
PID|1||7010523105^^^^DODID~EM010000001000000^^^^MR||ZZEDGross^Blaze^A^^Mr.||19890412|MALE||AMERICANINDIANORALASKANATIVE^^HL70005|^^^^^USA^H||||ENGLISH^^HL70296|SINGLE^^HL70002||EF010000000001000000||||HISPANICORLATINO^^HL70189|||||ACTIVEDUTY^^HL70172
PD1||||7000000011^Pennington^Zachary^^^^^^^^^^DODID
NK1|1|Wiggins^Deborah|FRIEND^^HL70063||^^^^1^334^891^8152||EMERGENCYCONTACT^^HL70131|||||||SINGLE^^HL70002|FEMALE|19900905
PV1|1|OUTPATIENT|^^^MBCC^^AMBLOC||||1853210951^Pennington^Zachary|||MEDICINEGENERAL|||||||1853210951^Pennington^Zachary|||||||||||||||||||01|||||ACTIVE|||20110103144500|20110103151500||||||V
PV2|||V70.0^General medical examination^I9C|||||||||||||||||||||||||||||||||||||||||N
OBX||TX|NOSSNREASON||Unknown||||||ACTIVE
ORC|NW|110103-470203^ExD|ORDERED^CERNER||ORDERED
GT1|1||ZZEDGross^Blaze||^^^^^USA^H||^^^^1^720^848^5159|19890412|MALE|PERSON|SELF^^HL70063
IN1|1|TRICARE^^HL70072|TRICARE|TRICARE||||||||||||ZZEDGross^Blaze|SELF^^HL70063|||||||||||||||||||7010523105|||||||||||||7010523105^^^^DODID
IN2||||||||||||||||||||||||||||||||||||||||||HISPANICORLATINO^^HL70189|SINGLE^^HL70002|||Military|||||||||||||||7010523105^^^^DODID||||||||||AMERICANINDIANORALASKANATIVE^^HL70005".
                    Replace('\n', '\r');

            // method under test
            ADT_A01 adtA01 = parser.Parse(encodedAdtA08) as ADT_A01;

            // assertions
	        Assert.That(adtA01.GT1RepetitionsUsed, Is.EqualTo(1));
	        GT1 gt1 = adtA01.GetGT1();
            Assert.That(gt1.GuarantorNameRepetitionsUsed, Is.EqualTo(1));
            Assert.That(gt1.GetGuarantorName(0).FamilyName.Surname.Value, Is.EqualTo("ZZEDGross"));
	    }        
    }
}