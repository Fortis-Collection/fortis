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
		string LanguageName { get; }
		string ItemLocation { get; }
		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_group")]
		Guid ItemID { get; }
		string ItemShortID { get; }
		string ItemName { get; }
		[TypeConverter(typeof(IndexFieldGuidValueConverter)), IndexField("_template")]
		Guid TemplateId { get; }
		bool HasChildren { get; }
		int ChildCount { get; }
		string SearchTitle { get; }
		void Save();
		void Delete();
		void Publish();
		void Publish(bool children);
		string GenerateUrl();
		string GenerateUrl(bool includeHostname);
		T Parent<T>() where T : IItemWrapper;
	}
}
