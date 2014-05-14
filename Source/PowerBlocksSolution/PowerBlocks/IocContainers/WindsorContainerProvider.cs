using System;
using Castle.Windsor;
using Castle.Windsor.Configuration.Interpreters;
using Castle.MicroKernel;
using System.Diagnostics;
using Castle.Core.Resource;

namespace PowerBlocks.IocContainers
{
	/// <summary>
	/// Windsor specific implementation of a container provider
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class WindsorContainerProvider : IComponentContainerProvider
	{
	
		private IWindsorContainer _container = null;
		private IKernel _kernel = null;

		private bool _initialized = false;
		private bool _registrationFinalized = false;

		/// <summary>
		/// Initializes the continer.
		/// </summary>
		/// <param name="configurationSource"></param>
		public void Initialize(ContainerConfigurationSource configurationSource)
		{
			
			if (configurationSource == ContainerConfigurationSource.ApplicationConfigurationFile)
			{
				_container = new WindsorContainer(new XmlInterpreter());
				_kernel = _container.Kernel;
			}
			else if (configurationSource == ContainerConfigurationSource.Programmatic)
			{
				_kernel = new DefaultKernel();
			}
			else if (configurationSource == ContainerConfigurationSource.CustomConfigurationFile)
			{
				if(string.IsNullOrEmpty(CustomConfigurationFileName))
				{
					const string error = "Failed to initialize WindsorContainerProvider. You must assign a value to CustomConfigurationFileName when using a CustomConfigurationFile Source";
					throw new ComponentContainerException(error);
				}

				using (FileResource fileResource = new FileResource(CustomConfigurationFileName))
				{
					_container = new WindsorContainer(new XmlInterpreter(fileResource));
					_kernel = _container.Kernel;
				}
			}
			else if (configurationSource == ContainerConfigurationSource.ApplicationConfigurationFileWithProgrammatic)
			{
				_container = new WindsorContainer(new XmlInterpreter());
				_kernel = _container.Kernel;
			}
			else if (configurationSource == ContainerConfigurationSource.CustomConfigurationFileWithProgrammatic)
			{
				if (string.IsNullOrEmpty(CustomConfigurationFileName))
				{
					const string error = "Failed to initialize WindsorContainerProvider. You must assign a value to CustomConfigurationFileName when using a CustomConfigurationFileWithProgrammatic Source";
					throw new ComponentContainerException(error);
				}

				using (FileResource fileResource = new FileResource(CustomConfigurationFileName))
				{
					_container = new WindsorContainer(new XmlInterpreter(fileResource));
					_kernel = _container.Kernel;
				}
			}
			else
			{
				Debug.Assert(false, "configurationSource has an invalid argument");
				//throw new NotImplementedException("Unknown configuration source");

			}
			_initialized = true;
		}

	
		/// <summary>
		/// Registers a component
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <param name="serviceType"></param>
		public void Register<TComponentType>(Type serviceType)
		{
			EnsureInitialized();
			_kernel.AddComponent<TComponentType>(serviceType);			
		}

		/// <summary>
		/// Registers a component.
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		public void Register<TComponentType, TServiceType>()
		{
			Register<TComponentType>(typeof(TServiceType));
		}

		/// <summary>
		/// Resolves a component
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		public TServiceType Resolve<TServiceType>()
		{
			EnsureRegistrationFinalized();
			TServiceType component = _kernel.Resolve<TServiceType>();
			return component;
		}

		/// <summary>
		/// Finalizes the registration and should be called after registration
		/// </summary>
		public void FinalizeRegistration()
		{
			// For this particular provider nothing needs to happen here but, to ensure the client\
			// is calling it we must set anyways. This is necessary in case they switch to a provider that does depend on it.
			_registrationFinalized = true;
		}


		private void EnsureInitialized()
		{
			if (!_initialized)
				throw new InvalidOperationException("You must call initialize!");

			// This is just to make sure the 
			if (_kernel == null)
				throw new InvalidOperationException("Object has not been initialized properly with unexpected results. The Kernel should not be null!");
		}

		private void EnsureRegistrationFinalized()
		{
			if (!_registrationFinalized)
				throw new InvalidOperationException("You must call FinalizeRegistration!");

		}




		/// <summary>
		/// Customer configuration file name
		/// </summary>
		public string CustomConfigurationFileName { get; set; }


		/// <summary>
		/// Returns true if the container contains a component of the specified type
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		public bool ContainsComponent<T>()
		{
			EnsureInitialized();
			return _kernel.HasComponent(typeof(T));
		}

		/// <summary>
		/// Returns true if the container contains a component of the specified type
		/// </summary>
		/// <param name="serviceName"></param>
		/// <returns></returns>
		public bool ContainsComponent(string serviceName)
		{
			EnsureInitialized();
			return _kernel.HasComponent(serviceName);
		}

		/// <summary>
		/// Sets a global reference to the container. This is not implemented in this version
		/// </summary>
		/// <param name="globalContainer"></param>
		public void SetGlobalReference(out IComponentContainer globalContainer)
		{
			throw new NotImplementedException();
		}
	}
}
