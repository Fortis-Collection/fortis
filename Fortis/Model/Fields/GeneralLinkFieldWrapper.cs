using System;
using Sitecore.Data.Fields;
using Fortis.Providers;

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

		public GeneralLinkFieldWrapper(Field field, ISpawnProvider spawnProvider)
			: base(field, spawnProvider) {	}

		public GeneralLinkFieldWrapper(string key, ref ItemWrapper item, ISpawnProvider spawnProvider, string value = null)
			: base(key, ref item, spawnProvider, value) { }

		public override T GetTarget<T>()
		{
			if (IsInternal || IsMediaLink)
			{
                var wrapper = SpawnProvider.FromItem<T>(LinkField.TargetItem);
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
