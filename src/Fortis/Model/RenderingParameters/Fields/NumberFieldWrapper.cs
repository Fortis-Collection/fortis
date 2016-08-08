using Fortis.Model.Fields;
using Fortis.Providers;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class NumberFieldWrapper : FieldWrapper, INumberFieldWrapper
	{
		private new float? _value;

		public NumberFieldWrapper(string value, ISpawnProvider spawnProvider)
			: base(value, spawnProvider) { }

		public float Value
		{
			get
			{
				if (!_value.HasValue)
				{
					float parsedValue;

					_value = float.TryParse(RawValue, out parsedValue) ? parsedValue : 0;
				}

				return _value.Value;
			}
		}
	}
}
