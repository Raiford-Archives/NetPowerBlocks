using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PowerBlocks.Configuration;

namespace PowerBlocks.Caching
{
	/// <summary>
	/// Used in combination with creating caches. This class is configurable and the actual values can be configured in the app config file
	/// </summary>
	public static class CacheExpiration
	{
		public static readonly int SeveralDays = Settings.DataCacheExpirationSeveralDays.GetInt();

		public static readonly int Daily = Settings.DataCacheExpirationDaily.GetInt();

		public static readonly int SeveralHours = Settings.DataCacheExpirationSeveralHours.GetInt();

		public static readonly int Hourly = Settings.DataCacheExpirationHourly.GetInt();

		public static readonly int SeveralMinutes = Settings.DataCacheExpirationSeveralMinutes.GetInt();

		public static readonly int WithinMinute = Settings.DataCacheExpirationWithinMinute.GetInt();

		public static readonly int NearRealTime = Settings.DataCacheExpirationNearRealTime.GetInt();

		/// <summary>
		/// Allows for realtime caching of data for several milliseconds. This can be handy for a high traffic site
		/// where even a 100ms cache can save hitting the database.
		/// </summary>
		public static readonly int RealTime = Settings.DataCacheExpirationRealTime.GetInt();

		
	}
}
