using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Fortis.Model
{
	public partial interface IItemWrapper : IWrapper
	{
		bool IsLazy { get; }
		string DatabaseName { get; }
		[IndexField("_language")]
		string LanguageName { get; }
		string ItemLocation { get; }
		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_group")]
		Guid ItemID { get; }
		string ItemShortID { get; }
		[IndexField("_name")]
		[Obsolete("Use Name property instead")]
		string ItemName { get; }
		[IndexField("_name")]
		string Name { get; }
		[IndexField("__display_name")]
		string DisplayName { get; }
		[IndexField("_latestversion")]
		bool IsLatestVersion { get; }
		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_template")]
		Guid TemplateId { get; }
		[IndexField("_templates")]
		IEnumerable<Guid> TemplateIds { get; }
		bool HasChildren { get; }
		int ChildCount { get; }
		string SearchTitle { get; }
		void Save();
		void Delete();
		void Publish();
		void Publish(bool children);
		string GenerateUrl();
		string GenerateUrl(bool includeHostname);
		IEnumerable<T> Children<T>(bool recursive = false) where T : IItemWrapper;
		IEnumerable<T> Siblings<T>() where T : IItemWrapper;
		T Parent<T>() where T : IItemWrapper;
	}
}
