using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// @author Administrator Created on 2-Sep-2003 To change this generated comment go to
	///         Window>Preferences>Java>Code Generation>Code and Comments
	/// </summary>
	[TestFixture]
    public class ProfileCodeStoreTest
	{

        private Uri url = new Uri("resources/conf/store/sampleTables.xml", UriKind.Relative);
		private string codeSys = "HL70001";
		// private String profileId =
		// "{ConfSig(1) CCO(1) 2.4(7) static-profile(1) ADT(3) A01(1) null(0) ADT_A01(4) HL7 2.4(1) Sender(1)}";
		private string code = "M";
		private string[] codeArray = new string[] {"M", "F", "O", "U"};
		private ProfileCodeStore codeStore;


        [SetUp]
		public virtual void setUp()
		{
			codeStore = new ProfileCodeStore(url);
		}


        [Test]
		public virtual void testGetValidCodesPos1()
        {
            string[] validCodes = codeStore.GetValidCodes(codeSys);
            Assert.True(validCodes.ToList().Contains(code));
        }


	    [Test]
		public virtual void testGetValidCodesPos2()
		{
            IList<string> codes = codeStore.GetValidCodes(codeSys).ToList();
			foreach (string code in codeArray)
			{
				Assert.True(codes.Contains(code));
			}
		}

        [Test]
		public virtual void testKnowsCodesPos()
		{
			Assert.True(codeStore.KnowsCodes(codeSys));
		}

        [Test]
		public virtual void testIsValidCodePos()
		{
			Assert.True(codeStore.IsValidCode(codeSys, code));
		}

		/// <summary>
		/// Test IsValidCode() using a code that maps to table values containing wildcard characters
		/// </summary>
        [Test]
		public virtual void testIsValidCodePos2()
		{
			string codeSys = "HL70396";
			string code1 = "99cbc";
			string code2 = "L";
			string code3 = "99zzz";
			string code4 = "HL76666";
			Assert.True(codeStore.IsValidCode(codeSys, code1));
			Assert.True(codeStore.IsValidCode(codeSys, code2));
			Assert.True(codeStore.IsValidCode(codeSys, code3));
			Assert.True(codeStore.IsValidCode(codeSys, code4));
		}

		/// <summary>
		/// Test IsValidCode() using a code that has a space
		/// </summary>
        [Test]
		public virtual void testIsValidCodePos3()
		{
			string codeSys = "HL70361";
			string code1 = "Misys CPR";
			codeStore.GetValidCodes(codeSys);
			Assert.True(codeStore.IsValidCode(codeSys, code1));
		}

        [Test, ExpectedException(typeof(ProfileException))]
		public virtual void testGetValidCodesNeg2()
		{
			codeStore.GetValidCodes("HL7XXXX");
		}

		/// <summary>
		/// Test for null check on codeSys
		/// </summary>
        [Test]
		public virtual void testGetValidCodesNeg3()
		{
			try
			{
				codeStore.GetValidCodes(null);
				Assert.Fail("codeSystem should not exist");
			}
			catch (ProfileException e)
			{
				Assert.AreEqual(e.Message, "The input codeSystem parameter cannot be null");
			}
		}

		/// <summary>
		/// Test for invalid codeSys length
		/// </summary>
        [Test]
		public virtual void testGetValidCodesNeg4()
		{
			try
			{
				codeStore.GetValidCodes("XXX");
				Assert.Fail("codeSystem should not exist");
			}
			catch (ProfileException e)
			{
				Assert.AreEqual(e.Message, "The input codeSystem parameter cannot be less than 4 characters long");
			}
		}

		/// <summary>
		/// Test for invalid codeSys
		/// </summary>
        [Test]
		public virtual void testKnowsCodesNeg()
		{
			Assert.True(!codeStore.KnowsCodes("XXX"));
		}

		/// <summary>
		/// Test for invalid code
		/// </summary>
        [Test]
		public virtual void testIsValidCodeNeg()
		{
			Assert.True(!codeStore.IsValidCode("HL70001", "X"));
		}

	}

}