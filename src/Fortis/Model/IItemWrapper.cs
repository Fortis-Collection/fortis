using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Converters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using Sitecore.Data;

namespace Fortis.Model
{
	public partial interface IItemWrapper : IWrapper
	{
		bool IsLazy { get; }

		[IndexField("_database")]
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
		[IndexField("_isstandardvalues")]
		bool IsStandardValues { get; }
		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_template")]
		Guid TemplateId { get; }
		[IndexField("_templates")]
		IEnumerable<Guid> TemplateIds { get; }
        [IndexField("_path")]
        string LongID { get; set; }
        [IndexField("_content")]
        string SearchContent { get; set; }

		/// <summary>
		/// Gets a value indicating whether the item has versions in the current context language.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has version; otherwise, <c>false</c>.
		/// </value>
		bool HasVersion { get; }
		bool HasChildren { get; }
		int ChildCount { get; }
		string SearchTitle { get; }
		void Save();
		void Save(bool silent);
		void Save(bool updateStatistics, bool silent);
		void Delete();
		void Publish();
		void Publish(bool children);
		string GenerateUrl();
		string GenerateUrl(bool includeHostname);
		string GenerateUrl(string language);
		IEnumerable<T> Children<T>(bool recursive = false) where T : IItemWrapper;
		IEnumerable<T> Siblings<T>() where T : IItemWrapper;
		T Parent<T>(bool ancestors = true) where T : IItemWrapper;
	    T ParentOrSelf<T>() where T : IItemWrapper;
	    T Ancestor<T>() where T : IItemWrapper;
	    T AncestorOrSelf<T>() where T : IItemWrapper;
	}
}
