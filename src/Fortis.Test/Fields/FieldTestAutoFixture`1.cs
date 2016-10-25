using Fortis.Fields;

namespace Fortis.Test.Fields
{
	public abstract class FieldTestAutoFixture<TField> : ItemTestAutoFixture
		where TField : BaseField
	{
		protected TField ModelledField
		{
			get
			{
				var modelledField = Create();

				modelledField.Field = Field;

				return modelledField;
			}
		}

		protected abstract TField Create();
	}
}
