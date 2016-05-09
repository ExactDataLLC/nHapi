using NHapi.Base.Conf.Spec.Message;

namespace NHapi.Base.Conf.Spec
{

	/// <summary>
	/// A conformance specification (see HL7 2.5 section 2.12).  
	/// @author Bryan Tripp
	/// </summary>
	public class RuntimeProfile
	{
		private Conformance conformance;
		/// <summary>
		/// Holds value of property conformanceType. </summary>
		private string conformanceType;
		private Encoding[] encodings;
		/// <summary>
		/// Holds value of property HL7OID. </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		private string HL7OID_Renamed;
		/// <summary>
		/// Holds value of property HL7Version. </summary>
//JAVA TO C# CONVERTER NOTE: Fields cannot have the same name as methods:
		private string HL7Version_Renamed;

		private string impNote;

		/// <summary>
		/// Holds value of property message. </summary>
		private StaticDef message;

		/// <summary>
		/// Holds value of property orgName. </summary>
		private string orgName;

		/// <summary>
		/// Holds value of property role. </summary>
		private string role;

		/// <summary>
		/// Holds value of property specName. </summary>
		private string specName;

		/// <summary>
		/// Holds value of property specVersion. </summary>
		private string specVersion;

		/// <summary>
		/// Holds value of property status. </summary>
		private string status;

		private UseCase.UseCase useCase;

		private string name;

		/// <summary>
		/// Getter for property conformance. </summary>
		/// <returns> Value of property conformance. </returns>
		public virtual Conformance Conformance
		{
			get
			{
				return this.conformance;
			}
			set
			{
				this.conformance = value;
			}
		}

		/// <summary>
		/// Getter for property conformanceType. </summary>
		/// <returns> Value of property conformanceType. </returns>
		public virtual string ConformanceType
		{
			get
			{
				return this.conformanceType;
			}
			set
			{
				this.conformanceType = value;
			}
		}

		/// <summary>
		/// Indexed getter for property encodings. </summary>
		/// <param name="index"> Index of the property. </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual Encoding getEncodings(int index)
		{
			return this.encodings[index];
		}

		/// <summary>
		/// Getter for property HL7OID. </summary>
		/// <returns> Value of property HL7OID. </returns>
		public virtual string HL7OID
		{
			get
			{
				return this.HL7OID_Renamed;
			}
			set
			{
				this.HL7OID_Renamed = value;
			}
		}

		/// <summary>
		/// Getter for property HL7Version. </summary>
		/// <returns> Value of property HL7Version. </returns>
		public virtual string HL7Version
		{
			get
			{
				return this.HL7Version_Renamed;
			}
			set
			{
				this.HL7Version_Renamed = value;
			}
		}

		/// <summary>
		/// Getter for property impNote. </summary>
		/// <returns> Value of property impNote. </returns>
		public virtual string ImpNote
		{
			get
			{
				return this.impNote;
			}
			set
			{
				this.impNote = value;
			}
		}

		/// <summary>
		/// Getter for property message. </summary>
		/// <returns> Value of property message. </returns>
		public virtual StaticDef Message
		{
			get
			{
				return this.message;
			}
			set
			{
				this.message = value;
			}
		}

		/// <summary>
		/// Getter for property orgName. </summary>
		/// <returns> Value of property orgName. </returns>
		public virtual string OrgName
		{
			get
			{
				return this.orgName;
			}
			set
			{
				this.orgName = value;
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

		/// <summary>
		/// Getter for property specName. </summary>
		/// <returns> Value of property specName. </returns>
		public virtual string SpecName
		{
			get
			{
				return this.specName;
			}
			set
			{
				this.specName = value;
			}
		}

		/// <summary>
		/// Getter for property specVersion. </summary>
		/// <returns> Value of property specVersion. </returns>
		public virtual string SpecVersion
		{
			get
			{
				return this.specVersion;
			}
			set
			{
				this.specVersion = value;
			}
		}

		/// <summary>
		/// Getter for property status. </summary>
		/// <returns> Value of property status. </returns>
		public virtual string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		/// <summary>
		/// Getter for property useCase. </summary>
		/// <returns> Value of property useCase. </returns>
		public virtual UseCase.UseCase UseCase
		{
			get
			{
				return this.useCase;
			}
			set
			{
				this.useCase = value;
			}
		}

		/// <summary>
		/// Indexed setter for property encodings. </summary>
		/// <param name="index"> Index of the property. </param>
		/// <param name="encodings"> New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setEncodings(int index, Encoding encodings) throws ca.uhn.hl7v2.conf.ProfileException
		public virtual void setEncodings(int index, Encoding encodings)
		{
			this.encodings[index] = encodings;
		}





//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setName(String theName) throws ca.uhn.hl7v2.conf.ProfileException
		public virtual string Name
		{
			set
			{
				this.name = value;
			}
			get
			{
				return name;
			}
		}








	}

}