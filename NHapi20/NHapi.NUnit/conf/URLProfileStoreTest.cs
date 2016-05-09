using System;
using System.IO;
using NUnit.Framework;

/*
 * URLProfileStoreTest.java
 * JUnit based test
 *
 * Created on October 21, 2003, 10:52 AM
 */

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// JUnit tests for URLProfileStore 
	/// @author Bryan Tripp
	/// </summary>
	[TestFixture]
	public class URLProfileStoreTest
	{

        [Test]
        public virtual void testWithEmbeddedResourceLoader()
		{
			URLProfileStore store = new URLProfileStoreAnonymousInnerClassHelper(this);

			string profile = store.GetProfile("embeddedresourceloader-test");
			Assert.AreEqual("<foo/>", profile);
		}

		private class URLProfileStoreAnonymousInnerClassHelper : URLProfileStore
		{
			private readonly URLProfileStoreTest outerInstance;

			public URLProfileStoreAnonymousInnerClassHelper(URLProfileStoreTest outerInstance)
			{
				this.outerInstance = outerInstance;
			}

            protected override Uri GetURL(string id)
			{
                return new Uri(Path.Combine(Directory.GetCurrentDirectory(), "resources/conf/store/" + id + ".xml"));
			}
		}


        [Test]        
		public virtual void testWithNonExistingProfile()
		{
			URLProfileStore store = new URLProfileStoreAnonymousInnerClassHelper2(this);

			string profile = store.GetProfile("non-existing");
			Assert.That(profile, Is.Null);
		}

		private class URLProfileStoreAnonymousInnerClassHelper2 : URLProfileStore
		{
			private readonly URLProfileStoreTest outerInstance;

			public URLProfileStoreAnonymousInnerClassHelper2(URLProfileStoreTest outerInstance)
			{
				this.outerInstance = outerInstance;
			}

            protected override Uri GetURL(string id)
			{
                return new Uri(Path.Combine(Directory.GetCurrentDirectory(), "resources/conf/store/" + id + ".xml"));
			}
		}


	}

}