using System;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Items
{
	public class TypesSource : ITypesSource
	{
		protected readonly ITemplateModelAssemblies TemplateModelAssemblies;

		public TypesSource(
			ITemplateModelAssemblies templateModelAssemblies)
		{
			TemplateModelAssemblies = templateModelAssemblies;
		}

		public IEnumerable<Type> Types => TemplateModelAssemblies.Assemblies.SelectMany(a => a.GetTypes());
	}
}
