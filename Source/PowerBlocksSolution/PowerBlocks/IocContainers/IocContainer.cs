namespace PowerBlocks.IocContainers
{
	
	/// <summary>
	/// Wrapper class to manage an IoC container.
	/// </summary>
	public static class IocContainer
	{
		/// <summary>
		/// Singlton instance holder for the single container for an application.
		/// </summary>
		//NOTE: I ended up making this a field vs property to allow it to be assigned to externally... this is not so ideal and open to new ways of doing this.		
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2211:NonConstantFieldsShouldNotBeVisible", Justification = "This is by design")]
		public static IComponentContainer Current = null;

		#region Static Methods

		/// <summary>
		/// Gets a component from the container
		/// </summary>
		/// <typeparam name="TServiceType"></typeparam>
		/// <returns></returns>
		public static TServiceType Get<TServiceType>()
		{
			if (Current == null) throw new ComponentContainerNotCreatedException("ComponentContainer has not been created. You must 'new' and instance of ComponentContainer before retrieving a component");

			return Current.Resolve<TServiceType>();
		}

		/// <summary>
		/// Destroys a container
		/// </summary>
		public static void DestroyContainer()
		{
			Current = null;
		}

		/// <summary>
		/// Return true if the container has been created.
		/// </summary>
		public static bool HasBeenCreated
		{
			get
			{
				return Current != null;
			}
		}


		//public static IComponentContainer Current
		//{
		//    get
		//    {
		//        return _currentContainer;
		//    }
		//    internal set
		//    {
		//        _currentContainer = value;
		//    }
		//}

		#endregion

	}
}
