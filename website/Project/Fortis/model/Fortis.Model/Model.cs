/*
 * Fortis Template Models for Poker Central
 *
 * Generated at 01/11/2017 18:40:30
 */

// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantExtendsListEntry
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using Sitecore.Data.Items;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq.Common;
using Fortis.Model;
using Fortis.Model.Fields;
using Fortis.Providers;

#region Content List (Ignite)
namespace LM.Model.Templates.Ignite
{
	/* 
	 * Base Templates Count:	[0] 
	 * Base Template Ids Count: [2] 
	 */

	/// <summary><para>Template: Content List</para><para>ID: {2a4cf0c6-e29e-49fe-b18f-6021d2e23122}</para><para>/sitecore/templates/Fortis/Content/Content List</para></summary>
	[TemplateMapping("{2a4cf0c6-e29e-49fe-b18f-6021d2e23122}", "InterfaceMap")]
	public partial interface IContentList : IItemWrapper
	{

    	/// <summary><para>Template: Content List</para><para>Field: Pages</para><para>Data type: Multilist</para></summary>
		[IndexField("pages")]
		IListFieldWrapper Pages { get; }

    	/// <summary><para>Template: Content List</para><para>Field: Pages</para><para>Data type: Multilist</para></summary>
		[IndexField("pages")]
		IEnumerable<Guid> PagesValue { get; }
	}
	
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{2a4cf0c6-e29e-49fe-b18f-6021d2e23122}", typeof(Guid))]
	[TemplateMapping("{2a4cf0c6-e29e-49fe-b18f-6021d2e23122}")]
	public partial class ContentList : ItemWrapper, IContentList
	{
		public ContentList(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider) { }

		public ContentList(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		/// <summary><para>Field: Pages</para><para>Data type: Multilist</para></summary>
		[IndexField("pages")]
		public virtual IListFieldWrapper Pages => this.GetField<ListFieldWrapper>("Pages");

		/// <summary>
		/// Enables searching on the Pages field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("pages")]
 		public IEnumerable<Guid> PagesValue => this.Pages.Value;
	}
}
#endregion
#region Content (Ignite)
namespace LM.Model.Templates.Ignite
{
	/* 
	 * Base Templates Count:	[0] 
	 * Base Template Ids Count: [2] 
	 */

	/// <summary><para>Template: Content</para><para>ID: {1dcb8dfb-bd0b-424b-8943-1f36ad5474cc}</para><para>/sitecore/templates/Fortis/Content/Content</para></summary>
	[TemplateMapping("{1dcb8dfb-bd0b-424b-8943-1f36ad5474cc}", "InterfaceMap")]
	public partial interface IContent : IItemWrapper
	{

    	/// <summary><para>Template: Content</para><para>Field: Content Body</para><para>Data type: Rich Text</para></summary>
		[IndexField("content_body")]
		IRichTextFieldWrapper ContentBody { get; }

    	/// <summary><para>Template: Content</para><para>Field: Content Body</para><para>Data type: Rich Text</para></summary>
		[IndexField("content_body")]
		string ContentBodyValue { get; }
	}
	
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{1dcb8dfb-bd0b-424b-8943-1f36ad5474cc}", typeof(Guid))]
	[TemplateMapping("{1dcb8dfb-bd0b-424b-8943-1f36ad5474cc}")]
	public partial class Content : ItemWrapper, IContent
	{
		public Content(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider) { }

		public Content(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		/// <summary><para>Field: ContentBody</para><para>Data type: Rich Text</para></summary>
		[IndexField("content_body")]
		public virtual IRichTextFieldWrapper ContentBody => this.GetField<RichTextFieldWrapper>("Content Body");

		/// <summary>
		/// Enables searching on the ContentBody field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("content_body")]
 		public string ContentBodyValue => this.ContentBody.Value;
	}
}
#endregion
#region Meta Data (Ignite)
namespace LM.Model.Templates.Ignite
{
	/* 
	 * Base Templates Count:	[0] 
	 * Base Template Ids Count: [2] 
	 */

	/// <summary><para>Template: Meta Data</para><para>ID: {744ecf2a-cc94-4754-8bda-4ec68ce14fba}</para><para>/sitecore/templates/Fortis/Content/Meta Data</para></summary>
	[TemplateMapping("{744ecf2a-cc94-4754-8bda-4ec68ce14fba}", "InterfaceMap")]
	public partial interface IMetaData : IItemWrapper
	{

