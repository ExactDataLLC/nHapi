namespace NHapi.Base.Conf.Check
{

	/// <summary>
	/// An element that a profile defines as "not used" (X) is present in the 
	/// message.  
	/// @author Bryan Tripp
	/// </summary>
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