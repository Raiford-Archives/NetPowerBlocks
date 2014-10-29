using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerBlocks.Configuration;

namespace Example.Web.Areas.Configuration.Controllers
{
    public class ConfigurationController : Controller
    {
        //
        // GET: /Configuration/Configuration/

        public ActionResult Index()
        {
            
            // Pull Basic Settings from Source
            string environmentMode = SettingsWeb.EnvironmentMode.GetString();
            
            string serviceUrl1 = SettingsWeb.ServiceUrl1.GetString();
            string serviceUrl2 = SettingsWeb.ServiceUrl2.GetString();
            string serviceUrl3 = SettingsWeb.ServiceUrl3.GetString();

            // Now pull an int
            int timeoutDb = SettingsWeb.DbTimeout.GetInt();

            
            // Now pull an optional setting and automatically convert it do a Bool
            bool enableTrace = SettingsWeb.FullTraceEnabled.GetBoolean();


            // You can also pull settings from the Common settings in a dependent project
            string email = Settings.EmailAddressAdmin.GetString();
            string urlImages = Settings.UrlImages.GetString();


            return View();
        }

    }
}
