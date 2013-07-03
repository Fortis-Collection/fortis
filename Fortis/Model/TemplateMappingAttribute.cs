namespace Fortis.Model
{
	public class TemplateMappingAttribute : System.Attribute
	{
		public readonly string id;

		public string Id { get; set; }
		public string Type { get; set; }

		public TemplateMappingAttribute(string id)
		{
			this.Id = id;
		}

		public TemplateMappingAttribute(string id, string type)
			: this(id)
		{
			this.Type = type;
		}
	}
}
