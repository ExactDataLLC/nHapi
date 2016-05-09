namespace NHapi.Base.Conf.Store
{

	/// <summary>
	/// A repository for conformance profile documents.
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public interface ProfileStore
	{

		/// <summary>
		/// Retrieves profile from persistent storage (by ID). 
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String getProfile(String ID) throws java.io.IOException;
		string getProfile(string ID);

		/// <summary>
		/// Stores profile in persistent storage with given ID. 
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void persistProfile(String ID, String profile) throws java.io.IOException;
		void persistProfile(string ID, string profile);

	}

}