using System;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace PowerBlocks.Configuration
{
#pragma warning disable 1591

	/// <summary>
	/// Summary description for AppConfigSetting.
	/// </summary>
	public class AppConfigSetting : ISetting
	{
		private readonly string _key = string.Empty;
				
		private readonly bool _optional = false;
		private readonly string _defaultValue;

		/// <summary>
		/// Use this constructor if the setting is optional. If the setting
		/// is not optional an error will be thrown.
		/// </summary>
		/// <param name="settingKey"></param>
		/// <param name="optional"></param>
		public AppConfigSetting(string settingKey, bool optional = false)
		{
			_key = settingKey;
			_optional = optional;
		}

		/// <summary>
		/// Use this constructor if you wish to specify a default value if the setting is not available.
		/// </summary>
		/// <param name="settingKey"></param>
		/// <param name="optional"></param>
		/// <param name="defaultValue"></param>
		public AppConfigSetting(string settingKey, bool optional, string defaultValue)
		{
			_key = settingKey;
			_optional = optional;
			_defaultValue = defaultValue;
		}

		protected string Key
		{
			get { return _key; }
		} 

		#region ISetting Members

		
		public string GetString()
		{
			return GetValue();
		}

		public bool GetBoolean()
		{
			string s = GetValue();
			return ConversionHelper.StringToBool(s);
		}
		public int GetInt()
		{
			string s = GetValue();
			return Convert.ToInt32(s);
		}
		public double GetDouble()
		{
			string s = GetValue();
			return Convert.ToDouble(s);
		}

		public void SetValue(string value)
		{
			Set(value);
		}

		public void SetValue(bool value)
		{
			Set(Convert.ToString(value));
		}

		public void SetValue(int value)
		{
			Set(Convert.ToString(value));
		}

		new public string ToString()
		{
			string s = GetValue();
			return Convert.ToString(s);
		}
		#endregion


		#region protected helpers
		/// <summary>
		/// This is the main Get() that actually pulls the value from the config, all the other methods will use this.
		/// </summary>
		/// <returns></returns>
		public virtual string GetValue()
		{
			string setting = ConfigurationManager.AppSettings[_key];

			if (string.IsNullOrEmpty(setting))
			{
				if (!_optional)
				{
					string m = string.Format("Check your Application Configuration file to ensure the value is present");
					throw new SettingNotFoundException(_key, m);
				}
				
				// Its optional so use the default Value 
				setting = _defaultValue;
			}			
			return setting;
		}
		[SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "settingsValue", Justification="Required for interface, but not implemented in this provider")]
		protected void Set(string settingsValue)
		{
			throw new NotImplementedException();
		}
		#endregion

	}

#pragma warning restore 1591
}
