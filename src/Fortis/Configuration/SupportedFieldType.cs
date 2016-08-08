namespace Fortis.Configuration
{
	using System;

	public class SupportedFieldType : ISupportedFieldType
	{
		public SupportedFieldType(string name, string type)
		{
			FieldName = name;
			FieldType = Type.GetType(type);
			FieldTypeName = type;
		}

		public string FieldName { get; }
		public Type FieldType { get; }
		public string FieldTypeName { get; }
	}
}
