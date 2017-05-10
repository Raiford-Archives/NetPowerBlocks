using System;
using System.Text;

namespace PowerBlocks.Utilities
{

	/// <summary>
	/// Provides various data generation methods.
	/// </summary>
	public static class RandomDataGenerator
	{
		
		/// <summary>
		/// Returns an alphabetic string of the random characters of the specified length. This can be useful
		/// to generate security related data such as hash keys, passwords etc.
		/// </summary>
		/// <param name="lengthOfString"></param>
		/// <returns></returns>
		public static string GetAlphabeticString(int lengthOfString)
		{
			StringBuilder builder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < lengthOfString; i++)
			{
				int randomNumber = Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65));
				char ch = Convert.ToChar(randomNumber);
				builder.Append(ch);
			}
			return builder.ToString();
		}

		/// <summary>
		/// Gets a Numeric String of a given length
		/// </summary>
		/// <param name="lengthOfString"></param>
		/// <returns></returns>
		public static string GetNumericString(int lengthOfString)
		{
			StringBuilder builder = new StringBuilder();
			Random random = new Random();
			for (int i = 0; i < lengthOfString; i++)
			{
				int randomNumber = Convert.ToInt32(Math.Floor(10 * random.NextDouble() + 48));
				char ch = Convert.ToChar(randomNumber);
				builder.Append(ch);
			}
			return builder.ToString();
		}
	}
}
