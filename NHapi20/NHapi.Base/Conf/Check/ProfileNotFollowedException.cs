namespace NHapi.Base.Conf.Check
{

	/// <summary>
	/// An exception indicating that some message contents don't reflect a static profile.   
	/// @author Bryan Tripp
	/// </summary>
    /// <exception cref="NHapi.Base.HL7Exception"></exception>
	public class ProfileNotFollowedException : Base.HL7Exception
	{

		/// <summary>
		/// Constructs an instance of <code>ProfileNotFollowedException</code> with the specified detail message. </summary>
		/// <param name="msg"> the detail message. </param>
		public ProfileNotFollowedException(string msg) : base(msg)
		{
		}


	}

}