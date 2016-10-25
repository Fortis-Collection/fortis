using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public interface IField
	{
		Field Field { get; }
		string Name { get; }
		string Type { get; }
		string Value { get; set; }
	}
}
