namespace NHapi.Base.Conf.Spec.Message
{
	/// <summary>
	/// An explicit data value specified in a message profile.  
	/// @author Bryan Tripp
	/// </summary>
	public class DataValue
	{
		/// <summary>
		/// Holds value of property exValue. </summary>
		private string exValue;

		/// <summary>
		/// Getter for property exValue. </summary>
		/// <returns> Value of property exValue. </returns>
		public virtual string ExValue
		{
			get
			{
				return this.exValue;
			}
			set
			{
				this.exValue = value;
			}
		}
	}
}