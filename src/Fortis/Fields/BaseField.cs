using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public class BaseField : IField
	{
		public string Value
		{
			get { return Field?.Value; }
			set { Field.Value = value; }
		}

		public string Name => Field.Name;
		public string Type => Field.Type;

		public Field Field { get; set; }
	}
}
