using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using System;

namespace Fortis.Items
{
	public class ItemTypeTemplateMatcher : IItemTypeTemplateMatcher
	{
		protected readonly ITemplateTypeMap TemplateTypeMap;

		public ItemTypeTemplateMatcher(
			ITemplateTypeMap templateTypeMap)
		{
			TemplateTypeMap = templateTypeMap;
		}

		public Type Find<T>(Item item)
		{
			var requestedItemType = typeof(T);
			var requestedItemTypeTemplateId = TemplateTypeMap.Find(requestedItemType);

			if (requestedItemTypeTemplateId == default(Guid))
			{
				return requestedItemType;
			}

			if (!ItemInheritsTemplate(requestedItemTypeTemplateId, item))
			{
				return null;
			}

			if (TemplateTypeMap.Contains(item.TemplateID.Guid))
			{
				var exactMatchType = TemplateTypeMap.Find(item.TemplateID.Guid);

				if (requestedItemType.IsAssignableFrom(exactMatchType))
				{
					return exactMatchType;
				}
				else
				{
					return requestedItemType;
				}
			}

			return null;
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
