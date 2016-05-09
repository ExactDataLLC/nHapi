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
        public virtual void testWithClassLoader()
		{
			URLProfileStore store = new URLProfileStoreAnonymousInnerClassHelper(this);

			string profile = store.getProfile("classloader-test");
			Assert.AreEqual("<foo/>", profile);
		}

		private class URLProfileStoreAnonymousInnerClassHelper : URLProfileStore
		{
			private readonly URLProfileStoreTest outerInstance;

			public URLProfileStoreAnonymousInnerClassHelper(URLProfileStoreTest outerInstance)
			{
				this.outerInstance = outerInstance;
			}

			public override Uri getURL(string id)
			{
                return new Uri("resources/store/" + id + ".xml");
			}
		}


        [Test]        
		public virtual void testWithNonExistingProfile()
		{
			URLProfileStore store = new URLProfileStoreAnonymousInnerClassHelper2(this);

			string profile = store.getProfile("non-existing");
			Assert.Null(profile);
		}

		private class URLProfileStoreAnonymousInnerClassHelper2 : URLProfileStore
		{
			private readonly URLProfileStoreTest outerInstance;

			public URLProfileStoreAnonymousInnerClassHelper2(URLProfileStoreTest outerInstance)
			{
				this.outerInstance = outerInstance;
			}

            public override Uri getURL(string id)
			{
                return new Uri("resources/store/" + id + ".xml");
			}
		}


        [Test]        
		public virtual void testWithHTTP()
		{
			URLProfileStore store = new URLProfileStoreAnonymousInnerClassHelper3(this);

			string @in = store.getProfile("test");
			Assert.True(@in.IndexOf("Google", StringComparison.Ordinal) >= 0);
		}

		private class URLProfileStoreAnonymousInnerClassHelper3 : URLProfileStore
		{
			private readonly URLProfileStoreTest outerInstance;

			public URLProfileStoreAnonymousInnerClassHelper3(URLProfileStoreTest outerInstance)
			{
				this.outerInstance = outerInstance;
			}

            public override Uri getURL(string ID)
			{
				return new Uri("http://google.com");
			}
		}

		/*public void testWithFile() throws Exception {
		    URLProfileStore store = new URLProfileStore() {
		        public Uri getReadURL(String ID) throws MalformedURLException {
		            File f = new File(".");
		            return new URL("file:///" + f.getAbsolutePath() + "/" + ID + ".xml");
		        }
		    };
		    
		    String out = "<test_profile/>";
		    store.persistProfile("test", out);
		    String in = store.getProfile("test");
		    Assert.Equalsout, in);
		}*/

	}

}