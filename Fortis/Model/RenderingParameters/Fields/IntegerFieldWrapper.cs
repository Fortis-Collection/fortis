using Fortis.Model.Fields;
using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class IntegerFieldWrapper : FieldWrapper, IIntegerFieldWrapper
	{
		private long? _value;

		public IntegerFieldWrapper(string value)
			: base(value) { }

		public long Value
		{
			get
			{
				if (!_value.HasValue)
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
