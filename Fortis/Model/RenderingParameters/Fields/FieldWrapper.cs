using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using System.Web;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class FieldWrapper : IFieldWrapper
	{
		protected string _value;

		public bool Modified
		{
			get { throw new NotImplementedException(); }
		}

		public object Original
		{
			get { return _value; }
		}

		public string RawValue
		{
			get
			{
				return _value;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public FieldWrapper(string value)
		{
			_value = value;
		}

		public virtual IHtmlString Render()
		{
			throw new NotImplementedException();
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
