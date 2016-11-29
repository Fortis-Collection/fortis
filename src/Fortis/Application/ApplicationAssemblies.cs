using System;
using System.Collections.Generic;
using System.Reflection;

namespace Fortis.Application
{
	public class ApplicationAssemblies : IApplicationAssemblies
	{
		public IEnumerable<Assembly> Assemblies => AppDomain.CurrentDomain.GetAssemblies();
	}
}
