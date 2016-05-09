using System;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// JUnit test cases for ProfileStoreFactory 
	/// 
	/// @author Bryan Tripp
	/// </summary>
	[TestFixture]
	public class ProfileStoreFactoryTest
	{
        [Test]
		public virtual void testCodeStoreRegistration()
		{
			Uri specURLAll = new Uri("resources/store/sampleTables.xml");
            Uri specURL1 = new Uri("resources/store/sampleTable1.xml");
            Uri specURL61 = new Uri("resources/store/sampleTable61.xml");
			CodeStore cs1 = new ProfileCodeStore(specURLAll);
			CodeStore cs2 = new ProfileCodeStore(specURL1);
			CodeStore cs3 = new ProfileCodeStore(specURL61);

			ProfileStoreFactory.addCodeStore(cs1, "foo"); //for foo profile
			ProfileStoreFactory.addCodeStore(cs2, ".*test.*");
			ProfileStoreFactory.addCodeStore(cs3); //for all profiles

			CodeStore codeStore = ProfileStoreFactory.getCodeStore("foo", "HL70001");
			Assert.True(codeStore.knowsCodes("HL70396"));

			Assert.True(!ProfileStoreFactory.getCodeStore("xxxtestxxx", "HL70001").knowsCodes("HL70061"));
			Assert.True(!ProfileStoreFactory.getCodeStore("xxx", "HL70061").knowsCodes("HL70001"));
			Assert.True(null == ProfileStoreFactory.getCodeStore("xxx", "xxx"));

		}
	}

}