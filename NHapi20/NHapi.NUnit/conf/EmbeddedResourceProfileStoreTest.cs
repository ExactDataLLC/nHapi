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
		public virtual void testWithClassLoader()
		{
            EmbeddedResourceProfileStore store = new EmbeddedResourceProfileStore(Assembly.GetExecutingAssembly(), "conf.store");
			string profile = store.getProfile("classloader-test");
			Assert.That("<foo/>", Is.EqualTo(profile));
		}

        [Test]
        public virtual void testWithNonExistingResource()
		{
			EmbeddedResourceProfileStore store = new EmbeddedResourceProfileStore();
			string profile = store.getProfile("non-existing");
			Assert.That(profile, Is.Null);
		}
	}

}