namespace NHapi.Base.Conf.Spec.UseCase
{

	/// <summary>
	/// The use case portion of a conformance specification.  
	/// @author Bryan Tripp
	/// </summary>
	public class UseCase
	{
		/// <summary>
		/// Holds value of property actor. </summary>
		private Actor actor;

		/// <summary>
		/// Holds value of property preCondition. </summary>
		private PreCondition preCondition;

		/// <summary>
		/// Holds value of property postCondition. </summary>
		private PostCondition postCondition;

		/// <summary>
		/// Holds value of property eventFlow. </summary>
		private EventFlow eventFlow;

		/// <summary>
		/// Holds value of property derivativeEvent. </summary>
		private DerivativeEvent derivativeEvent;

		/// <summary>
		/// Getter for property actor. </summary>
		/// <returns> Value of property actor. </returns>
		public virtual Actor Actor
		{
			get
			{
				return this.actor;
			}
			set
			{
				this.actor = value;
			}
		}


		/// <summary>
		/// Getter for property preCondition. </summary>
		/// <returns> Value of property preCondition. </returns>
		public virtual PreCondition PreCondition
		{
			get
			{
				return this.preCondition;
			}
			set
			{
				this.preCondition = value;
			}
		}


		/// <summary>
		/// Getter for property postCondition. </summary>
		/// <returns> Value of property postCondition. </returns>
		public virtual PostCondition PostCondition
		{
			get
			{
				return this.postCondition;
			}
			set
			{
				this.postCondition = value;
			}
		}


		/// <summary>
		/// Getter for property eventFlow. </summary>
		/// <returns> Value of property eventFlow. </returns>
		public virtual EventFlow EventFlow
		{
			get
			{
				return this.eventFlow;
			}
			set
			{
				this.eventFlow = value;
			}
		}


		/// <summary>
		/// Getter for property derivativeEvent. </summary>
		/// <returns> Value of property derivativeEvent. </returns>
		public virtual DerivativeEvent DerivativeEvent
		{
			get
			{
				return this.derivativeEvent;
			}
			set
			{
				this.derivativeEvent = value;
			}
		}
	}

}