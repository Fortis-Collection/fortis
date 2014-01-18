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
		private ISpawnProvider _spawnProvider;
		private IItemFactory _itemFactory;
		private Mock<IContextProvider> _mockContextProvider;

		[TestFixtureSetUp]
		public void Initialise()
		{
			// Create mock objects
			_mockContextProvider = new Mock<IContextProvider>();
			_spawnProvider = new SpawnProvider();
			_itemFactory = new ItemFactory(_mockContextProvider.Object, _spawnProvider);
		}

		[SetUp]
		public void CleanTestObjects()
		{
			// Clean any test objects used
			_mockContextProvider.ResetCalls();
		}

		private Item CreateTestItem(string id = null, string name = "Test Item", Guid templateId = default(Guid), FieldCollection fields = null)
		{
			Item item = new ShimItem()
			{
				NameGet = () => name ?? "Test Item",
				IDGet = () => new Sitecore.Data.ID(id ?? new Guid().ToString()),
				TemplateIDGet = () => new Sitecore.Data.ID(templateId),
				FieldsGet = () => fields ?? CreateTestFields(),
			};

			new ShimBaseItem(item)
			{
				ItemGetString = fieldName => item.Fields[fieldName].Value
			};

			return item;
		}

		private FieldCollection CreateTestFields(List<Field> fields = null)
		{
			if (fields == null)
			{
				fields = new List<Field>();

				fields.Add(CreateTestTextField());
			}

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
				var templateId = new Guid("{02F5002C-325E-4E5A-9C93-A97724ED3400}");
				var testItem = CreateTestItem(templateId: templateId);

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}

		[Test]
		public void IncorrectTemplateForContext()
		{
			using (ShimsContext.Create())
			{
				var templateId = new Guid("{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}");
				var testItem = CreateTestItem(templateId: templateId);

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScTemplate>();

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

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScUnmappedTemplate>();
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


				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScBaseTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}

		[Test]
		public void CorrectDirectTemplatesForRenderingContext()
		{
			using (ShimsContext.Create())
			{
				// Rendering Item
				var scRenderingTemplateType = typeof(IScRenderingParametersTemplate);
				var renderingTemplateAttribute = (TemplateMappingAttribute)scRenderingTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var renderingTemplateId = renderingTemplateAttribute.Id;

				Field parametersTemplateField = new ShimField()
				{
					NameGet = () => "Parameters Template",
					ValueGet = () => renderingTemplateId.ToString(),
				};

				var testRenderingItem = CreateTestItem(fields: CreateTestFields(new List<Field>() { parametersTemplateField }));

				// Context Item

				var scBaseTemplateType = typeof(IScTemplate);
				var templateAttribute = (TemplateMappingAttribute)scBaseTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var templateId = templateAttribute.Id;
				var testItem = CreateTestItem(templateId: templateId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns(testRenderingItem);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IScTemplate, IScTemplate, IScRenderingParametersTemplate>();

				Assert.IsNotNull(renderingModel.PageItem);
				Assert.IsNotNull(renderingModel.RenderingItem);
				Assert.IsNotNull(renderingModel.RenderingParametersItem);
			}
		}

		[Test]
		[Ignore("Needs writing")]
		public void CorrectInheritedTemplatesForRenderingContext()
		{
			using (ShimsContext.Create())
			{
				var tId = new Guid("{02F5002C-325E-4E5A-9C93-A97724ED3400}");
				var testItem = CreateTestItem(templateId: tId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var wrappedItem = _itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}
	}
}
