namespace Fortis.Configuration
{
	using System.Collections.Generic;

	public class FortisConfiguration : IFortisConfiguration
	{
		public IEnumerable<IFortisModelConfiguration> Models { get; set; }

		public IEnumerable<ISupportedFieldType> Fields { get; set; } 
	}
}
