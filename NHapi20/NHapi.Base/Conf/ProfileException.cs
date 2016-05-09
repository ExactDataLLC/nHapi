using System;

namespace NHapi.Base.Conf
{

	/// <summary>
	/// Represents something wrong with a profile.  This is distinguished from 
	/// something that is wrong with a message according to a profile (in the latter
	/// case, an HL7Exception might be thrown when the message is validated against 
	/// the profile).  
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public class ProfileException : Exception
	{

		/// <summary>
		/// Constructs an instance of <code>ProfileException</code> with the specified detail message. </summary>
		/// <param name="msg"> the detail message. </param>
		public ProfileException(string msg) : base(msg)
		{
		}


		/// <summary>
		/// Constructs an instance of <code>ProfileException</code> with the specified 
		/// cause and detail message. </summary>
		/// <param name="msg"> the detail message. </param>
		/// <param name="cause"> the underlying cause of the exception  </param>
		public ProfileException(string msg, Exception cause) : base(msg, cause)
		{
		}

	}

}