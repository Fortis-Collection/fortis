using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortis.Model.Fields
{
	public class ImageFieldWrapperOptions
	{
		private int[] _cornerRadii;

		public int Width { get; set; }
		public int Height { get; set; }
		public bool Crop { get; set; }
		public int CornerRadius
		{
			set
			{
				_cornerRadii = new int[] { value };
			}
		}
		public int[] CornerRadii
		{
			get
			{
				if (_cornerRadii == null)
				{
					_cornerRadii = new int[] { 0, 0, 0, 0 };
				}

				return _cornerRadii;
			}
			set
			{
				_cornerRadii = value;
			}
		}
	}
}
