using System;
using NHapi.Base.Model;
using NHapi.Base.Log;
using NHapi.Base;
using NHapi.Base.Model.Primitive;

namespace NHapi.Model.V251.Datatype
{

///<summary>
/// <p>The HL7 CQ (Composite Quantity with Units) data type.  Consists of the following components: </p><ol>
/// <li>Quantity (NM)</li>
/// <li>Units (CE)</li>
/// </ol>
///</summary>
[Serializable]
public class CQ : AbstractComposite{
	private IType[] data;

	///<summary>
	/// Creates a CQ.
	/// <param name="message">The Message to which this Type belongs</param>
	///</summary>
	public CQ(IMessage message) : this(message, null){}

	///<summary>
	/// Creates a CQ.
	/// <param name="message">The Message to which this Type belongs</param>
	/// <param name="description">The description of this type</param>
	///</summary>
	public CQ(IMessage message, string description) : base(message, description){
		data = new IType[2];
		data[0] = new NM(message,"Quantity");
		data[1] = new CE(message,"Units");
	}

	///<summary>
	/// Returns an array containing the data elements.
	///</summary>
	public override IType[] Components
	{ 
		get{
			return this.data; 
		}
	}

	///<summary>
	/// Returns an individual data component.
	/// @throws DataTypeException if the given element number is out of range.
	///<param name="index">The index item to get (zero based)</param>
	///<returns>The data component (as a type) at the requested number (ordinal)</returns>
	///</summary>
	public override IType this[int index] { 

get{
		try { 
			return this.data[index]; 
		} catch (System.ArgumentOutOfRangeException) { 
			throw new DataTypeException("Element " + index + " doesn't exist in 2 element CQ composite"); 
		} 
	} 
	} 
	///<summary>
	/// Returns Quantity (component #0).  This is a convenience method that saves you from 
	/// casting and handling an exception.
	///</summary>
	public NM Quantity {
get{
	   NM ret = null;
	   try {
	      ret = (NM)this[0];
	   } catch (DataTypeException e) {
	      HapiLogFactory.GetHapiLog(this.GetType()).Error("Unexpected problem accessing known data type component - this is a bug.", e);
	      throw new System.Exception("An unexpected error ocurred",e);
	   }
	   return ret;
}

}
	///<summary>
	/// Returns Units (component #1).  This is a convenience method that saves you from 
	/// casting and handling an exception.
	///</summary>
	public CE Units {
get{
	   CE ret = null;
	   try {
	      ret = (CE)this[1];
	   } catch (DataTypeException e) {
	      HapiLogFactory.GetHapiLog(this.GetType()).Error("Unexpected problem accessing known data type component - this is a bug.", e);
	      throw new System.Exception("An unexpected error ocurred",e);
	   }
	   return ret;
}

}
}}