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

namespace NHapi.Base.Conf.Store
{


	/// <summary>
	/// Looks up the profile in a file "ID.xml" somewhere in in classpath
	/// (where ID is the profile ID).
	/// 
	/// @author Christian Ohr
	/// </summary>
	public class ClasspathProfileStore : URLProfileStore
	{

		public const string DEFAULT_PROFILE_PREFIX = "/ca/uhn/hl7v2/conf/store";

		private string prefix;

		public ClasspathProfileStore() : this(DEFAULT_PROFILE_PREFIX)
		{
		}

		public ClasspathProfileStore(string prefix) : base()
		{
			this.prefix = prefix;
		}

//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: @Override public java.net.URL getURL(String ID) throws java.net.MalformedURLException
		public override Uri getURL(string ID)
		{
            Uri rval = new Uri(prefix + "/" + ID + ".xml");
		    return rval;
		}

	}

}