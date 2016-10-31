using Sitecore.Data.Items;
using System;

namespace Fortis.Items
{
	public interface IItemTypeTemplateMatcher
	{
		Type Find<T>(Item item);
	}
}
