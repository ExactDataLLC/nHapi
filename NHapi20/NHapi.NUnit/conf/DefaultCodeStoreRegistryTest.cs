using System;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	[TestFixture]
	public class DefaultCodeStoreRegistryTest
	{
        [Test]
		public virtual void testCodeStoreRegistration()
		{
            Uri specURLAll = new Uri("resources/conf/store/sampleTables.xml", UriKind.Relative);
            Uri specURL1 = new Uri("resources/conf/store/sampleTable1.xml", UriKind.Relative);
            Uri specURL61 = new Uri("resources/conf/store/sampleTable61.xml", UriKind.Relative);
			ICodeStore cs1 = new ProfileCodeStore(specURLAll);
            ICodeStore cs2 = new ProfileCodeStore(specURL1);
            ICodeStore cs3 = new ProfileCodeStore(specURL61);

            ICodeStoreRegistry registry = new DefaultCodeStoreRegistry();

			registry.AddCodeStore(cs1, "foo"); //for foo profile
			registry.AddCodeStore(cs2, ".*test.*");
			registry.AddCodeStore(cs3); //for all profiles

            ICodeStore codeStore = registry.GetCodeStore("foo", "HL70001");
			Assert.True(codeStore.KnowsCodes("HL70396"));

			Assert.True(!registry.GetCodeStore("xxxtestxxx", "HL70001").KnowsCodes("HL70061"));
			Assert.True(!registry.GetCodeStore("xxx", "HL70061").KnowsCodes("HL70001"));
			Assert.True(null == registry.GetCodeStore("xxx", "xxx"));

		}
	}

}