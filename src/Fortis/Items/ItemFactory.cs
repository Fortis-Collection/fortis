using Sitecore.Data.Items;
using Fortis.Extensions;
using System.Linq;
using System.Collections.Generic;
using Fortis.Fields;
using Fortis.Fields.Dynamics;
using System;
using ImpromptuInterface;

namespace Fortis.Items
{
	public class ItemFactory
	{
		protected readonly IFieldFactory FieldFactory;
		protected readonly IPropertyInfoFieldNameParser FieldNameParser;
		protected readonly IAddFieldDynamicProperty AddFieldDynamicProperty;

		public ItemFactory(
			IFieldFactory fieldFactory,
			IPropertyInfoFieldNameParser fieldNameParser,
			IAddFieldDynamicProperty addFieldDynamicProperty)
		{
			FieldFactory = fieldFactory;
			FieldNameParser = fieldNameParser;
			AddFieldDynamicProperty = addFieldDynamicProperty;
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

			var modelledFields = new Dictionary<string, IField>(); // temporary store for all the fields we're creating

			foreach (var property in requestedItemTypeProperties)
			{
				var sitecoreFieldName = FieldNameParser.Parse(property);

				// Check if the field actually exists
				if (!item.Fields.Any(f => string.Equals(f.Name, sitecoreFieldName, StringComparison.InvariantCultureIgnoreCase)))
				{
					// Do we add the property but make it null/default?
					continue;
				}

				// Retrieve existing field or create it
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

			T castedItem = Impromptu.ActLike(modelledItem); // Re-factor to its own dependency so to abstract away the usage of Impromptu

			return castedItem;
		}
	}
}
