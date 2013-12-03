using System;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;
using System.Web;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class FieldWrapper : IFieldWrapper
	{
		protected string Value;

		public virtual bool Modified
		{
			get { throw new NotImplementedException(); }
		}

		public virtual object Original
		{
			get { return Value; }
		}

		public virtual string RawValue
		{
			get
			{
				return Value;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public FieldWrapper(string value)
		{
			Value = value;
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

		public virtual string ToHtmlString()
        {
            return Render().ToString();
        }
	}
}
