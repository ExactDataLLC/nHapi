/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1
/// (the "License"); you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/
/// Software distributed under the License is distributed on an "AS IS" basis,
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the
/// specific language governing rights and limitations under the License.
/// 
/// The Original Code is "ReadOnlyProfileStore.java".  Description:
/// "An abstract read-only profile store."
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
namespace NHapi.Base.Conf.Store
{

	/// <summary>
	/// Base class for read-only profile stores.
	/// 
	/// @author Christian Ohr
	/// </summary>
	public abstract class ReadOnlyProfileStore : ProfileStore
	{
		public abstract string getProfile(string ID);

		/// <summary>
		/// Stores profile in persistent storage with given ID.
		/// </summary>
		public virtual void persistProfile(string ID, string profile)
		{
			throw new System.NotSupportedException("Can't persist profile -- this profile store is read-only");
		}
	}

}