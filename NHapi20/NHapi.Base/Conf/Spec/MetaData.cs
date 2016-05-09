namespace NHapi.Base.Conf.Spec
{

	/// <summary>
	/// Conformance Profile meta data (an element of ConformanceProfile and StaticDef).  
	/// @author Bryan Tripp
	/// </summary>
	public class MetaData
	{
		/// <summary>
		/// Holds value of property name. </summary>
		private string name;

		/// <summary>
		/// Holds value of property orgName. </summary>
		private string orgName;

		/// <summary>
		/// Holds value of property version. </summary>
		private string version;

		/// <summary>
		/// Holds value of property status. </summary>
		private string status;

		/// <summary>
		/// Holds value of property topics. </summary>
		private string topics;

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
		/// Getter for property orgName. </summary>
		/// <returns> Value of property orgName. </returns>
		public virtual string OrgName
		{
			get
			{
				return this.orgName;
			}
			set
			{
				this.orgName = value;
			}
		}


		/// <summary>
		/// Getter for property version. </summary>
		/// <returns> Value of property version. </returns>
		public virtual string Version
		{
			get
			{
				return this.version;
			}
			set
			{
				this.version = value;
			}
		}


		/// <summary>
		/// Getter for property status. </summary>
		/// <returns> Value of property status. </returns>
		public virtual string Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}


		/// <summary>
		/// Getter for property topics. </summary>
		/// <returns> Value of property topics. </returns>
		public virtual string Topics
		{
			get
			{
				return this.topics;
			}
			set
			{
				this.topics = value;
			}
		}
	}
}