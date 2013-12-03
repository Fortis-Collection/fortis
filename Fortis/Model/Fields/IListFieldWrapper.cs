using System;
using System.Collections.Generic;

namespace Fortis.Model.Fields
{
	public interface IListFieldWrapper : IFieldWrapper<IEnumerable<Guid>>, IEnumerable<IItemWrapper>
	{
		IEnumerable<T> GetItems<T>() where T : IItemWrapper;
	}
}
