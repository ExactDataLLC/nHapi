using System;
using System.IO;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	[TestFixture]
	public class URLProfileStoreTest
	{

        [Test]
        public virtual void testWithLoadFileSystemAccessor()
		{
			URLProfileStore store = new URLProfileStoreAnonymousInnerClassHelper(this);

			string profile = store.GetProfile("localfilesystemloader-test");
            Assert.AreEqual(profile, "<foo/>");
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