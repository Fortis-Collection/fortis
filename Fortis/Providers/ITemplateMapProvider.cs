using Fortis.Model;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;

namespace Fortis.Providers
{
	public interface ITemplateMapProvider
	{
		Dictionary<Guid, Type> TemplateMap { get; }
		Dictionary<Type, Guid> InterfaceTemplateMap { get; }
		Dictionary<Guid, Type> RenderingParametersTemplateMap { get; }
		Type GetImplementation<T>() where T : IItemWrapper;
		bool IsCompatibleTemplate<T>(Guid templateId) where T : IItemWrapper;
		bool IsCompatibleTemplate(Guid templateId, Type template);
		bool IsCompatibleFieldType<T>(string fieldType) where T : IFieldWrapper;
		bool IsCompatibleFieldType(string scFieldType, Type fieldType);
	}
}
