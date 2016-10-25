using System;
using Fortis.Extensions;

namespace Fortis.Fields.LinkField
{
	public class LinkField : BaseField, ILinkField
	{
		public new Guid Value
		{
			get { return Field.Value.SafeParseIdGuid(); }
			set { base.Value = value.ToIdString(); }
		}
	}
}
