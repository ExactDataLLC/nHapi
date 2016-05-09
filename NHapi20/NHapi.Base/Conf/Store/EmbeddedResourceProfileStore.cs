/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1
/// (the "License"); you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/
/// Software distributed under the License is distributed on an "AS IS" basis,
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the
/// specific language governing rights and limitations under the License.
/// 
/// The Original Code is "ClasspathProfileStore.java".  Description:
/// "A read-only profile store that loads profiles from the classpath"
/// 
/// The Initial Developer of the Original Code is University Health Network. Copyright (C)
/// 2012.  All Rights Reserved.
/// 
/// Contributor(s): ______________________________________.
/// 
/// Alternatively, the contents of this file may be used under the terms of the
/// GNU General Public License (the "GPL"), in which case the provisions of the GPL are
/// applicable instead of those above.  If you wish to allow use of your version of this
/// file only under the terms of the GPL and not to allow others to use your version
/// of this file under the MPL, indicate your decision by deleting  the provisions above
/// and replace  them with the notice and other provisions required by the GPL License.
/// If you do not delete the provisions above, a recipient may use your version of
/// this file under either the MPL or the GPL.
/// 
/// </summary>

using System;
using System.IO;
using System.Reflection;

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// Looks up the profile in a file "ID.xml" somewhere in in classpath
	/// (where ID is the profile ID).
	/// 
	/// @author Christian Ohr
	/// </summary>
    public class EmbeddedResourceProfileStore : ReadOnlyProfileStore
	{
		public const string DEFAULT_PROFILE_PREFIX = "conf/store";

		public EmbeddedResourceProfileStore() : this(Assembly.GetCallingAssembly(), DEFAULT_PROFILE_PREFIX)
		{
		}

		public EmbeddedResourceProfileStore(Assembly container, string prefix) : base()
		{
			this.prefix = prefix;
		    this.container = container;
		}

        public override string GetProfile(string ID)
        {
            string profilePath = string.Concat(prefix, ".", ID, ".xml");
            using (Stream resourceStream = container.GetManifestResourceStream(profilePath))
            using (TextReader reader = new StreamReader(resourceStream))
            {
                return reader.ReadToEnd();
            }
        }
		
	    protected Assembly container;
        protected string prefix;
	    
	}
}