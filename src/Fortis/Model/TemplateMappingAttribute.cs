using System;

namespace Fortis.Model
{
	public class TemplateMappingAttribute : Attribute
	{
		public Guid Id { get; set; }
		public string Type { get; set; }

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
