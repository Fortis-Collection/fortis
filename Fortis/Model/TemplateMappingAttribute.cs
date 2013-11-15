using System;

namespace Fortis.Model
{
	public class TemplateMappingAttribute : Attribute
	{
		public Guid Id { get; private set; }
		public string Type { get; private set; }

		public TemplateMappingAttribute(string id)
		{
			Id = new Guid(id);
		}

		public TemplateMappingAttribute(string id, string type)
			: this(id)
		{
			Type = type;
		}
	}
}
