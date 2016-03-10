namespace Fortis.Configuration
{
	public interface IConfigurationProvider
	{
		IFortisConfiguration DefaultConfiguration { get; }
	}
}
