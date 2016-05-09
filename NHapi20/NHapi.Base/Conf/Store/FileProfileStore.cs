using System.IO;
using NHapi.Base.Conf.Parser;
using NHapi.Base.Log;

namespace NHapi.Base.Conf.Store
{

	/// <summary>
	/// Stores profiles in a local directory.  Profiles are stored as text
	/// in files named ID.xml (where ID is the profile ID).
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public class FileProfileStore : ProfileStore
	{

		private DirectoryInfo root;
        private static readonly ILog log = HapiLogFactory.GetHapiLog(typeof(ProfileParser));

		/// <summary>
		/// Creates a new instance of FileProfileStore </summary>
		public FileProfileStore(string theDirectory)
		{
			root = new DirectoryInfo(theDirectory);
			if (!root.Exists)
			{
                log.Warn("Profile store directory doesn't exist or is not a directory (won't be able to retrieve any profiles): " + theDirectory);
			}
		}

		/// <summary>
		/// Retrieves profile from persistent storage (by ID).  Returns null
		/// if the profile isn't found.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public String getProfile(String theID) throws java.io.IOException
		public virtual string getProfile(string theID)
		{
			string profile = null;

			string fileName = getFileName(theID);
            FileInfo source = new FileInfo(fileName);
			if (!source.Exists)
			{
				log.Debug(string.Format("File for profile {0} doesn't exist: {1}", theID, fileName));
			}
			else
			{
			    StreamReader @in = source.OpenText();
				char[] buf = new char[(int) source.Length];
				int check = @in.Read(buf, 0, buf.Length);
				@in.Close();
				if (check != buf.Length)
				{
					throw new IOException("Only read " + check + " of " + buf.Length + " bytes of file " + source.FullName);
				}
				profile = new string(buf);
				log.Debug(string.Format("Got profile {0}: \r\n {1}", theID, profile));
			}
			return profile;
		}

		/// <summary>
		/// Stores profile in persistent storage with given ID.
		/// </summary>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void persistProfile(String ID, String profile) throws java.io.IOException
		public virtual void persistProfile(string ID, string profile)
		{
			if (!root.Exists)
			{
				throw new IOException("Can't persist profile. Directory doesn't exist or not a directory: " + root.FullName);
			}

            FileInfo dest = new FileInfo(getFileName(ID));
		    StreamWriter @out = dest.CreateText();
			@out.Write(profile);
			@out.Flush();
			@out.Close();
		}

		private string getFileName(string ID)
		{
			return root.FullName + "/" + ID + ".xml";
		}
	}

}