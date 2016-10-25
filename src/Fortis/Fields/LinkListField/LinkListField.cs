using Sitecore.Data.Fields;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Fields.LinkListField
{
	public class LinkListField : BaseField, ILinkListField
	{
		public new MultilistField Field => base.Field;

		public new IEnumerable<Guid> Value
		{
			get { return Field.TargetIDs.Select(ti => ti.Guid); }
			set { base.Value = string.Join("|", value); }
		}
	}
}
