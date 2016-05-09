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

        private Uri url = new Uri("resources/store/sampleTables.xml");
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
            string[] validCodes = codeStore.getValidCodes(codeSys);
            Assert.True(validCodes.ToList().Contains(code));
        }


	    [Test]
		public virtual void testGetValidCodesPos2()
		{
			IList<string> codes = codeStore.getValidCodes(codeSys).ToList();
			foreach (string code in codeArray)
			{
				Assert.True(codes.Contains(code));
			}
		}

        [Test]
		public virtual void testKnowsCodesPos()
		{
			Assert.True(codeStore.knowsCodes(codeSys));
		}

        [Test]
		public virtual void testIsValidCodePos()
		{
			Assert.True(codeStore.isValidCode(codeSys, code));
		}

		/// <summary>
		/// Test isValidCode() using a code that maps to table values containing wildcard characters
		/// </summary>
        [Test]
		public virtual void testIsValidCodePos2()
		{
			string codeSys = "HL70396";
			string code1 = "99cbc";
			string code2 = "L";
			string code3 = "99zzz";
			string code4 = "HL76666";
			Assert.True(codeStore.isValidCode(codeSys, code1));
			Assert.True(codeStore.isValidCode(codeSys, code2));
			Assert.True(codeStore.isValidCode(codeSys, code3));
			Assert.True(codeStore.isValidCode(codeSys, code4));
		}

		/// <summary>
		/// Test isValidCode() using a code that has a space
		/// </summary>
        [Test]
		public virtual void testIsValidCodePos3()
		{
			string codeSys = "HL70361";
			string code1 = "Misys CPR";
			codeStore.getValidCodes(codeSys);
			Assert.True(codeStore.isValidCode(codeSys, code1));
		}

		/// <exception cref="ProfileException"> void Test for presence of invalid codeSys </exception>
        [Test]
		public virtual void testGetValidCodesNeg2()
		{
			codeStore.getValidCodes("HL7XXXX");
		}

		/// <summary>
		/// Test for null check on codeSys
		/// </summary>
        [Test]
		public virtual void testGetValidCodesNeg3()
		{
			try
			{
				codeStore.getValidCodes(null);
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
				codeStore.getValidCodes("XXX");
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
			Assert.True(!codeStore.knowsCodes("XXX"));
		}

		/// <summary>
		/// Test for invalid code
		/// </summary>
        [Test]
		public virtual void testIsValidCodeNeg()
		{
			Assert.True(!codeStore.isValidCode("HL70001", "X"));
		}

	}

}