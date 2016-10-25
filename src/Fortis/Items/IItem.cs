using System;

namespace Fortis.Items
{
	public interface IItem
	{
		Guid Id { get; }
		string Name { get; }
	}
}
