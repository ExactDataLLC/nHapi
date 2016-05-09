using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// A specification for a message segment in a conformance profile.  
	/// @author Bryan Tripp
	/// </summary>
	public class Seg : ProfileStructure
	{
		private string impNote;
		private string description;
		private string reference;
		private string predicate;
		private readonly IList<Field> fields = new List<Field>();
		private string name;
		private string longName;
		private string usage;
		private short min;
		private short max;

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
		/// Getter for property description. </summary>
		/// <returns> Value of property description. </returns>
		public virtual string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}


		/// <summary>
		/// Getter for property reference. </summary>
		/// <returns> Value of property reference. </returns>
		public virtual string Reference
		{
			get
			{
				return this.reference;
			}
			set
			{
				this.reference = value;
			}
		}


		/// <summary>
		/// Getter for property predicate. </summary>
		/// <returns> Value of property predicate. </returns>
		public virtual string Predicate
		{
			get
			{
				return this.predicate;
			}
			set
			{
				this.predicate = value;
			}
		}


		/// <summary>
		/// Indexed getter for property field (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual Field getField(int index)
		{
			return this.fields[index - 1];
		}

		/// <summary>
		/// Indexed setter for property field (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <param name="field"> New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setField(int index, Field field) throws ca.uhn.hl7v2.conf.ProfileException
		public virtual void setField(int index, Field field)
		{
			index--;
			while (fields.Count <= index)
			{
				fields.Add(null);
			}
			this.fields[index] = field;
		}

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
		/// Getter for property longName. </summary>
		/// <returns> Value of property longName. </returns>
		public virtual string LongName
		{
			get
			{
				return this.longName;
			}
			set
			{
				this.longName = value;
			}
		}


		/// <summary>
		/// Getter for property usage. </summary>
		/// <returns> Value of property usage. </returns>
		public virtual string Usage
		{
			get
			{
				return this.usage;
			}
			set
			{
				this.usage = value;
			}
		}


		/// <summary>
		/// Getter for property min. </summary>
		/// <returns> Value of property min. </returns>
		public virtual short Min
		{
			get
			{
				return this.min;
			}
			set
			{
				this.min = value;
			}
		}


		/// <summary>
		/// Getter for property max. </summary>
		/// <returns> Value of property max. </returns>
		public virtual short Max
		{
			get
			{
				return this.max;
			}
			set
			{
				this.max = value;
			}
		}



		/// <summary>
		/// Returns the number of fields in the segment </summary>
		public virtual int Fields
		{
			get
			{
				return this.fields.Count;
			}
		}

		/// <summary>
		/// Returns the number of fields in the segment </summary>
		public virtual IList<Field> FieldsAsList
		{
			get
			{
				return (this.fields);
			}
		}

		/// <summary>
		/// Returns the number of fields in the segment </summary>
		public virtual IList<Field> ChildrenAsList
		{
			get
			{
				return (this.fields);
			}
		}

	}

}