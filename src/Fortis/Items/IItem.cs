using System;

namespace Fortis.Items
{
	public interface IItem
	{
		Guid ItemId { get; }
		string ItemName { get; }
	}
}
