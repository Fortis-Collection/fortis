using Sitecore.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Fortis.Application;
using Fortis.Context;
using Fortis.Databases;
using Fortis.Dynamics;
using Fortis.Fields;
using Fortis.Fields.BooleanField;
using Fortis.Fields.DateTimeField;
using Fortis.Fields.FileField;
using Fortis.Fields.GeneralLinkField;
using Fortis.Fields.ImageField;
using Fortis.Fields.IntegerField;
using Fortis.Fields.LinkField;
using Fortis.Fields.LinkListField;
using Fortis.Fields.NameValueListField;
using Fortis.Fields.NumberField;
using Fortis.Fields.TextField;
using Fortis.Fields.Dynamics;
using System.Collections.Generic;
using System;
using Fortis.Items;
using Fortis.Items.Context;
using System.Collections.Specialized;

namespace Fortis.IoC
{
	public class RegisterDependencies : IServicesConfigurator
	{
		public void Configure(IServiceCollection serviceCollection)
		{
			// Application
			serviceCollection.AddSingleton<IApplicationAssemblies, ApplicationAssemblies>();

			// Context
			serviceCollection.AddSingleton<ISitecoreContextDatabase, SitecoreContextDatabase>();
			//serviceCollection.AddSingleton<ISitecoreContextItem, SitecoreContextItem>();

			// Databases
			serviceCollection.AddSingleton<ISitecoreDatabaseFactory, SitecoreDatabaseFactory>();

			// Dynamics
			serviceCollection.AddSingleton<IDynamicObjectCaster, DynamicObjectCaster>();

			// Fields
			serviceCollection.AddSingleton<IFieldFactory, FieldFactory>();
			serviceCollection.AddSingleton<IPropertyInfoFieldNameParser, PropertyInfoFieldNameParser>();
			serviceCollection.AddSingleton<ITypedFieldFactories, TypedFieldFactories>();
			serviceCollection.AddSingleton<ITypedFieldFactoryConfiguration, TypedFieldFactoryConfiguration>();
			serviceCollection.AddSingleton<ITypedFieldMappingValidator, TypedFieldMappingValidator>();
			// Fields - factories implementing ITypedFieldFactory
			serviceCollection.AddSingleton<ITypedFieldFactory, BooleanFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, DateTimeFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, FileFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, GeneralLinkFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, ImageFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, IntegerFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, LinkFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, LinkListFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, NameValueListFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, NumberFieldFactory>();
			serviceCollection.AddSingleton<ITypedFieldFactory, TextFieldFactory>();
			// Fields - dynamics
			serviceCollection.AddSingleton<IAddFieldDynamicProperty, AddFieldDynamicProperty>();
			var addValueFieldDynamicProperty = new AddValueFieldDynamicProperty();
			serviceCollection.AddSingleton<IAddFieldDynamicPropertyStrategies>(service => new AddFieldDynamicPropertyStrategies(
				new List<IAddFieldDynamicPropertyStrategy>
				{
					new AddFieldDynamicPropertyStrategy(),
					new AddFieldDynamicPropertyStrategy<bool>(addValueFieldDynamicProperty),
					new AddFieldDynamicPropertyStrategy<int>(addValueFieldDynamicProperty),
					new AddFieldDynamicPropertyStrategy<float>(addValueFieldDynamicProperty),
					new AddFieldDynamicPropertyStrategy<DateTime>(addValueFieldDynamicProperty),
					new AddFieldDynamicPropertyStrategy<Guid>(addValueFieldDynamicProperty),
					new AddFieldDynamicPropertyStrategy<NameValueCollection>(addValueFieldDynamicProperty),
					new AddFieldDynamicPropertyStrategy<IEnumerable<Guid>>(addValueFieldDynamicProperty),
					new StringAddFieldDynamicPropertyStrategy()
				}
			));

			// Items - modelling
			serviceCollection.AddSingleton<IItemFactory, ItemFactory>();
			serviceCollection.AddSingleton<IItemTypeTemplateMatcher, ItemTypeTemplateMatcher>();
			serviceCollection.AddSingleton<ITemplateModelAssemblies, TemplateModelAssemblies>();
			serviceCollection.AddSingleton<ITemplateModelAssembliesConfiguration, TemplateModelAssembliesConfiguration>();
			serviceCollection.AddSingleton<ITemplateTypeMap, TemplateTypeMap>();
			serviceCollection.AddSingleton<ITypesSource, AssembliesTypesSource>();
			// Items - database
			serviceCollection.AddSingleton<ISitecoreItemGetter, SitecoreItemGetter>();
			serviceCollection.AddSingleton<ISitecoreItemsGetter, SitecoreItemsGetter>();
			serviceCollection.AddSingleton<IItemGetChildren, ItemGetChildren>();
			serviceCollection.AddSingleton<IItemGetter, ItemGetter>();
			serviceCollection.AddSingleton<IItemsGetter, ItemsGetter>();
			// Items - context
			serviceCollection.AddSingleton<IContextItem, ContextItem>();
			serviceCollection.AddSingleton<IContextItemGetChildren, ContextItemGetChildren>();
			serviceCollection.AddSingleton<IContextItemGetter, ContextItemGetter>();
			serviceCollection.AddSingleton<IContextItemsGetter, ContextItemsGetter>();
			serviceCollection.AddSingleton<IContextSiteHome, ContextSiteHome>();
			serviceCollection.AddSingleton<IContextSiteRoot, ContextSiteRoot>();
		}
	}
}
