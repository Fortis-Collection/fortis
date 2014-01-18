using Fortis.Providers;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Model.Fields
{
	public class IntegerFieldWrapper : FieldWrapper, IIntegerFieldWrapper
	{
		private long? _value;

		public IntegerFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public IntegerFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public IntegerFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, long value)
			: base(key, ref item, value.ToString(), spawnProvider)
		{
			_value = value;
		}

		public long Value
		{
			get
			{
				if (!IsLazy && !_value.HasValue)
				{
					long parsedValue;

					if (long.TryParse(RawValue, out parsedValue))
					{
						_value = parsedValue;
					}
				}

				return _value.Value;
			}
		}
	}
}
