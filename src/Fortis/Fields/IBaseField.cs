using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public interface IBaseField : IField
	{
		Field Field { get; set; }
	}
}