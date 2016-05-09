using NHapi.Base.Conf.Spec.Message;

namespace NHapi.Base.Conf.Spec
{

	/// <summary>
	/// A conformance specification (see HL7 2.5 section 2.12).  
	/// @author Bryan Tripp
	/// </summary>
	public class RuntimeProfile
	{
	    private Encoding[] encodings;

	    /// <summary>
		/// Getter for property conformance. </summary>
		/// <returns> Value of property conformance. </returns>
		public virtual Conformance Conformance { get; set; }

	    /// <summary>
		/// Getter for property conformanceType. </summary>
		/// <returns> Value of property conformanceType. </returns>
		public virtual string ConformanceType { get; set; }

	    /// <summary>
		/// Indexed getter for property encodings. </summary>
		/// <param name="index"> Index of the property. </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual Encoding GetEncodings(int index)
		{
			return encodings[index];
		}

        /// <summary>
        /// Indexed setter for property encodings. </summary>
        /// <param name="index"> Index of the property. </param>
        /// <param name="encodings"> New value of the property at <CODE>index</CODE>.
        /// </param>
        /// <exception cref="ProfileException"> </exception>
        public virtual void SetEncodings(int index, Encoding encodings)
        {
            this.encodings[index] = encodings;
        }

		/// <summary>
		/// Getter for property HL7OID. </summary>
		/// <returns> Value of property HL7OID. </returns>
		public virtual string HL7OID { get; set; }

	    /// <summary>
		/// Getter for property HL7Version. </summary>
		/// <returns> Value of property HL7Version. </returns>
		public virtual string HL7Version { get; set; }

	    /// <summary>
		/// Getter for property impNote. </summary>
		/// <returns> Value of property impNote. </returns>
		public virtual string ImpNote { get; set; }

	    /// <summary>
		/// Getter for property message. </summary>
		/// <returns> Value of property message. </returns>
		public virtual StaticDef Message { get; set; }

	    /// <summary>
		/// Getter for property orgName. </summary>
		/// <returns> Value of property orgName. </returns>
		public virtual string OrgName { get; set; }

	    /// <summary>
		/// Getter for property role. </summary>
		/// <returns> Value of property role. </returns>
		public virtual string Role { get; set; }

	    /// <summary>
		/// Getter for property specName. </summary>
		/// <returns> Value of property specName. </returns>
		public virtual string SpecName { get; set; }

	    /// <summary>
		/// Getter for property specVersion. </summary>
		/// <returns> Value of property specVersion. </returns>
		public virtual string SpecVersion { get; set; }

	    /// <summary>
		/// Getter for property status. </summary>
		/// <returns> Value of property status. </returns>
		public virtual string Status { get; set; }

	    /// <summary>
		/// Getter for property useCase. </summary>
		/// <returns> Value of property useCase. </returns>
		public virtual UseCase.UseCase UseCase { get; set; }

		public virtual string Name { set; get; }
	}
}