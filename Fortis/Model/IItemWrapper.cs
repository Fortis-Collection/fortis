using System;
using System.Collections.Generic;

namespace Fortis.Model
{
	public partial interface IItemWrapper : IWrapper
	{
		string DatabaseName { get; }
		string LanguageName { get; }
		string ItemLocation { get; }
		Guid ItemID { get; }
		string ItemShortID { get; }
		string ItemName { get; }
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
