using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fortis.Model.Pipelines
{
	public abstract class HttpRequestPipeline<T> : HttpRequestProcessor where T : IItemWrapper
	{
		public override void Process(HttpRequestArgs args)
		{
			var item = Global.SpawnProvider.FromItem<T>(Sitecore.Context.Item);

			OnProcess((T)((item is T) ? item : null), args);
		}

		protected abstract void OnProcess(T item, object args);
	}
}
