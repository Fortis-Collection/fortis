using Fortis.Providers;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	using System.Collections.Specialized;
	using System.Web;

	public class NameValueListFieldWrapper : FieldWrapper, INameValueListFieldWrapper
	{
		public NameValueListFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public NameValueListFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		private NameValueCollection _value;

		public NameValueCollection Value
		{
			get
			{
				if (_value == null)
				{
					if (string.IsNullOrWhiteSpace(this.RawValue))
					{
						return new NameValueCollection(0);
					}

					this._value = HttpUtility.ParseQueryString(this.RawValue);
				}

				return _value;
			}
		}
	}
}
