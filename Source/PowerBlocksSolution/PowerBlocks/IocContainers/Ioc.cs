using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.Windsor;
using Castle.MicroKernel;
using System.IO;
using Castle.Core.Resource;
using Castle.Windsor.Configuration.Interpreters;
using System.Diagnostics;
using Castle.MicroKernel.Registration;

namespace PowerBlocks.IocContainers
{

	/// <summary>
	/// Ioc is a wrapper around the Windsor Castle DI container. It hides all the configuration and make
	/// using the container VERY easy. All you need to do is configure a file named ComponentContainer.config in your 
	/// application root and your set to go.
	/// </summary>
	/// <example>
	/// IEmployee employee = Ioc.Resolve<IEmployee>();
	/// 
	/// Thats it. No need to create any objects just call the resolve method and your done.
	/// </example>
	public class Ioc
	{
		#region Static Global Fields
		
		private static object _lockObject = new object();
		private static Ioc _instance = null;
		private static bool _initialized = false;
		private const string _configFileName = "WindsorComponents.config";
		
		#endregion


		#region Instance Fields
		private IWindsorContainer _container = null;
		private IKernel _kernel = null;
		#endregion


		#region Private Instance Methods
		private void InternalInitialize(string componentContainerFullFileName)
		{

			Debug.WriteLine("InternalInitialize " + componentContainerFullFileName);

			if ( string.IsNullOrEmpty(componentContainerFullFileName))
			{
				throw new ArgumentNullException("componentContainerFullFileName", "You must pass in a valid file name.");
			}
			
			if(!File.Exists(componentContainerFullFileName))
			{
				throw new InvalidOperationException(string.Format("Filename: {0} is missing. Please ensure that this file exists in the specified folder", componentContainerFullFileName));
			}
			
			using (FileResource fileResource = new FileResource(componentContainerFullFileName))
			{
				_container = new WindsorContainer(new XmlInterpreter(fileResource));
				_kernel = _container.Kernel;
			}

			_initialized = true;
		}
		
		/// <summary>
		/// Registers a component
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <param name="serviceType"></param>
		private void Register<TComponentType>(Type serviceType) where TComponentType:class
		{
			EnsureInitialized();
			//_kernel.AddComponent<TComponentType>(serviceType);
			_kernel.Register
			(
				Component.For<TComponentType>()
						 .ImplementedBy(serviceType)
			);

		}

		/// <summary>
		/// Registers a component.
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		private void Register<TComponentType, TServiceType>() where TComponentType : class  where  TServiceType : class
		{
			Register<TComponentType>(typeof(TServiceType));
		}

		/// <summary>
		/// Resolves a component
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		private TServiceType InternalResolve<TServiceType>() where TServiceType : class
		{
			TServiceType component = null;

			try
			{
				component = _kernel.Resolve<TServiceType>();			
			}
			catch (ComponentNotFoundException e)
			{
				string s = string.Format("{0}. Check the [{1}] file to ensure that there is an entry for {2}", e.Message, Ioc._configFileName, typeof(TServiceType).FullName);
				throw new InvalidOperationException(s, e);			
			}
			return component;
		}
		#endregion


		#region Private Static Methods
			private static void Initialize()
		{
			// Get the file name. You can add afeature to pull from app settings if you need,
			// but I think it is good to hard code it so it is standardize among all of our apps
			string componentContainerFullFileName = String.Format("{0}\\{1}", EnvironmentHelper.ApplicationBaseFolder, _configFileName);
			
			if(_instance != null || _initialized)
			{
				//throw new InvalidOperationException("Ioc.Initialize() can only be called once in an application lifetime");
			}

			//Debug.Assert(_instance==null);

			lock(_lockObject)
			{
				// Create an internal instance
				_instance = new Ioc();
				_instance.InternalInitialize(componentContainerFullFileName);
			}

			Debug.Assert(_instance != null);
		}


		private static void EnsureInitialized()
		{
			if (!_initialized)
			{
				Initialize();
			}

			// Few sanity checks
			Debug.Assert(_initialized);
			Debug.Assert(_instance._kernel != null);
			Debug.Assert(_instance._container != null);
		}
		#endregion


		#region Public Static Methods
		/// <summary>
		/// Resolves a component
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		public static TServiceType Resolve<TServiceType>() where TServiceType:class
		{
			EnsureInitialized();

			Debug.Assert(_instance != null);

			return _instance.InternalResolve<TServiceType>();
		}
		#endregion



		//public Ioc Instance
		//{
		//    get
		//    {
		//        EnsureInitialized();
		//        return _instance;
		//    }
		//}

	

	}
}
