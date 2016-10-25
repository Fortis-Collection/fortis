using System.Collections.Generic;

namespace Fortis.Fields
{
	public interface ITypedFieldFactories : IEnumerable<ITypedFieldFactory>
	{
		ITypedFieldFactory Find(string fieldType);
	}
}
