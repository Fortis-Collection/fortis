using Sitecore.Data.Fields;
using System;

namespace Fortis.Fields
{
	public abstract class TypedFieldFactory<TConcreteField, TInterfaceField, TScField> : TypedFieldFactory<TConcreteField>, ITypedFieldFactory<TInterfaceField>
			where TConcreteField : BaseField, TInterfaceField, new()
			where TInterfaceField : IField
	{
		public TypedFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
			: base(mappingValidator)
		{
		}

		public override TConcreteField Create(Field field)
		{
			if (!CanCreate(field.Type))
			{
				throw new Exception($"Fortis: {Name} - invalid field type for factory [ Item ID: {field.Item.ID} | Name: {field.Name} | Type: {field.Type} ]");
			}

			return new TConcreteField()
			{
				Field = field
			};
		}

		TInterfaceField ITypedFieldFactory<TInterfaceField>.Create(Field field)
		{
			return Create(field);
		}
	}
}
