using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Fortis.Fields
{
	public class TypedFieldFactories : ITypedFieldFactories
	{
		protected readonly IEnumerable<ITypedFieldFactory> Factories;

		public TypedFieldFactories(
			IEnumerable<ITypedFieldFactory> factories)
		{
			Factories = factories;
		}

		public ITypedFieldFactory Find(string fieldType)
		{
			return Factories.FirstOrDefault(f => f.CanCreate(fieldType));
		}

		public IEnumerator<ITypedFieldFactory> GetEnumerator()
		{
			return Factories.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
