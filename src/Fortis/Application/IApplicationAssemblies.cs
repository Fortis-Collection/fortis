using System.Collections.Generic;
using System.Reflection;

namespace Fortis.Application
{
	public interface IApplicationAssemblies
	{
		IEnumerable<Assembly> Assemblies { get; }
	}
}
