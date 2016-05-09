namespace NHapi.Base.Conf.Spec
{

	/// <summary>
	/// An implementation note.  
	/// @author Bryan Tripp
	/// </summary>
	public class ImpNote
	{
        /// <summary>
		/// Holds value of property text. </summary>
		private string text;

		/// <summary>
		/// Getter for property text. </summary>
		/// <returns> Value of property text. </returns>
		public virtual string Text
		{
			get
			{
				return this.text;
			}
			set
			{
				this.text = value;
			}
		}
	}
}