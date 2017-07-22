using Fortis.Items;

namespace Fortis.Fields.GeneralLinkField
{
	public class GeneralLinkField : BaseField, IGeneralLinkField
	{
		public ISitecoreItemUrlGenerator ItemUrlGenerator;
		public ISitecoreMediaItemUrlGenerator MediaItemUrlGenerator;

		public new Sitecore.Data.Fields.LinkField Field
		{
			get => base.Field;
		}

		public override string Value
		{
			get => GenerateUrl();
			set => RawValue = value;
		}
		public string AltText => Field.Title;
		public string Description => Field.Text;
		public bool IsInternal => Field.IsInternal;
		public bool IsMediaLink => Field.IsMediaLink;
		public string Styles => Field.Class;
		public string BrowserTarget => Field.Target;
		public string GenerateUrl()
		{
			switch (Field.LinkType.ToLower())
			{
				case "internal":
					return Field.TargetItem != null ? ItemUrlGenerator.Generate(Field.TargetItem) : string.Empty;
				case "media":
					return Field.TargetItem != null ? MediaItemUrlGenerator.Generate(Field.TargetItem) : string.Empty;
				case "anchor":
					return string.IsNullOrEmpty(Field.Anchor) ? string.Empty : "#" + Field.Anchor;
				case "external":
				case "mailto":
				case "javascript":
				default:
					return Field.Url;
			}
		}
	}
}
