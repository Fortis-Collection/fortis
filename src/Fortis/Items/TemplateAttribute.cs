using System;

namespace Fortis.Items
{
	public class TemplateAttribute : Attribute
	{
		public Guid TemplateId { get; private set; }

		public TemplateAttribute(string templateId)
		{
			TemplateId = Guid.Parse(templateId);
		}
	}
}
