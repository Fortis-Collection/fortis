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

		public IntegerFieldWrapper(Field field)
			: base(field) { }

		public IntegerFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: base(key, ref item, value) { }

		public IntegerFieldWrapper(string key, ref ItemWrapper item, long value)
			: base(key, ref item, value.ToString())
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
