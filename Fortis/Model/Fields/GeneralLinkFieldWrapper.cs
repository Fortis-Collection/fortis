using System;
using Sitecore.Data.Fields;

namespace Fortis.Model.Fields
{
	public class GeneralLinkFieldWrapper : LinkFieldWrapper, IGeneralLinkFieldWrapper
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

		public override string Url
		{
			get
			{
				if (IsMediaLink)
				{
					return Sitecore.Resources.Media.MediaManager.GetMediaUrl(LinkField.TargetItem);
				}
				if (IsInternal && Target != null)
				{
					return Target.GenerateUrl();
				}

				return LinkField.Url;
			}
		}

		public GeneralLinkFieldWrapper(Field field)
			: base(field)
		{

		}

		public override T GetTarget<T>()
		{
			if (IsInternal || IsMediaLink)
			{
                var wrapper = Spawn.FromItem<T>(LinkField.TargetItem);
                return (T)((wrapper is T) ? wrapper : null);;
			}

			return default(T);
		}

		public static implicit operator string(GeneralLinkFieldWrapper field)
		{
			return field.Url;
		}
	}
}
