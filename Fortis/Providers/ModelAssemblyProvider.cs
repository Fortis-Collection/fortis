using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace Fortis.Providers
{
	public class ModelAssemblyProvider : IModelAssemblyProvider
	{
		public ModelAssemblyProvider()
		{
			_configuration = (NameValueCollection)WebConfigurationManager.GetSection(_configurationKey);
		}

		private readonly string _configurationKey = "fortis";
		private readonly string _assemblyConfigurationKey = "assembly";
		private Assembly _modelAssembly;
		private readonly NameValueCollection _configuration;
		private string ModelAssemblyName { get { return _configuration[_assemblyConfigurationKey]; } }

		public Assembly Assembly
		{
			get
			{
				if (_modelAssembly == null)
				{
					foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						if (assembly.FullName.Equals(ModelAssemblyName))
						{
							_modelAssembly = assembly;
							break;
						}
					}

					if (_modelAssembly == null)
					{
						throw new Exception("Forits | Unable to find model assembly: " + ModelAssemblyName);
					}
				}

				return _modelAssembly;
			}
		}
	}
}
