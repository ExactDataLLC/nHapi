namespace NHapi.Base.Conf.Spec.Message
{



	/// <summary>
	/// <para>A "static message profile" (see HL7 2.5 section 2.12).  Message profiles are 
	/// a precise method of documenting message constraints, using a standard XML syntax 
	/// defined by HL7 (introduced in version 2.5).  XML message profiles define
	/// constraints on message content and structure in a well-defined manner, so that 
	/// the conformance of a certain message to a certain profile can be tested automatically.  
	/// There are several types of profiles: 
	/// <ul><li>HL7 Profiles - the standard messages (relatively loosely constrained)</li>
	/// <li>Implementable Profiles - profiles with additional constraints such that all optionality 
	///      has been removed (e.g. optional fields marked as "required" or "not supported").   
	/// <li>Constrainable Profiles - any profile with optionality that can be further constrained.</li></ul>
	/// Thus profiles can constrain other profiles.  A typical case would be for a country to create a 
	/// constrainable profile based on an HL7 profile, for a vendor to create a different constrainable 
	/// profile based on the same HL7 profile, and for a hospital to create an implementable profile for 
	/// a particular implementation that constrains both.  </para>
	/// <para>The MessageProfile class is a parsed object representation of the XML profile.</para>
	/// @author Bryan Tripp
	/// </summary>
	public class StaticDef : AbstractSegmentContainer
	{
		private MetaData metaData;
		private string msgType;
		private string eventType;
		private string msgStructID;
		private string orderControl;
		private string eventDesc;
		private string identifier;
		private string role;

		/// <summary>
		/// Getter for property metaData. </summary>
		/// <returns> Value of property metaData. </returns>
		public virtual MetaData MetaData
		{
			get
			{
				return this.metaData;
			}
			set
			{
				this.metaData = value;
			}
		}


		/// <summary>
		/// Getter for property msgType. </summary>
		/// <returns> Value of property msgType. </returns>
		public virtual string MsgType
		{
			get
			{
				return this.msgType;
			}
			set
			{
				this.msgType = value;
			}
		}


		/// <summary>
		/// Getter for property eventType. </summary>
		/// <returns> Value of property eventType. </returns>
		public virtual string EventType
		{
			get
			{
				return this.eventType;
			}
			set
			{
				this.eventType = value;
			}
		}


		/// <summary>
		/// Getter for property msgStructID. </summary>
		/// <returns> Value of property msgStructID. </returns>
		public virtual string MsgStructID
		{
			get
			{
				return this.msgStructID;
			}
			set
			{
				this.msgStructID = value;
			}
		}


		/// <summary>
		/// Getter for property orderControl. </summary>
		/// <returns> Value of property orderControl. </returns>
		public virtual string OrderControl
		{
			get
			{
				return this.orderControl;
			}
			set
			{
				this.orderControl = value;
			}
		}


		/// <summary>
		/// Getter for property eventDesc. </summary>
		/// <returns> Value of property eventDesc. </returns>
		public virtual string EventDesc
		{
			get
			{
				return this.eventDesc;
			}
			set
			{
				this.eventDesc = value;
			}
		}


		/// <summary>
		/// Getter for property identifier. </summary>
		/// <returns> Value of property identifier. </returns>
		public virtual string Identifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}


		/// <summary>
		/// Getter for property role. </summary>
		/// <returns> Value of property role. </returns>
		public virtual string Role
		{
			get
			{
				return this.role;
			}
			set
			{
				this.role = value;
			}
		}


	}

}