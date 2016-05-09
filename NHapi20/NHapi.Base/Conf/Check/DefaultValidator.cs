using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NHapi.Base.Conf.Parser;
using NHapi.Base.Conf.Spec;
using NHapi.Base.Conf.Spec.Message;
using NHapi.Base.Conf.Store;
using NHapi.Base.Log;
using NHapi.Base.Model;
using NHapi.Base.Parser;
using NHapi.Base.Util;

/// <summary>
/// The contents of this file are subject to the Mozilla Public License Version 1.1 
/// (the "License"); you may not use this file except in compliance with the License. 
/// You may obtain a copy of the License at http://www.mozilla.org/MPL/ 
/// Software distributed under the License is distributed on an "AS IS" basis, 
/// WITHOUT WARRANTY OF ANY KIND, either express or implied. See the License for the 
/// specific language governing rights and limitations under the License. 
/// 
/// The Original Code is "DefaultValidator.java".  Description: 
/// "A default conformance validator." 
/// 
/// The Initial Developer of the Original Code is University Health Network. Copyright (C) 
/// 2003.  All Rights Reserved. 
/// 
/// Contributor(s): ______________________________________. 
/// 
/// Alternatively, the contents of this file may be used under the terms of the 
/// GNU General Public License (the  �GPL�), in which case the provisions of the GPL are 
/// applicable instead of those above.  If you wish to allow use of your version of this 
/// file only under the terms of the GPL and not to allow others to use your version 
/// of this file under the MPL, indicate your decision by deleting  the provisions above 
/// and replace  them with the notice and other provisions required by the GPL License.  
/// If you do not delete the provisions above, a recipient may use your version of 
/// this file under either the MPL or the GPL. 
/// 
/// </summary>

namespace NHapi.Base.Conf.Check
{
	/// <summary>
	/// A default conformance profile validator.
	/// 
	/// Note: this class is currently NOT thread-safe!
	/// 
	/// @author Bryan Tripp
	/// </summary>
	public class DefaultValidator : IValidator
	{

		private EncodingCharacters enc; // used to check for content in parts of a message
        private static readonly ILog log = HapiLogFactory.GetHapiLog(typeof(ProfileParser));
		private bool validateChildren = true;
		private ICodeStore codeStore;

		/// <summary>
		/// Creates a new instance of DefaultValidator </summary>
		public DefaultValidator() 
		{
            codeStoreRegistry = new DefaultCodeStoreRegistry();
		}

		public DefaultValidator(ICodeStoreRegistry codeStoreRegistry)
		{
		    this.codeStoreRegistry = codeStoreRegistry;
			enc = new EncodingCharacters('|', null); // the | is assumed later -- don't change
		}

		/// <summary>
		/// If set to false (default is true), each testXX and validateXX method will only test the
		/// direct object it is responsible for, not its children.
		/// </summary>
		public virtual bool ValidateChildren
		{
			set
			{
				this.validateChildren = value;
			}
		}

		/// <summary>
		/// <para>
		/// Provides a code store to use to provide the code tables which will be used to Validate coded
		/// value types. If a code store has not been set (which is the default),
		/// <seealso cref="ProfileStoreFactory"/> will be checked for an appropriate code store, and if none is
		/// found then coded values will not be validated.
		/// </para>
		/// </summary>
		public virtual ICodeStore CodeStore
		{
			set
			{
				codeStore = value;
			}
		}

		/// <seealso cref= IValidator#Validate </seealso>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		public virtual HL7Exception[] Validate(IMessage message, StaticDef profile)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();
			Terser t = new Terser(message);

			checkMessageType(t.Get("/MSH-9-1"), profile, exList);
			checkEventType(t.Get("/MSH-9-2"), profile, exList);
			checkMessageStructure(t.Get("/MSH-9-3"), profile, exList);

