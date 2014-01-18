using System;
using System.Collections;
using System.Collections.Generic;
using Sitecore.Data.Fields;
using Fortis.Extensions;
using Fortis.SitecoreExtensions;

namespace Fortis.Model.Fields
{
	public class ListFieldWrapper : FieldWrapper, IListFieldWrapper
	{
		private const char _delimiter = '|';
		private IEnumerable<Guid> _ids;

		public ListFieldWrapper(Field field)
			: base(field) { }

		public ListFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: this(key, ref item, value.Split(_delimiter).ToGuidEnumerable()) { }

		public ListFieldWrapper(string key, ref ItemWrapper item, IEnumerable ids = null)
			: this(key, ref item, ids.ToGuidEnumerable()) { }

		public ListFieldWrapper(string key, ref ItemWrapper item, IEnumerable<Guid> ids = null)
			: base(key, ref item, ids.ToDelimitedString(_delimiter))
		{
			_ids = ids;
		}

		public IEnumerable<T> GetItems<T>() where T : IItemWrapper
		{
			foreach (var id in Value)
			{
                var item = Sitecore.Context.Database.GetItem(id.ToString());

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
				if (_ids == null)
				{
					var listField = (MultilistField)Field;
					var ids = new List<Guid>();

					foreach (var id in listField.Items)
					{
						Guid guid;

						if (Guid.TryParse(id, out guid))
						{
							ids.Add(guid);
						}
					}

					_ids = ids;
				}

				return _ids;
			}
		}
	}
}
