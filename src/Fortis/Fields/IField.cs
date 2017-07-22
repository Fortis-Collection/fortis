namespace Fortis.Fields
{
	public interface IField
	{
		string Name { get; }
		string Type { get; }
		string Value { get; set; }
		string RawValue { get; set; }
	}
}