			((List<HL7Exception>)exList).AddRange(doTestGroup(message, profile, profile.Identifier, validateChildren));
			return exList.ToArray();
		}

        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		protected internal virtual void checkEventType(string evType, StaticDef profile, IList<HL7Exception> exList)
		{
			if (!evType.Equals(profile.EventType) && !profile.EventType.Equals("ALL", StringComparison.CurrentCultureIgnoreCase))
			{
				HL7Exception e = new ProfileNotFollowedException("Event type " + evType + " doesn't match profile type of " + profile.EventType);
				exList.Add(e);
			}
		}

        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		protected internal virtual void checkMessageType(string msgType, StaticDef profile, IList<HL7Exception> exList)
		{
			if (!msgType.Equals(profile.MsgType))
			{
				HL7Exception e = new ProfileNotFollowedException("Message type " + msgType + " doesn't match profile type of " + profile.MsgType);
				exList.Add(e);
			}
		}

		protected internal virtual void checkMessageStructure(string msgStruct, StaticDef profile, IList<HL7Exception> exList)
		{
			if (string.ReferenceEquals(msgStruct, null) || !msgStruct.Equals(profile.MsgStructID))
			{
				HL7Exception e = new ProfileNotFollowedException("Message structure " + msgStruct + " doesn't match profile type of " + profile.MsgStructID);
				exList.Add(e);
			}
		}

		/// <summary>
		/// Tests a group against a group section of a profile.
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		public virtual IList<HL7Exception> testGroup(IGroup group, SegGroup profile, string profileID)
		{
			return doTestGroup(group, profile, profileID, true);
		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		protected internal virtual IList<HL7Exception> doTestGroup(IGroup group, AbstractSegmentContainer profile, string profileID, bool theValidateChildren)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();
			IList<string> allowedStructures = new List<string>();

			foreach (IProfileStructure @struct in profile)
			{

				// only test a structure in detail if it isn't X
				if (!@struct.Usage.Equals("X", StringComparison.CurrentCultureIgnoreCase))
				{
					allowedStructures.Add(@struct.Name);

					// see which instances have content
					try
					{
						IList<IStructure> instancesWithContent = new List<IStructure>();
                        foreach (IStructure instance in group.GetAll(@struct.Name))
						{
							if (!instance.IsEmpty())
							{
								instancesWithContent.Add(instance);
							}
						}

						testCardinality(instancesWithContent.Count, @struct.Min, @struct.Max, @struct.Usage, @struct.Name, exList);

						// test children on instances with content
						if (theValidateChildren)
						{
							foreach (IStructure s in instancesWithContent)
							{
								((List<HL7Exception>)exList).AddRange(testStructure(s, @struct, profileID));
							}
						}

					}
					catch (HL7Exception)
					{
						exList.Add(new ProfileNotHL7CompliantException(@struct.Name + " not found in message"));
					}
				}
			}

			// complain about X structures that have content
		   checkForExtraStructures(group, allowedStructures, exList);

			return exList;
		}

		/// <summary>
		/// Checks a group's children against a list of allowed structures for the group (ie those
		/// mentioned in the profile with usage other than X). Returns a list of exceptions representing
		/// structures that appear in the message but are not supposed to.
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		protected internal virtual void checkForExtraStructures(IGroup group, IList<string> allowedStructures, IList<HL7Exception> exList)
		{
			foreach (string childName in group.Names)
			{
				if (!allowedStructures.Contains(childName))
				{
					try
					{
						foreach (IStructure rep in group.GetAll(childName))
						{
							if (!rep.IsEmpty())
							{
								HL7Exception e = new XElementPresentException("The structure " + childName + " appears in the message but not in the profile");
								exList.Add(e);
							}
						}
					}
					catch (HL7Exception he)
					{
						throw new ProfileException("Problem checking profile", he);
					}
				}
			}
		}

		/// <summary>
		/// Checks cardinality and creates an appropriate exception if out of bounds. The usage code is
		/// needed because if min cardinality is > 0, the min # of reps is only required if the usage
		/// code is 'R' (see HL7 v2.5 section 2.12.6.4).
		/// </summary>
		/// <param name="reps"> the number of reps </param>
		/// <param name="min"> the minimum number of reps </param>
		/// <param name="max"> the maximum number of reps (-1 means *) </param>
		/// <param name="usage"> the usage code </param>
		/// <param name="name"> the name of the repeating structure (used in exception msg) </param>
		/// <returns> null if cardinality OK, exception otherwise </returns>
		protected internal virtual HL7Exception testCardinality(int reps, int min, int max, string usage, string name, IList<HL7Exception> exList)
		{
			HL7Exception e = null;
			if (reps < min && usage.Equals("R", StringComparison.CurrentCultureIgnoreCase))
			{
				e = new ProfileNotFollowedException(name + " must have at least " + min + " repetitions (has " + reps + ")");
			}
			else if (max > 0 && reps > max)
			{
				e = new ProfileNotFollowedException(name + " must have no more than " + max + " repetitions (has " + reps + ")");
			}
			if (e != null)
			{
				exList.Add(e);
			}
			return e;
		}