    	/// <summary><para>Template: Meta Data</para><para>Field: Browser Title</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("browser_title")]
		ITextFieldWrapper BrowserTitle { get; }

    	/// <summary><para>Template: Meta Data</para><para>Field: Browser Title</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("browser_title")]
		string BrowserTitleValue { get; }

    	/// <summary><para>Template: Meta Data</para><para>Field: Meta Description</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("meta_description")]
		ITextFieldWrapper MetaDescription { get; }

    	/// <summary><para>Template: Meta Data</para><para>Field: Meta Description</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("meta_description")]
		string MetaDescriptionValue { get; }
	}
	
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{744ecf2a-cc94-4754-8bda-4ec68ce14fba}", typeof(Guid))]
	[TemplateMapping("{744ecf2a-cc94-4754-8bda-4ec68ce14fba}")]
	public partial class MetaData : ItemWrapper, IMetaData
	{
		public MetaData(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider) { }

		public MetaData(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		/// <summary><para>Field: BrowserTitle</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("browser_title")]
		public virtual ITextFieldWrapper BrowserTitle => this.GetField<TextFieldWrapper>("Browser Title");

		/// <summary>
		/// Enables searching on the BrowserTitle field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("browser_title")]
 		public string BrowserTitleValue => this.BrowserTitle.Value;

		/// <summary><para>Field: MetaDescription</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("meta_description")]
		public virtual ITextFieldWrapper MetaDescription => this.GetField<TextFieldWrapper>("Meta Description");

		/// <summary>
		/// Enables searching on the MetaDescription field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("meta_description")]
 		public string MetaDescriptionValue => this.MetaDescription.Value;
	}
}
#endregion
#region Content Page (Ignite)
namespace LM.Model.Templates.Ignite
{
	/* 
	 * Base Templates Count:	[3] 
	 * Base Template Ids Count: [5] 
	 */

	/// <summary><para>Template: Content Page</para><para>ID: {53a47e66-b7f9-45bd-90e4-6ea9248587a8}</para><para>/sitecore/templates/Fortis/Page Types/Content Page</para></summary>
	[TemplateMapping("{53a47e66-b7f9-45bd-90e4-6ea9248587a8}", "InterfaceMap")]
	public partial interface IContentPage : IItemWrapper, LM.Model.Templates.Ignite.IContent, LM.Model.Templates.Ignite.IMetaData, LM.Model.Templates.Ignite.IBasePage
	{
	}
	
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{53a47e66-b7f9-45bd-90e4-6ea9248587a8}", typeof(Guid))]
	[TemplateMapping("{53a47e66-b7f9-45bd-90e4-6ea9248587a8}")]
	public partial class ContentPage : ItemWrapper, IContentPage
	{
		public ContentPage(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider) { }

		public ContentPage(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		/// <summary><para>Field: BrowserTitle</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("browser_title")]
		public virtual ITextFieldWrapper BrowserTitle => this.GetField<TextFieldWrapper>("Browser Title");

		/// <summary>
		/// Enables searching on the BrowserTitle field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("browser_title")]
 		public string BrowserTitleValue => this.BrowserTitle.Value;

		/// <summary><para>Field: ContentBody</para><para>Data type: Rich Text</para></summary>
		[IndexField("content_body")]
		public virtual IRichTextFieldWrapper ContentBody => this.GetField<RichTextFieldWrapper>("Content Body");

		/// <summary>
		/// Enables searching on the ContentBody field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("content_body")]
 		public string ContentBodyValue => this.ContentBody.Value;

		/// <summary><para>Field: MetaDescription</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("meta_description")]
		public virtual ITextFieldWrapper MetaDescription => this.GetField<TextFieldWrapper>("Meta Description");

		/// <summary>
		/// Enables searching on the MetaDescription field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("meta_description")]
 		public string MetaDescriptionValue => this.MetaDescription.Value;
	}
}
#endregion
#region SiteRoot (Ignite)
namespace LM.Model.Templates.Ignite
{
	/* 
	 * Base Templates Count:	[3] 
	 * Base Template Ids Count: [5] 
	 */

