namespace Fortis.Test
{
	using System;
	using Sitecore.Data.Items;
	using Fortis.Model;
	using Fortis.Model.Fields;
	using System.Collections.Generic;

	#region Base Template

	[TemplateMapping("{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}", "InterfaceMap")]
	public partial interface IScBaseTemplate : IItemWrapper
	{
		BooleanFieldWrapper BaseBooleanField { get; }
		DateTimeFieldWrapper BaseDateTimeField { get; }
		FileFieldWrapper BaseFileField { get; }
		GeneralLinkFieldWrapper BaseGeneralLinkField { get; }
		ImageFieldWrapper BaseImageField { get; }
		LinkFieldWrapper BaseLinkField { get; }
		ListFieldWrapper BaseListField { get; }
		RichTextFieldWrapper BaseRichTextField { get; }
		TextFieldWrapper BaseTextField { get; }
	}

	[TemplateMapping("{AF49395C-74BB-4ACF-8E01-F2B5BEECA8FE}")]
	public partial class ScBaseTemplate : ItemWrapper, IScBaseTemplate
	{
		private Item _item = null;

		public ScBaseTemplate(Item item)
			: base(item)
		{
			_item = item;
		}

		public TextFieldWrapper BaseTextField
		{
			get { throw new NotImplementedException(); }
		}

		public BooleanFieldWrapper BaseBooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public DateTimeFieldWrapper BaseDateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public FileFieldWrapper BaseFileField
		{
			get { throw new NotImplementedException(); }
		}

		public GeneralLinkFieldWrapper BaseGeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ImageFieldWrapper BaseImageField
		{
			get { throw new NotImplementedException(); }
		}

		public LinkFieldWrapper BaseLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ListFieldWrapper BaseListField
		{
			get { throw new NotImplementedException(); }
		}

		public RichTextFieldWrapper BaseRichTextField
		{
			get { throw new NotImplementedException(); }
		}
	}

	#endregion

	#region Template

	[TemplateMapping("{02F5002C-325E-4E5A-9C93-A97724ED3400}", "InterfaceMap")]
	public partial interface IScTemplate : IItemWrapper, IScBaseTemplate
	{
		BooleanFieldWrapper BooleanField { get; }
		DateTimeFieldWrapper DateTimeField { get; }
		FileFieldWrapper FileField { get; }
		GeneralLinkFieldWrapper GeneralLinkField { get; }
		ImageFieldWrapper ImageField { get; }
		LinkFieldWrapper LinkField { get; }
		ListFieldWrapper ListField { get; }
		RichTextFieldWrapper RichTextField { get; }
		TextFieldWrapper TextField { get; }
	}

	[TemplateMapping("{02F5002C-325E-4E5A-9C93-A97724ED3400}")]
	public partial class ScTemplate : ItemWrapper, IScTemplate
	{
		private Item _item = null;

		public ScTemplate(Item item)
			: base(item)
		{
			_item = item;
		}

		public BooleanFieldWrapper BooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public DateTimeFieldWrapper DateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public FileFieldWrapper FileField
		{
			get { throw new NotImplementedException(); }
		}

		public GeneralLinkFieldWrapper GeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ImageFieldWrapper ImageField
		{
			get { throw new NotImplementedException(); }
		}

		public LinkFieldWrapper LinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ListFieldWrapper ListField
		{
			get { throw new NotImplementedException(); }
		}

		public RichTextFieldWrapper RichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public TextFieldWrapper TextField
		{
			get { throw new NotImplementedException(); }
		}

		public BooleanFieldWrapper BaseBooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public DateTimeFieldWrapper BaseDateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public FileFieldWrapper BaseFileField
		{
			get { throw new NotImplementedException(); }
		}

		public GeneralLinkFieldWrapper BaseGeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ImageFieldWrapper BaseImageField
		{
			get { throw new NotImplementedException(); }
		}

		public LinkFieldWrapper BaseLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ListFieldWrapper BaseListField
		{
			get { throw new NotImplementedException(); }
		}

		public RichTextFieldWrapper BaseRichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public TextFieldWrapper BaseTextField
		{
			get { throw new NotImplementedException(); }
		}
	}

	#endregion

	#region Rendering Parameters Template

	[TemplateMapping("{59CD942B-E316-4ACD-B0A1-AAA19B3C8946}", "InterfaceRenderingParameter")]
	public partial interface IScRenderingParametersTemplate : IRenderingParameterWrapper
	{

	}

	[TemplateMapping("{59CD942B-E316-4ACD-B0A1-AAA19B3C8946}", "RenderingParameter")]
	public partial class ScRenderingParametersTemplate : RenderingParameterWrapper, IScRenderingParametersTemplate
	{
		public ScRenderingParametersTemplate(Dictionary<string, string> parameters)
			: base(parameters)
		{
			
		}
	}

	#endregion

	#region Unmapped Template

	public partial interface IScUnmappedTemplate : IItemWrapper, IScBaseTemplate
	{
		BooleanFieldWrapper BooleanField { get; }
		DateTimeFieldWrapper DateTimeField { get; }
		FileFieldWrapper FileField { get; }
		GeneralLinkFieldWrapper GeneralLinkField { get; }
		ImageFieldWrapper ImageField { get; }
		LinkFieldWrapper LinkField { get; }
		ListFieldWrapper ListField { get; }
		RichTextFieldWrapper RichTextField { get; }
		TextFieldWrapper TextField { get; }
	}

	public partial class ScUnmappedTemplateTemplate : ItemWrapper, IScUnmappedTemplate
	{
		private Item _item = null;

		public ScUnmappedTemplateTemplate(Item item)
			: base(item)
		{
			_item = item;
		}

		public BooleanFieldWrapper BooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public DateTimeFieldWrapper DateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public FileFieldWrapper FileField
		{
			get { throw new NotImplementedException(); }
		}

		public GeneralLinkFieldWrapper GeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ImageFieldWrapper ImageField
		{
			get { throw new NotImplementedException(); }
		}

		public LinkFieldWrapper LinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ListFieldWrapper ListField
		{
			get { throw new NotImplementedException(); }
		}

		public RichTextFieldWrapper RichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public TextFieldWrapper TextField
		{
			get { throw new NotImplementedException(); }
		}

		public BooleanFieldWrapper BaseBooleanField
		{
			get { throw new NotImplementedException(); }
		}

		public DateTimeFieldWrapper BaseDateTimeField
		{
			get { throw new NotImplementedException(); }
		}

		public FileFieldWrapper BaseFileField
		{
			get { throw new NotImplementedException(); }
		}

		public GeneralLinkFieldWrapper BaseGeneralLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ImageFieldWrapper BaseImageField
		{
			get { throw new NotImplementedException(); }
		}

		public LinkFieldWrapper BaseLinkField
		{
			get { throw new NotImplementedException(); }
		}

		public ListFieldWrapper BaseListField
		{
			get { throw new NotImplementedException(); }
		}

		public RichTextFieldWrapper BaseRichTextField
		{
			get { throw new NotImplementedException(); }
		}

		public TextFieldWrapper BaseTextField
		{
			get { throw new NotImplementedException(); }
		}
	}

	#endregion
}
