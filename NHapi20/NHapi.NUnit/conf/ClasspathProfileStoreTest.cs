/*
 * URLProfileStoreTest.java
 * JUnit based test
 *
 * Created on October 21, 2003, 10:52 AM
 */


using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// JUnit tests for URLProfileStore 
	/// @author Bryan Tripp
	/// </summary>
	[TestFixture]
	public class ClasspathProfileStoreTest
	{

        [Test]
		public virtual void testWithClassLoader()
		{
			ClasspathProfileStore store = new ClasspathProfileStore();
			string profile = store.getProfile("resources/conf/store/classloader-test");
			Assert.That("<foo/>", Is.EqualTo(profile));
		}

        [Test]
        public virtual void testWithNonExistingResource()
		{
			ClasspathProfileStore store = new ClasspathProfileStore();
			string profile = store.getProfile("non-existing");
			Assert.That(profile, Is.Null);
		}
	}

}