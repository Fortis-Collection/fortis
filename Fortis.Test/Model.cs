namespace Fortis.Tests
{
	using System;
	using Sitecore.Data.Items;
	using Fortis.Model;
	using Fortis.Model.Fields;
	using System.Collections.Generic;
	using Sitecore.ContentSearch;
	using Sitecore.ContentSearch.Linq.Common;
	using Fortis.Providers;

	#region Base Template

	[TemplateMapping("{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}", "InterfaceMap")]
	public partial interface IScBaseTemplate : IItemWrapper
	{
		IBooleanFieldWrapper BaseBooleanField { get; }
		IDateTimeFieldWrapper BaseDateTimeField { get; }
		IFileFieldWrapper BaseFileField { get; }
		IGeneralLinkFieldWrapper BaseGeneralLinkField { get; }
		IImageFieldWrapper BaseImageField { get; }
		ILinkFieldWrapper BaseLinkField { get; }
		IListFieldWrapper BaseListField { get; }
		IRichTextFieldWrapper BaseRichTextField { get; }
		ITextFieldWrapper BaseTextField { get; }
	}

	[PredefinedQuery("template", ComparisonType.Equal, "{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}", typeof(Guid))]
	[TemplateMapping("{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}")]
	public partial class ScBaseTemplate : ItemWrapper, IScBaseTemplate
	{
		private Item _item = null;

		public ScBaseTemplate(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider)
		{
			_item = item;
		}

		public ITextFieldWrapper BaseTextField
		{
			get { throw new NotImplementedException(); }
		}

		public IBooleanFieldWrapper BaseBooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public IDateTimeFieldWrapper BaseDateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public IFileFieldWrapper BaseFileField
		{
			get { throw new NotImplementedException(); }
		}

		public IGeneralLinkFieldWrapper BaseGeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IImageFieldWrapper BaseImageField
		{
			get { throw new NotImplementedException(); }
		}

		public ILinkFieldWrapper BaseLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IListFieldWrapper BaseListField
		{
			get { throw new NotImplementedException(); }
		}

		public IRichTextFieldWrapper BaseRichTextField
		{
			get { throw new NotImplementedException(); }
		}
	}

	#endregion

	#region Template

	[TemplateMapping("{02F5002C-325E-4E5A-9C93-A97724ED3400}", "InterfaceMap")]
	public partial interface IScTemplate : IItemWrapper, IScBaseTemplate
	{
		IBooleanFieldWrapper BooleanField { get; }
		IDateTimeFieldWrapper DateTimeField { get; }
		IFileFieldWrapper FileField { get; }
		IGeneralLinkFieldWrapper GeneralLinkField { get; }
		IImageFieldWrapper ImageField { get; }
		ILinkFieldWrapper LinkField { get; }
		IListFieldWrapper ListField { get; }
		IRichTextFieldWrapper RichTextField { get; }
		ITextFieldWrapper TextField { get; }
	}

	[PredefinedQuery("template", ComparisonType.Equal, "{02F5002C-325E-4E5A-9C93-A97724ED3400}", typeof(Guid))]
	[TemplateMapping("{02F5002C-325E-4E5A-9C93-A97724ED3400}")]
	public partial class ScTemplate : ItemWrapper, IScTemplate
	{
		private Item _item = null;

		public ScTemplate(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider)
		{
			_item = item;
		}

		public IBooleanFieldWrapper BooleanField
		{
			get { return GetField<IBooleanFieldWrapper>("Boolean Field", this["Boolean Field"]); }
		}

		public IDateTimeFieldWrapper DateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public IFileFieldWrapper FileField
		{
			get { throw new NotImplementedException(); }
		}

		public IGeneralLinkFieldWrapper GeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IImageFieldWrapper ImageField
		{
			get { throw new NotImplementedException(); }
		}

		public ILinkFieldWrapper LinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IListFieldWrapper ListField
		{
			get { throw new NotImplementedException(); }
		}

		public IRichTextFieldWrapper RichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public ITextFieldWrapper TextField
		{
			get { throw new NotImplementedException(); }
		}

		public IBooleanFieldWrapper BaseBooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public IDateTimeFieldWrapper BaseDateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public IFileFieldWrapper BaseFileField
		{
			get { throw new NotImplementedException(); }
		}

		public IGeneralLinkFieldWrapper BaseGeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IImageFieldWrapper BaseImageField
		{
			get { throw new NotImplementedException(); }
		}

		public ILinkFieldWrapper BaseLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IListFieldWrapper BaseListField
		{
			get { throw new NotImplementedException(); }
		}

		public IRichTextFieldWrapper BaseRichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public ITextFieldWrapper BaseTextField
		{
			get { throw new NotImplementedException(); }
		}
	}

	#endregion

	#region Base Rendering Parameters Template

	[TemplateMapping("{6DEBF469-8D6D-4FFC-80BF-897518C143B2}", "InterfaceRenderingParameter")]
	public partial interface IScBaseRenderingParametersTemplate : IRenderingParameterWrapper
	{

	}

	[TemplateMapping("{6DEBF469-8D6D-4FFC-80BF-897518C143B2}", "RenderingParameter")]
	public partial class ScBaseRenderingParametersTemplate : RenderingParameterWrapper, IScBaseRenderingParametersTemplate
	{
		public ScBaseRenderingParametersTemplate(Dictionary<string, string> parameters, ISpawnProvider spawnProvider)
			: base(parameters, spawnProvider)
		{

		}
	}

	#endregion

	#region Rendering Parameters Template

	[TemplateMapping("{59CD942B-E316-4ACD-B0A1-AAA19B3C8946}", "InterfaceRenderingParameter")]
	public partial interface IScRenderingParametersTemplate : IRenderingParameterWrapper, IScBaseRenderingParametersTemplate
	{

	}

	[TemplateMapping("{59CD942B-E316-4ACD-B0A1-AAA19B3C8946}", "RenderingParameter")]
	public partial class ScRenderingParametersTemplate : RenderingParameterWrapper, IScRenderingParametersTemplate
	{
		public ScRenderingParametersTemplate(Dictionary<string, string> parameters, ISpawnProvider spawnProvider)
			: base(parameters, spawnProvider)
		{
			
		}
	}

	#endregion

	#region Unmapped Template

	public partial interface IScUnmappedTemplate : IItemWrapper, IScBaseTemplate
	{
		IBooleanFieldWrapper BooleanField { get; }
		IDateTimeFieldWrapper DateTimeField { get; }
		IFileFieldWrapper FileField { get; }
		IGeneralLinkFieldWrapper GeneralLinkField { get; }
		IImageFieldWrapper ImageField { get; }
		ILinkFieldWrapper LinkField { get; }
		IListFieldWrapper ListField { get; }
		IRichTextFieldWrapper RichTextField { get; }
		ITextFieldWrapper TextField { get; }
	}

	public partial class ScUnmappedTemplateTemplate : ItemWrapper, IScUnmappedTemplate
	{
		private Item _item = null;

		public ScUnmappedTemplateTemplate(Item item, ISpawnProvider spawnProvider)
			: base(item, spawnProvider)
		{
			_item = item;
		}

		public IBooleanFieldWrapper BooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public IDateTimeFieldWrapper DateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public IFileFieldWrapper FileField
		{
			get { throw new NotImplementedException(); }
		}

		public IGeneralLinkFieldWrapper GeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IImageFieldWrapper ImageField
		{
			get { throw new NotImplementedException(); }
		}

		public ILinkFieldWrapper LinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IListFieldWrapper ListField
		{
			get { throw new NotImplementedException(); }
		}

		public IRichTextFieldWrapper RichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public ITextFieldWrapper TextField
		{
			get { throw new NotImplementedException(); }
		}

		public IBooleanFieldWrapper BaseBooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public IDateTimeFieldWrapper BaseDateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public IFileFieldWrapper BaseFileField
		{
			get { throw new NotImplementedException(); }
		}

		public IGeneralLinkFieldWrapper BaseGeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IImageFieldWrapper BaseImageField
		{
			get { throw new NotImplementedException(); }
		}

		public ILinkFieldWrapper BaseLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public IListFieldWrapper BaseListField
		{
			get { throw new NotImplementedException(); }
		}

		public IRichTextFieldWrapper BaseRichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public ITextFieldWrapper BaseTextField
		{
			get { throw new NotImplementedException(); }
		}
	}

	#endregion
}
