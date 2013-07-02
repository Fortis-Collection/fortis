using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Sitecore.Data.Fields;
using Sitecore.Web.UI.WebControls;

namespace Fortis.Model.RenderingParameters.Fields
{
	public class LinkFieldWrapper : FieldWrapper
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

		public virtual string URL
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

		public LinkFieldWrapper(string value)
			: base(value)
		{

		}

		public virtual T GetTarget<T>() where T : IItemWrapper
		{
            var item = Sitecore.Context.Database.GetItem(Value);
            if (item != null)
            {
                var wrapper = Spawn.FromItem<T>(item);
                return (T)((wrapper is T) ? wrapper : null); ;
            }

            return default(T);
		}

        public static implicit operator string(LinkFieldWrapper field)
		{
			return field.URL;
		}
	}
}
