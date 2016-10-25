namespace Fortis.Fields
{
	public interface IGeneralLinkField : IField
	{
		string Url { get; }
		string AltText { get; }
		string Description { get; }
		bool IsInternal { get; }
		bool IsMediaLink { get; }
		string Styles { get; }
		string BrowserTarget { get; }
	}
}
