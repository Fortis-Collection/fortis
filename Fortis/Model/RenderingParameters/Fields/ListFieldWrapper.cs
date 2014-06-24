using System;
using System.Collections;
using System.Collections.Generic;
using Fortis.Model.Fields;
using Fortis.Providers;
using Sitecore;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class ListFieldWrapper : FieldWrapper, IListFieldWrapper
	{
		public ListFieldWrapper(string value, ISpawnProvider spawnProvider)
			: base(value, spawnProvider)
		{
		}

		public IEnumerable<T> GetItems<T>() where T : IItemWrapper
		{
			if (string.IsNullOrWhiteSpace(_value))
			{
				yield break;
			}

			var list = (string.IsNullOrWhiteSpace(_value) ? string.Empty : _value).Split('|'); 

			foreach (var id in list)
			{
                var item = Context.Database.GetItem(id);
                if (item != null)
                {
                    var wrapper = SpawnProvider.FromItem<T>(item);
                    if (wrapper is T)
                    {
                        yield return (T)wrapper;
                    }
                }
			}
		}

		public IEnumerator<IItemWrapper> GetEnumerator()
		{
			return GetItems<IItemWrapper>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		public IEnumerable<Guid> Value
		{
			get
			{
				if (string.IsNullOrWhiteSpace(_value))
				{
					yield break;
				}

				var list = (string.IsNullOrWhiteSpace(_value) ? string.Empty : _value).Split('|'); 

				foreach (var id in list)
				{
					Guid guid;
					if (Guid.TryParse(id, out guid))
					{
						yield return guid;
					}
				}
			}
		}
	}
}