		/// <summary>
		/// Tests a structure (segment or group) against the corresponding part of a profile.
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		public virtual IList<HL7Exception> testStructure(IStructure s, IProfileStructure profile, string profileID)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();
			if (profile is Seg)
			{
				if (s.GetType().IsSubclassOf(typeof(ISegment)))
				{
					((List<HL7Exception>)exList).AddRange(doTestSegment((ISegment) s, (Seg) profile, profileID, validateChildren));
				}
				else
				{
					exList.Add(new ProfileNotHL7CompliantException("Mismatch between a segment in the profile and the structure " + s.GetType().FullName + " in the message"));
				}
			}
			else if (profile is SegGroup)
			{
			    IGroup sGroup = s as IGroup;
			    if (sGroup != null)
				{
					((List<HL7Exception>)exList).AddRange(testGroup(sGroup, (SegGroup) profile, profileID));
				}
				else
				{
					exList.Add(new ProfileNotHL7CompliantException("Mismatch between a group in the profile and the structure " + s.GetType().FullName + " in the message"));
				}
			}
		    return exList;
		}

		/// <summary>
		/// Tests a segment against a segment section of a profile.
		/// </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		public virtual IList<HL7Exception> testSegment(ISegment segment, Seg profile, string profileID)
		{
			return doTestSegment(segment, profile, profileID, true);
		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		protected internal virtual IList<HL7Exception> doTestSegment(ISegment segment, Seg profile, string profileID, bool theValidateChildren)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();
			IList<int?> allowedFields = new List<int?>();

			for (int i = 1; i <= profile.NumFields; i++)
			{
				Field field = profile.GetField(i);

				// only test a field in detail if it isn't X
				if (!field.Usage.Equals("X", StringComparison.CurrentCultureIgnoreCase))
				{
					allowedFields.Add(i);

					// see which instances have content
					try
					{
						IType[] instances = segment.GetField(i);
						IList<IType> instancesWithContent = instances.Where(instance => !instance.IsEmpty()).ToList();

					    HL7Exception ce = testCardinality(instancesWithContent.Count, field.Min, field.Max, field.Usage, field.Name, exList);
						if (ce != null)
						{
							ce.FieldPosition = i;
						}

						// test field instances with content
						if (theValidateChildren)
						{
							foreach (IType s in instancesWithContent)
							{
								// escape field value when checking length
								bool escape = !(profile.Name.Equals("MSH", StringComparison.CurrentCultureIgnoreCase) && i < 3);
								IList<HL7Exception> childExceptions = doTestField(s, field, escape, profileID, validateChildren);
								foreach (HL7Exception ex in childExceptions)
								{
									ex.FieldPosition = i;
								}
								((List<HL7Exception>)exList).AddRange(childExceptions);
							}
						}

					}
					catch (HL7Exception)
					{
						exList.Add(new ProfileNotHL7CompliantException("Field " + i + " not found in message"));
					}
				}

			}

			// complain about X fields with content
			checkForExtraFields(segment, allowedFields, exList);

			foreach (HL7Exception ex in exList)
			{
				ex.SegmentName = profile.Name;
			}
			return exList;
		}

		/// <summary>
		/// Checks a segment against a list of allowed fields (ie those mentioned in the profile with
		/// usage other than X). Returns a list of exceptions representing field that appear but are not
		/// supposed to.
		/// </summary>
		/// <param name="allowedFields"> an array of Integers containing field #s of allowed fields </param>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		protected internal virtual void checkForExtraFields(ISegment segment, IList<int?> allowedFields, IList<HL7Exception> exList)
		{
			for (int i = 1; i <= segment.NumFields(); i++)
			{
				if (!allowedFields.Contains(i))
				{
					try
					{
						IType[] reps = segment.GetField(i);
						foreach (IType rep in reps)
						{
							if (!rep.IsEmpty())
							{
								HL7Exception e = new XElementPresentException("Field " + i + " in " + segment.GetStructureName() + " appears in the message but not in the profile");
								exList.Add(e);
							}
						}
					}
					catch (HL7Exception he)
					{
						throw new ProfileException("Problem testing against profile", he);
					}
				}
			}
		}

		/// <summary>
		/// Tests a Type against the corresponding section of a profile.
		/// </summary>
		/// <param name="encoded"> optional encoded form of type (if you want to specify this -- if null, default
		///            pipe-encoded form is used to check length and constant val) </param>
		public virtual IList<HL7Exception> testType(IType type, AbstractComponent profile, string encoded, string profileID)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();
			if (string.ReferenceEquals(encoded, null))
			{
				encoded = PipeParser.Encode(type, this.enc);
			}

			testUsage(encoded, profile.Usage, profile.Name, exList);

			if (!profile.Usage.Equals("X"))
			{
				checkDataType(profile.Datatype, type, exList);
				checkLength(profile.Length, profile.Name, encoded, exList);
				checkConstantValue(profile.ConstantValue, encoded, exList);

				testTypeAgainstTable(type, profile, profileID, exList);
			}

			return exList;
		}

		protected internal virtual void checkConstantValue(string value, string encoded, IList<HL7Exception> exList)
		{
			// check constant value
			if (!string.ReferenceEquals(value, null) && value.Length > 0)
			{
				if (!encoded.Equals(value))
				{
					exList.Add(new ProfileNotFollowedException("'" + encoded + "' doesn't equal constant value of '" + value + "'"));
				}
			}
		}

		protected internal virtual void checkLength(long length, string name, string encoded, IList<HL7Exception> exList)
		{
			// check length
			if (encoded.Length > length)
			{
				exList.Add(new ProfileNotFollowedException("The type " + name + " has length " + encoded.Length + " which exceeds max of " + length));
			}
		}

		protected internal virtual void checkDataType(string dataType, IType type, IList<HL7Exception> exList)
		{
			// check datatype
			string typeName = type.TypeName;
			if (!(type is Varies || typeName.Equals(dataType)))
			{
				exList.Add(new ProfileNotHL7CompliantException("HL7 datatype " + typeName + " doesn't match profile datatype " + dataType));
			}
		}

		/// <summary>
		/// Tests whether the given type falls within a maximum length.
		/// </summary>
		/// <returns> null of OK, an HL7Exception otherwise </returns>
		public virtual HL7Exception testLength(IType type, int maxLength)
		{
			HL7Exception e = null;
			string encoded = PipeParser.Encode(type, this.enc);
			if (encoded.Length > maxLength)
			{
				e = new ProfileNotFollowedException("Length of " + encoded.Length + " exceeds maximum of " + maxLength);
			}
			return e;
		}

		/// <summary>
		/// Tests an element against the corresponding usage code. The element is required in its encoded
		/// form.
		/// </summary>
		/// <param name="encoded"> the pipe-encoded message element </param>
		/// <param name="usage"> the usage code (e.g. "CE") </param>
		/// <param name="name"> the name of the element (for use in exception messages) </param>
		/// <returns> null if there is no problem, an HL7Exception otherwise </returns>
		protected internal virtual void testUsage(string encoded, string usage, string name, IList<HL7Exception> exList)
		{
			if (usage.Equals("R", StringComparison.CurrentCultureIgnoreCase))
			{
				if (encoded.Length == 0)
				{
					exList.Add(new ProfileNotFollowedException("Required element " + name + " is missing"));
				}
			}
			else if (usage.Equals("RE", StringComparison.CurrentCultureIgnoreCase))
			{
				// can't test anything
			}
			else if (usage.Equals("O", StringComparison.CurrentCultureIgnoreCase))
			{
				// can't test anything
			}
			else if (usage.Equals("C", StringComparison.CurrentCultureIgnoreCase))
			{
				// can't test anything yet -- wait for condition syntax in v2.6
			}
			else if (usage.Equals("CE", StringComparison.CurrentCultureIgnoreCase))
			{
				// can't test anything
			}
			else if (usage.Equals("X", StringComparison.CurrentCultureIgnoreCase))
			{
				if (encoded.Length > 0)
				{
					exList.Add(new XElementPresentException("Element \"" + name + "\" is present but specified as not used (X)"));
				}
			}
			else if (usage.Equals("B", StringComparison.CurrentCultureIgnoreCase))
			{
				// can't test anything
			}
		}

		/// <summary>
		/// Tests table values for ID, IS, and CE types. An empty list is returned for all other types or
		/// if the table name or number is missing.
		/// </summary>
		protected internal virtual void testTypeAgainstTable(IType type, AbstractComponent profile, string profileID, IList<HL7Exception> exList)
		{
			if (!string.ReferenceEquals(profile.Table, null) && (type.TypeName.Equals("IS") || type.TypeName.Equals("ID")))
			{
				string codeSystem = string.Format("HL7{0,4}", profile.Table).Replace(" ", "0");
				string value = ((IPrimitive) type).Value;
				addTableTestResult(profileID, codeSystem, value, exList);
			}
			else if (type.TypeName.Equals("CE"))
			{
				string value = Terser.getPrimitive(type, 1, 1).Value;
				string codeSystem = Terser.getPrimitive(type, 3, 1).Value;
				addTableTestResult(profileID, codeSystem, value, exList);

				value = Terser.getPrimitive(type, 4, 1).Value;
				codeSystem = Terser.getPrimitive(type, 6, 1).Value;
				addTableTestResult(profileID, codeSystem, value, exList);
			}
		}

		protected internal virtual void addTableTestResult(string profileID, string codeSystem, string value, IList<HL7Exception> exList)
		{
			if (!string.ReferenceEquals(codeSystem, null) && !string.ReferenceEquals(value, null) && validateChildren)
			{
				testValueAgainstTable(profileID, codeSystem, value, exList);
			}
		}

		protected internal virtual void testValueAgainstTable(string profileID, string codeSystem, string value, IList<HL7Exception> exList)
		{
			ICodeStore store = codeStore;
			if (codeStore == null)
			{
				store = codeStoreRegistry.GetCodeStore(profileID, codeSystem);
			}

			if (store == null)
			{
				log.Info(string.Format("Not checking value {0}: no code store was found for profile {1} code system {2}", new object[] {value, profileID, codeSystem}));
			}
			else
			{
				if (!store.KnowsCodes(codeSystem))
				{
					log.Warn(string.Format("Not checking value {0}: Don't have a table for code system {1}", value, codeSystem));
				}
				else if (!store.IsValidCode(codeSystem, value))
				{
					exList.Add(new ProfileNotFollowedException("Code '" + value + "' not found in table " + codeSystem + ", profile " + profileID));
				}
			}

		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		public virtual IList<HL7Exception> testField(IType type, Field profile, bool escape, string profileID)
		{
			return doTestField(type, profile, escape, profileID, true);
		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		protected internal virtual IList<HL7Exception> doTestField(IType type, Field profile, bool escape, string profileID, bool theValidateChildren)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();

			// account for MSH 1 & 2 which aren't escaped
			string encoded = null;
			if (!escape && type.GetType().IsSubclassOf(typeof(IPrimitive)))
			{
				encoded = ((IPrimitive) type).Value;
			}

			((List<HL7Exception>)exList).AddRange(testType(type, profile, encoded, profileID));

			// test children
			if (theValidateChildren)
			{
				if (profile.Components > 0 && !profile.Usage.Equals("X"))
				{
					if (type.GetType().IsSubclassOf(typeof(IComposite)))
					{
						IComposite comp = (IComposite) type;
						for (int i = 1; i <= profile.Components; i++)
						{
							Component childProfile = profile.getComponent(i);
							try
							{
								IType child = comp[i - 1];
								((List<HL7Exception>)exList).AddRange(doTestComponent(child, childProfile, profileID, validateChildren));
							}
							catch (DataTypeException de)
							{
								exList.Add(new ProfileNotHL7CompliantException("More components in profile than allowed in message: " + de.Message));
							}
						}
						checkExtraComponents(comp, profile.Components, exList);
					}
					else
					{
						exList.Add(new ProfileNotHL7CompliantException("A field has type primitive " + type.GetType().FullName + " but the profile defines components"));
					}
				}
			}

			return exList;
		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		public virtual IList<HL7Exception> testComponent(IType type, Component profile, string profileID)
		{
			return doTestComponent(type, profile, profileID, true);
		}

        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
        /// <exception cref="NHapi.Base.HL7Exception"></exception>
		protected internal virtual IList<HL7Exception> doTestComponent(IType type, Component profile, string profileID, bool theValidateChildren)
		{
			IList<HL7Exception> exList = new List<HL7Exception>();
			((List<HL7Exception>)exList).AddRange(testType(type, profile, null, profileID));

			// test children
			if (profile.SubComponents > 0 && !profile.Usage.Equals("X") && (!type.IsEmpty()))
			{
				if (type.GetType().IsSubclassOf(typeof(IComposite)))
				{
					IComposite comp = (IComposite) type;

					if (theValidateChildren)
					{
						for (int i = 1; i <= profile.SubComponents; i++)
						{
							SubComponent childProfile = profile.getSubComponent(i);
							try
							{
								IType child = comp[i - 1];
								((List<HL7Exception>)exList).AddRange(testType(child, childProfile, null, profileID));
							}
							catch (DataTypeException de)
							{
								exList.Add(new ProfileNotHL7CompliantException("More subcomponents in profile than allowed in message: " + de.Message));
							}
						}
					}

					checkExtraComponents(comp, profile.SubComponents, exList);
				}
				else
				{
					exList.Add(new ProfileNotFollowedException("A component has primitive type " + type.GetType().FullName + " but the profile defines subcomponents"));
				}
			}

			return exList;
		}

		/// <summary>
		/// Tests for extra components (ie any not defined in the profile) </summary>
        /// <exception cref="NHapi.Base.Conf.ProfileException"></exception>
		protected internal virtual void checkExtraComponents(IComposite comp, int numInProfile, IList<HL7Exception> exList)
		{
			StringBuilder extra = new StringBuilder();
			for (int i = numInProfile; i < comp.Components.Length; i++)
			{
				try
				{
					string s = PipeParser.Encode(comp[i], enc);
					if (s.Length > 0)
					{
						extra.Append(s).Append(enc.ComponentSeparator);
					}
				}
				catch (DataTypeException de)
				{
					throw new ProfileException("Problem testing against profile", de);
				}
			}

			if (extra.Length > 0)
			{
				exList.Add(new XElementPresentException("The following components are not defined in the profile: " + extra.ToString()));
			}

		}

		public static void Main(string[] args)
		{

			if (args.Length != 2)
			{
				Console.WriteLine("Usage: DefaultValidator message_file profile_file");
				Environment.Exit(1);
			}

			DefaultValidator val = new DefaultValidator();
			try
			{
				string msgString = loadFile(args[0]);
				PipeParser parser = new PipeParser();
				IMessage message = parser.Parse(msgString);

				string profileString = loadFile(args[1]);
				ProfileParser profParser = new ProfileParser(true);
				RuntimeProfile profile = profParser.parse(profileString);

				HL7Exception[] exceptions = val.Validate(message, profile.Message);

				Console.WriteLine("Exceptions: ");
				for (int i = 0; i < exceptions.Length; i++)
				{
					Console.WriteLine((i + 1) + ". " + exceptions[i].Message);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e.ToString());
				Console.Write(e.StackTrace);
			}
		}

		/// <summary>
		/// loads file at the given path </summary>
		private static string loadFile(string path)
		{
			// char[] cbuf = new char[(int) file.length()];
			StreamReader @in = new StreamReader(path);
			StringBuilder buf = new StringBuilder(5000);
			int c;
			while ((c = @in.Read()) != -1)
			{
				buf.Append((char) c);
			}
			// in.read(cbuf, 0, (int) file.length());
			@in.Close();
			// return String.valueOf(cbuf);
			return buf.ToString();
		}

	    protected ICodeStoreRegistry codeStoreRegistry;

	}

}