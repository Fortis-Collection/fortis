﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Fields;
using System.Web;

namespace Fortis.Model.Fields
{
	public class DateTimeFieldWrapper : FieldWrapper
	{
		protected DateField DateField
		{
			get { return (Sitecore.Data.Fields.DateField)Field; }
		}

		public virtual DateTime Value
		{
			get { return DateField.DateTime; }
		}

		public DateTimeFieldWrapper(Field field)
			: base(field)
		{
		}

		public virtual IHtmlString Render(bool includeTime)
		{
			return Render(includeTime ? Sitecore.Context.Language.CultureInfo.DateTimeFormat.FullDateTimePattern : Sitecore.Context.Language.CultureInfo.DateTimeFormat.ShortDatePattern);
		}

		public override IHtmlString Render(string dateTimeFormat)
		{
			return base.Render("format=" + dateTimeFormat);
		}

		public static implicit operator DateTime(DateTimeFieldWrapper field)
		{
			return field.Value;
		}
	}
}
