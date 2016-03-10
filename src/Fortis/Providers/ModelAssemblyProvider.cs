using System;
using System.Linq;
using System.Reflection;

namespace Fortis.Providers
{
	using System.Collections.Generic;

	using Fortis.Configuration;

	public class ModelAssemblyProvider : IModelAssemblyProvider
	{
		private readonly object _lock = new object();

		protected IEnumerable<Type> ModelTypes;

		public virtual Type[] Types
		{
			get
			{
				lock (_lock)
				{
					if (ModelTypes == null)
					{
						var models = FortisConfigurationManager.Provider.DefaultConfiguration.Models;
						var assemblies = AppDomain.CurrentDomain.GetAssemblies();
						var types = new List<Type>();

						foreach (var model in models)
						{
							var assembley = assemblies.FirstOrDefault(assembly => assembly.FullName.Equals(model.Assembley));
							if (assembley == null)
							{
								throw new Exception("Forits: Unable to find model assembly: " + model.Assembley);
							}

							types.AddRange(assembley.GetTypes());
						}

						ModelTypes = types;
					}

					return ModelTypes.ToArray();
				}
			}
		}

		[Obsolete("Please use Types instead", true)]
		Assembly IModelAssemblyProvider.Assembly
		{
			get
			{
				throw new NotImplementedException("This method is obsolete. Please modify the code to use the Types property instead.");
			}
		}
	}
}