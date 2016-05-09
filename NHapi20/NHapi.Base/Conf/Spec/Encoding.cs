namespace NHapi.Base.Conf.Spec
{

	/// <summary>
	/// A class that indicated an HL7 encoding acceptable within a 
	/// specification. 
	/// @author Bryan Tripp
	/// </summary>
	public class Encoding
	{

		/// <summary>
		/// Creates a new instance of Encoding </summary>
		public Encoding()
		{
		}

		public class ER7 : Encoding
		{
		}
		public class XML : Encoding
		{
		}

	}

}