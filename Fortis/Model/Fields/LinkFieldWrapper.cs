using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data;
using System.Web;

namespace Fortis.Model.Fields
{
	public class LinkFieldWrapper : FieldWrapper, ILinkFieldWrapper
	{
		private IItemWrapper _target;

		protected virtual IItemWrapper Target
		{
			get
			{
				if (_target == null)
				{
					_target = GetTarget<IItemWrapper>();
				}

				return _target;
			}
		}

		public Guid ItemId
		{
			get
			{
				if (ShortID.IsShortID(RawValue))
				{
					return ShortID.Parse(RawValue).ToID().Guid;
				}
				else if (ID.IsID(RawValue))
				{
					return ID.Parse(RawValue).Guid;
				}
				else
				{
					return Guid.Parse(RawValue);
				}
			}
		}

		public virtual string Url
		{
			get
			{
				if (Target == null)
				{
					return string.Empty;
				}

				return Target.GenerateUrl();
			}
		}

		public LinkFieldWrapper(Field field)
			: base(field) { }

		public LinkFieldWrapper(string key, ref ItemWrapper item, string value = null)
			: base(key, ref item, value) { }

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
				fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.HrefDefaultParameterName, options.DisplayHrefByDefault);
				fieldRenderer.RenderParameters.Add(LinkFieldWrapperOptions.EditorCssParameterName, options.IncludeContentEditorCss);
			}

			fieldRenderer.Item = Field.Item;
			fieldRenderer.FieldName = Field.Key;

			var result = fieldRenderer.RenderField();

			return new HtmlString(result.FirstPart + result.LastPart);
		}

		public virtual T GetTarget<T>() where T : IItemWrapper
		{
			if (ShortID.IsShortID(RawValue))
			{
				return GetTarget<T>(ShortID.Parse(RawValue).ToID());
			}
			else if (ID.IsID(RawValue))
			{
				return GetTarget<T>(ID.Parse(RawValue));
			}

			return default(T);
		}

		private T GetTarget<T>(ID id) where T : IItemWrapper
		{
			var item = Sitecore.Context.Database.GetItem(id);
			if (item != null)
			{
				var wrapper = Spawn.FromItem<T>(item);
				return (T)((wrapper is T) ? wrapper : null); ;
			}

			return default(T);
		}

		public static implicit operator string(LinkFieldWrapper field)
		{
			return field.Url;
		}

		public string Value
		{
			get { return Url; }
		}
	}
}
