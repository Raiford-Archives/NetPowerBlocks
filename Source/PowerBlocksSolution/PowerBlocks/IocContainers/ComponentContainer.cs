using System;

namespace PowerBlocks.IocContainers
{

#pragma warning disable 1591

	[Serializable]
	public class ComponentContainerException : Exception
	{
		public ComponentContainerException(string message) : base(message)
		{
		}

		public ComponentContainerException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}

	[Serializable]
	public class ComponentContainerNotCreatedException : ComponentContainerException
	{
		public ComponentContainerNotCreatedException(string message)
			: base(message)
		{
		}

		public ComponentContainerNotCreatedException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}

	[Serializable]
	public class ComponentContainerComponentNotRegisteredException : ComponentContainerException
	{
		public ComponentContainerComponentNotRegisteredException(string message)
			: base(message)
		{
		}

		public ComponentContainerComponentNotRegisteredException(string message, Exception innerException)
			: base(message, innerException)
		{
		}
	}


	/// <summary>
	/// WRapper class around a component container
	/// </summary>
	public class ComponentContainer : IComponentContainer 
	{

		/// <summary>
		/// singlton instance holder for the single container for an application.
		/// </summary>
		// ReSharper disable FieldCanBeMadeReadOnly.Local
		private static IComponentContainer _currentContainer = null;
		// ReSharper restore FieldCanBeMadeReadOnly.Local


		readonly IComponentContainerProvider _containerProvider;

		/// <summary>
		/// Custom configuration file name
		/// </summary>
		public string CustomConfigurationFileName { get; set; }

		public ComponentContainer(IComponentContainerProvider provider, ContainerConfigurationSource configurationSource)
		{
			if (provider == null) throw new ArgumentNullException("provider", "provider argument must not be null");

			//Debug.Assert(_currentContainer == null, "I think what happened here is that this class was created more than once. The central concept of a component container is that you have a single container within an AppDomain... No? some thought should be put into this");
			if(_currentContainer != null) throw new ComponentContainerException("You can only create a single instance of ComponentContainer for each App Domain. I think what happened here is that this class was created more than once. The central concept of a component container is that you have a single container within an AppDomain... No? some thought should be put into this");
			
			_containerProvider = provider;
			
			// Initialize it
			_containerProvider.Initialize(configurationSource);

			// set current container to THIS container
			//_currentContainer = this;
		}


		/// <summary>
		/// Initializes the Default environment. This will generally be the same for the entire application makeing
		/// it much easier to initialize with a single call. For example in your UnitTest and applications just call
		/// this method and it should be ready to go
		/// </summary>
		public static void InitializeDefaultEnvironment()
		{
			// Create and configure your Ioc Container for this application
			IComponentContainerProvider containerProvider = new WindsorContainerProvider
			{
				CustomConfigurationFileName = "WindsorComponents.config" // This file must be in your root
			};
			IComponentContainer container = new ComponentContainer(containerProvider, ContainerConfigurationSource.CustomConfigurationFileWithProgrammatic);
			container.FinalizeRegistration();
			container.SetGlobalReference(out IocContainer.Current); // Set a reference so you may now use the IocContainer object in the rest of your code.
		}

		/// <summary>
		/// Shu
		/// </summary>
		public static void Shutdown()
		{
			// Not sure we need to do anythig here.
		}



		/// <summary>
		/// Registers a component
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <param name="serviceType"></param>
		public void Register<TComponentType>(Type serviceType)
		{
			_containerProvider.Register<TComponentType>(serviceType);
		}

		/// <summary>
		/// Registers a component
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		public void Register<TComponentType, TServiceType>()
		{
			_containerProvider.Register<TComponentType, TServiceType>();
		}

		/// <summary>
		/// Resolves a component
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		public TServiceType Resolve<TServiceType>()
		{
			return _containerProvider.Resolve<TServiceType>();
		}

		/// <summary>
		/// Called after the registration process
		/// </summary>
		public void FinalizeRegistration()
		{
			_containerProvider.FinalizeRegistration();
		}

		/// <summary>
		/// Determines if the container contains a component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool ContainsComponent<T>()
		{
			return _containerProvider.ContainsComponent<T>();
		}

		/// <summary>
		/// Determines if the container contains a component
		/// </summary>
		/// <param name="serviceName"></param>
		/// <returns></returns>
		public bool ContainsComponent(string serviceName)
		{
			return _containerProvider.ContainsComponent(serviceName);
		}

		/// <summary>
		/// Used to set  global reference to a container, this should generally be called once in an application initialization
		/// </summary>
		/// <param name="globalContainer"></param>
		public void SetGlobalReference(out IComponentContainer globalContainer)
		{
			globalContainer = this;
			//_currentContainer = globalContainer;
		}		
	}

#pragma warning restore 1591

}
