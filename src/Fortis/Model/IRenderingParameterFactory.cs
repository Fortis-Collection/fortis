using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Fortis.Model
{
	public interface IRenderingParameterFactory
	{
		T GetParameters<T>(Control control) where T : IRenderingParameterWrapper;
	}
}
