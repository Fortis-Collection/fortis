using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Fortis.Model
{
	public interface IRenderingParameterFactory
	{
		Dictionary<string, Guid> Renderings { get; }
		T GetParameters<T>(Control control)
			where T : IRenderingParameterWrapper;
		T GetParameters<T>(string filePath, Dictionary<string, string> parameters)
			where T : IRenderingParameterWrapper;
	}
}
