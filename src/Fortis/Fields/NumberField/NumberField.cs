namespace Fortis.Fields.NumberField
{
	public class NumberField : BaseField, INumberField
	{
		public new float Value
		{
			get
			{
				float value;

				if (float.TryParse(base.Value, out value))
				{
					return value;
				}

				return default(float);
			}

			set { base.Value = value.ToString(); }
		}
	}
}
