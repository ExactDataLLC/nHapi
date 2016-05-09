using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using NHapi.Base.Conf.Spec;
using NHapi.Base.Conf.Spec.Message;
using NHapi.Base.Log;

/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1 
/// (the "License"); you may not use this file except in compliance with the License. 
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/ 
/// Software distributed under the License is distributed on an "AS IS" basis, 
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the 
/// specific language governing rights and limitations under the License. 
/// 
/// The Original Code is "ProfileParser.java".  Description: 
/// "Parses a Message Profile XML document into a RuntimeProfile object." 
/// 
/// The Initial Developer of the Original Code is University Health Network. Copyright (C) 
/// 2003.  All Rights Reserved. 
/// 
/// Contributor(s): ______________________________________. 
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

namespace NHapi.Base.Conf.Parser
{

	/// <summary>
	/// <para>
	/// Parses a Message Profile XML document into a RuntimeProfile object. A Message Profile is a formal
	/// description of additional constraints on a message (beyond what is specified in the HL7
	/// specification), usually for a particular system, region, etc. Message profiles are introduced in
	/// HL7 version 2.5 section 2.12. The RuntimeProfile object is simply an object representation of the
	/// profile, which may be used for validating messages or editing the profile.
	/// </para>
	/// <para>
	/// Example usage: <code><pre>
	/// 		// Load the profile from the classpath
	///      ProfileParser parser = new ProfileParser(false);
	///      RuntimeProfile profile = parser.parseClasspath("ca/uhn/hl7v2/conf/parser/example_ack.xml");
	/// 
	///      // Create a message to Validate
	///      String message = "MSH|^~\\&|||||||ACK^A01|1|D|2.4|||||CAN|wrong|F^^HL70001^x^^HL78888|\r"; //note HL7888 doesn't exist
	///      ACK msg = (ACK) (new PipeParser()).parse(message);
	/// 
	///      // Validate
	/// 		HL7Exception[] errors = new DefaultValidator().Validate(msg, profile.getMessage());
	/// 
	/// 		// Each exception is a validation error
	/// 		System.out.println("Validation errors: " + Arrays.asList(errors));
	/// </pre></code>
	/// </para>
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public class ProfileParser
	{

        private const string PROFILE_XSD = "NHapi.Base.Conf.message_profile.xsd";

		private static readonly ILog log = HapiLogFactory.GetHapiLog(typeof(ProfileParser));

		private bool alwaysValidate;
		private ValidationEventHandler errorHandler;

		/// <summary>
		/// Creates a new instance of ProfileParser
		/// </summary>
		/// <param name="alwaysValidate"> if true, validates all profiles against a local copy of the
		///            profile XSD; if false, validates against declared grammar (if any) </param>
		public ProfileParser(bool alwaysValidate)
		{

			this.alwaysValidate = alwaysValidate;
            errorHandler = handleError;
		}

	    private void handleError(object sender, ValidationEventArgs e)
	    {
	        if (e.Severity == XmlSeverityType.Warning)
            {
                log.Warn(string.Format("Warning: {0}", e.Message));
            }
            else
            {
                throw e.Exception;
            }
	    }

		/// <summary>
		/// Parses an XML profile string into a RuntimeProfile object.
        /// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		public virtual RuntimeProfile parse(string profileString)
		{
			RuntimeProfile profile = new RuntimeProfile();
			XDocument doc = parseIntoDOM(profileString);

			XElement root = doc.Root;
			profile.HL7Version = root.Attribute("HL7Version").Value;

			XElement metadata = root.Element("MetaData");
			if (metadata != null)
			{
                string name = metadata.Attribute("Name").Value;
				profile.Name = name;
			}

			// get static definition
		    XElement staticDef = root.Element("HL7v2xStaticDef");
			StaticDef sd = parseStaticProfile(staticDef);
			profile.Message = sd;
			return profile;
		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private StaticDef parseStaticProfile(XElement elem)
		{
		    StaticDef message = new StaticDef
		    {
                MsgType = elem.GetAttribute("MsgType"),
                EventType = elem.GetAttribute("EventType"),
                MsgStructID = elem.GetAttribute("MsgStructID"),
                OrderControl = elem.GetAttribute("OrderControl"),
                EventDesc = elem.GetAttribute("EventDesc"),
                Identifier = elem.GetAttribute("identifier"),
                Role = elem.GetAttribute("role")
		    };

            XElement md = elem.Element("MetaData");
			if (md != null)
			{
				message.MetaData = parseMetaData(md);
			}

			message.ImpNote = getValueOfFirstElement("ImpNote", elem);
			message.Description = getValueOfFirstElement("Description", elem);
			message.Reference = getValueOfFirstElement("Reference", elem);

			parseChildren(message, elem);
			return message;
		}

		/// <summary>
		/// Parses metadata element </summary>
		private MetaData parseMetaData(XElement elem)
		{
			log.Debug("ProfileParser.parseMetaData() has been called ... note that this method does nothing.");
			return null;
		}

		/// <summary>
		/// Parses children (i.e. segment groups, segments) of a segment group or message profile
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
        private void parseChildren(AbstractSegmentContainer parent, XElement elem)
		{
			int childIndex = 1;
            foreach (XElement child in elem.Descendants())
			{
				if (child.Name.LocalName.Equals("SegGroup", StringComparison.OrdinalIgnoreCase))
				{
					SegGroup group = parseSegmentGroupProfile(child);
					parent.setChild(childIndex++, group);
				}
                else if (child.Name.LocalName.Equals("Segment", StringComparison.OrdinalIgnoreCase))
				{
					Seg segment = parseSegmentProfile(child);
					parent.setChild(childIndex++, segment);
				}
			}
		}

		/// <summary>
		/// Parses a segment group profile </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private SegGroup parseSegmentGroupProfile(XElement elem)
		{
			SegGroup group = new SegGroup();
			log.Debug("Parsing segment group profile: " + elem.Attribute("Name").Value);

			parseProfileStuctureData(group, elem);

			parseChildren(group, elem);
			return group;
		}

		/// <summary>
		/// Parses a segment profile </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private Seg parseSegmentProfile(XElement elem)
		{
			Seg segment = new Seg();
            log.Debug("Parsing segment profile: " + elem.Attribute("Name").Value);

			parseProfileStuctureData(segment, elem);

			int childIndex = 1;
            foreach (XElement child in elem.Descendants().Where(e => e.Name.LocalName.Equals("Field", StringComparison.OrdinalIgnoreCase)))
			{
				Field field = parseFieldProfile(child);
				segment.SetField(childIndex++, field);
			}

			return segment;
		}

		/// <summary>
		/// Parse common data in profile structure (eg SegGroup, Segment) </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private void parseProfileStuctureData(IProfileStructure @struct, XElement elem)
		{
			@struct.Name = elem.Attribute("Name").Value;
            @struct.LongName = elem.Attribute("LongName").Value;
            @struct.Usage = elem.Attribute("Usage").Value;
            string min = elem.Attribute("Min").Value;
            string max = elem.Attribute("Max").Value;
			try
			{
				@struct.Min = short.Parse(min);
				if (max.IndexOf('*') >= 0)
				{
					@struct.Max = (short) -1;
				}
				else
				{
					@struct.Max = short.Parse(max);
				}
			}
			catch (System.FormatException e)
			{
				throw new ProfileException("Min and max must be short integers: " + min + ", " + max, e);
			}

			@struct.ImpNote = getValueOfFirstElement("ImpNote", elem);
			@struct.Description = getValueOfFirstElement("Description", elem);
			@struct.Reference = getValueOfFirstElement("Reference", elem);
			@struct.Predicate = getValueOfFirstElement("Predicate", elem);
		}

		/// <summary>
		/// Parses a field profile </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private Field parseFieldProfile(XElement elem)
		{
			Field field = new Field();
            log.Debug("  Parsing field profile: " + elem.Attribute("Name").Value);

            field.Usage = elem.Attribute("Usage").Value;
            string itemNo = elem.Attribute("ItemNo").Value;
            string min = elem.Attribute("Min").Value;
            string max = elem.Attribute("Max").Value;

			try
			{
				if (itemNo.Length > 0)
				{
					field.ItemNo = short.Parse(itemNo);
				}
			}
			catch (System.FormatException e)
			{
                throw new ProfileException("Invalid ItemNo: " + itemNo + "( for name " + elem.Attribute("Name").Value + ")", e);
			} // try-catch

			try
			{
				field.Min = short.Parse(min);
				if (max.IndexOf('*') >= 0)
				{
					field.Max = (short) -1;
				}
				else
				{
					field.Max = short.Parse(max);
				}
			}
			catch (System.FormatException e)
			{
				throw new ProfileException("Min and max must be short integers: " + min + ", " + max, e);
			}

			parseAbstractComponentData(field, elem);

			int childIndex = 1;
            foreach (XElement child in elem.Descendants().Where(e => e.Name.LocalName.Equals("Component", StringComparison.OrdinalIgnoreCase)))
			{
				Component comp = (Component) parseComponentProfile(child, false);
				field.setComponent(childIndex++, comp);
			}

			return field;
		}

		/// <summary>
		/// Parses a component profile </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private AbstractComponent parseComponentProfile(XElement elem, bool isSubComponent)
		{
			AbstractComponent comp = null;
			if (isSubComponent)
			{
                log.Debug("      Parsing subcomp profile: " + elem.Attribute("Name").Value);
				comp = new SubComponent();
			}
			else
			{
                log.Debug("    Parsing comp profile: " + elem.Attribute("Name").Value);
				comp = new Component();

				int childIndex = 1;
                foreach (XElement child in elem.Descendants().Where(e => e.Name.LocalName.Equals("SubComponent", StringComparison.OrdinalIgnoreCase)))
			    {
				    SubComponent subcomp = (SubComponent) parseComponentProfile(child, true);
				    ((Component) comp).setSubComponent(childIndex++, subcomp);
				}
			}

			parseAbstractComponentData(comp, elem);

			return comp;
		}

		/// <summary>
		/// Parses common features of AbstractComponents (ie field, component, subcomponent)
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private void parseAbstractComponentData(AbstractComponent comp, XElement elem)
		{
            comp.Name = elem.GetAttribute("Name");
            comp.Usage = elem.GetAttribute("Usage");
            comp.Datatype = elem.GetAttribute("Datatype");
            string length = elem.GetAttribute("Length");
			if (!string.ReferenceEquals(length, null) && length.Length > 0)
			{
				try
				{
					comp.Length = long.Parse(length);
				}
				catch (System.FormatException e)
				{
					throw new ProfileException("Length must be a long integer: " + length, e);
				}
			}
            comp.ConstantValue = elem.GetAttribute("ConstantValue");
            string table = elem.GetAttribute("Table");
			if (!string.IsNullOrEmpty(table))
			{
				try
				{
					comp.Table = table;
				}
				catch (FormatException e)
				{
					throw new ProfileException("Table must be a short integer: " + table, e);
				}
			}

			comp.ImpNote = getValueOfFirstElement("ImpNote", elem);
			comp.Description = getValueOfFirstElement("Description", elem);
			comp.Reference = getValueOfFirstElement("Reference", elem);
			comp.Predicate = getValueOfFirstElement("Predicate", elem);

			int dataValIndex = 0;
            foreach (XElement child in elem.Descendants().Where(e => e.Name.LocalName.Equals("DataValues", StringComparison.OrdinalIgnoreCase)))
			{
				DataValue val = new DataValue();
                val.ExValue = child.GetAttribute("ExValue");
				comp.setDataValues(dataValIndex++, val);
			}

		}

		/// <summary>
		/// Parses profile string into DOM document </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private XDocument parseIntoDOM(string profileString)
		{
			try
			{
                XDocument doc = XDocument.Parse(profileString);
				if (alwaysValidate)
				{
				    Stream schemaStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(PROFILE_XSD);
                    XmlSchema schema = XmlSchema.Read(new XmlTextReader(schemaStream), errorHandler);
                    XmlSchemaSet schemaSet = new XmlSchemaSet();
				    schemaSet.Add(schema);
				    doc.Validate(schemaSet, errorHandler);
				}
				return doc;
			}
			catch (Exception e)
			{
				throw new ProfileException("Exception parsing message profile: " + e.Message, e);
			}
		}

		/// <summary>
		/// Gets the result of getFirstElementByTagName() and returns the value of that element.
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		private string getValueOfFirstElement(string name, XElement parent)
		{
		    XElement el = parent.Element(name);
			string val = null;
			if (el != null)
			{
			    if (el.HasElements)
			    {
                    XElement n = el.Descendants().First();
                    if (n.NodeType == XmlNodeType.Text)
                    {
                        val = n.Value;
                    }
			    }
			    else
			    {
			        val = el.Value;
			    }								
			}
			return val;
		}

		public static void Main(string[] args)
		{

			if (args.Length != 1)
			{
				Console.WriteLine("Usage: ProfileParser profile_file");
				Environment.Exit(1);
			}

			try
			{
				// FileInfo f = new
				// FileInfo("C:\\Documents and Settings\\bryan\\hapilocal\\hapi\\ca\\uhn\\hl7v2\\conf\\parser\\example_ack.xml");
                FileInfo f = new FileInfo(args[0]);
			    StreamReader @in = f.OpenText();
				char[] cbuf = new char[(int) f.Length];
				@in.Read(cbuf, 0, (int) f.Length);
				string xml = new string(cbuf);
				// System.out.println(xml);

				ProfileParser pp = new ProfileParser(true);
				pp.parse(xml);
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
		}

	}

}