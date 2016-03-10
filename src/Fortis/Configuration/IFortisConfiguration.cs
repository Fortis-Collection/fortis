namespace Fortis.Configuration
{
	using System.Collections.Generic;

	public interface IFortisConfiguration
	{
		IEnumerable<IFortisModelConfiguration> Models { get; set; }

		IEnumerable<ISupportedFieldType> Fields { get; set; } 
	}
}
