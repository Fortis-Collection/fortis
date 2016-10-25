namespace Fortis.Fields
{
	public interface ITypedFieldMappingValidator
	{
		bool IsValid(string fieldType, string factoryName);
	}
}
