using PowerBlocks.Encryption;

namespace PowerBlocks.Configuration
{
	/// <summary>
	/// Pulls a encrypted setting
	/// </summary>
	public class SecureAppConfigSetting : AppConfigSetting
	{
		
		/// <summary>
		/// Pulls a encrypted setting
		/// </summary>
		/// <param name="key"></param>
		public SecureAppConfigSetting(string key) : base(key) {}

		/// <summary>
		/// Pulls a value from configuration file and decrypts it.
		/// </summary>
		/// <returns></returns>
		public override string GetValue()
		{
			string encryptedValue = base.GetValue();
			string decryptedValue = DecryptValue(encryptedValue);
			return decryptedValue;
		}

		/// <summary>
		/// Decrypts string and return unencrypted string
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private string DecryptValue(string value)
		{
			// DeCrypt It
			Encryptor encryptor = new Encryptor();
			string decrypted = encryptor.DecryptString(value);
			return decrypted;
		}
	}
}
