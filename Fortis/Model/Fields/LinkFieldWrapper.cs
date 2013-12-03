using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;

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
			: base(field)
		{

		}

		public string Render(LinkFieldWrapperOptions options)
		{
			var fieldRenderer = new FieldRenderer();

			if (options.HTML.Length > 0)
			{
				fieldRenderer.RenderParameters.Add("innerHTML", options.HTML);
			}

			if (options.CSS.Length > 0)
			{
				fieldRenderer.RenderParameters.Add("css", options.CSS);
			}

			fieldRenderer.Item = Field.Item;
			fieldRenderer.FieldName = Field.Key;

			var result = fieldRenderer.RenderField();

			return result.FirstPart + result.LastPart;
		}

		public virtual T GetTarget<T>() where T : IItemWrapper
		{
            var item = Sitecore.Context.Database.GetItem(RawValue);
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
