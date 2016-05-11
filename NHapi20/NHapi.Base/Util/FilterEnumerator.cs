/// <summary> This class is based on the Iterators.FilterIterator class from
/// araSpect (araspect.sourceforge.net).  The original copyright follows ...
/// 
/// =================================================================
/// Copyright (c) 2001,2002 aragost ag, Zürich, Switzerland.
/// All rights reserved.
/// 
/// This software is provided 'as-is', without any express or implied
/// warranty. In no event will the authors be held liable for any
/// damages arising from the use of this software.
/// 
/// Permission is granted to anyone to use this software for any
/// purpose, including commercial applications, and to alter it and
/// redistribute it freely, subject to the following restrictions:
/// 
/// 1. The origin of this software must not be misrepresented; you
/// must not claim that you wrote the original software. If you
/// use this software in a product, an acknowledgment in the
/// product documentation would be appreciated but is not required.
/// 
/// 2. Altered source versions must be plainly marked as such, and
/// must not be misrepresented as being the original software.
/// 
/// 3. This notice may not be removed or altered from any source
/// distribution.
/// 
/// ==================================================================
/// 
/// Changes (c) 2003 University Health Network include the following:
/// - move to non-nested class
/// - collapse inherited method remove()
/// - accept iterator instead of object in constructor
/// - moved to HAPI package
/// - Predicate added as an inner class; also changed to an interface
/// 
/// These changes are distributed under the same terms as the original (above). 
/// </summary>

using System;
using System.Collections;

namespace NHapi.Base.Util
{
	/// <summary>
	/// Filter iterator class
	/// </summary>
	public class FilterEnumerator : IEnumerator
	{
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="iter"></param>
		/// <param name="predicate"></param>
		public FilterEnumerator(IEnumerator iter, IPredicate predicate)
		{
		    nextObject = null;
			underlyingIterator = iter;
			thePredicate = predicate;
		    beforeFirst = true;
		}

		/// <summary>
		/// The current item
		/// </summary>
		public virtual Object Current
		{
			get
			{
                if (nextObject == null)
				{
					throw new ArgumentOutOfRangeException();
				}

                return nextObject;
			}
		}

	    /// <summary>
	    /// Move next
	    /// </summary>
	    /// <returns></returns>
	    public virtual bool MoveNext()
	    {
	        while (underlyingIterator.MoveNext())
	        {
	            if (thePredicate.Evaluate(underlyingIterator.Current))
                {
                    nextObject = underlyingIterator.Current;
                    return true;
                }	           
	        }

	        nextObject = null;
	        return false;
	    }
        
	    /// <summary>
		/// IPredicate interface
		/// </summary>
		public interface IPredicate
		{
			/// <summary>
			/// Evaluate the object
			/// </summary>
			/// <param name="obj"></param>
			/// <returns></returns>
			bool Evaluate(Object obj);
		}

		/// <summary>
		/// Reset
		/// </summary>
		public virtual void Reset()
		{
		    beforeFirst = true;
		    nextObject = null;
		    underlyingIterator.Reset();
		}

        private readonly IPredicate thePredicate;
        private readonly IEnumerator underlyingIterator;
        private Object nextObject;
        private bool beforeFirst = true;

	}
}