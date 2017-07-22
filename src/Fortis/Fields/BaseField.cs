using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public class BaseField : IField, IBaseField
	{
		public virtual string Value
		{
			get => RawValue;
			set => RawValue = value;
		}
		public string RawValue
		{
			get => Field?.Value;
			set => Field.Value = value;
		}
		public string Name => Field.Name;
		public string Type => Field.Type;
		public Field Field { get; set; }
	}
}
