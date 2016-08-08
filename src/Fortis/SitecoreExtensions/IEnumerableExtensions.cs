using Sitecore.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.SitecoreExtensions
{
	public static class IEnumerableExtensions
	{
		public static IEnumerable<Guid> ToGuidEnumerable(this IEnumerable ids, bool throwOnBadId = true)
		{
			var convertedIds = new List<Guid>();

			foreach (var id in ids)
			{
				if (id is Guid)
				{
					convertedIds.Add((Guid)id);
					continue;
				}
				else
				{
					var idString = id.ToString();
					Guid idGuid;

					if (ID.IsID(idString))
					{
						convertedIds.Add(ID.Parse(idString).Guid);
						continue;
					}
					else if (ShortID.IsShortID(idString))
					{
						convertedIds.Add(ShortID.Parse(idString).Guid);
						continue;
					}
					else if (Guid.TryParse(idString, out idGuid))
					{
						convertedIds.Add(idGuid);
						continue;
					}
				}
				
				if (throwOnBadId)
				{
					throw new Exception("Fortis: Unable to convert enumerable object to Guid");
				}
			}

			return convertedIds;
		}
	}
}
