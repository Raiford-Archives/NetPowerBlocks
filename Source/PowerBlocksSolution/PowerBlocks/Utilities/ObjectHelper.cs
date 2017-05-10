using System;

namespace PowerBlocks.Utilities
{
	/// <summary>
	/// Contains an assortment of helper methods for the objects.
	/// </summary>
	public static class ObjectHelper
	{
		/// <summary>
		/// Safefully dispose a disposable object. If the object is null, then the method will safely return
		/// with no exceptions thrown.
		/// </summary>
		/// <param name="objectToDispose"></param>
		public static void SafeDispose(IDisposable objectToDispose)
		{
			if (objectToDispose != null)
			{
				objectToDispose.Dispose();
			}
		}
	}
}
