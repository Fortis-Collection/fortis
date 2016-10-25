namespace Fortis.Fields
{
	public interface IField<TReturn> : IField
	{
		new TReturn Value { get; set; }
	}
}
