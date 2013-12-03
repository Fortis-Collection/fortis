using System.Collections;
using System.Collections.Generic;
using Sitecore.Data.Fields;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class ListFieldWrapper : FieldWrapper, IEnumerable<IItemWrapper>
	{
		public ListFieldWrapper(string value)
			: base(value)
		{
		}

		public virtual IEnumerable<T> GetItems<T>() where T : IItemWrapper
		{
			var list = Value.Split('|');

			foreach (var id in list)
			{
                var item = Sitecore.Context.Database.GetItem(id);
                if (item != null)
                {
                    var wrapper = Spawn.FromItem<T>(item);
                    if (wrapper is T)
                    {
                        yield return (T)wrapper;
                    }
                }
			}
		}

		public virtual IEnumerator<IItemWrapper> GetEnumerator()
		{
			return GetItems<IItemWrapper>().GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
