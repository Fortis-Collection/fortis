using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Fortis.Model
{
	public interface IItemFactory
	{
		string GetTemplateID(Type type);
		Type GetInterfaceType(string templateId);
		void Publish(IEnumerable<IItemWrapper> wrappers);
		void Publish(IEnumerable<IItemWrapper> wrappers, bool children);
		T GetSiteHome<T>() where T : IItemWrapper;
		T GetContextItem<T>() where T : IItemWrapper;
		IRenderingModel<TPageItem, TRenderingItem> GetRenderingContextItems<TPageItem, TRenderingItem>()
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper;
		IRenderingModel<TPageItem, TRenderingItem, TRenderingParametersItem> GetRenderingContextItems<TPageItem, TRenderingItem, TRenderingParametersItem>()
			where TPageItem : IItemWrapper
			where TRenderingItem : IItemWrapper
			where TRenderingParametersItem : IRenderingParameterWrapper;
		T GetContextItemsItem<T>(string key) where T : IItemWrapper;
		T Create<T>(IItemWrapper parent, string itemName) where T : IItemWrapper;
		T Create<T>(string parentPathOrId, string itemName) where T : IItemWrapper;
		T Select<T>(string path) where T : IItemWrapper;
		T Select<T>(string path, string database) where T : IItemWrapper;
		IEnumerable<T> SelectAll<T>(string path) where T : IItemWrapper;
		IEnumerable<T> SelectAll<T>(string path, string database) where T : IItemWrapper;
		T SelectChild<T>(IItemWrapper item) where T : IItemWrapper;
		T SelectChild<T>(string path) where T : IItemWrapper;
		T SelectChildRecursive<T>(string path) where T : IItemWrapper;
		IEnumerable<T> SelectChildren<T>(IItemWrapper item) where T : IItemWrapper;
		IEnumerable<T> SelectChildren<T>(string path) where T : IItemWrapper;
		IEnumerable<T> SelectChildrenRecursive<T>(IItemWrapper wrapper) where T : IItemWrapper;
		T SelectSibling<T>(IItemWrapper wrapper) where T : IItemWrapper;
		IEnumerable<T> SelectSiblings<T>(IItemWrapper wrapper) where T : IItemWrapper;
		T GetRenderingDataSource<T>(Control control) where T : IItemWrapper;
	}
}
