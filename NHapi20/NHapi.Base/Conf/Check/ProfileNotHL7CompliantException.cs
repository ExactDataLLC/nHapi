using System;

namespace NHapi.Base.Conf.Check
{

	/// <summary>
	/// An exception indicating that a static profile doesn't correspond to HL7 rules.  
	/// @author Bryan Tripp
    /// </summary>
    /// <exception cref="NHapi.Base.HL7Exception"></exception>
	public class ProfileNotHL7CompliantException : Base.HL7Exception
	{

		/// <summary>
		/// Constructs an instance of <code>ProfileNotHL7CompliantHL7Exception</code> with the specified detail message. </summary>
		/// <param name="msg"> the detail message. </param>
		public ProfileNotHL7CompliantException(string msg) : base(msg)
		{
		}

		/// <summary>
		/// Constructs an instance of <code>ProfileNotHL7CompliantHL7Exception</code> with the specified detail message. </summary>
		/// <param name="msg"> the detail message. </param>
		/// <param name="cause"> an underlying exception  </param>
		public ProfileNotHL7CompliantException(string msg, Exception cause) : base(msg, cause)
		{
		}

	}

}