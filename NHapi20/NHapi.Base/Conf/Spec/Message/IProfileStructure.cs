namespace NHapi.Base.Conf.Spec.Message
{
	/// <summary>
	/// A message profile Segment or SegGroup. 
	/// @author Bryan Tripp
	/// </summary>
	public interface IProfileStructure
	{
		string Name {get;set;}

		string LongName {get;set;}

		string Usage {get;set;}

		short Min {get;set;}

		short Max {get;set;}

		string ImpNote {get;set;}

		string Description {get;set;}

		string Reference {get;set;}

		string Predicate {get;set;}
	}
}