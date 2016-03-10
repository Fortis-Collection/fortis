using Fortis.Providers;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	using System.Collections;
	using System.Collections.Specialized;
	using System.Linq;
	using System.Web;

	public class NameValueListFieldWrapper : FieldWrapper, INameValueListFieldWrapper
	{
		public NameValueListFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public NameValueListFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: this(key, ref item, spawnProvider, HttpUtility.ParseQueryString(value ?? "")) { }

		public NameValueListFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, ArrayList value = null)
			: base(key, ref item, string.Empty, spawnProvider)
		{
			if (value == null)
			{
				return;
			}

			_value = new NameValueCollection();
			foreach (object val in value)
			{
				if (val is ArrayList)
				{
					var tmp = val as ArrayList;
					_value.Add(tmp[0].ToString(), tmp[1].ToString());
				}
			}

			this._rawValue = _value.ToString();
		}

		public NameValueListFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, NameValueCollection value = null)
			: base(key, ref item, (value ?? new NameValueCollection(0)).ToString(), spawnProvider)
		{
			_value = value;
		}

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
