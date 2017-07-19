using System;
using System.Collections.Generic;

namespace Fortis.Items
{
	public interface IItem
	{
		Guid ItemId { get; }
		string ItemLongId { get; }
		string ItemShortId { get; }
		string ItemPath { get; }
		string ItemName { get; }
		string ItemDisplayName { get; }
		string ItemLanguage { get; }
		string ItemDatabase { get; }
		Guid ItemTemplateId { get; }
		string ItemTemplateName { get; }
		IEnumerable<Guid> ItemTemplateIds { get; }
		int ItemVersion { get; }
		bool ItemIsLatestVersion { get; }
		bool ItemIsStandardValues { get; }
		int ItemChildrenCount { get; }
		bool ItemHasChildren { get; }
		IEnumerable<T> GetChildren<T>();
	}
}
