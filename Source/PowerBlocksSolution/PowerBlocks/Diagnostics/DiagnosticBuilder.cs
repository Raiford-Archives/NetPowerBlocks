using PowerBlocks.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

// Uncomment if you want to include extended diags
//using System.ServiceProcess;
//using Microsoft.SqlServer;
//using Microsoft.SqlServer.Management.Common;
//using Microsoft.SqlServer.Management.Smo;
//using Microsoft.SqlServer.Server;

namespace PowerBlocks.Diagnostics
{
	/// <summary>
	/// Build a detailed diagnostic report.
	/// </summary>
	public class DiagnosticsBuilder
	{
		private StringBuilder _report = new StringBuilder();
		
		/// <summary>
		/// Constructs a diagnostics object
		/// </summary>
		/// <param name="reportName"></param>
		public DiagnosticsBuilder(string reportName)
		{
			AddReportHeader(reportName);
		}


		#region Private Methods
		/// <summary>
		/// Adds an object to the report. It will print a name value pair of the properties
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="sectionHeader"></param>
		private void AddObjectPropertyValues(object obj)
		{
			IDictionary<string, object> values = GetObjectPropertyValues(obj);
			AddNameValueCollection(values);
		}


		/// <summary>
		/// Adds a IDictionary of name / values to the report.
		/// </summary>
		/// <param name="valueCollection"></param>
		private void AddNameValueCollection(IDictionary<string, object> valueCollection)
		{
			Debug.Assert(valueCollection != null);


			//IOrderedEnumerable<string, object> ordered = valueCollection.OrderBy(k => k.Key);

			SortedDictionary<string, object> sortedCollection = new SortedDictionary<string, object>(valueCollection);

			foreach (KeyValuePair<string, object> pair in sortedCollection)
			{
				_report.AppendFormat("{0}={1}{2}", pair.Key, pair.Value, Environment.NewLine);
			}
		}

		/// <summary>
		/// Adds a name / values pair to the report.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="value"></param>
		private void AddNameValuePair(string name, object value)
		{
			_report.AppendFormat("{0}={1}{2}", name, value, Environment.NewLine);
		}

		/// <summary>
		/// Gets a dictionary of an objects name / values. This must be an instance of an object.
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		private IDictionary<string, object> GetObjectPropertyValues(object obj)
		{
			Debug.Assert(obj != null);
			Dictionary<string, object> values = new Dictionary<string, object>();

			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				string name = descriptor.Name;
				object value;

				try
				{
					value = descriptor.GetValue(obj);

				}
				catch (Exception e)
				{
					value = "N/A - " + e.Message;
				}
				values.Add(name, value);
			}
			return values;
		}

		/// <summary>
		/// Gets a dictionary of A static objects name / values. This must be the type of a static object.
		/// </summary>
		/// <param name="staticObjectType"></param>
		/// <returns></returns>
		private IDictionary<string, object> GetStaticObjectPropertyValues(Type staticObjectType)
		{
			Dictionary<string, object> values = new Dictionary<string, object>();

			// get all public static properties of MyClass type
			PropertyInfo[] propertyInfos;
			propertyInfos = staticObjectType.GetProperties(BindingFlags.Public | BindingFlags.Static);

			// write property names
			foreach (PropertyInfo propertyInfo in propertyInfos)
			{
				string name = propertyInfo.Name;
				object value;

				try
				{
					value = propertyInfo.GetValue(null, null);

				}
				catch (Exception e)
				{
					value = "N/A - " + e.Message;
				}

				values.Add(name, value);
			}
			return values;
		}
		#endregion


		#region Protected Methods

		/// <summary>
		/// Adds a report header
		/// </summary>
		/// <param name="headerText"></param>
		protected void AddReportHeader(string headerText)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append("======================================================================================" + Environment.NewLine);
			builder.AppendFormat("   {0} generated on {1}" + Environment.NewLine, headerText, DateTime.Now.ToString());
			builder.Append("======================================================================================" + Environment.NewLine);
			builder.Append(Environment.NewLine);
	
