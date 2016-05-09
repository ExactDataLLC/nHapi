namespace NHapi.Base.Conf.Spec.Message
{

	/// <summary>
	/// A message profile Segment or SegGroup. 
	/// @author Bryan Tripp
	/// </summary>
	public interface ProfileStructure
	{

		/// <summary>
		/// Getter for property name. </summary>
		/// <returns> Value of property name. </returns>
		string Name {get;set;}

		/// <summary>
		/// Setter for property name. </summary>
		/// <param name="name"> New value of property name.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setName(String name) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property longName. </summary>
		/// <returns> Value of property longName. </returns>
		string LongName {get;set;}

		/// <summary>
		/// Setter for property longName. </summary>
		/// <param name="longName"> New value of property longName.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setLongName(String longName) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property usage. </summary>
		/// <returns> Value of property usage. </returns>
		string Usage {get;set;}

		/// <summary>
		/// Setter for property usage. </summary>
		/// <param name="usage"> New value of property usage.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setUsage(String usage) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property min. </summary>
		/// <returns> Value of property min. </returns>
		short Min {get;set;}

		/// <summary>
		/// Setter for property min. </summary>
		/// <param name="min"> New value of property min.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setMin(short min) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property max. </summary>
		/// <returns> Value of property max. </returns>
		short Max {get;set;}

		/// <summary>
		/// Setter for property max. </summary>
		/// <param name="max"> New value of property max.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setMax(short max) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property impNote. </summary>
		/// <returns> Value of property impNote. </returns>
		string ImpNote {get;set;}

		/// <summary>
		/// Setter for property impNote. </summary>
		/// <param name="impNote"> New value of property impNote.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setImpNote(String impNote) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property description. </summary>
		/// <returns> Value of property description. </returns>
		string Description {get;set;}

		/// <summary>
		/// Setter for property description. </summary>
		/// <param name="description"> New value of property description.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setDescription(String description) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property reference. </summary>
		/// <returns> Value of property reference. </returns>
		string Reference {get;set;}

		/// <summary>
		/// Setter for property reference. </summary>
		/// <param name="reference"> New value of property reference.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setReference(String reference) throws ca.uhn.hl7v2.conf.ProfileException;

		/// <summary>
		/// Getter for property predicate. </summary>
		/// <returns> Value of property predicate. </returns>
		string Predicate {get;set;}

		/// <summary>
		/// Setter for property predicate. </summary>
		/// <param name="predicate"> New value of property predicate.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setPredicate(String predicate) throws ca.uhn.hl7v2.conf.ProfileException;

	}

}