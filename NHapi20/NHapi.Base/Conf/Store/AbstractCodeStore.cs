using System;
using NHapi.Base.Conf.Parser;
using NHapi.Base.Log;

namespace NHapi.Base.Conf.Store
{

	/// <summary>
	/// Abstract class for used retrieving and validating codes from user defined and HL7 specific tables
	/// that correspond to a conformance profile.
	/// 
	/// @author Neal Acharya
	/// </summary>
	public abstract class AbstractCodeStore : CodeStore
	{
		public abstract bool knowsCodes(string codeSystem);
		public abstract string[] getValidCodes(string codeSystem);

        private static readonly ILog log = HapiLogFactory.GetHapiLog(typeof(ProfileParser));

		private static readonly RegisteredPattern[] WILDCARDS = new RegisteredPattern[]
		{
			new RegisteredPattern("ISOnnnn", "ISO\\d\\d\\d\\d"),
			new RegisteredPattern("HL7nnnn", "HL7\\d\\d\\d\\d"),
			new RegisteredPattern("99zzz", "99[\\w]*"),
			new RegisteredPattern("NNxxx", "99[\\w]*")
		};

		/// <seealso cref= ca.uhn.hl7v2.conf.store.CodeStore#isValidCode(java.lang.String, java.lang.String) </seealso>
		public virtual bool isValidCode(string codeSystem, string code)
		{
			try
			{
				foreach (string validCode in getValidCodes(codeSystem))
				{
					if (checkCode(code, validCode))
					{
						return true;
					}
				}
				return false;
			}
			catch (Exception e)
			{
				log.Error("Error checking code " + code + " in code system " + codeSystem, e);
				return false;
			}
		}

		/// <summary>
		/// Checks a code for an exact match, and using certain sequences where some characters are
		/// wildcards (e.g. HL7nnnn). If the pattern contains one of " or ", " OR ", or "," each operand
		/// is checked.
		/// </summary>
		private bool checkCode(string code, string pattern)
		{
			// mod by Neal acharya - Do full match on with the pattern. If code matches pattern then
			// return true
			// else parse pattern to look for wildcard characters
			if (code.Equals(pattern))
			{
				return true;
			}

			if (pattern.IndexOf(' ') >= 0 || pattern.IndexOf(',') >= 0)
			{
                SupportClass.Tokenizer tok = new SupportClass.Tokenizer(pattern, ", ", false);
				while (tok.HasMoreTokens())
				{
					string t = tok.NextToken();
					if (!t.Equals("or", StringComparison.CurrentCultureIgnoreCase) && checkCode(code, t))
					{
						return true;
					}
				}
			}
			else
			{
				foreach (RegisteredPattern wildcard in WILDCARDS)
				{
					if (wildcard.matches(pattern, code))
					{
						return true;
					}
				}
			}

			return false;
		}

	}

}