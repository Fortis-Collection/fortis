namespace Fortis.Pipelines.RenderField
{
	using System.Linq;
	using Sitecore.Pipelines.RenderField;
	using Sitecore;
	using Sitecore.Xml.Xsl;
	using Sitecore.Data.Fields;
	using Fortis.Model.Fields;

	public class LinkRenderer
	{
		public void Process([NotNull] RenderFieldArgs args)
		{
			if (args.RenderParameters.ContainsKey(LinkFieldWrapperOptions.OptionsParameterName))
			{
				var renderer = new Renderers.LinkRenderer(args.Item)
				{
					InnerHTML = args.RenderParameters[LinkFieldWrapperOptions.InnerHtmlParameterName],
					CSS = args.RenderParameters[LinkFieldWrapperOptions.CssParameterName],
					IncludeContentEditorCss = ConvertParameterToBool(args.RenderParameters[LinkFieldWrapperOptions.EditorCssParameterName]),
					DisplayHrefByDefault = ConvertParameterToBool(args.RenderParameters[LinkFieldWrapperOptions.HrefDefaultParameterName]),
					FieldName = args.FieldName,
					FieldValue = args.FieldValue,
					Parameters = args.Parameters,
					RawParameters = args.RawParameters
				};

				args.DisableWebEditContentEditing = true;

				RenderFieldResult result = renderer.Render();

				args.Result.FirstPart = result.FirstPart;
				args.Result.LastPart = result.LastPart;
			}
		}

		private bool ConvertParameterToBool(string parameter)
		{
			var boolParameter = false;

			bool.TryParse(parameter, out boolParameter);

			return boolParameter;
		}
	}
}
