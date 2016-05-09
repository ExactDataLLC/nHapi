using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace NHapi.Base.Conf.Store
{

	/// 
	/// <summary>
	/// This particular implementation of ICodeStore retrieves valid codes and validates codeSystems using
	/// tables found in 'spec xml tables only' docs generated from the HL7 Messaging Workbench tool.
	/// 
	/// Note: The codeSystem parameter value used in the following methods must be a concatenation of a
	/// coding authority and coding table number that is 4 digits long.
	/// 
	/// Note: The current implementation only accepts a coding authority of HL7
	/// 
	/// @author Neal Acharya
	/// @author Christian Ohr
	/// </summary>
	public class ProfileCodeStore : AbstractCodeStore
	{

		private IDictionary<string, IList<string>> codes = new Dictionary<string, IList<string>>();

		/// <summary>
		/// Creates a ProfileCodeStore object that uses tables found in an 'spec xml tables only' xml doc
		/// specified by the input URI. The private field member tableDoc is created with content from
		/// the xml doc specified by the URI.
		/// </summary>
		/// <param name="uri"> the location of the specification XML file
		/// </param>
		/// <exception cref="ProfileException"> </exception>
		/// <exception cref="IOException">
		///  </exception>
		public ProfileCodeStore(string uri)
		{
			try
			{
				if (string.ReferenceEquals(uri, null))
				{
					throw new ProfileException("The input url parameter cannot be null");
				}

			    XmlSAXDocumentManager saxDocumentManager = XmlSAXDocumentManager.NewInstance();
                saxDocumentManager.parse(new FileInfo(uri), new ProfileCodeStoreHandler(this));
			}
			catch (IOException e)
			{
				throw e;
			}
			catch (Exception e)
			{
				throw new ProfileException(e.ToString(), e);
			}
		}

		/// <summary>
		/// As string constructor but accepts a URL object 
		/// </summary>
		public ProfileCodeStore(Uri url) : this(url.ToString())
		{
		}

		/// <summary>
		/// Retrieves all codes for a given conformance profile and codeSystem. Note: The codeSystem
		/// parameter value must be a concatenation of a coding authority and coding table number that is
		/// 4 digits long.
		/// 
		/// Note: The current implementation only accepts a coding authority of HL7
		/// </summary>
		/// <param name="codeSystem"> </param>
		/// <returns> String[] </returns>
		/// <exception cref="ProfileException"> </exception>
		public override string[] GetValidCodes(string codeSystem)
		{
			IList<string> result = getCodeTable(codeSystem);
			if (result == null)
			{
				throw new ProfileException("Unknown code system: " + codeSystem);
			}
			return result.ToArray();
		}

		/// <summary>
		/// Validates the codeSystem against the input conformance profile. If valid then output is
		/// 'true' else 'false'. Note: The codeSystem parameter value must be a concatenation of a coding
		/// authority and coding table number that is 4 digits long.
		/// 
		/// Note: The current implementation only accepts a coding authority of HL7
		/// </summary>
		/// <param name="codeSystem">
		/// </param>
		/// <returns> boolean </returns>
		public override bool KnowsCodes(string codeSystem)
		{
			try
			{
				return getCodeTable(codeSystem) != null;
			}
			catch (ProfileException)
			{
				return false;
			}
		}

		/// <summary>
		/// Retrieves the hl7Table Element from the tableDoc object defined by the table number in the
		/// input codeSystem.
		/// </summary>
		/// <param name="profileId">
		/// </param>
		/// <param name="codeSystem"> </param>
		/// <returns> Element </returns>
		/// <exception cref="ProfileException"> Element </exception>
		private IList<string> getCodeTable(string codeSystem)
		{
			if (string.ReferenceEquals(codeSystem, null))
			{
				throw new ProfileException("The input codeSystem parameter cannot be null");
			}
			if (codeSystem.Length < 4)
			{
				throw new ProfileException("The input codeSystem parameter cannot be less than 4 characters long");
			}
			// Extract the last 4 characters from the codeSystem param
			string tableId = codeSystem.Substring(codeSystem.Length - 4);
		    if (!codes.ContainsKey(tableId))
		    {
		        return null;
		    }
			return codes[tableId];
		}

		private class ProfileCodeStoreHandler : XmlSaxDefaultHandler
		{
			private readonly ProfileCodeStore outerInstance;

			public ProfileCodeStoreHandler(ProfileCodeStore outerInstance)
			{
				this.outerInstance = outerInstance;
			}

		    private string currentTable;
		    private const string HL7_TABLE_QNAME = "hl7table";
		    private const string TABLE_ELEMENT_QNAME = "tableElement";

			public override void startElement(string uri, string localName, string qName, SaxAttributesSupport attributes)
			{
				if (HL7_TABLE_QNAME.Equals(qName))
				{
					currentTable = attributes.GetValue("id");
					outerInstance.codes[currentTable] = new List<string>();
				}
				else if (TABLE_ELEMENT_QNAME.Equals(qName))
				{
					outerInstance.codes[currentTable].Add(attributes.GetValue("code"));
				}
			}

		}
	}

}