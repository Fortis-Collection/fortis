using Fortis.Providers;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class NumberFieldWrapper : FieldWrapper, INumberFieldWrapper
	{
		private float? _value;

		public NumberFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public NumberFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public NumberFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, float value)
			: base(key, ref item, value.ToString(), spawnProvider)
		{
			_value = value;
		}

		public override bool HasValue
		{
		    get
		    {
		        InitializeValue();
                return _value.HasValue;
		    }
		}

		public float Value
		{
			get
			{
			    this.InitializeValue();

                return _value ?? default(float);
			}
		}

	    protected void InitializeValue()
	    {
            if (!IsLazy && !_value.HasValue)
            {
                float parsedValue;

                if (float.TryParse(RawValue, out parsedValue))
                {
                    _value = parsedValue;
                }
            }
        }
	}
}
