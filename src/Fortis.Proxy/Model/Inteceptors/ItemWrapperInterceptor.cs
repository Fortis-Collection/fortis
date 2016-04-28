namespace Fortis.Proxy.Model.Inteceptors
{
	using System;

	using Castle.DynamicProxy;

	using Fortis.Model;

	public class ItemWrapperInterceptor : IInterceptor
	{
		protected readonly IItemWrapper ItemWrapper;

		public ItemWrapperInterceptor(IItemWrapper itemWrapper)
		{
			ItemWrapper = itemWrapper;
		}

		public void Intercept(IInvocation invocation)
		{
			throw new NotImplementedException();
		}
	}
}