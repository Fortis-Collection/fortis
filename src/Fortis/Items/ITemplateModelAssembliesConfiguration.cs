using System.Collections.Generic;

namespace Fortis.Items
{
	public interface ITemplateModelAssembliesConfiguration
	{
		IEnumerable<ITemplateModelAssembly> Assemblies { get; }
	}
}
