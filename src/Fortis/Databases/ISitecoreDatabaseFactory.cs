using Sitecore.Data;

namespace Fortis.Databases
{
	public interface ISitecoreDatabaseFactory
	{
		Database Create(string database);
	}
}
