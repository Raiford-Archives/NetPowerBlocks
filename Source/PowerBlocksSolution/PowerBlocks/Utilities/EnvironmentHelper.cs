using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Web;
using System.Security.Policy;

namespace PowerBlocks.Utilities
{
	public class EnvironmentHelper
	{

		public static string ExecutingFolderPath
		{
			get
			{
				return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			}
		}


		/// <summary>
		/// Returns the physical application root for a web application or the executing folder for a windows app. 
		/// 
		/// </summary>
		public static string ApplicationBaseFolder
		{
			get
			{	
				// This needs testing
				string path = System.AppDomain.CurrentDomain.BaseDirectory;
				return path;
			}
		}
	}
}
