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
		Guid ItemID { get; }
		string ItemShortID { get; }
		[Obsolete("Use Name property instead")]
		string ItemName { get; }
		string Name { get; }
		string DisplayName { get; }
		bool IsLatestVersion { get; }
		Guid TemplateId { get; }
		IEnumerable<Guid> TemplateIds { get; }
        string LongID { get; set; }
        string SearchContent { get; set; }
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
		T Parent<T>(bool ancestors = true) where T : IItemWrapper;
	    T ParentOrSelf<T>() where T : IItemWrapper;
	    T Ancestor<T>() where T : IItemWrapper;
	    T AncestorOrSelf<T>() where T : IItemWrapper;
	}
}
