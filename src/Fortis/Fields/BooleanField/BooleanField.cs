using Sitecore.Data.Fields;

namespace Fortis.Fields.BooleanField
{
	public class BooleanField : BaseField, IBooleanField
	{
		public new CheckboxField Field
		{
			get { return base.Field; }
		}

		public new bool Value
		{
			get { return Field.Checked; }
			set { base.Value = value ? "1" : string.Empty; }
		}
	}
}
