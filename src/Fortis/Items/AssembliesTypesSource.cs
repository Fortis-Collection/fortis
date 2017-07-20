using System;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items
{
	public class AssembliesTypesSource : ITypesSource
	{
		protected readonly ITemplateModelAssemblies TemplateModelAssemblies;

		public AssembliesTypesSource(
			ITemplateModelAssemblies templateModelAssemblies)
		{
			TemplateModelAssemblies = templateModelAssemblies;
		}

		public IEnumerable<Type> Types => TemplateModelAssemblies.Assemblies.SelectMany(a => a.GetTypes());
	}
}
