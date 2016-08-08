namespace Fortis.Tests.Configuration
{
	using System.Linq;

	using Fortis.Model.Fields;

	using Xunit;

	public class XmlConfigurationProviderTests
	{
		[Fact]
		public void ShouldLoadFortisModels()
		{
			var testProvider = new TestXmlConfigurationProvider();

			Assert.NotEmpty(testProvider.DefaultConfiguration.Models);
			Assert.NotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Name == "Fortis.Model"));
			Assert.NotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Name == "Project.Model"));
			Assert.NotNull(testProvider.DefaultConfiguration.Models.FirstOrDefault(x => x.Assembly == "Fortis.Model, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null"));
			Assert.Equal(2, testProvider.DefaultConfiguration.Models.Count());
		}

		[Fact]
		public void ShouldLoadSupportedFieldTypes()
		{
			var testProvider = new TestXmlConfigurationProvider();

			Assert.NotEmpty(testProvider.DefaultConfiguration.Fields);
			Assert.NotNull(testProvider.DefaultConfiguration.Fields.FirstOrDefault(x => x.FieldName == "droplist"));
			Assert.NotNull(testProvider.DefaultConfiguration.Fields.FirstOrDefault(x => x.FieldName == "single-line text"));
			Assert.Equal(testProvider.DefaultConfiguration.Fields.Count(), 23);
		}

		[Fact]
		public void ShouldCompareSupportedFieldType()
		{
			var testProvider = new TestXmlConfigurationProvider();

			Assert.NotEmpty(testProvider.DefaultConfiguration.Fields);

			var booleanFieldType = testProvider.DefaultConfiguration.Fields.FirstOrDefault(x => x.FieldName == "checkbox");
			Assert.NotNull(booleanFieldType);

			Assert.Equal(typeof (BooleanFieldWrapper), booleanFieldType.FieldType);
		}
	}
}