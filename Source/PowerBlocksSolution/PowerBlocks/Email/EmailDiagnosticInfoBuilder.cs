using System;
using System.Text;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace PowerBlocks.Email
{
	/// <summary>
	/// Email builder that builds up a diagnostic email message. This is useful for debuggin on a remote machine
	/// that is not easily accessible.
	/// </summary>
	public class EmailDiagnosticInfoBuilder
	{

		/// <summary>
		/// Builds a report of the properties based on the passed in object
		/// </summary>
		/// <param name="reportHeader"></param>
		/// <param name="obj"></param>
		/// <returns></returns>
		public string BuildObjectPropertyReport(string reportHeader, object obj)
		{
			StringBuilder builder = new StringBuilder();
			builder.Append(reportHeader + Environment.NewLine + "========================================================");
					
			foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(obj))
			{
				string name = descriptor.Name;
				object value = descriptor.GetValue(obj);
				builder.AppendFormat("{0}={1}{2}", name, value, Environment.NewLine);
			}

			return builder.ToString();
		}

		/// <summary>
		/// Returns the Environment Information
		/// </summary>
		/// <returns></returns>
		public string BuildEnvironmentPropertyReport()
		{
			StringBuilder builder = new StringBuilder();
			builder.Append("Environment Information"  + Environment.NewLine + "========================================================");

			builder.AppendFormat("MachineName={0}{1}", Environment.MachineName, Environment.NewLine);
			builder.AppendFormat("OSVersion={0}{1}", Environment.OSVersion, Environment.NewLine);
			builder.AppendFormat("ProcessorCount={0}{1}", Environment.ProcessorCount.ToString(), Environment.NewLine);
			builder.AppendFormat("Working Memory Set={0}{1}", Environment.WorkingSet.ToString(), Environment.NewLine);
			builder.AppendFormat("Current Directory={0}{1}", Environment.CurrentDirectory, Environment.NewLine);
			builder.AppendFormat("SystemDirectory={0}{1}", Environment.SystemDirectory, Environment.NewLine);
			builder.AppendFormat("System Running Tick Count={0}{1}", Environment.TickCount, Environment.NewLine);
			builder.AppendFormat("UserDomainName={0}{1}", Environment.UserDomainName, Environment.NewLine);
			builder.AppendFormat("UserName={0}{1}", Environment.UserName, Environment.NewLine);
			builder.AppendFormat("CLR Version={0}{1}", Environment.Version, Environment.NewLine);			

			return builder.ToString();
		}



		/// <summary>
		/// Returns a section with advanced diagnostic information that can be included in a dump or an error log or 
		/// an email message.
		/// </summary>
		/// <returns></returns>
		[SuppressMessage("Microsoft.Security", "CA2122:DoNotIndirectlyExposeMethodsWithLinkDemands", Justification="We must assume the caller is a safe caller")]
		public string GetAdvancedDiagnosticInfo()
		{
			StringBuilder advancedReport = new StringBuilder();

			// Add environment info
			advancedReport.AppendLine(BuildEnvironmentPropertyReport());
			
			// Add process info			
			Process process = Process.GetCurrentProcess();
			advancedReport.AppendLine(BuildObjectPropertyReport("Process Information", process));
			
			// Add process list info		
			// ?????
			
			return advancedReport.ToString();
		}

	}
}
