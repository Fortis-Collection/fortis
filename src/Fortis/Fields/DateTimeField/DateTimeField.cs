using Sitecore;
using Sitecore.Data.Fields;
using System;

namespace Fortis.Fields.DateTimeField
{
	public class DateTimeField : BaseField, IDateTimeField
	{
		public new DateField Field
		{
			get => base.Field;
		}

		public new DateTime Value
		{
			get => Field.DateTime;
			set => base.Value = DateUtil.ToIsoDate(value);
		}
	}
}
