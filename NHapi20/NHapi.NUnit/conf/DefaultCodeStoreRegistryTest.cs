using System;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{

	/// <summary>
	/// JUnit test cases for DefaultCodeStoreRegistryTest
	/// 
	/// @author Christian Ohr
	/// </summary>
	[TestFixture]
	public class DefaultCodeStoreRegistryTest
	{
        [Test]
		public virtual void testCodeStoreRegistration()
		{
            Uri specURLAll = new Uri("resources/conf/store/sampleTables.xml");
            Uri specURL1 = new Uri("resources/conf/store/sampleTable1.xml");
            Uri specURL61 = new Uri("resources/conf/store/sampleTable61.xml");
			CodeStore cs1 = new ProfileCodeStore(specURLAll);
			CodeStore cs2 = new ProfileCodeStore(specURL1);
			CodeStore cs3 = new ProfileCodeStore(specURL61);

			CodeStoreRegistry registry = new DefaultCodeStoreRegistry();

			registry.addCodeStore(cs1, "foo"); //for foo profile
			registry.addCodeStore(cs2, ".*test.*");
			registry.addCodeStore(cs3); //for all profiles

			CodeStore codeStore = registry.getCodeStore("foo", "HL70001");
			Assert.True(codeStore.knowsCodes("HL70396"));

			Assert.True(!registry.getCodeStore("xxxtestxxx", "HL70001").knowsCodes("HL70061"));
			Assert.True(!registry.getCodeStore("xxx", "HL70061").knowsCodes("HL70001"));
			Assert.True(null == registry.getCodeStore("xxx", "xxx"));

		}
	}

}