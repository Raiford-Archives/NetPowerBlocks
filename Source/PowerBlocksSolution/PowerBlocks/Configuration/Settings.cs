using PowerBlocks.Configuration;

namespace PowerBlocks.Configuration
{

	// do not require comments on the settings, they are self documenting
	#pragma warning disable 1591
		
	/// <summary>
	/// Contains the Common settings used by the Common components. These settings could come from the app confi or other settings
	///  sources. Independant of the physical location of the setting they can be universally retrieved from this class.
	/// </summary>
	public static class Settings
	{
		
		////////////////////////////////////////////////////////////////////////////////////
		// Idea - Write a VerifySettingsConfigured() method that uses reflection that will
		//		  dynamically iterate all the settings on this class and ensure that they can 
		//		  be retrieved. if they cant instead of throwing error on each one return a list
		//		  of settings that need to be set so they can easily be configured in the config file
		////////////////////////////////////////////////////////////////////////////////////
		
		private const bool Optional = true;

		
		// General settings
		




		// Data Cache / Results Settings
		public static readonly ISetting DataCacheEnable = new AppConfigSetting("DataCacheEnable");
		public static readonly ISetting DataCacheExpirationSeveralDays = new AppConfigSetting("DataCacheExpirationSeveralDays", Optional, "20000");
		public static readonly ISetting DataCacheExpirationDaily = new AppConfigSetting("DataCacheExpirationDaily", Optional, "20000");
		public static readonly ISetting DataCacheExpirationSeveralHours = new AppConfigSetting("DataCacheExpirationSeveralHours", Optional, "20000");
		public static readonly ISetting DataCacheExpirationHourly = new AppConfigSetting("DataCacheExpirationHourly", Optional, "20000");
		public static readonly ISetting DataCacheExpirationSeveralMinutes = new AppConfigSetting("DataCacheExpirationSeveralMinutes", Optional, "10000");
		public static readonly ISetting DataCacheExpirationWithinMinute = new AppConfigSetting("DataCacheExpirationWithinMinute", Optional, "10000");
		public static readonly ISetting DataCacheExpirationNearRealTime = new AppConfigSetting("DataCacheExpirationNearRealTime", Optional, "1000");
		public static readonly ISetting DataCacheExpirationRealTime = new AppConfigSetting("DataCacheExpirationRealTime", Optional, "500");

		
		// Email Server / Smtp settings
		public static readonly ISetting SmtpEnableEmailSend = new AppConfigSetting("SmtpEnableEmailSend", Optional, "false");
		public static readonly ISetting SmtpServer = new AppConfigSetting("SmtpServer");
		public static readonly ISetting SmtpServerPort = new AppConfigSetting("SmtpServerPort", Optional, "25");
		public static readonly ISetting SmtpServerRequireLogin = new AppConfigSetting("SmtpServerRequireLogin");
		public static readonly ISetting SmtpServerEnableSsl = new AppConfigSetting("SmtpServerEnableSsl");
		public static readonly ISetting SmtpServerUserName = new AppConfigSetting("SmtpServerUserName");
		public static readonly ISetting SmtpServerPassword = new AppConfigSetting("SmtpServerPassword");
		public static readonly ISetting SmtpServerFromEmailAddress = new AppConfigSetting("SmtpServerFromEmailAddress");

		// Email Addresses and Settings
		public static readonly ISetting EmailAddressAdmin = new AppConfigSetting("EmailAddressAdmin");
		public static readonly ISetting EmailAddressDevelopmentTest = new AppConfigSetting("EmailAddressDevelopmentTest");
		public static readonly ISetting EmailAddressAdminFrom = new AppConfigSetting("EmailAddressAdminFrom");
		public static readonly ISetting EmailAddressDebugNotifications = new AppConfigSetting("EmailAddressDebugNotifications");
		public static readonly ISetting EmailAddressUserFrom = new AppConfigSetting("EmailAddressUserFrom");
		public static readonly ISetting EmailAddressRegistrationConfirmationFrom = new AppConfigSetting("EmailAddressRegistrationConfirmationFrom");
		public static readonly ISetting EmailAddressRegistrationConfirmationFromDisplayName = new AppConfigSetting("EmailAddressRegistrationConfirmationFromDisplayName");
		public static readonly ISetting EmailAddressResetPasswordRequestFrom = new AppConfigSetting("EmailAddressResetPasswordRequestFrom");
		public static readonly ISetting EmailAddressResetPasswordRequestFromDisplayName = new AppConfigSetting("EmailAddressResetPasswordRequestFromDisplayName");
		public static readonly ISetting EmailAddressesContactFormMessageTo = new AppConfigSetting("EmailAddressesContactFormMessageTo");
		public static readonly ISetting EmailAddressContactFormMessageFrom = new AppConfigSetting("EmailAddressContactFormMessageFrom");
		
		// Database related settings
		public static readonly ISetting DatabaseConnectionStringPureClassifieds = new AppConfigSetting("DatabaseConnectionStringPureClassifieds");
		public static readonly ISetting DatabaseExtendedLogging = new AppConfigSetting("DatabaseExtendedLogging");
		public static readonly ISetting DatabaseCommandTimeout = new AppConfigSetting("DatabaseCommandTimeout");
		
		// Account Registration Settings
		public static readonly ISetting AccountRegistrationPasswordRetrievalExpireInDays = new AppConfigSetting("AccountRegistrationPasswordRetrievalExpireInDays");
		public static readonly ISetting AccountRegistrationVerificationUrlTemplate = new AppConfigSetting("AccountRegistrationVerificationUrlTemplate");
			
		/// Web Server Settings
		public static readonly ISetting UrlImages = new AppConfigSetting("UrlImages");
		public static readonly ISetting UrlMemberImages = new AppConfigSetting("UrlMemberImages");

	











		// Static Read only settings
		public static readonly ISetting MyStaticSetting = new ReadOnlySetting(@"MyStaticSetting");



	}
	#pragma warning restore 1591
}
