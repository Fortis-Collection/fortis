using System;
using Fortis.Model.Fields;
using Fortis.Providers;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class BooleanFieldWrapper : FieldWrapper, IBooleanFieldWrapper
	{
		public bool Value
		{
			get { return string.IsNullOrWhiteSpace(RawValue) ? false : RawValue.Equals("1"); }
			set { throw new NotImplementedException(); }
		}

		public BooleanFieldWrapper(string value, ISpawnProvider spawnProvider)
			: base(value, spawnProvider)
		{
		}

		public static implicit operator bool(BooleanFieldWrapper field)
		{
			return field.Value;
		}

		public static implicit operator string(BooleanFieldWrapper field)
		{
			return field.Value.ToString();
		}

		public static implicit operator int(BooleanFieldWrapper field)
		{
			return Convert.ToInt32(field.Value);
		}
	}
}
