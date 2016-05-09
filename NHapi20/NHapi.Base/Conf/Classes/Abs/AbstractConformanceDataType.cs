/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1
/// (the "License"); you may not use this file except in compliance with the License.
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/
/// Software distributed under the License is distributed on an "AS IS" basis,
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the
/// specific language governing rights and limitations under the License.
/// 
/// The Original Code is "AbstractConformanceDataType.java".  Description:
/// "This class contains the functionality for a Data Type in the Conformance class set"
/// 
/// The Initial Developer of the Original Code is University Health Network. Copyright (C)
/// 2001.  All Rights Reserved.
/// 
/// Contributor(s): James Agnew
/// Paul Brohman
/// Mitch Delachevrotiere
/// Shawn Dyck
/// Cory Metcalf
/// 
/// Alternatively, the contents of this file may be used under the terms of the
/// GNU General Public License (the "GPL"), in which case the provisions of the GPL are
/// applicable instead of those above.  If you wish to allow use of your version of this
/// file only under the terms of the GPL and not to allow others to use your version
/// of this file under the MPL, indicate your decision by deleting  the provisions above
/// and replace  them with the notice and other provisions required by the GPL License.
/// If you do not delete the provisions above, a recipient may use your version of
/// this file under either the MPL or the GPL.
/// 
/// </summary>

using NHapi.Base.Conf.Classes.Exceptions;
using NHapi.Base.Conf.Parser;
using NHapi.Base.Model;
using NHapi.Base.Log;

namespace NHapi.Base.Conf.Classes.Abs
{

	/// <summary>
	/// This class contains the functionality for a Data Type in the Conformance class set
	/// @author <table><tr>James Agnew</tr>
	///                <tr>Paul Brohman</tr>
	///                <tr>Mitch Delachevrotiere</tr>
	///                <tr>Shawn Dyck</tr>
	/// 		  <tr>Cory Metcalf</tr></table>
	/// </summary>
	public abstract class AbstractConformanceDataType
	{

        private static readonly ILog log = HapiLogFactory.GetHapiLog(typeof(ProfileParser));

		private IPrimitive hapiPrimitive;

		/// <summary>
		/// Constructor for AbstractConformanceDataType </summary>
		/// <param name="hapiPrimitive"> the underlying primitive that the extending class represents </param>
		public AbstractConformanceDataType(IPrimitive hapiPrimitive)
		{
			this.hapiPrimitive = hapiPrimitive;

			try
			{
				if (!string.ReferenceEquals(ConstantValue, null))
				{
					Value = ConstantValue;
				}
			}
			catch (ConfDataException e)
			{
				log.Error("Could not enforce constant value.", e);
			}
		}

		/// <summary>
		/// This method returns the constant value for the extending class. </summary>
		///  <returns> the constant value </returns>
		public abstract string ConstantValue {get;}

		/// <summary>
		/// This method returns the Maximum length of the extending object. </summary>
		///  <returns> the maximum length </returns>
		public abstract long MaxLength {get;}

		/// <summary>
		/// This method validates the <code>String</code> value passed in to be no greater then the
		/// maximum allowable length for the extending class.  If the <code>String</code> value is valid, this
		/// method will set the underlying HAPI class's value. If the data passed in is invalid for the given
		/// data type, a ConfDataException is thrown. </summary>
		/// <exception cref="ConformaceException"> </exception>
		/// <param name="value"> the value of the Data Type </param>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: protected void setValue(java.lang.String value) throws ca.uhn.hl7v2.conf.classes.exceptions.ConfDataException
		protected internal virtual string Value
		{
			set
			{
				if ((this.MaxLength > 0) && (value.Length > this.MaxLength))
				{
					throw new ConfDataException("DataType length exceeds the Maximum allowable length");
				}
    
				try
				{
					this.hapiPrimitive.Value = value;
				}
				catch (DataTypeException)
				{
					throw new ConfDataException("Invalid Data Populated");
				}
			}
		}

	}

}