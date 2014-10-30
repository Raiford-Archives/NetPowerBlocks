using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using PowerBlocks.Utilities;

namespace PowerBlocks
{
	/// <summary>
	/// Log4NetLogger  
	/// </summary>	
	public class Log4NetLogger : ILog	
	{
		readonly log4net.ILog _log4NetLog = null;

		public Log4NetLogger(Type type)
		{
			_log4NetLog = log4net.LogManager.GetLogger(type);

			// NOTE: I am intentionally hard coding this value. If we decide on another folder for our logs then we should modify.
			//       Should this need to change often then we pull value from the app config
			FileInfo fileInfo = new FileInfo("Log4Net.config");
			if (!fileInfo.Exists)
			{
				string error = string.Format("Log4Net.config file could not be loaded, please make sure this file exists in {0}",
				                             EnvironmentHelper.ApplicationBaseFolder);

			//	throw new InvalidOperationException(error);
			}


			log4net.Config.XmlConfigurator.ConfigureAndWatch(fileInfo);
		}

		#region Private Methods

		private void CommonHandler(object message)
		{
			// Dump To Trace for debugging
			System.Diagnostics.Debug.WriteLine(message);
		}
		

		private void CommonHandler(object message, Exception exception)
		{
			string combinedMessage;

			if (exception == null)
			{
				combinedMessage = message.ToString();
			}
			else
			{
				combinedMessage = message.ToString() + " ErrorMessage: " + exception.Message;
			}

			CommonHandler(combinedMessage);
		}
		#endregion

		public bool IsDebugEnabled
		{
			get { return _log4NetLog.IsDebugEnabled; }
		}

		public bool IsInfoEnabled
		{
			get { return _log4NetLog.IsInfoEnabled; }
		}

		public bool IsWarnEnabled
		{
			get { return _log4NetLog.IsWarnEnabled; }
		}

		public bool IsErrorEnabled
		{
			get { return _log4NetLog.IsErrorEnabled; }
		}

		public bool IsFatalEnabled
		{
			get { return IsFatalEnabled; }
		}

		public void Debug(object message)
		{
			CommonHandler(message);
			_log4NetLog.Debug(message);
		}

		public void Debug(object message, Exception exception)
		{
			CommonHandler(message, exception);
			_log4NetLog.Debug(message, exception);
		}

		public void DebugFormat(string format, params object[] args)
		{
			_log4NetLog.DebugFormat(format, args);
		}

		public void DebugFormat(string format, object arg0)
		{
			_log4NetLog.DebugFormat(format, arg0);
		}

		public void DebugFormat(string format, object arg0, object arg1)
		{
			_log4NetLog.DebugFormat(format, arg0, arg1);
		}

		public void DebugFormat(string format, object arg0, object arg1, object arg2)
		{
			_log4NetLog.DebugFormat(format, arg0, arg1, arg2);
		}

		public void DebugFormat(IFormatProvider provider, string format, params object[] args)
		{
			_log4NetLog.DebugFormat(provider, format, args);
		}

		public void Info(object message)
		{
			CommonHandler(message);
			_log4NetLog.Info(message);
		}

		public void Info(object message, Exception exception)
		{
			CommonHandler(message, exception); 
			_log4NetLog.Info(message, exception);
		}

		public void InfoFormat(string format, params object[] args)
		{
			_log4NetLog.InfoFormat(format, args);
		}

		public void InfoFormat(string format, object arg0)
		{
			_log4NetLog.InfoFormat(format, arg0);
		}

		public void InfoFormat(string format, object arg0, object arg1)
		{
			_log4NetLog.InfoFormat(format, arg0, arg1);
		}

		public void InfoFormat(string format, object arg0, object arg1, object arg2)
		{
			_log4NetLog.InfoFormat(format, arg0, arg1, arg2);
		}

		public void InfoFormat(IFormatProvider provider, string format, params object[] args)
		{
			_log4NetLog.InfoFormat(provider, format, args);
		}

		public void Warn(object message)
		{
			CommonHandler(message);
			_log4NetLog.Warn(message);
		}

		public void Warn(object message, Exception exception)
		{
			CommonHandler(message, exception); 
			_log4NetLog.Warn(message, exception);
		}

		public void WarnFormat(string format, params object[] args)
		{
			_log4NetLog.WarnFormat(format, args);
		}

		public void WarnFormat(string format, object arg0)
		{
			_log4NetLog.WarnFormat(format, arg0);
		}

		public void WarnFormat(string format, object arg0, object arg1)
		{
			_log4NetLog.WarnFormat(format, arg0, arg1);
		}

		public void WarnFormat(string format, object arg0, object arg1, object arg2)
		{
			_log4NetLog.WarnFormat(format, arg0, arg1, arg2);
		}

		public void WarnFormat(IFormatProvider provider, string format, params object[] args)
		{
			_log4NetLog.WarnFormat(provider, format, args);
		}

		public void Error(object message)
		{
			CommonHandler(message);
			_log4NetLog.Error(message);
		}

		public void Error(object message, Exception exception)
		{
			CommonHandler(message, exception); 
			_log4NetLog.Error(message, exception);
		}

		public void ErrorFormat(string format, params object[] args)
		{
			_log4NetLog.ErrorFormat(format, args);
		}

		public void ErrorFormat(string format, object arg0)
		{
			_log4NetLog.ErrorFormat(format, arg0);
		}

		public void ErrorFormat(string format, object arg0, object arg1)
		{
			_log4NetLog.ErrorFormat(format, arg0, arg1);
		}

		public void ErrorFormat(string format, object arg0, object arg1, object arg2)
		{
			_log4NetLog.ErrorFormat(format, arg0, arg1, arg2);
		}

		public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
		{
			_log4NetLog.ErrorFormat(provider, format, args);
		}

		public void Fatal(object message)
		{
			CommonHandler(message);
			_log4NetLog.Fatal(message);
		}

		public void Fatal(object message, Exception exception)
		{
			CommonHandler(message, exception); 
			_log4NetLog.Fatal(message, exception);
		}

		public void FatalFormat(string format, params object[] args)
		{
			_log4NetLog.FatalFormat(format, args);
		}

		public void FatalFormat(string format, object arg0)
		{
			_log4NetLog.FatalFormat(format, arg0);
		}

		public void FatalFormat(string format, object arg0, object arg1)
		{
			_log4NetLog.FatalFormat(format, arg0, arg1);
		}

		public void FatalFormat(string format, object arg0, object arg1, object arg2)
		{
			_log4NetLog.FatalFormat(format, arg0, arg1, arg2);
		}

		public void FatalFormat(IFormatProvider provider, string format, params object[] args)
		{
			_log4NetLog.FatalFormat(provider, format, args);
		}



		public void MethodErrorException(object thisObject, string methodName, Exception exception, string custommMessage = "")
		{
			string message = ExceptionHelper.FormatErrorMessage(thisObject, methodName, exception);
			Error(message, exception);
			
		
		}

	}
}
