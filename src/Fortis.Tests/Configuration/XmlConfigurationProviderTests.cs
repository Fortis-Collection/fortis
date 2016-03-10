namespace Fortis.Tests.Configuration
{
	using System.Linq;

	using NUnit.Framework;

	[TestFixture]
	public class XmlConfigurationProviderTests
	{
		[Test]
		public void ShouldLoadFortisModels()
		{
			var testProvider = new TestXmlConfigurationProvider();

			Assert.IsNotEmpty(testProvider.DefaultConfiguration.Models);
			Assert.IsNotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Name == "Fortis.Model"));
			Assert.IsNotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Name == "Project.Model"));
			Assert.IsNotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Assembley == "Fortis.Model, Fortis"));
		}
	}
}
