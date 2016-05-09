using System.Collections.Generic;

namespace NHapi.Base.Conf.Spec.Message
{


	/// <summary>
	/// The specification for a particular field component in a message profile.
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public class Component : AbstractComponent
	{

		private readonly IList<SubComponent> components = new List<SubComponent>();

		/// <summary>
		/// Creates a new instance of Component </summary>
		public Component()
		{
		}

		/// <summary>
		/// Indexed getter for property components (index starts at 1 following HL7
		/// convention).
		/// </summary>
		/// <param name="index">
		///            Index of the property (starts at 1 following HL7 convention). </param>
		/// <returns> Value of the property at <CODE>index</CODE>. </returns>
		public virtual SubComponent getSubComponent(int index)
		{
			return this.components[index - 1];
		}

		/// <summary>
		/// Indexed setter for property components (index starts at 1 following HL7
		/// convention).
		/// </summary>
		/// <param name="index">
		///            Index of the property (starts at 1 following HL7 convention). </param>
		/// <param name="component">
		///            New value of the property at <CODE>index</CODE>.
		/// </param>
		/// <exception cref="ProfileException"> </exception>
		public virtual void setSubComponent(int index, SubComponent component)
		{
			index--;
			while (components.Count <= index)
			{
				components.Add(null);
			}
			this.components[index] = component;
		}


		/// <summary>
		/// Returns the number of subcomponents in this component </summary>
		public virtual int SubComponents
		{
			get
			{
				return this.components.Count;
			}
		}

		public virtual IList<SubComponent> ChildrenAsList
		{
			get
			{
				return (this.components);
			}
		}

		public override string ToString()
		{
			return "Component[" + Name + "]";
		}

	}

}