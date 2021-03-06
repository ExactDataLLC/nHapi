﻿using System;

namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// Provides access to a (configurable) ProfileStore.
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public class ProfileStoreFactory
	{

		/// <summary>
		/// The default profile store directory
		/// </summary>
		public static readonly string DEFAULT_PROFILE_STORE_DIRECTORY = AppDomain.CurrentDomain.BaseDirectory + "/profiles";

		private static IProfileStore instance;
		private static ICodeStoreRegistry codeRegistry = new DefaultCodeStoreRegistry();

		/// <summary>
		/// Non instantiable </summary>
		private ProfileStoreFactory() : base()
		{
		}

		/// <summary>
		/// Returns a single configurable instance of a ProfileStore. Configurable by calling setStore().
		/// Defaults to FileProfileStore using the current <hapi.home>/profiles as a base directory Note:
		/// not a singleton (by design) in that nothing prevents creation of profile stores by other
		/// means.
		/// </summary>
		public static IProfileStore ProfileStore
		{
			get
			{
				lock (typeof(ProfileStoreFactory))
				{
					if (instance == null)
					{
						instance = new FileProfileStore(DEFAULT_PROFILE_STORE_DIRECTORY);
					}
            
					return instance;
				}
			}
		}

		/// <summary>
		/// Sets the profile store that will be returned in subsequent calls to getProfileStore().
		/// </summary>
		/// @deprecated use HapiContext to define the ProfileStore to be used 
		public static IProfileStore Store
		{
			set
			{
				lock (typeof(ProfileStoreFactory))
				{
					instance = value;
				}
			}
		}

		/// <summary>
		/// Registers a code store for use with all profiles.
		/// </summary>
		/// @deprecated use <seealso cref="ICodeStoreRegistry#AddCodeStore(ICodeStore)"/> 
		public static void addCodeStore(ICodeStore store)
		{
			codeRegistry.AddCodeStore(store);
		}

		/// <summary>
		/// Registers a code store for use with certain profiles. The profiles with which the code store
		/// are used are determined by profileIdPattern, which is a regular expression that will be
		/// matched against profile IDs. For example suppose there are three profiles in the profile
		/// store, with the following IDs:
		/// <ol>
		/// <li>ADT:confsig-UHN-2.4-profile-AL-NE-Immediate</li>
		/// <li>ADT:confsig-CIHI-2.4-profile-AL-NE-Immediate</li>
		/// <li>ADT:confsig-CIHI-2.3-profile-AL-NE-Immediate</li>
		/// </ol>
		/// Then to use a code store with only the first profile, the profileIdPattern would be
		/// "ADT:confsig-UHN-2.4-profile-AL-NE-Immediate". To use a code store with both of the 2.4
		/// profiles, the pattern would be ".*2\\.4.*". To use a code store with all profiles, the
		/// pattern would be '.*". Multiple stores can be registered for use with the same profile. If
		/// this happens, the first one that returned true for KnowsCodes(codeSystem) will used. Stores
		/// are searched in the order they are added here.
		/// </summary>
		/// @deprecated use <seealso cref="ICodeStoreRegistry#AddCodeStore(ICodeStore, String)"/> 
		public static void addCodeStore(ICodeStore store, string profileID)
		{
			codeRegistry.AddCodeStore(store, profileID);
		}

		/// <summary>
		/// Returns the first code store that knows the codes in the given code system (as per
		/// ICodeStore.KnowsCodes) and is registered for the given profile. Code stores are checked in the
		/// order in which they are added (with AddCodeStore()).
		/// </summary>
		/// <returns> null if none are found
		/// </returns>
		/// @deprecated use <seealso cref="ICodeStoreRegistry#GetCodeStore(String, String)"/> 
		public static ICodeStore getCodeStore(string profileID, string codeSystem)
		{
			return codeRegistry.GetCodeStore(profileID, codeSystem);
		}
	}

}