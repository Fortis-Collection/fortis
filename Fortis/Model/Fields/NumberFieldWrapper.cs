using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Model.Fields
{
	public class NumberFieldWrapper : FieldWrapper, INumberFieldWrapper
	{
		private float? _value;

		public NumberFieldWrapper(Field field)
			: base(field) { }

		public NumberFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: base(key, ref item, value) { }

		public NumberFieldWrapper(string key, ref ItemWrapper item, float value)
			: base(key, ref item, value.ToString())
		{
			_value = value;
		}

		public float Value
		{
			get
			{
				if (!IsLazy && !_value.HasValue)
				{
					float parsedValue;

					if (float.TryParse(RawValue, out parsedValue))
					{
						_value = parsedValue;
					}
				}

				return _value.Value;
			}
		}
	}
}