			_report.Append(builder.ToString());
		}

		/// <summary>
		/// Adds a report section
		/// </summary>
		/// <param name="sectionText"></param>
		protected void AddReportSection(string sectionText)
		{
			StringBuilder builder = new StringBuilder();

			builder.Append(Environment.NewLine);
			builder.Append("----------------------------------------------------------------------------------------" + Environment.NewLine);
			builder.AppendFormat("   {0} " + Environment.NewLine, sectionText);
			builder.Append("----------------------------------------------------------------------------------------" + Environment.NewLine);
			
			_report.Append(builder.ToString());
		}



		#endregion



		#region Public Methods

		public void AddFullReport()
		{
			AddCurrentProcessInformation();
			AddEnvironmentInformation();
			AddRunningProcessesInformation();
			AddInstalledServicesInformation();
			
		}




		/// <summary>
		/// Adds environment information
		/// </summary>
		/// <returns></returns>
		public void AddEnvironmentInformation()
		{
			string value;
			

			AddReportSection("Environment Information");
			IDictionary<string, object> values = GetStaticObjectPropertyValues(typeof(Environment));
			AddNameValueCollection(values);


			// Add aditional stuff but you must wrap everything in a try since anything can fail in a remote environment
			try
			{
				value = EnvironmentHelper.ApplicationBaseFolder;
			}
			catch(Exception e)
			{
				value = "n/a - " + e;
			}
			AddNameValuePair("AppDomain_ApplicationBaseFolder", value);



			try
			{
				value = EnvironmentHelper.ExecutingFolderPath;
			}
			catch (Exception e)
			{
				value = "n/a - " + e;
			}
			AddNameValuePair("AppDomain_ExecutingFolderPath", value);




			//_report.AppendFormat("MachineName={0}{1}", Environment.MachineName, Environment.NewLine);
			//_report.AppendFormat("OSVersion={0}{1}", Environment.OSVersion, Environment.NewLine);
			//_report.AppendFormat("ProcessorCount={0}{1}", Environment.ProcessorCount.ToString(), Environment.NewLine);
			//_report.AppendFormat("Working Memory Set={0}{1}", Environment.WorkingSet.ToString(), Environment.NewLine);
			//_report.AppendFormat("Current Directory={0}{1}", Environment.CurrentDirectory, Environment.NewLine);
			//_report.AppendFormat("SystemDirectory={0}{1}", Environment.SystemDirectory, Environment.NewLine);
			//_report.AppendFormat("System Running Tick Count={0}{1}", Environment.TickCount, Environment.NewLine);
			//_report.AppendFormat("UserDomainName={0}{1}", Environment.UserDomainName, Environment.NewLine);
			//_report.AppendFormat("UserName={0}{1}", Environment.UserName, Environment.NewLine);
			//_report.AppendFormat("CLR Version={0}{1}", Environment.Version, Environment.NewLine);
		}



		/// <summary>
		/// Returns Process information
		/// </summary>
		/// <returns></returns>
		public void AddCurrentProcessInformation()
		{
			// Add process info			
			Process process = Process.GetCurrentProcess();

			AddReportSection("Current Processes Information");
			AddObjectPropertyValues(process);
		}

		/// <summary>
		/// Adds all the running processes on the machine
		/// </summary>
		public void AddRunningProcessesInformation()
		{
			AddReportSection("Running Processes Information");

			Process[] processlist = Process.GetProcesses();

			foreach(Process process in processlist)
			{
				string name = process.ProcessName;
				string memory;
				string threadCount;
				string runTime;

				// NOTE: I access each property in a try cause it could throw various execptions

				try { memory = process.WorkingSet64.ToString(); }
				catch (Exception e) { memory = "N/A -" + e.Message; }

				try { threadCount = process.Threads.Count.ToString(); }
				catch (Exception e) { threadCount = "N/A - " + e.Message; }

				try { runTime = (DateTime.Now - process.StartTime).ToString(); }
				catch (Exception e) { runTime = "N/A - " + e.Message; }

				

				string row = string.Format("Id:{0} Name:{1} Memory:{2} Threads:{3} RunTime:{4}", process.Id,
				                           process.ProcessName,
				                           memory,
				                           threadCount,
				                           runTime);

				_report.Append(row + Environment.NewLine);
			}
		}


		/// <summary>
		/// Adds the installed services on the machine
		/// </summary>
		public void AddInstalledServicesInformation()
		{

			/* Uncomment to Query Services Info AND uncomment the 

			AddReportSection("Installed Services Information");

			// get list of Windows services
			ServiceController[] services = ServiceController.GetServices();

			// try to find service name
			foreach (ServiceController service in services)
			{
				//if (service.ServiceName == serviceName)
				//    return true;
				string name = service.ServiceName;
				string displayName = service.DisplayName;
				string status = Enum.GetName(typeof(ServiceControllerStatus), service.Status);
				
				string row = string.Format("Name:{0} DisplayName:{1} Status:{2}",
										   name,
										   displayName,
										   status);

				_report.Append(row + Environment.NewLine);

			}
		}


		public void AddSqlServerInformation()
		{


			/* Uncomment to Query Sql Server

			
			String sqlServerLogin = "sa";
			String password = "test";
			String instanceName = "";
			String remoteSvrName = "localhost";

			// Connecting to an instance of SQL Server using SQL Server Authentication
			Server srv1 = new Server();   // connects to default instance
			srv1.ConnectionContext.LoginSecure = false;   // set to true for Windows Authentication
			srv1.ConnectionContext.Login = sqlServerLogin;
			srv1.ConnectionContext.Password = password;
			Console.WriteLine(srv1.Information.Version);   // connection is established

			// Connecting to a named instance of SQL Server with SQL Server Authentication using ServerConnection
			ServerConnection srvConn = new ServerConnection();
			srvConn.ServerInstance = @".\" + instanceName;   // connects to named instance
			srvConn.LoginSecure = false;   // set to true for Windows Authentication
			srvConn.Login = sqlServerLogin;
			srvConn.Password = password;
			Server srv2 = new Server(srvConn);
			Trace.WriteLine(srv2.Information.Version);   // connection is established


			// For remote connection, remote server name / ServerInstance needs to be specified
			ServerConnection srvConn2 = new ServerConnection(remoteSvrName);
			srvConn2.LoginSecure = false;
			srvConn2.Login = sqlServerLogin;
			srvConn2.Password = password;
			Server srv3 = new Server(srvConn2);
			Console.WriteLine(srv3.Information.Version);   // connection is established





			AddReportSection("Database Server Information");
			AddObjectPropertyValues(srvConn);


			AddReportSection("Database Server Connection Information");
			AddObjectPropertyValues(srv1);

			foreach(Database d in srv1.Databases)
			{
				AddReportSection("Database " + d.Name);
				AddObjectPropertyValues(d);

				foreach (LogFile logFile in d.LogFiles)
				{
					AddReportSection("Log File " + logFile.Name);
					AddObjectPropertyValues(logFile);
				}
			}
			
			 * */

			

		}


		/// <summary>
		/// Returns a string of the diagnostics report. 
		/// </summary>
		/// <returns></returns>
		public string ToString()
		{
			return _report.ToString();

		}
		#endregion

	}
}