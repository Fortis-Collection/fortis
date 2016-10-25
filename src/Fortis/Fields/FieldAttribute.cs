using System;

namespace Fortis.Fields
{
	[AttributeUsage(AttributeTargets.Property)]
	public class FieldAttribute : Attribute
	{
		public string Name { get; private set; }

		public FieldAttribute(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new Exception("Fortis: Field attribute name must not be empty or whitepsace");
			}

			Name = name;
		}
	}
}
