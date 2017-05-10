using System;

namespace PowerBlocks.Configuration
{

#pragma warning disable 1591

	/// <summary>
	/// Summary description for ReadOnlySetting.
	/// </summary>
	public class ReadOnlySetting : ISetting
	{
		private const string SettingReadonlyError = "Setting is read-only";

		private readonly string _setting = string.Empty;

		public ReadOnlySetting(string settingValue)
		{
			_setting = settingValue;
		}
		#region ISetting Members

		public string GetValue()
		{
			return _setting;
		}

		public string GetString()
		{
			return _setting;
		}

		public bool GetBoolean()
		{
			return Convert.ToBoolean(_setting);
		}

		public int GetInt()
		{
			return Convert.ToInt32(_setting);
		}
		public double GetDouble()
		{
			return Convert.ToDouble(_setting);
		}
		public void SetValue(string val)
		{
			throw new SettingsException(SettingReadonlyError);
		}

		void ISetting.SetValue(bool val)
		{
			throw new SettingsException(SettingReadonlyError);
		}

		void ISetting.SetValue(int val)
		{
			throw new SettingsException(SettingReadonlyError);
		}

		#endregion
	}

#pragma warning restore 1591
}
