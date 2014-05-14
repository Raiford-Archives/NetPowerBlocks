using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;



namespace PowerBlocks.Utilities
{

	/// <summary>
	/// Summary description for StringHelper.
	/// </summary>
	public static class StringHelper
	{
		
		/// <summary>
		/// An array of non printable characters
		/// </summary>
		public static readonly char[] NonPrintableCharactersArray;
		
		
		/// <summary>
		/// An Array of NonPrintable characters including the space character
		/// </summary>
		public static readonly char[] NonPrintableCharArrayIncludingSpace;

		static StringHelper()
		{
			// Fill Array
			List<char> charArray = new List<char>(33);
			for (int i = 0; i <= 32; i++)
			{
				charArray.Add(Convert.ToChar(i));
			}
			NonPrintableCharArrayIncludingSpace = charArray.ToArray();


			// Fill List
			List<char> charArray2 = new List<char>(32);
			for (int i = 0; i <= 31; i++)
			{
				charArray2.Add(Convert.ToChar(i));
			}
			NonPrintableCharactersArray = charArray2.ToArray();

			
		}
				
		/// <summary>
		/// Returns the string, but if it is null value it will return 
		/// an empty string vs null
		/// </summary>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static string ToSafeString(string stringValue)
		{
			if (!string.IsNullOrWhiteSpace(stringValue))
			{
				return stringValue;
			}
			return "";
		}

		/// <summary>
		/// Converts an object to a string via the ToString() method. If it is empty returns
		/// an empty string vs a null string.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public static string ToSafeString(object obj)
		{
			string stringValue;
			try
			{
				stringValue = Convert.ToString(obj);
			}
			catch
			{
				stringValue = "";
			}
			return stringValue;
		}

		/// <summary>
		/// Determines if the string contains all numeric values
		/// </summary>
		/// <param name="anyString"></param>
		/// <returns></returns>
		public static bool IsNumeric(string anyString)
		{
			////////////////////////////////////////
			// NOTE: this checks if a string can covnert to a number.
			//		 if you need to know if it is all numeric chars then
			//		 you will need to employ ascii code range determination.

			if (anyString == null)
			{
				return false;
			}

			if (anyString.Length > 0)
			{
				double dummyOut;
				System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo("en-US", true);
				bool retval = Double.TryParse(anyString, System.Globalization.NumberStyles.Any, cultureInfo.NumberFormat, out dummyOut);
				return retval;
			}
			
			return false;
		}

		/// <summary>
		/// Determines whether the string is alphabetic
		/// </summary>
		/// <param name="stringValue">The s.</param>
		/// <returns>
		/// 	<c>true</c> if the specified s is alphabetic; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsAlphabetic(string stringValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue)) return false;

			Regex regEx = new Regex("[A-Z, a-z]");
			bool result = regEx.IsMatch(stringValue);
			return result;
		}

