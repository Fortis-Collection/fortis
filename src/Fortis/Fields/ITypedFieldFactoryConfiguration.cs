using System.Collections.Generic;

namespace Fortis.Fields
{
	public interface ITypedFieldFactoryConfiguration
	{
		Dictionary<string, string> FieldTypeMappings { get; }
	}
}
