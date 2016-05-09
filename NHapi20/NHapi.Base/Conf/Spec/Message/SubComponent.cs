using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// The specification for a particular field sub-component in a message profile.  
	/// @author Bryan Tripp
	/// </summary>
	public class SubComponent : AbstractComponent
	{

		/// <summary>
		/// Creates a new instance of SubComponent </summary>
		public SubComponent()
		{
		}

		public virtual IList<object> ChildrenAsList
		{
			get
			{
				return new List<object>();
			}
		}

	}

}