using System;

namespace PowerBlocks.Configuration
{

#pragma warning disable 1591

	/// <summary>
	/// ISetting returns a strongly typed setting
	/// </summary>
	public interface ISetting
	{
		string GetValue();
		string GetString();
		bool GetBoolean();
		int GetInt();
		double GetDouble();
		void SetValue(string stringValue);
		void SetValue(bool boolValue);
		void SetValue(int intValue);

		string ToString();


	}

	[Serializable]
	public class SettingsException : Exception
	{		public SettingsException(string message, Exception innerException) : base(message, innerException) { }
		public SettingsException(string message) : base(message) { }
	}

	[Serializable]
	public class SettingNotFoundException : SettingsException
	{
		public string KeyName { get; set; }

		public SettingNotFoundException(string keyName) : this(keyName, string.Empty, null) { }
		public SettingNotFoundException(string keyName, string message) : this(keyName, message, null) { }
		public SettingNotFoundException
		(
			string keyName, 
			string message, 
			Exception innerException)
			: base("Setting '" + keyName + "' Not Found in the configuration source. " + message, innerException) 
		{
			KeyName = keyName;			
		}
	}

		#pragma warning restore 1591
}
