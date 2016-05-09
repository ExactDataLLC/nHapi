namespace NHapi.Base.Conf.Spec.Message
{

	/// <summary>
	/// A specification for a segment group in a conformance profile.  
	/// @author Bryan Tripp
	/// </summary>
	public class SegGroup : AbstractSegmentContainer, IProfileStructure
	{
		private string predicate;
		private string name;
		private string longName;
		private string usage;
		private short min;
		private short max;

		/// <summary>
		/// {@inheritDoc}
		/// </summary>
		public override string ToString()
		{
			return "SegGroup[" + Name + "]";
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


	}

}