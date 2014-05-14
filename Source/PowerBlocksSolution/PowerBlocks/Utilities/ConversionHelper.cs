using System;

namespace PowerBlocks.Utilities
{
	/// <summary>
	/// General conversion helper to convert between various types.
	/// </summary>
	public static class ConversionHelper
	{
		/// <summary>
		/// Converts a string from many formats to a bool. For example you can pass in "true", "false"
		/// "1", "0"
		/// </summary>
		/// <param name="stringValue">The string to convert</param>
		public static bool StringToBool(string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				return false;
			}

			string val = stringValue.Trim().ToLower();

			if ((val.Equals("0")) ||
				(val.Equals("false")) ||
				(val.Equals("no")) ||
				(val.Equals("f")))
			{
				return false;
			}

			if ((val.Equals("1")) ||
				(val.Equals("true")) ||
				(val.Equals("yes")) ||
				(val.Equals("t")))
			{
				return true;
			}
			return false;
		}

		/// <summary>
		/// Converts a string to a guid value
		/// </summary>
		/// <param name="stringValue"></param>
		/// <returns></returns>
		public static Guid StringToGuid(string stringValue)
		{
			if (string.IsNullOrEmpty(stringValue))
			{
				return default(Guid);
			}

			Guid guid = new Guid(stringValue);

			return guid;
		}

	}
}
