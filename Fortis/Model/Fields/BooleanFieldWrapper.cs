using System;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class BooleanFieldWrapper : FieldWrapper, IBooleanFieldWrapper
	{
		public bool Value
		{
			get { return RawValue.Equals("1"); }
			set { RawValue = value ? "1" : ""; }
		}

		public BooleanFieldWrapper(Field field)
			: base(field)
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
