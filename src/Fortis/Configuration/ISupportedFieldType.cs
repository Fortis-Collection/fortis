namespace Fortis.Configuration
{
	using System;

	public interface ISupportedFieldType
	{
		string FieldName { get; }
		Type FieldType { get; }
		string FieldTypeName { get; }
	}
}
