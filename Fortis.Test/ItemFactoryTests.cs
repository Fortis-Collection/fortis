namespace Fortis.Test
{
	using System;
	using NUnit.Framework;
	using Sitecore.Data.Items;
	using Fortis.Providers;
	using Moq;
	using Fortis.Model;
	using System.Collections.Generic;
	using Sitecore.Data;
	using Sitecore.Globalization;
	using Microsoft.QualityTools.Testing.Fakes;
	using Sitecore.Data.Items.Fakes;
	using Sitecore.Collections;
	using Sitecore.Collections.Fakes;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Fields.Fakes;
	using Sitecore.Data.Managers.Fakes;
	using Sitecore.Data.Templates;
	using Sitecore.Data.Templates.Fakes;
	using System.Linq;

	[TestFixture]
	public class ItemFactoryTests
	{
		[TestFixtureSetUp]
		public void Initialise()
		{
			// Create mock objects
		}

		[SetUp]
		public void CleanTestObjects()
		{
			// Clean any test objects used
		}

		private Item CreateTestItem(string id = null, string name = "Test Item", string templateId = null, FieldCollection fields = null)
		{
			return new ShimItem()
			{
				NameGet = () => name ?? "Test Item",
				IDGet = () => new Sitecore.Data.ID(id ?? new Guid().ToString()),
				TemplateIDGet = () => new Sitecore.Data.ID(templateId ?? new Guid().ToString()),
				FieldsGet = () => fields ?? CreateTestFields(),
			};
		}

		private FieldCollection CreateTestFields()
		{
			var fields = new List<Field>();

			fields.Add(CreateTestTextField());

			return new ShimFieldCollection()
			{
				ItemGetString = name => { return fields.FirstOrDefault(f => f.Name.Equals(name)); },
				ItemGetID = id => { return fields.FirstOrDefault(f => f.ID.Equals(id)); },
			};
		}

		private Field CreateTestTextField(string id = null, string name = null, string value = null)
		{
			return new ShimField()
			{
				IDGet = () => new Sitecore.Data.ID(id ?? new Guid().ToString()),
				NameGet = () => name ?? "Text Field",
				ValueGet = () => value ?? "Text Field Value",
			};
		}

		[Test]
		public void CorrectDirectTemplateForContext()
		{
			using (ShimsContext.Create())
			{
				var templateId = "{02F5002C-325E-4E5A-9C93-A97724ED3400}";
				var testItem = CreateTestItem(templateId: templateId);
				var contextProvider = new Mock<IContextProvider>();

				contextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var itemFactory = new ItemFactory(contextProvider.Object);
				var wrappedItem = itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}

		[Test]
		public void IncorrectTemplateForContext()
		{
			using (ShimsContext.Create())
			{
				var templateId = "{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}";
				var testItem = CreateTestItem(templateId: templateId);
				var contextProvider = new Mock<IContextProvider>();

				contextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var itemFactory = new ItemFactory(contextProvider.Object);
				var wrappedItem = itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNull(wrappedItem);
			}
		}

		[Test]
		[ExpectedException()]
		public void UnmappedTemplateForContext()
		{
			using (ShimsContext.Create())
			{
				var testItem = CreateTestItem();
				var contextProvider = new Mock<IContextProvider>();

				contextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var itemFactory = new ItemFactory(contextProvider.Object);
				var wrappedItem = itemFactory.GetContextItem<IScUnmappedTemplate>();
			}
		}

		[Test]
		public void CorrectInheritedTemplateForContext()
		{
			using (ShimsContext.Create())
			{
				var testItem = CreateTestItem();

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				var contextProvider = new Mock<IContextProvider>();

				contextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var itemFactory = new ItemFactory(contextProvider.Object);
				var wrappedItem = itemFactory.GetContextItem<IScBaseTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}

		[Test]
		[Ignore("Needs writing")]
		public void CorrectDirectTemplatesForRenderingContext()
		{
			using (ShimsContext.Create())
			{
				var tId = "{02F5002C-325E-4E5A-9C93-A97724ED3400}";
				var testItem = CreateTestItem(templateId: tId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				var contextProvider = new Mock<IContextProvider>();

				contextProvider.Setup(c => c.PageContextItem).Returns(testItem);
				contextProvider.Setup(c => c.RenderingContextItem).Returns(testItem);
				contextProvider.Setup(c => c.RenderingItem).Returns(testItem);
				contextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var itemFactory = new ItemFactory(contextProvider.Object);
				var wrappedItem = itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}

		[Test]
		[Ignore("Needs writing")]
		public void CorrectInheritedTemplatesForRenderingContext()
		{
			using (ShimsContext.Create())
			{
				var tId = "{02F5002C-325E-4E5A-9C93-A97724ED3400}";
				var testItem = CreateTestItem(templateId: tId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				var contextProvider = new Mock<IContextProvider>();

				contextProvider.Setup(c => c.PageContextItem).Returns(testItem);
				contextProvider.Setup(c => c.RenderingContextItem).Returns(testItem);
				contextProvider.Setup(c => c.RenderingItem).Returns(testItem);
				contextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var itemFactory = new ItemFactory(contextProvider.Object);
				var wrappedItem = itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}
	}
}
