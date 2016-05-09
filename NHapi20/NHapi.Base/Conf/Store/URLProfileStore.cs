using System;
using System.IO;
using System.Text;

/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1
/// (the "License"); you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/
/// Software distributed under the License is distributed on an "AS IS" basis,
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the
/// specific language governing rights and limitations under the License.
/// <para>
/// The Original Code is "URLProfileStore.java".  Description:
/// "A read-only profile store that loads profiles from URLs."
/// </para>
/// <para>
/// The Initial Developer of the Original Code is University Health Network. Copyright (C)
/// 2003.  All Rights Reserved.
/// </para>
/// <para>
/// Contributor(s): ______________________________________.
/// </para>
/// <para>
/// Alternatively, the contents of this file may be used under the terms of the
/// GNU General Public License (the "GPL"), in which case the provisions of the GPL are
/// applicable instead of those above.  If you wish to allow use of your version of this
/// file only under the terms of the GPL and not to allow others to use your version
/// of this file under the MPL, indicate your decision by deleting  the provisions above
/// and replace  them with the notice and other provisions required by the GPL License.
/// If you do not delete the provisions above, a recipient may use your version of
/// this file under either the MPL or the GPL.
/// </para>
/// </summary>
namespace NHapi.Base.Conf.Store
{


	/// <summary>
	/// A read-only profile store that loads profiles from URLs.  The URL 
	/// for a profile is determined by the method getURL().  An 
	/// attempt is also made to write 
	/// @author Bryan Tripp
	/// </summary>
	public abstract class URLProfileStore : ReadOnlyProfileStore
	{

		/// <summary>
		/// Creates a new instance of URLProfileStore </summary>
		public URLProfileStore()
		{
		}

		/// <summary>
		/// Retrieves profile from persistent storage (by ID).
		/// </summary>
		/// <param name="id"> profile id </param>
		/// <returns> profile content or null if profile could not be found </returns>
		public override string getProfile(string id)
		{
			string profile = null;
			StreamReader @in = null;
			try
			{
				Uri url = getURL(id);
				if (url != null)
				{
					@in = new StreamReader(url.AbsolutePath);
					StringBuilder buf = new StringBuilder();
					int c;
					while ((c = @in.Read()) != -1)
					{
						buf.Append((char) c);
					}
					profile = buf.ToString();
				}
			}
			catch (UriFormatException e)
			{
                throw new IOException("UriFormatException: " + e.Message);
			}
			finally
			{
				if (@in != null)
				{
					@in.Close();
				}
			}
			return profile;
		}


		/// <summary>
		/// Returns the URL from which to read a profile given the profile ID.  For example
		/// given "123" it could return ftp://hospital_x.org/hl7/123.xml, or 
		/// http://hl7_conformance_service.com?profile=123.  
		/// </summary>
		protected abstract Uri getURL(string ID);
	}

}