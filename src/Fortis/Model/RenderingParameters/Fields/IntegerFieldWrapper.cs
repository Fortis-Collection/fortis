using Fortis.Model.Fields;
using Fortis.Providers;
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
		private new long? _value;

		public IntegerFieldWrapper(string fieldName, string value, ISpawnProvider spawnProvider)
			: base(fieldName, value, spawnProvider) { }

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
					else
					{
						_value = 0;
					}
				}

				return _value.Value;
			}
		}
	}
}
