using System.Linq;
using System.Web.Compilation;
using System.Web.Mvc;
using Fortis.Model;

using Sitecore.Mvc.Pipelines.Response.GetModel;
using Sitecore.Mvc.Presentation;

namespace Fortis.Mvc.Pipelines.GetModel
{
	using System.Web;
	using System.Web.Caching;

	public class GetFromView : GetModelProcessor
	{
		protected virtual object GetFromViewPath(Rendering rendering, GetModelArgs args)
		{
			var path = rendering.Renderer is ViewRenderer 
				? ((ViewRenderer)rendering.Renderer).ViewPath 
				: rendering.ToString().Replace("View: ", string.Empty);

			if (string.IsNullOrWhiteSpace(path))
			{
				return null;
			}

			// Retrieve the compiled view
			var compiledViewType = BuildManager.GetCompiledType(path);
			var baseType = compiledViewType.BaseType;

			// Check to see if the view has been found and that it is a generic type
			if (baseType == null || !baseType.IsGenericType)
			{
				return null;
			}

			var modelType = baseType.GetGenericArguments()[0];

			// Check to see if no model has been set
			if (modelType == typeof(object))
			{
				return null;
			}

			var modelGenericArgs = modelType.GetGenericArguments();

			var itemFactory = DependencyResolver.Current.GetService<IItemFactory>();

			var method = itemFactory.GetType().GetMethods().FirstOrDefault(m => string.Equals(m.Name, "GetRenderingContextItems")
				&& m.GetGenericArguments().Count().Equals(modelGenericArgs.Count()));

			var genericMethod = method?.MakeGenericMethod(modelGenericArgs);

			return genericMethod?.Invoke(itemFactory, new object[] { itemFactory });
		}

		public override void Process(GetModelArgs args)
		{
			if (args.Result == null)
			{
				args.Result = GetFromViewPath(args.Rendering, args);
			}
		}
	}
}
