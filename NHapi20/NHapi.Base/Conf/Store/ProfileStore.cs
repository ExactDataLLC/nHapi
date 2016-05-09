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
		string getProfile(string ID);

		/// <summary>
		/// Stores profile in persistent storage with given ID. 
		/// </summary>
		void persistProfile(string ID, string profile);

	}

}