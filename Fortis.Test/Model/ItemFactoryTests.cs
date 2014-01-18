namespace Fortis.Tests.Model
{
	using Fortis.Model;
	using Fortis.Providers;
	using Fortis.Test;
	using Microsoft.QualityTools.Testing.Fakes;
	using Moq;
	using NUnit.Framework;
	using Sitecore.Data.Fields;
	using Sitecore.Data.Fields.Fakes;
	using Sitecore.Data.Items;
	using Sitecore.Data.Managers.Fakes;
	using Sitecore.Data.Templates;
	using Sitecore.Data.Templates.Fakes;
	using System;
	using System.Collections.Generic;
	using System.Linq;

	[TestFixture]
	public class ItemFactoryTests
	{
		private IModelAssemblyProvider _modelAssemblyProvider;
		private ITemplateMapProvider _templateMappingProvider;
		private ISpawnProvider _spawnProvider;
		private IItemFactory _itemFactory;
		private Mock<IContextProvider> _mockContextProvider;

		[TestFixtureSetUp]
		public void Initialise()
		{
			// Create mock objects
			_mockContextProvider = new Mock<IContextProvider>();
			_modelAssemblyProvider = new ModelAssemblyProvider();
			_templateMappingProvider = new TemplateMapProvider(_modelAssemblyProvider);
			_spawnProvider = new SpawnProvider(_templateMappingProvider);
			_itemFactory = new ItemFactory(_mockContextProvider.Object, _spawnProvider);
		}

		[SetUp]
		public void CleanTestObjects()
		{
			// Clean any test objects used
			_mockContextProvider.ResetCalls();
		}

		[Test]
		public void GetContextItem_MatchingTemplateInterface_ItemNotNull()
		{
			using (ShimsContext.Create())
			{
				var templateId = new Guid("{02F5002C-325E-4E5A-9C93-A97724ED3400}");
				var testItem = FakesHelpers.CreateTestItem(templateId: templateId);

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNotNull(wrappedItem);
			}
		}

		[Test]
		public void GetContextItem_NonMatchingTemplateInterface_ItemNull()
		{
			using (ShimsContext.Create())
			{
				var templateId = new Guid("{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}");
				var testItem = FakesHelpers.CreateTestItem(templateId: templateId);

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScTemplate>();

				Assert.IsNull(wrappedItem);
			}
		}

		[Test]
		[ExpectedException()]
		public void GetContextItem_UnamppedTemplateInterface_Exception()
		{
			using (ShimsContext.Create())
			{
				var testItem = FakesHelpers.CreateTestItem();

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);

				var wrappedItem = _itemFactory.GetContextItem<IScUnmappedTemplate>();
			}
		}

		[Test]
		public void GetContextItem_InheritedTemplateInterface_ItemNotNull()
		{
			using (ShimsContext.Create())
			{
				var testItem = FakesHelpers.CreateTestItem();

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
		public void GetRenderingContextItems_MatchingTemplateInterface_PageItemNotNull()
		{
			using (ShimsContext.Create())
			{
				// Context Item
				var scBaseTemplateType = typeof(IScTemplate);
				var templateAttribute = (TemplateMappingAttribute)scBaseTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var templateId = templateAttribute.Id;
				var testItem = FakesHelpers.CreateTestItem(templateId: templateId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IScTemplate, IItemWrapper, IRenderingParameterWrapper>();

				Assert.IsNotNull(renderingModel.PageItem);
			}
		}

		[Test]
		public void GetRenderingContextItems_MatchingTemplateInterface_RenderingItemNotNull()
		{
			using (ShimsContext.Create())
			{
				// Context Item
				var scBaseTemplateType = typeof(IScTemplate);
				var templateAttribute = (TemplateMappingAttribute)scBaseTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var templateId = templateAttribute.Id;
				var testItem = FakesHelpers.CreateTestItem(templateId: templateId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				_mockContextProvider.Setup(c => c.PageContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IItemWrapper, IScTemplate, IRenderingParameterWrapper>();

				Assert.IsNotNull(renderingModel.RenderingItem);
			}
		}

		[Test]
		public void GetRenderingContextItems_MatchingTemplateInterface_RenderingParametersItemNotNull()
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

				var testRenderingItem = FakesHelpers.CreateTestItem(fields: FakesHelpers.CreateTestFields(new List<Field>() { parametersTemplateField }));

				_mockContextProvider.Setup(c => c.PageContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns(testRenderingItem);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IItemWrapper, IItemWrapper, IScRenderingParametersTemplate>();

				Assert.IsNotNull(renderingModel.RenderingParametersItem);
			}
		}

		[Test]
		public void GetRenderingContextItems_NonMatchingTemplateInterface_PageItemNull()
		{
			using (ShimsContext.Create())
			{
				// Context Item
				var scBaseTemplateType = typeof(IScBaseTemplate);
				var templateAttribute = (TemplateMappingAttribute)scBaseTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var templateId = templateAttribute.Id;
				var testItem = FakesHelpers.CreateTestItem(templateId: templateId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				_mockContextProvider.Setup(c => c.PageContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IScTemplate, IItemWrapper, IRenderingParameterWrapper>();

				Assert.IsNull(renderingModel.PageItem);
			}
		}

		[Test]
		public void GetRenderingContextItems_NonMatchingTemplateInterface_RenderingItemNull()
		{
			using (ShimsContext.Create())
			{
				// Context Item
				var scBaseTemplateType = typeof(IScBaseTemplate);
				var templateAttribute = (TemplateMappingAttribute)scBaseTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var templateId = templateAttribute.Id;
				var testItem = FakesHelpers.CreateTestItem(templateId: templateId);

				Template templateItem = new ShimTemplate()
				{
					DescendsFromID = id => true,
				};
				ShimTemplateManager.GetTemplateItem = id =>
				{
					return templateItem;
				};

				_mockContextProvider.Setup(c => c.PageContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns(testItem);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IItemWrapper, IScTemplate, IRenderingParameterWrapper>();

				Assert.IsNull(renderingModel.RenderingItem);
			}
		}

		[Test]
		public void GetRenderingContextItems_NonMatchingTemplateInterface_RenderingParametersItemNull()
		{
			using (ShimsContext.Create())
			{
				// Rendering Item
				var scRenderingTemplateType = typeof(IScBaseRenderingParametersTemplate);
				var renderingTemplateAttribute = (TemplateMappingAttribute)scRenderingTemplateType.GetCustomAttributes(typeof(TemplateMappingAttribute), false).First();
				var renderingTemplateId = renderingTemplateAttribute.Id;

				Field parametersTemplateField = new ShimField()
				{
					NameGet = () => "Parameters Template",
					ValueGet = () => renderingTemplateId.ToString(),
				};

				var testRenderingItem = FakesHelpers.CreateTestItem(fields: FakesHelpers.CreateTestFields(new List<Field>() { parametersTemplateField }));

				_mockContextProvider.Setup(c => c.PageContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingContextItem).Returns((Item)null);
				_mockContextProvider.Setup(c => c.RenderingItem).Returns(testRenderingItem);
				_mockContextProvider.Setup(c => c.RenderingParameters).Returns(new Dictionary<string, string>());

				var renderingModel = _itemFactory.GetRenderingContextItems<IItemWrapper, IItemWrapper, IScRenderingParametersTemplate>();

				Assert.IsNull(renderingModel.RenderingParametersItem);
			}
		}

		[Test]
		[Ignore("Needs writing")]
		public void CorrectInheritedTemplatesForRenderingContext()
		{
			using (ShimsContext.Create())
			{
				var tId = new Guid("{02F5002C-325E-4E5A-9C93-A97724ED3400}");
				var testItem = FakesHelpers.CreateTestItem(templateId: tId);

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
