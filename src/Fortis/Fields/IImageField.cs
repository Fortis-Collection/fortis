using System;

namespace Fortis.Fields
{
	public interface IImageField : IField
	{
		string Url { get; }
		string AltText { get; }
		Guid MediaId { get; }
	}
}
