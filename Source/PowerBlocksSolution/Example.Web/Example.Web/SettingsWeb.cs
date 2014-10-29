using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PowerBlocks.Configuration;

namespace Example.Web
{
    // do not require comments on the settings, they are self documenting
#pragma warning disable 1591

    /// <summary>
    /// Settings for the Web project
    /// </summary>
    public static class SettingsWeb
    {
        private const bool Optional = true;


        // General settings
        public static readonly ISetting EnvironmentMode = new AppConfigSetting("EnvironmentMode", Optional, "DEV");
        public static readonly ISetting ServiceUrl1 = new AppConfigSetting("ServiceUrl1");
        public static readonly ISetting ServiceUrl2 = new AppConfigSetting("ServiceUrl2");
        public static readonly ISetting ServiceUrl3 = new AppConfigSetting("ServiceUrl3");


        // Database stuff
        public static readonly ISetting DbTimeout = new AppConfigSetting("DBTimeout");
        public static readonly ISetting FullTraceEnabled = new AppConfigSetting("FullTraceEnabled", Optional, "0");

        // Static Read only settings
        public static readonly ISetting MyStaticSetting = new ReadOnlySetting("MyStaticSetting");



    }
#pragma warning restore 1591

}