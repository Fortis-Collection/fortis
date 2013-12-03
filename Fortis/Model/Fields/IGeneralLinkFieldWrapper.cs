namespace Fortis.Model.Fields
{
	public interface IGeneralLinkFieldWrapper : ILinkFieldWrapper
	{
		string AlternateText { get; }

		string Description { get; }

		bool IsInternal { get; }

		bool IsMediaLink { get; }

		string Styles { get; }
	}
}
