using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Model
{
	public class RenderingContext : IRenderingContext
	{
		IItemWrapper _context;
		private string _relativeUrl;

		public RenderingContext(IItemWrapper context)
		{
			_context = context;
		}

		public string RelativeUrl
		{
			get
			{
				if (_context != null && _relativeUrl == null)
				{
					_relativeUrl = _context.GenerateUrl();
				}

				return _relativeUrl;
			}
		}
	}
}
