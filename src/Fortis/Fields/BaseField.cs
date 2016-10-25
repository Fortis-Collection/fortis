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

		public Field Field { get; set; }
	}
}
