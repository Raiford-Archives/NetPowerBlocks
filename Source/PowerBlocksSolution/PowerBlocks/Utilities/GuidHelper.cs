using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace PowerBlocks.Utilities
{
	public class GuidHelper
	{
	
		/// <summary>
		/// Converts the Guid to a Uid. The current version will simply be a string Guid without the dashes, but
		/// the implementation could change later if we need more encryption
		/// </summary>
		/// <param name="guid"></param>
		/// <returns></returns>
		public static string GuidToUid(Guid guid)
		{
			string encodedUid = guid.ToString();

			// replace - in guid
			encodedUid = encodedUid.Replace("-", "");

			////////////////////////////////////////////////////////////////////////////////////
			// TODO - In the future we can  encrypt the string if we feel we need to hide it
			//			It still needs to be url friendly so a simple encrypt such as reverse
			//			it or simply replace a few chars... high security i dont think is required
			//////////////////////////////////////////////////////////////////////////////////////
			
			return encodedUid;
		}

		/// <summary>
		/// convert the Uid string to a Guid
		/// </summary>
		/// <param name="uid"></param>
		/// <returns></returns>
		public static Guid UidToGuid(string uid)
		{
			string guidString = uid.Insert(8, "-").Insert(13, "-").Insert(18, "-").Insert(23, "-");

			Guid guid = new Guid(guidString);

			return guid;
		}
		

		/// <summary>
		/// converts a nullable Guid to a guid
		/// </summary>
		/// <param name="nullableGuid"></param>
		/// <returns></returns>
		public static Guid ToGuid(Guid? nullableGuid)
		{
			if(nullableGuid == null || !nullableGuid.HasValue)
			{
				return Guid.Empty;
			}
			return nullableGuid.Value;

		}

		public bool IsEmpty(Guid guid)
		{
			return guid.Equals(Guid.Empty);
		}

	}
}
