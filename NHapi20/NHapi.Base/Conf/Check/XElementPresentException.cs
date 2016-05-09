namespace NHapi.Base.Conf.Check
{

	/// <summary>
	/// An element that a profile defines as "not used" (X) is present in the 
	/// message.  
	/// @author Bryan Tripp
	/// </summary>
//JAVA TO C# CONVERTER TODO TASK: Most Java annotations will not have direct .NET equivalent attributes:
//ORIGINAL LINE: @SuppressWarnings("serial") public class XElementPresentException extends ProfileNotFollowedException
	public class XElementPresentException : ProfileNotFollowedException
	{

		/// <summary>
		/// Constructs an instance of <code>XElementPresentException</code> with the specified detail message. </summary>
		/// <param name="msg"> the detail message. </param>
		public XElementPresentException(string msg) : base(msg)
		{
		}

	}

}