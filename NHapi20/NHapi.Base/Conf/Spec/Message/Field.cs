using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// The specification for a specific field in a message profile.  
	/// @author Bryan Tripp
	/// </summary>
	public class Field : AbstractComponent
	{
		private short min;
		private short max;
		private short itemNo;

		private readonly IList<Component> components = new List<Component>();

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
		/// Getter for property itemNo. </summary>
		/// <returns> Value of property itemNo. </returns>
		public virtual short ItemNo
		{
			get
			{
				return this.itemNo;
			}
			set
			{
				this.itemNo = value;
			}
		}


		/// <summary>
		/// Indexed getter for property components (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual Component getComponent(int index)
		{
			return this.components[index - 1];
		}

		/// <summary>
		/// Indexed setter for property components (index starts at 1 following HL7 convention). </summary>
		/// <param name="index"> Index of the property (starts at 1 following HL7 convention). </param>
		/// <param name="component"> New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public void setComponent(int index, Component component) throws ca.uhn.hl7v2.conf.ProfileException
		public virtual void setComponent(int index, Component component)
		{
			index--;
			while (components.Count <= index)
			{
				components.Add(null);
			}
			this.components[index] = component;
		}


		/// <summary>
		/// Returns the number of components </summary>
		public virtual int Components
		{
			get
			{
				return this.components.Count;
			}
		}

		/// <summary>
		/// Returns the number of components </summary>
		public virtual IList<Component> ChildrenAsList
		{
			get
			{
				return (this.components);
			}
		}

	}



}