using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using Sitecore.Data;
using System.Web;
using Fortis.Providers;

namespace Fortis.Model.Fields
{
	public class LinkFieldWrapper : FieldWrapper, ILinkFieldWrapper
	{
		public Guid ItemId
		{
			get
			{
				if (string.IsNullOrWhiteSpace(RawValue))
				{
					return Guid.Empty;
				}
				if (ShortID.IsShortID(RawValue))
				{
					return ShortID.Parse(RawValue).ToID().Guid;
				}
				if (ID.IsID(RawValue))
				{
					return ID.Parse(RawValue).Guid;
				}
				return Guid.Parse(RawValue);
			}
		}

		public virtual string Url
		{
			get
			{
			    var target = GetTarget<IItemWrapper>();
				if (target == null)
				{
					return string.Empty;
				}

				return target.GenerateUrl();
			}
		}

		public LinkFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) { }

		public LinkFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, value, spawnProvider) { }

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

		/// <summary>
		/// Gets the target Sitecore Item and spawns a new T object.
		/// </summary>
		/// <typeparam name="T">The generic type to return. Must be of type <see cref="Fortis.Model.IItemWrapper"/></typeparam>
		/// <returns>If the target is not set, returns null, otherwise returns T</returns>
		public virtual T GetTarget<T>() where T : IItemWrapper
		{
			if (string.IsNullOrWhiteSpace(RawValue))
			{
				return default(T);
			}
			if (ShortID.IsShortID(RawValue))
			{
				return GetTarget<T>(ShortID.Parse(RawValue).ToID());
			}
			if (ID.IsID(RawValue))
			{
				return GetTarget<T>(ID.Parse(RawValue));
			}

			return default(T);
		}

		private T GetTarget<T>(ID id) where T : IItemWrapper
		{
			if (ID.IsNullOrEmpty(id))
			{
				return default(T);
			}

			var item = Database.GetItem(id);
			if (item == null)
			{
				return default(T);
			}
			var wrapper = SpawnProvider.FromItem<T>(item);
			return (T)((wrapper is T) ? wrapper : null);
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