	/// <summary><para>Template: SiteRoot</para><para>ID: {e2c4f1ff-88c7-48b5-b291-72ce8ecd9242}</para><para>/sitecore/templates/Fortis/Page Types/SiteRoot</para></summary>
	[TemplateMapping("{e2c4f1ff-88c7-48b5-b291-72ce8ecd9242}", "InterfaceMap")]
	public partial interface ISiteRoot : IItemWrapper, LM.Model.Templates.Ignite.IContent, LM.Model.Templates.Ignite.IMetaData, LM.Model.Templates.Ignite.IBasePage
	{
	}
	
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{e2c4f1ff-88c7-48b5-b291-72ce8ecd9242}", typeof(Guid))]
	[TemplateMapping("{e2c4f1ff-88c7-48b5-b291-72ce8ecd9242}")]
	public partial class SiteRoot : ItemWrapper, ISiteRoot
	{
		public SiteRoot(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider) { }

		public SiteRoot(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		/// <summary><para>Field: BrowserTitle</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("browser_title")]
		public virtual ITextFieldWrapper BrowserTitle => this.GetField<TextFieldWrapper>("Browser Title");

		/// <summary>
		/// Enables searching on the BrowserTitle field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("browser_title")]
 		public string BrowserTitleValue => this.BrowserTitle.Value;

		/// <summary><para>Field: ContentBody</para><para>Data type: Rich Text</para></summary>
		[IndexField("content_body")]
		public virtual IRichTextFieldWrapper ContentBody => this.GetField<RichTextFieldWrapper>("Content Body");

		/// <summary>
		/// Enables searching on the ContentBody field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("content_body")]
 		public string ContentBodyValue => this.ContentBody.Value;

		/// <summary><para>Field: MetaDescription</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("meta_description")]
		public virtual ITextFieldWrapper MetaDescription => this.GetField<TextFieldWrapper>("Meta Description");

		/// <summary>
		/// Enables searching on the MetaDescription field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("meta_description")]
 		public string MetaDescriptionValue => this.MetaDescription.Value;
	}
}
#endregion
#region _BasePage (Ignite)
namespace LM.Model.Templates.Ignite
{
	/* 
	 * Base Templates Count:	[2] 
	 * Base Template Ids Count: [4] 
	 */

	/// <summary><para>Template: _BasePage</para><para>ID: {704167fe-a2a0-44bc-9e40-a9bf79d02cb1}</para><para>/sitecore/templates/Fortis/Page Types/_BasePage</para></summary>
	[TemplateMapping("{704167fe-a2a0-44bc-9e40-a9bf79d02cb1}", "InterfaceMap")]
	public partial interface IBasePage : IItemWrapper, LM.Model.Templates.Ignite.IContent, LM.Model.Templates.Ignite.IMetaData
	{
	}
	
	[PredefinedQuery("TemplateId", ComparisonType.Equal, "{704167fe-a2a0-44bc-9e40-a9bf79d02cb1}", typeof(Guid))]
	[TemplateMapping("{704167fe-a2a0-44bc-9e40-a9bf79d02cb1}")]
	public partial class BasePage : ItemWrapper, IBasePage
	{
		public BasePage(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider) { }

		public BasePage(Guid id, Dictionary<string, object> lazyFields, ISpawnProvider spawnProvider)
			: base(id, lazyFields, spawnProvider) { }

		/// <summary><para>Field: BrowserTitle</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("browser_title")]
		public virtual ITextFieldWrapper BrowserTitle => this.GetField<TextFieldWrapper>("Browser Title");

		/// <summary>
		/// Enables searching on the BrowserTitle field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("browser_title")]
 		public string BrowserTitleValue => this.BrowserTitle.Value;

		/// <summary><para>Field: ContentBody</para><para>Data type: Rich Text</para></summary>
		[IndexField("content_body")]
		public virtual IRichTextFieldWrapper ContentBody => this.GetField<RichTextFieldWrapper>("Content Body");

		/// <summary>
		/// Enables searching on the ContentBody field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("content_body")]
 		public string ContentBodyValue => this.ContentBody.Value;

		/// <summary><para>Field: MetaDescription</para><para>Data type: Single-Line Text</para></summary>
		[IndexField("meta_description")]
		public virtual ITextFieldWrapper MetaDescription => this.GetField<TextFieldWrapper>("Meta Description");

		/// <summary>
		/// Enables searching on the MetaDescription field. DO NOT USE FOR POPULATING FIELDS OR RENDERING CONTENT. ONLY USE FOR SITECORE SEARCH API
		/// </summary>
		[IndexField("meta_description")]
 		public string MetaDescriptionValue => this.MetaDescription.Value;
	}
}
#endregion


// Generated in 119ms
// Found 6 templates in 1 folders [C:\Projects\fortis\website\..\website\Project\Fortis\serialization]
