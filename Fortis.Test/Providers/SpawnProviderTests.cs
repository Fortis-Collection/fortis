namespace Fortis.Test.Providers
{
	using System;
	using System.Text;
	using System.Collections.Generic;
	using NUnit.Framework;
	using Fortis.Providers;

	[TestFixture]
	public class SpawnProviderTests
	{
		private IModelAssemblyProvider _modelAssemblyProvider;
		private ITemplateMapProvider _templateMappingProvider;
		private ISpawnProvider _spawnProvider;

		[TestFixtureSetUp]
		public void Initialise()
		{
			// Create mock objects
			_modelAssemblyProvider = new ModelAssemblyProvider();
			_templateMappingProvider = new TemplateMapProvider(_modelAssemblyProvider);
			_spawnProvider = new SpawnProvider(_templateMappingProvider);
		}

		[SetUp]
		public void CleanTestObjects()
		{
			// Clean any test objects used
		}
	}
}