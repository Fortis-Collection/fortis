using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public interface IField
	{
		Field Field { get; }
		string Value { get; set; }
	}
}
