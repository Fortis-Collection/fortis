using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using System.Web;

namespace Fortis.Model.Fields
{
	public class FieldWrapper : IFieldWrapper
	{
		private bool _modified;
		private Field _field;

		protected Field Field
		{
			get { return _field; }
		}

		public bool Modified
		{
			get { return _modified; }
		}

		public object Original
		{
			get { return _field; }
		}

		public string RawValue
		{
			get
			{
				return _field.Value;
			}
			set
			{
				if (!_field.Item.Editing.IsEditing)
				{
					_field.Item.Editing.BeginEdit();
				}

				_field.Value = value;
				_modified = true;
			}
		}

		public FieldWrapper(Field field)
		{
			Sitecore.Diagnostics.Assert.ArgumentNotNull(field, "field");

			_modified = false;
			_field = field;
		}

		public virtual IHtmlString Render()
		{
			return new HtmlString(FieldRenderer.Render(_field.Item, _field.Key));
		}

		public virtual IHtmlString Render(string parameters)
		{
			return new HtmlString(FieldRenderer.Render(_field.Item, _field.Key, parameters));
		}

		public override string ToString()
		{
			return RawValue;
		}

        public static implicit operator string(FieldWrapper field)
		{
			return field.RawValue;
		}

        public string ToHtmlString()
        {
            return Render().ToString();
        }
    }
}
