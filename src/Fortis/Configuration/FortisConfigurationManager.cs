namespace Fortis.Configuration
{
	using Sitecore.Configuration;

	public class FortisConfigurationManager
	{
		static FortisConfigurationManager()
		{
			Provider = (IConfigurationProvider)Factory.CreateObject("/sitecore/fortis/configurationProvider", true);
		}

		/// <summary>
		/// Gets the fortis configuration provider.
		/// </summary>
		/// <value>
		/// The configuration.
		/// </value>
		public static IConfigurationProvider Provider { get; }
	}
}
