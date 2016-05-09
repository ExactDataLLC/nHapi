using System.Collections;
using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// An abstraction of SegGroup and MessageProfile (both are containers for segment specs).  
	/// @author Bryan Tripp
	/// </summary>
	public class AbstractSegmentContainer : IEnumerable<IProfileStructure>
	{
		private string description;
		private string reference;
		private string impNote;
		private IList<IProfileStructure> children = new List<IProfileStructure>();

		public virtual IList<IProfileStructure> ChildrenAsList
		{
			get
			{
				return (children);
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
		/// Indexed getter for property structure (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual IProfileStructure getChild(int index)
		{
			return this.children[index - 1];
		}

		/// <summary>
		/// Indexed setter for property structure.  Lengthens child list if necessary. </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <param name="structure"> New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
		public virtual void setChild(int index, IProfileStructure structure)
		{
			index--;
			while (children.Count <= index)
			{
				children.Add(null);
			}
			this.children[index] = structure;
		}

		/// <summary>
		/// Returns the number of children </summary>
		public virtual int Children
		{
			get
			{
				return this.children.Count;
			}
		}

		public virtual IEnumerator<IProfileStructure> GetEnumerator()
		{
			return (this.children).GetEnumerator();
		}

	    IEnumerator IEnumerable.GetEnumerator()
	    {
	        return GetEnumerator();
	    }
	}

}