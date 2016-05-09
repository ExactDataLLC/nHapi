using System;
using System.Collections.Generic;
using System.Reflection;

/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1 
/// (the "License"); you may not use this file except in compliance with the License. 
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/ 
/// Software distributed under the License is distributed on an "AS IS" basis, 
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the 
/// specific language governing rights and limitations under the License. 
/// 
/// The Original Code is "FiniteList.java".  Description: 
/// "Holds a group of repetitions for a given Profile and exercises cardinality constraints" 
/// 
/// The Initial Developer of the Original Code is University Health Network. Copyright (C) 
/// 2001.  All Rights Reserved. 
/// 
/// Contributor(s): James Agnew
///                Paul Brohman
///                Mitch Delachevrotiere
///                Shawn Dyck
///  				Cory Metcalf 
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
namespace NHapi.Base.Conf.Classes.Abs
{

	using Exceptions;

	/// <summary>
	/// Holds a group of repetitions for a given Profile and exercises cardinality constraints
	/// @author <table><tr>James Agnew</tr>
	///                <tr>Paul Brohman</tr>
	///                <tr>Mitch Delachevrotiere</tr>
	///                <tr>Shawn Dyck</tr>
	/// 				  <tr>Cory Metcalf</tr></table>
	/// </summary>
	public class FiniteList
	{

		private List<Repeatable> reps; // Stores the reps
		private int maxReps; // The maximum allowable number of reps
	//	private int minReps; // The minimum allowable number of reps	
		private Type repType; // The type of repetition being stored here
		private object underlyingObject; // The underlying HAPI object

		/// <summary>
		/// Constructor for FiniteList </summary>
		/// <param name="repType"> the Class which is repeating </param>
		/// <param name="underlyingObject"> the underlying object that the extending class represents </param>
		public FiniteList(Type repType, object underlyingObject)
		{
			this.repType = repType;
			this.underlyingObject = underlyingObject;

			Repeatable firstRep = createRep(0);
			this.maxReps = firstRep.MaxReps;
	//		this.minReps = firstRep.getMinReps();

			reps = new List<Repeatable>();
			reps.Add(firstRep);
			createNewReps(maxReps);
		}

		/// <summary>
		/// Creates a new repetition </summary>
		/// <param name="rep"> the number representing the number of repetitions </param>
		private void createNewReps(int rep)
		{
			while (reps.Count <= rep)
			{
				reps.Add(createRep(reps.Count));
			}
		}

		/// <summary>
		/// Creates the repition </summary>
		/// <param name="rep"> the number representing which repition </param>
		private Repeatable createRep(int rep)
		{
			try
			{
//JAVA TO C# CONVERTER WARNING: Java wildcard generics have no direct equivalent in .NET:
//ORIGINAL LINE: Constructor<?> theCon = repType.getConstructors()[0];
				ConstructorInfo theCon = repType.GetConstructors()[0];
				Repeatable thisRep = (Repeatable) theCon.Invoke(new [] {underlyingObject, rep});
				return thisRep;
			}
            catch (TargetInvocationException e)
			{
				throw new ConformanceError("Error creating underlying repetition. This is a bug.\nError is: " + e + "\n" + e.InnerException);
			}
			catch (Exception e)
			{
				throw new ConformanceError("Error creating underlying repetition. This is a bug. Error is: " + e);
			}
		}

		/// <summary>
		/// Returns the desired repetition </summary>
		/// <param name="rep"> The desired repetition number. Note that in accordance with the HL7 standard </param>
		/// <returns> The desired repetition </returns>
		/// <exception cref="ConformanceException"> if repetition is not accessible </exception>
//JAVA TO C# CONVERTER WARNING: Method 'throws' clauses are not available in .NET:
//ORIGINAL LINE: public Repeatable getRep(int rep) throws ConfRepException
		public virtual Repeatable getRep(int rep)
		{
			if (rep < 1 || (maxReps != -1 && maxReps < rep))
			{
				throw new ConfRepException(maxReps, rep);
			}

			rep--; // Decremented because HL7 standard wants 1-offset arrays
			createNewReps(rep); // Create new reps if needed

			return reps[rep];
		}

	}

}