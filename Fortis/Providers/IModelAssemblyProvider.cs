using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Providers
{
	public interface IModelAssemblyProvider
	{
		Assembly Assembly { get; }
	}
}
