namespace Fortis.Fields.IntegerField
{
	public class IntegerField : BaseField, IIntegerField
	{
		public new int Value
		{
			get
			{
				int value;

				if (int.TryParse(base.Value, out value))
				{
					return value;
				}

				return default(int);
			}

			set { base.Value = value.ToString(); }
		}
	}
}
