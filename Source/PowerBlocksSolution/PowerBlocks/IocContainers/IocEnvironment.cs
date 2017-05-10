namespace PowerBlocks.IocContainers
{
	/// <summary>
	/// Setups a the environment for the container, this should be used to make it easy to setup a new application.
	/// 
	/// </summary>
	public static class IocEnvironment
	{
		/// <summary>
		/// Initialized the Ioc environment
		/// </summary>
		public static void Initialize()
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
	}
}
