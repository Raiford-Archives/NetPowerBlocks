using System;

namespace PowerBlocks.IocContainers
{

	/// <summary>
	/// Indicates the source of the containers configuration will come from. This will usually be
	/// either a configuration file, app/web.config or programmatically. It will be up to the individual provider 
	/// to handle this in its own specific way.
	/// </summary>
	public enum ContainerConfigurationSource
	{
		/// <summary>
		/// Container will get its configuration from the Application configuration file, such as web.config, app.config.
		/// </summary>
		ApplicationConfigurationFile = 1,
		
		/// <summary>
		/// Container will get its configuration from a Custom configuration file. Some providers may support this other may not.
		/// </summary>
		CustomConfigurationFile = 2,

		/// <summary>
		/// Container will gets its configuration programmatically by the client application.
		/// </summary>
		Programmatic=3,

		/// <summary>
		/// Allows you to first put from a config file then add additional components programmatically. This is useful given many if not most applications
		/// will never be reconfigured in the field, but will be done programmatically to ensure proper testing before going to production. This is also 
		/// usefull for Unit Testing where you want to plug in a Dummy / Mock component for a single test case.
		/// </summary>
		ApplicationConfigurationFileWithProgrammatic = 4,

		/// <summary>
		/// Allows you to first put from a custom config file then add additional components programmatically. This is useful given many if not most applications
		/// will never be reconfigured in the field, but will be done programmatically to ensure proper testing before going to production. This is also 
		/// usefull for Unit Testing where you want to plug in a Dummy / Mock component for a single test case.
		/// </summary>
		CustomConfigurationFileWithProgrammatic = 5,

		/// <summary>
		/// Indicates this value is not set and it needs to be set.
		/// </summary>
		Unknown = 0
		
	}


	/// <summary>
	/// Required interface for all Container providers. This 
	/// </summary>
	public interface IComponentContainerProvider : IComponentContainer
	{
		/// <summary>
		/// Called upon creating of this container. In most cases the provider will create its own container, object builder or other factory in here.
		/// </summary>
		/// <param name="configurationSource"></param>
		void Initialize(ContainerConfigurationSource configurationSource);		
	}


	/// <summary>
	/// 
	/// </summary>
	public interface IComponentContainer
	{

		/// <summary>
		/// Custom config file name
		/// </summary>
		string CustomConfigurationFileName { get; set; }

		/// <summary>
		/// Registers a component (concrete class) and its service (interface)s
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <param name="serviceType"></param>
		void Register<TComponentType>(Type serviceType);
		
		/// <summary>
		/// Registers a a component (concrete class) and its service (interface).
		/// </summary>
		/// <typeparam name="TComponentType"></typeparam>
		/// <typeparam name="TServiceType"></typeparam>
		void Register<TComponentType, TServiceType>();

		/// <summary>
		/// Must be called after registering your components
		/// </summary>
		void FinalizeRegistration();
		
		/// <summary>
		/// Resolves or returns and instance of the component (concrete class) associated with this service
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		TServiceType Resolve<TServiceType>();


		/// <summary>
		/// Returns true if the container contains this component
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <returns></returns>
		bool ContainsComponent<T>();
		
		/// <summary>
		/// Returns true if the container contains this component
		/// </summary>
		/// <param name="serviceName"></param>
		/// <returns></returns>
		bool ContainsComponent(string serviceName);

		/// <summary>
		/// This Allows an application to for the container to set a reference to a global instance, usually a static class singlton. This is encourages, for 
		/// both ease of programming and ensuring that your application has a single container for the entire app.
		/// </summary>
		/// <param name="globalContainer"></param>
		void SetGlobalReference(out IComponentContainer globalContainer);


	}
}
