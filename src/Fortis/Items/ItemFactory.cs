using Sitecore.Data.Items;
using Fortis.Extensions;
using System.Linq;
using System.Collections.Generic;
using Fortis.Fields;
using Fortis.Fields.Dynamics;
using System;
using Fortis.Dynamics;

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
		}

		public T Create<T>(Item item)
		{
			// TODO:	Find best interface for the item otherwise use T
			//			* This only happens if T has a template attribute on it
			//			* This is for when there's template inheritance by interface heirachy
			//			* We want to create an object which as best matches the item
			//			* Look at the template of the item and its inheritance
			var requestedItemType = typeof(T); // IItemTypeStrategy.Create<T>() > System.Type;
			var baseItemType = typeof(BaseItem);

			var modelledItem = new BaseItem
			{
				Item = item
			};

			// Find properties on interface that aren't part of the BaseItem class
			// IFindRequestedItemTypeProperties.Find<T>();
			//		IDiffProperties.Find(type baseType, type requestedType);
			var baseItemTypeProperties = baseItemType.GetProperties(); // TODO: Re-factor to dependency and add caching
			var requestedItemTypeProperties = requestedItemType.GetPublicProperties().Where(rp => !baseItemTypeProperties.Any(bp => string.Equals(bp.Name, rp.Name)));  // TODO: Re-factor to dependency and add caching

			var modelledFields = new Dictionary<string, IField>();

			foreach (var property in requestedItemTypeProperties)
			{
				var sitecoreFieldName = FieldNameParser.Parse(property);

				if (!item.Fields.Any(f => string.Equals(f.Name, sitecoreFieldName, StringComparison.InvariantCultureIgnoreCase)))
				{
					// Do we add the property but make it null/default if it doesn't exist as a field?
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
	}
}
