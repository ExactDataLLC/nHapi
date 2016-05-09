namespace NHapi.Base.Conf.Spec.UseCase
{

	/// <summary>
	/// An abstraction of the parts of a use case (eg EventFlow), all of which have a name and a body.  
	/// @author Bryan Tripp 
	/// </summary>
	public class AbstractUseCaseComponent
	{
		/// <summary>
		/// Holds value of property name. </summary>
		private string name;

		/// <summary>
		/// Holds value of property body. </summary>
		private string body;

		/// <summary>
		/// Getter for property name. </summary>
		/// <returns> Value of property name. </returns>
		public virtual string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}


		/// <summary>
		/// Getter for property body. </summary>
		/// <returns> Value of property body. </returns>
		public virtual string Body
		{
			get
			{
				return this.body;
			}
			set
			{
				this.body = value;
			}
		}
	}

}