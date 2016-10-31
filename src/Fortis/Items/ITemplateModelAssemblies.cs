using System.Collections.Generic;
using System.Reflection;

namespace Fortis.Items
{
	public interface ITemplateModelAssemblies
	{
		IEnumerable<Assembly> Assemblies { get; }
	}
}
