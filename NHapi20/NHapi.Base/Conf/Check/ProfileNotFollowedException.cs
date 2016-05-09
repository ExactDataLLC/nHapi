namespace NHapi.Base.Conf.Check
{

	/// <summary>
	/// An exception indicating that some message contents don't reflect a static profile.   
	/// @author Bryan Tripp
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("serial") public class ProfileNotFollowedException extends ca.uhn.hl7v2.HL7Exception
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