using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// An abstraction of the common features of Field, Component, and SubComponent.  
	/// @author Bryan Tripp
	/// </summary>
	public class AbstractComponent
	{
		/// <summary>
		/// Creates a new instance of AbstractComponent </summary>
		public AbstractComponent()
		{
		}

	    private string reference;
		private string predicate;
		private readonly IList<DataValue> dataValues = new List<DataValue>();
		private string name;
		private string usage;
		private string datatype;
		private long length;
		private string constantValue;
		private string table;

		
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
		/// Indexed getter for property dataValues. </summary>
		/// <param name="index"> Index of the property. </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual DataValue getDataValues(int index)
		{
			return this.dataValues[index];
		}

		/// <summary>
		/// Indexed setter for property dataValues. </summary>
		/// <param name="index"> Index of the property. </param>
		/// <param name="dataValue"> New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
		public virtual void setDataValues(int index, DataValue dataValue)
		{
			while (dataValues.Count <= index)
			{
				dataValues.Add(null);
			}
			this.dataValues[index] = dataValue;			
		}




	    /// <summary>
	    /// Getter for property name. </summary>
	    /// <returns> Value of property name. </returns>
	    public virtual string Name { get; set; }

		/// <summary>
		/// Getter for property usage. </summary>
		/// <returns> Value of property usage. </returns>
		public virtual string Usage
		{
			get; set; 
		}


		/// <summary>
		/// Getter for property datatype. </summary>
		/// <returns> Value of property datatype. </returns>
		public virtual string Datatype
		{
			get; set; 

		}


		/// <summary>
		/// Getter for property length. </summary>
		/// <returns> Value of property length. </returns>
		public virtual long Length
		{
			get; set; 			
		}


		/// <summary>
		/// Getter for property constantValue. </summary>
		/// <returns> Value of property constantValue. </returns>
		public virtual string ConstantValue
		{
			get; set; 
		}


		/// <summary>
		/// Getter for property table. </summary>
		/// <returns> Value of property table. </returns>
		public virtual string Table
		{
			get; set; 
		}


	}

}