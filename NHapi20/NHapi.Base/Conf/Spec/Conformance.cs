namespace NHapi.Base.Conf.Spec
{

	/// <summary>
	/// Part of a specification that defines application behavior and IDs 
	/// for other parts of the spec.   
	/// @author Bryan Tripp
	/// </summary>
	public class Conformance
	{
		/// <summary>
		/// Holds value of property acceptAck. </summary>
		private string acceptAck;

		/// <summary>
		/// Holds value of property applicationAck. </summary>
		private string applicationAck;

		/// <summary>
		/// Holds value of property staticID. </summary>
		private string staticID;

		/// <summary>
		/// Holds value of property dnamicID. </summary>
		private string dnamicID;

		/// <summary>
		/// Holds value of property msgAckMode. </summary>
		private string msgAckMode;

		/// <summary>
		/// Getter for property acceptAck. </summary>
		/// <returns> Value of property acceptAck. </returns>
		public virtual string AcceptAck
		{
			get
			{
				return this.acceptAck;
			}
			set
			{
				this.acceptAck = value;
			}
		}


		/// <summary>
		/// Getter for property applicationAck. </summary>
		/// <returns> Value of property applicationAck. </returns>
		public virtual string ApplicationAck
		{
			get
			{
				return this.applicationAck;
			}
			set
			{
				this.applicationAck = value;
			}
		}


		/// <summary>
		/// Getter for property staticID. </summary>
		/// <returns> Value of property staticID. </returns>
		public virtual string StaticID
		{
			get
			{
				return this.staticID;
			}
			set
			{
				this.staticID = value;
			}
		}


		/// <summary>
		/// Getter for property dnamicID. </summary>
		/// <returns> Value of property dnamicID. </returns>
		public virtual string DnamicID
		{
			get
			{
				return this.dnamicID;
			}
			set
			{
				this.dnamicID = value;
			}
		}


		/// <summary>
		/// Getter for property msgAckMode. </summary>
		/// <returns> Value of property msgAckMode. </returns>
		public virtual string MsgAckMode
		{
			get
			{
				return this.msgAckMode;
			}
			set
			{
				this.msgAckMode = value;
			}
		}


	}

}