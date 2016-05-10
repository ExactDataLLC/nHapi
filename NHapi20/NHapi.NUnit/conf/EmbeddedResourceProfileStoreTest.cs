using System.Reflection;
using NUnit.Framework;

namespace NHapi.Base.Conf.Store
{
	[TestFixture]
	public class EmbeddedResourceProfileStoreTest
	{
        [Test]
		public void testWithExistingResource()
		{
            EmbeddedResourceProfileStore store = new EmbeddedResourceProfileStore(Assembly.GetExecutingAssembly(), "NHapi.NUnit.resources.conf.store");
			string profile = store.GetProfile("embeddedresourceloader-test");
			Assert.That(profile, Is.EqualTo("<foo/>"));
		}

        [Test]
        public void testWithNonExistingResource()
		{
			EmbeddedResourceProfileStore store = new EmbeddedResourceProfileStore();
            string profile = store.GetProfile("non-existing");
			Assert.That(profile, Is.Null);
		}
	}

}