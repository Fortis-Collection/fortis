namespace Fortis.Configuration
{
	public class FortisModelConfiguration : IFortisModelConfiguration
	{
		public FortisModelConfiguration(string name, string assembley)
		{
			Name = name;
			Assembley = assembley;
		}

		public string Name { get; }
		public string Assembley { get; }
	}
}
