using System;
using Fortis.Extensions;

namespace Fortis.Fields.LinkField
{
	public class LinkField : BaseField, ILinkField
	{
		public new Guid Value
		{
			get { return Field.Value.SafeParseSitecoreIdGuid(); }
			set { base.Value = value.ToSitecoreIdString(); }
		}
	}
}
