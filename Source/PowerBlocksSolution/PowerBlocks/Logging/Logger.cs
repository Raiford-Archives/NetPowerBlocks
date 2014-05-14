using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;
using System.IO;

namespace PowerBlocks
{
	/// <summary>
	/// Logger creates an instance of a logger for your application to use. In general, your class will create a single instance of the log and
	/// assign it to the ILog private field. As a convention, it is good practice to standardize the naming convention for getting a logger. 
	/// </summary>	
	/// <example>
	/// ILog Log = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	/// </example>
	public class Logger
	{
		#region Private Fields
		//private static readonly ILog Log = null;	
		#endregion
	
		#region Public Static Methods
		/// <summary>
		/// Gets a logger for a class to use. In practice you can use reflection to reflect the Type of type parameter, however, if you require absolute performance,
		/// for example in a class that is created very often you can use the typeof() operator to get the class name.
		/// </summary>
		/// <example>
		/// ILog Log = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
		/// OR
		/// ILog Log = Logger.GetLogger(typeof(ClassName));
		/// </example>
		/// <param name="type"></param>
		/// <returns></returns>
		public static ILog GetLogger(Type type)
		{
			// Note that we are using DI get prevent being tied into any specific logging framework
			// NOTE: I could not get this to work because of AddSubResolver System.Type error messages
			//ILog log = Ioc.Resolve<ILog>();

			// SO! for now I will create a concrete isntance since that is really all i will use anyway
			ILog log = new Log4NetLogger(type);

           

			return log;			
		}


		#endregion

	}
}
