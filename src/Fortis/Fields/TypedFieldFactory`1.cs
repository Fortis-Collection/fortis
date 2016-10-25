using Sitecore.Data.Fields;

namespace Fortis.Fields
{
	public abstract class TypedFieldFactory<T> : ITypedFieldFactory
		where T : IField
	{
		protected readonly ITypedFieldMappingValidator MappingValidator;

		public TypedFieldFactory(
			ITypedFieldMappingValidator mappingValidator)
		{
			MappingValidator = mappingValidator;
		}

		public abstract string Name { get; }

		public virtual bool CanCreate(string fieldType)
		{
			return MappingValidator.IsValid(fieldType, Name);
		}

		public abstract T Create(Field field);

		IField ITypedFieldFactory.Create(Field field)
		{
			return Create(field);
		}
	}
}
