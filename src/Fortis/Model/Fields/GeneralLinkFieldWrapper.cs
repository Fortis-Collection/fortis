using System;
using Sitecore.Data.Fields;
using Fortis.Providers;
using Sitecore.Data;
using System.Web;
using Sitecore.Web.UI.WebControls;

namespace Fortis.Model.Fields
{
	public class GeneralLinkFieldWrapper : FieldWrapper, IGeneralLinkFieldWrapper
	{
		protected LinkField LinkField
		{
			get { return Field; }
		}

		public string AlternateText
		{
			get { return LinkField.Title; }
		}

		public string Description
		{
			get { return LinkField.Text; }
		}

		public bool IsInternal
		{
			get { return LinkField.IsInternal; }
		}

		public bool IsMediaLink
		{
			get { return LinkField.IsMediaLink; }
		}

		public string Styles
		{
			get { return LinkField.Class; }
		}

		public string Target
		{
			get { return LinkField.Target; }
		}

		public string Url
		{
			get
			{
				if (IsMediaLink)
				{
					return Sitecore.Resources.Media.MediaManager.GetMediaUrl(LinkField.TargetItem);
				}

				if (IsInternal)
				{
					var target = GetTarget<IItemWrapper>();

					if (target != null)
					{
						return target.GenerateUrl();
					}
				}

				return LinkField.Url;
			}
		}

		public GeneralLinkFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) {	}

		public GeneralLinkFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

		public Guid ItemId
		{
			get
			{
				return LinkField.TargetID.ToGuid();
			}
		}

		public IHtmlString Render(LinkFieldWrapperOptions options)
		{
			var fieldRenderer = new FieldRenderer();

			if (options != null)
			{
				if (!string.IsNullOrWhiteSpace(options.InnerHtml))
				{
					fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.InnerHtmlParameterName, options.InnerHtml);
				}

				if (!string.IsNullOrWhiteSpace(options.Css))
				{
					fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.CssParameterName, options.Css);
				}

				fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.OptionsParameterName, string.Empty);
				fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.HrefDefaultParameterName, options.DisplayHrefByDefault.ToString());
				fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.EditorCssParameterName, options.IncludeContentEditorCss.ToString());
			}

			fieldRenderer.Item = Field.Item;
			fieldRenderer.FieldName = Field.Key;

			var result = fieldRenderer.RenderField();
			return new HtmlString(result.FirstPart + result.LastPart);
		}

		public T GetTarget<T>() where T : IItemWrapper
		{
			if (IsInternal || IsMediaLink)
			{
				var wrapper = SpawnProvider.FromItem<T>(LinkField.TargetItem);
				return (T)((wrapper is T) ? wrapper : null);;
			}

			return default(T);
		}

		public static implicit operator string(GeneralLinkFieldWrapper field)
		{
			return field.Url;
		}

		public string Value
		{
			get { return Url; }
		}
	}
}
