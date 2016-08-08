namespace Fortis.Configuration
{
	public class FortisModelConfiguration : IFortisModelConfiguration
	{
		public FortisModelConfiguration(string name, string assembly)
		{
			Name = name;
			Assembly = assembly;
		}

		public string Name { get; }
		public string Assembly { get; }
	}
}
