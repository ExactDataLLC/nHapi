using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// A specification for a message segment in a conformance profile.  
	/// @author Bryan Tripp
	/// </summary>
	public class Seg : IProfileStructure
	{
	    private readonly IList<Field> fields = new List<Field>();

	    /// <summary>
		/// Getter for property impNote. </summary>
		/// <returns> Value of property impNote. </returns>
		public virtual string ImpNote { get; set; }


	    /// <summary>
		/// Getter for property description. </summary>
		/// <returns> Value of property description. </returns>
		public virtual string Description { get; set; }


	    /// <summary>
		/// Getter for property reference. </summary>
		/// <returns> Value of property reference. </returns>
		public virtual string Reference { get; set; }


	    /// <summary>
		/// Getter for property predicate. </summary>
		/// <returns> Value of property predicate. </returns>
		public virtual string Predicate { get; set; }


	    /// <summary>
		/// Indexed getter for property field (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual Field GetField(int index)
		{
			return fields[index - 1];
		}

		/// <summary>
		/// Indexed setter for property field (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <param name="field"> New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
		public virtual void SetField(int index, Field field)
		{
			index--;
			while (fields.Count <= index)
			{
				fields.Add(null);
			}
			fields[index] = field;
		}

		/// <summary>
		/// Getter for property name. </summary>
		/// <returns> Value of property name. </returns>
		public virtual string Name { get; set; }


	    /// <summary>
		/// Getter for property longName. </summary>
		/// <returns> Value of property longName. </returns>
		public virtual string LongName { get; set; }


	    /// <summary>
		/// Getter for property usage. </summary>
		/// <returns> Value of property usage. </returns>
		public virtual string Usage { get; set; }


	    /// <summary>
		/// Getter for property min. </summary>
		/// <returns> Value of property min. </returns>
		public virtual short Min { get; set; }


	    /// <summary>
		/// Getter for property max. </summary>
		/// <returns> Value of property max. </returns>
		public virtual short Max { get; set; }


	    /// <summary>
		/// Returns the number of fields in the segment </summary>
		public virtual int NumFields
		{
			get
			{
				return fields.Count;
			}
		}

		/// <summary>
		/// Returns the number of fields in the segment </summary>
		public virtual IList<Field> Fields
		{
			get
			{
				return fields;
			}
		}

		/// <summary>
		/// Returns the number of fields in the segment </summary>
		public virtual IList<Field> Children
		{
			get
			{
				return fields;
			}
		}
	}
}