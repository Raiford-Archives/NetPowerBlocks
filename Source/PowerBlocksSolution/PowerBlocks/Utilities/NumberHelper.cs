using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerBlocks.Utilities
{
	public static class NumberHelper
	{
		public static int GenerateRandomInt(int min, int max)
		{
			Random random = new Random();
			return random.Next(min, max);
		}
	}
}
