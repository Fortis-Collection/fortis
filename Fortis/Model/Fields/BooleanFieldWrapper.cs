using System;
using Sitecore.Data.Fields;
using Fortis.Providers;

namespace Fortis.Model.Fields
{
	public class BooleanFieldWrapper : FieldWrapper, IBooleanFieldWrapper
	{
		private bool _boolean;

		public bool Value
		{
			get { return IsLazy ? _boolean : RawValue.Equals("1"); }
			set { RawValue = value ? "1" : ""; }
		}

		public BooleanFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public BooleanFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public BooleanFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, bool value)
			: base(key, ref item, value ? "1" : "", spawnProvider)
		{
			_boolean = value;
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
