using System;
namespace Fortis.Model.Fields
{
	public interface IGeneralLinkFieldWrapper : ILinkFieldWrapper<string>
	{
		Guid ItemId { get; }
		string AlternateText { get; }
		string Description { get; }
		bool IsInternal { get; }
		bool IsMediaLink { get; }
		string Styles { get; }
		string Target { get; }
	}
}