		/// <summary>
		/// Determines whether the string is a single alphabet character such as A, B, C, a, b, c etc.
		/// </summary>
		/// <param name="stringValue">The s.</param>
		/// <returns>
		/// 	<c>true</c> if [is single alphabet character] [the specified s]; otherwise, <c>false</c>.
		/// </returns>
		public static bool IsSingleAlphabetCharacter(string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue)) return false;

			if (stringValue.Length > 1) return false;

			Regex regEx = new Regex("[A-Z, a-z]");
			bool result = regEx.IsMatch(stringValue);
			return result;
		}

		/// <summary>
		/// Compares 2 strings that can be null, empty, white space etc. and makes the assumption that if they do not contain
		/// a value then they are equal. For example, if you get a "null" value back from Linq, and compare it to an "Empty" string
		/// then they are considered comparable. This is very handy when you do not really care if its null you just need to know if the strings are 
		/// the same.
		/// 
		/// You may pass in the caseSensitive flag if you want to ensure the case, but the default is case insensitive.
		/// </summary>
		/// <param name="string1"></param>
		/// <param name="string2"></param>
		/// <param name="caseSensitive"></param>
		/// <returns></returns>
		public static bool IsComparable(string string1, string string2, bool caseSensitive=false)
		{
			// Format out any nulls or white space
			string value1 = ToSafeString(string1).Trim();
			string value2 = ToSafeString(string2).Trim();

			if(!caseSensitive)
			{
				value1 = value1.ToUpper();
				value2 = value2.ToUpper();
			}
			return value1.Equals(value2);
		}
       

		/// <summary>
		/// Splits a string on a Delimeter and if the delimiter is not found will return a single
		/// element array with the string. This is useful where you have a delimited list, with the possibility
		/// of a single item with no delimeter
		/// </summary>
		/// <param name="stringValue"></param>
		/// <param name="delimeterChar"></param>
		/// <returns></returns>
		public static string[] SplitOrSingleItem(string stringValue, char delimeterChar)
		{
			string[] strings = {}; // Create empty list
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return strings;
			}

			// ReSharper disable ConvertIfStatementToConditionalTernaryExpression
			if (!stringValue.Contains(delimeterChar.ToString()))
			{
				strings = new List<string>{stringValue}.ToArray();
			}
			else
			{
				// You have a delimeter so split it
				strings = stringValue.Split(delimeterChar);				
			}
			// ReSharper restore ConvertIfStatementToConditionalTernaryExpression
			
			return strings;
		}

		/// <summary>
		/// Returns a string within the specified length. Any characters beyond the length are stripped away.
		/// if the string is shorter than the length argument then the orginal string will be returned.
		/// 
		/// IMPORTANT: the length parameter is NOT the o-based index but the character count.
		/// </summary>
		/// <param name="length"></param>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static string TrimLength(int length, string stringValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return stringValue;
			}

			return stringValue.Length <= length ? stringValue : stringValue.Substring(0, length);
		}

		/// <summary>
		/// Works as the TrimLength() method but will append a  on the end indicating it was trimed
		///
		/// IMPORTANT: the length parameter is NOT the o-based index but the character count.
		/// </summary>
		/// <param name="length"></param>
		/// <param name="stringValue"></param>
		/// <param name="stringToAppend"></param>
		/// <returns></returns>
		public static string TrimLengthAppendString(int length, string stringValue, string stringToAppend)
		{
			if (string.IsNullOrWhiteSpace(stringValue)) return stringValue;

			return stringValue.Length <= length ? stringValue : stringValue.Substring(0, length) + stringToAppend;
		}

		/// <summary>
		/// Converts a string to Uppercase safely. If you pass in Null string it will pass it back.
		/// Use this when the string you need to convert could be a null string, for example when returning
		/// from the database using Linq or MVC controller etc.
		/// </summary>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static string SafeToUpper(string stringValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return stringValue;
			}
			return stringValue.ToUpper();			
		}

		/// <summary>
		/// Trims a string safely. If you pass in Null string it will pass it back.
		/// Use this when the string you need to convert could be a null string, for example when returning
		/// from the database using Linq or MVC controller etc.
		/// </summary>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static string SafeTrim(string stringValue)
		{
			if(string.IsNullOrEmpty(stringValue))
			{
				return stringValue;
			}
			return stringValue.Trim();
		}


		/// <summary>
		/// Safely returns a bool indicating if a searchPhrase is contained in a string. 
		/// If you pass in null strings then you can expect a false return value.
		/// 
		/// You have the option make the search case sensitive by passing in true. The default is NOT
		/// case sensitive.
		/// </summary>
		/// <param name="searchPhrase"></param>
		/// <param name="stringToSearch"></param>
		/// <param name="caseSensitive"> </param>
		/// <returns></returns>
		public static bool SafeContains(string searchPhrase, string stringToSearch, bool caseSensitive = false)
		{
			// Ensure you have valid input and return false if you dont
			if (string.IsNullOrWhiteSpace(searchPhrase)) return false;
			if (string.IsNullOrWhiteSpace(stringToSearch)) return false;

			if(!caseSensitive)
			{
				searchPhrase = searchPhrase.ToUpper();
				stringToSearch = stringToSearch.ToUpper();
			}

			return stringToSearch.Contains(searchPhrase);
		}

		/// <summary>
		/// Removes any non printable characters and Extended characters. This is useful if you are getting input
		/// from unknown sources espeacially UNIX and various other systems. Note this will also filter out any 
		/// CRLF as well.
		///
		/// NOTE: This will NOT remove white spaces
		/// </summary>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static string CleanString(string stringValue)
		{
			// Clean all non printable characters
			string result = Regex.Replace(stringValue, "[\u0000-\u001F]", "");
			
			// Clean all extended ASCII codes
			result = Regex.Replace(stringValue, "[\u0080-\u0255]", "");

			return result;
		}

		/// <summary>
		/// Trims a string and then emoves any non printable characters and Extended characters. This is useful if you are getting input
		/// from unknown sources espeacially UNIX and various other systems. Note this will also filter out any 
		/// CRLF as well.
		/// 
		/// NOTE: This will NOT remove white spaces from the inner string. Although it will trim them
		/// </summary>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static string TrimClean(string stringValue)
		{
			if (string.IsNullOrWhiteSpace(stringValue))
			{
				return stringValue;
			}

			string result = stringValue.Trim(NonPrintableCharArrayIncludingSpace);
			
			// Some other options decided against this approach but leave it here for ideas
			//string result = Regex.Replace(stringVal, "[\u0000-\u001F]", "");
			//result = Regex.Replace(stringVal, "[\u0080-\u0255]", "");

			return result;
		}


		
	}
}