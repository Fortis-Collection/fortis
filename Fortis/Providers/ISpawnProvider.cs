using Fortis.Model;
using Sitecore.Collections;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fortis.Providers
{
	public interface ISpawnProvider
	{
		Dictionary<Guid, Type> TemplateMap { get; }
		Dictionary<Type, Guid> InterfaceTemplateMap { get; }
		Dictionary<Guid, Type> RenderingParametersTemplateMap { get; }
		Type GetImplementation<T>() where T : IItemWrapper;
		IItemWrapper FromItem<T>(Guid itemId, Guid templateId) where T : IItemWrapper;
		IItemWrapper FromItem(Guid itemId, Guid templateId, Type template = null, Dictionary<string, object> lazyFields = null);
		IItemWrapper FromItem(Item item);
		IItemWrapper FromItem<T>(Item item) where T : IItemWrapper;
		IItemWrapper FromItem(Item item, Type template = null);
		IEnumerable<IItemWrapper> FromItems(IEnumerable<Item> items);
		IEnumerable<T> FromItems<T>(IEnumerable<Item> items) where T : IItemWrapper;
		IRenderingParameterWrapper FromRenderingParameters<T>(Item renderingItem, Dictionary<string, string> parameters) where T : IRenderingParameterWrapper;
		IEnumerable<IFieldWrapper> FromFields(FieldCollection fields);
		IEnumerable<IFieldWrapper> FromFields(FieldChangeList fields);
		bool IsCompatibleTemplate<T>(Guid templateId) where T : IItemWrapper;
		bool IsCompatibleTemplate(Guid templateId, Type template);
		bool IsCompatibleFieldType<T>(string fieldType) where T : IFieldWrapper;
		bool IsCompatibleFieldType(string scFieldType, Type fieldType);
		IFieldWrapper FromField(Field field);
		IEnumerable<T> FilterWrapperTypes<T>(IEnumerable<IItemWrapper> wrappers);
	}
}
