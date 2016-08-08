using System;
using System.Reflection;

namespace Fortis.Providers
{
	public interface IModelAssemblyProvider
	{
		[Obsolete("Use Types method instead", true)]
		Assembly Assembly { get; }

		/// <summary>
		/// Gets the model types found in the Model assemblies for the project.
		/// </summary>
		/// <value>
		/// The types.
		/// </value>
		Type[] Types { get; }
	}
}
