namespace Fortis.Model.Fields
{
	public class ImageFieldWrapperOptions
	{
		private int[] _cornerRadii;

		public int Width { get; set; }
		public int MaxWidth { get; set; }
		public int Height { get; set; }
		public int MaxHeight { get; set; }
		public bool Crop { get; set; }
		public int CornerRadius
		{
			set { _cornerRadii = new[] { value }; }
		}
		public int[] CornerRadii
		{
			get { return _cornerRadii ?? (_cornerRadii = new[] {0, 0, 0, 0}); }
			set { _cornerRadii = value; }
		}
	}
}
