namespace Fortis.Tests.Configuration
{
	using System.Linq;

	using Fortis.Model.Fields;

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
			Assert.IsNotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Assembley == "Fortis.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
			Assert.AreEqual(2, testProvider.DefaultConfiguration.Models.Count());
		}

		[Test]
		public void ShouldLoadSupportedFieldTypes()
		{
			var testProvider = new TestXmlConfigurationProvider();

			Assert.IsNotEmpty(testProvider.DefaultConfiguration.Fields);
			Assert.IsNotNull(testProvider.DefaultConfiguration.Fields.FirstOrDefault(x => x.FieldName == "droplist"));
			Assert.IsNotNull(testProvider.DefaultConfiguration.Fields.FirstOrDefault(x => x.FieldName == "single-line text"));
			Assert.AreEqual(testProvider.DefaultConfiguration.Fields.Count(), 23);
		}

		[Test]
		public void ShouldCompareSupportedFieldType()
		{
			var testProvider = new TestXmlConfigurationProvider();

			Assert.IsNotEmpty(testProvider.DefaultConfiguration.Fields);

			var booleanFieldType = testProvider.DefaultConfiguration.Fields.FirstOrDefault(x => x.FieldName == "checkbox");
			Assert.IsNotNull(booleanFieldType);

			Assert.AreEqual(typeof (BooleanFieldWrapper), booleanFieldType.FieldType);
		}
	}
}
