using System;
using Fortis.Model.Fields;

namespace Fortis.Model.RenderingParameters.Fields
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

		public LinkFieldWrapper(string value)
			: base(value)
		{

		}

		public string Render(LinkFieldWrapperOptions options)
		{
			throw new NotImplementedException();
		}

		public virtual T GetTarget<T>() where T : IItemWrapper
		{
            var item = Sitecore.Context.Database.GetItem(_value);
            if (item != null)
            {
                var wrapper = Spawn.FromItem<T>(item);
                return (T)((wrapper is T) ? wrapper : null); 
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

		public Guid ItemId
		{
			get
			{
				Guid id;

				if (!Guid.TryParse(RawValue, out id))
				{
					id = default(Guid);
				}

				return id;
			}
		}
	}
}
