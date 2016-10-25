using Fortis.Extensions;

namespace Fortis.Fields
{
	public class FieldNameParser : IFieldNameParser
	{
		private const string affixPrefix = "Field";

		public string Parse(string name)
		{
			return name.Replace(affixPrefix, string.Empty).SpaceSeparatePascal();
		}
	}
}
