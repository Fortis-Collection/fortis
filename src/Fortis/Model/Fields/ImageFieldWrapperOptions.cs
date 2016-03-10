using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortis.Model.Fields
{
	public class ImageFieldWrapperOptions
	{
		private int[] _cornerRadii;

		public virtual int Width { get; set; }
		public virtual int Height { get; set; }
		public virtual bool Crop { get; set; }
		public virtual int CornerRadius
		{
			set
			{
				_cornerRadii = new int[] { value };
			}
		}
		public virtual int[] CornerRadii
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
