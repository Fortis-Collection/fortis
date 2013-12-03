using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using System.Web;
using Sitecore.Data.Items;

namespace Fortis.Model.Fields
{
	public class FieldWrapper : IFieldWrapper
	{
		private bool _modified;
		private Field _field;
		private ItemWrapper _item;
		private string _rawValue;
		private string _key;

		public FieldWrapper(Field field)
		{
			Sitecore.Diagnostics.Assert.ArgumentNotNull(field, "field");

			_modified = false;
			_field = field;
		}

		public FieldWrapper(string key, ref ItemWrapper item, string value = null)
		{
			Sitecore.Diagnostics.Assert.ArgumentNotNull(key, "key");
			Sitecore.Diagnostics.Assert.ArgumentNotNull(item, "item");

			_key = key;
			_item = item;
			_rawValue = value;
		}

		protected Field Field
		{
			get
			{
				if (_field == null && _item != null)
				{
					_field = _item.Item.Fields[_key];

					if (_field == null)
					{
						throw new Exception("Fortis: Field " + _key + " does not exist in item " + _item.ItemID);
					}

					if (!Spawn.IsCompatibleFieldType(_field.Type, this.GetType()))
					{
						throw new Exception("Fortis: Field " + _key + " of type " + _field.Type + " for item " + _item.ItemID + " is not compatible with Fortis type " + this.GetType());
					}
				}

				return _field;
			}
		}

		public bool Modified
		{
			get { return _modified; }
		}

		public object Original
		{
			get { return Field; }
		}

		public string RawValue
		{
			get
			{
				if (_field == null)
				{
					return _rawValue;
				}

				return Field.Value;
			}
			set
			{
				if (!Field.Item.Editing.IsEditing)
				{
					Field.Item.Editing.BeginEdit();
				}

				Field.Value = value;
				_rawValue = value;
				_modified = true;
			}
		}

		public virtual IHtmlString Render(string parameters = null, bool editing = true)
		{
			return new HtmlString(editing ? FieldRenderer.Render(Field.Item, Field.Key, parameters ?? string.Empty) : RawValue);
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
