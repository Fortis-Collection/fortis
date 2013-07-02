using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortis.Model
{
	public interface IFieldWrapper : IWrapper
	{
		string RawValue { get; set; }
		bool Modified { get; }
		string Render();
		string ToString();
	}
}
