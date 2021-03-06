﻿using System;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	[TestFixture]
	public class ProfileStoreFactoryTest
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

			ProfileStoreFactory.addCodeStore(cs1, "foo"); //for foo profile
			ProfileStoreFactory.addCodeStore(cs2, ".*test.*");
			ProfileStoreFactory.addCodeStore(cs3); //for all profiles

            ICodeStore codeStore = ProfileStoreFactory.getCodeStore("foo", "HL70001");
			Assert.True(codeStore.KnowsCodes("HL70396"));

            Assert.True(!ProfileStoreFactory.getCodeStore("xxxtestxxx", "HL70001").KnowsCodes("HL70061"));
            Assert.True(!ProfileStoreFactory.getCodeStore("xxx", "HL70061").KnowsCodes("HL70001"));
			Assert.True(null == ProfileStoreFactory.getCodeStore("xxx", "xxx"));

		}
	}

}