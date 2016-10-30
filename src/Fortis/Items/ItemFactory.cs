using Sitecore.Data.Items;
using Fortis.Extensions;
using System.Linq;
using System.Collections.Generic;
using Fortis.Fields;
using Fortis.Fields.Dynamics;
using System;
using Fortis.Dynamics;
using System.Reflection;
using Sitecore.Data.Managers;
using Sitecore.Data;

namespace Fortis.Items
{
	public class ItemFactory : IItemFactory
	{
		protected readonly IFieldFactory FieldFactory;
		protected readonly IPropertyInfoFieldNameParser FieldNameParser;
		protected readonly IAddFieldDynamicProperty AddFieldDynamicProperty;
		protected readonly IDynamicObjectCaster DynamicObjectCaster;

		public ItemFactory(
			IFieldFactory fieldFactory,
			IPropertyInfoFieldNameParser fieldNameParser,
			IAddFieldDynamicProperty addFieldDynamicProperty,
			IDynamicObjectCaster dynamicObjectCaster)
		{
			FieldFactory = fieldFactory;
			FieldNameParser = fieldNameParser;
			AddFieldDynamicProperty = addFieldDynamicProperty;
			DynamicObjectCaster = dynamicObjectCaster;

			BaseItemTypeProperties = typeof(BaseItem).GetProperties();
			RequestedItemTypesProperties = new Dictionary<Type, IEnumerable<PropertyInfo>>();
		}

		public PropertyInfo[] BaseItemTypeProperties;
		public Dictionary<Type, IEnumerable<PropertyInfo>> RequestedItemTypesProperties;
		private object requestedItemTypesPropertiesLock = new object();

		public T Create<T>(Item item)
		{
			var requestedItemType = typeof(T);
			var templateAttribute = requestedItemType.GetCustomAttribute<TemplateAttribute>();

			if (templateAttribute != null && !ItemInheritsTemplate(templateAttribute.TemplateId, item))
			{
				return default(T);
			}

			var modelledItem = new BaseItem
			{
				Item = item
			};

			IEnumerable<PropertyInfo> requestedItemTypeProperties = GetPropertyDiff(requestedItemType);
			var modelledFields = new Dictionary<string, IField>();

			foreach (var property in requestedItemTypeProperties)
			{
				var sitecoreFieldName = FieldNameParser.Parse(property);

				if (!item.Fields.Any(f => string.Equals(f.Name, sitecoreFieldName, StringComparison.InvariantCultureIgnoreCase)))
				{
					var returnType = property.PropertyType;
					object value = returnType.IsValueType ? Activator.CreateInstance(returnType) : null;

					modelledItem.AddDynamicProperty(property.Name, value);

					continue;
				}

				IField modelledField = modelledFields.ContainsKey(sitecoreFieldName) ?
										modelledFields[sitecoreFieldName] :
										FieldFactory.Create(item.Fields[sitecoreFieldName]);

				if (modelledFields.ContainsKey(sitecoreFieldName))
				{
					modelledField = modelledFields[sitecoreFieldName];
				}
				else
				{
					modelledField = FieldFactory.Create(item.Fields[sitecoreFieldName]);
					modelledFields.Add(sitecoreFieldName, modelledField);
				}

				AddFieldDynamicProperty.Add(modelledItem, property, modelledField);
			}

			T castedItem = DynamicObjectCaster.Cast<T>(modelledItem);

			return castedItem;
		}

		public IEnumerable<PropertyInfo> GetPropertyDiff(Type requestedItemType)
		{
			if (RequestedItemTypesProperties.ContainsKey(requestedItemType))
			{
				return RequestedItemTypesProperties[requestedItemType];
			}

			lock(requestedItemTypesPropertiesLock)
			{
				if (RequestedItemTypesProperties.ContainsKey(requestedItemType))
				{
					return RequestedItemTypesProperties[requestedItemType];
				}

				var propertyDiff = RequestedItemTypesProperties[requestedItemType] = CreatePropertyDiff(requestedItemType);

				return propertyDiff;
			}
		}

		public IEnumerable<PropertyInfo> CreatePropertyDiff(Type requestedItemType)
		{
			return requestedItemType.GetPublicProperties().Where(rp => !BaseItemTypeProperties.Any(bp => string.Equals(bp.Name, rp.Name)));
		}

		public bool ItemInheritsTemplate(Guid templateId, Item item)
		{
			if (item.Template.ID.Guid == templateId)
			{
				return true;
			}

			var itemTemplate = TemplateManager.GetTemplate(item);

			return itemTemplate != null && itemTemplate.DescendsFrom(new ID(templateId));
		}
	}
}
