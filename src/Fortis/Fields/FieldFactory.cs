using Sitecore.Data.Fields;
using System;

namespace Fortis.Fields
{
	public class FieldFactory : IFieldFactory
	{
		protected readonly ITypedFieldFactories Factories;

		public FieldFactory(
			ITypedFieldFactories factories)
		{
			Factories = factories;
		}

		public IField Create(Field field)
		{
			var factory = Factories.Find(field.Type);

			if (factory == null)
			{
				throw new Exception($"Fortis: Unsupported field type encountered [ Item ID: {field.Item.ID} | Name: {field.Name} | Type: {field.Type} ]");
			}

			return factory.Create(field);
		}
	}
}
