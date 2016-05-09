/*
 * URLProfileStoreTest.java
 * JUnit based test
 *
 * Created on October 21, 2003, 10:52 AM
 */


using System.Reflection;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// JUnit tests for URLProfileStore 
	/// @author Bryan Tripp
	/// </summary>
	[TestFixture]
	public class EmbeddedResourceProfileStoreTest
	{
        [Test]
		public virtual void testWithExistingResource()
		{
            EmbeddedResourceProfileStore store = new EmbeddedResourceProfileStore(Assembly.GetExecutingAssembly(), "NHapi.NUnit.resources.conf.store");
			string profile = store.GetProfile("embeddedresourceloader-test");
			Assert.That(profile, Is.EqualTo("<foo/>"));
		}

        [Test]
        public virtual void testWithNonExistingResource()
		{
			EmbeddedResourceProfileStore store = new EmbeddedResourceProfileStore();
            string profile = store.GetProfile("non-existing");
			Assert.That(profile, Is.Null);
		}
	}

}