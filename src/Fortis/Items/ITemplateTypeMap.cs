using System;

namespace Fortis.Items
{
	public interface ITemplateTypeMap
	{
		Guid Find(Type type);
		Type Find(Guid templateId);
		bool Contains(Type type);
		bool Contains(Guid templateId);
	}
}
