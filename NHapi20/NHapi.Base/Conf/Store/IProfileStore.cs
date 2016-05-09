namespace NHapi.Base.Conf.Store
{
	/// <summary>
	/// A repository for conformance profile documents.
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public interface IProfileStore
	{

		/// <summary>
		/// Retrieves profile from persistent storage (by ID). 
		/// </summary>
		string GetProfile(string ID);

		/// <summary>
		/// Stores profile in persistent storage with given ID. 
		/// </summary>
		void PersistProfile(string ID, string profile);
	}
}