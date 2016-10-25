using System.Collections.Specialized;

namespace Fortis.Fields.NameValueListField
{
	public class NameValueListField : BaseField, INameValueListField
	{
		public new Sitecore.Data.Fields.NameValueListField Field => base.Field;

		public new NameValueCollection Value
		{
			get { return Field.NameValues; }
			set { Field.NameValues = value; }
		}
	}
}
