using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Model
{
	public interface IRenderingContext
	{
		string RelativeUrl { get; }
	}
}
