namespace Fortis.Fields.GeneralLinkField
{
	public class GeneralLinkField : BaseField, IGeneralLinkField
	{
		public new Sitecore.Data.Fields.LinkField Field
		{
			get { return base.Field; }
		}

		public string AltText => Field.Title;

		public string Description => Field.Text;

		public bool IsInternal => Field.IsInternal;

		public bool IsMediaLink => Field.IsMediaLink;

		public string Styles => Field.Class;

		public string BrowserTarget => Field.Target;

		public string Url => Field.Url;
	}
}
