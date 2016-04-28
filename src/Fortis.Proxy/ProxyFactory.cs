namespace Fortis.Proxy
{
	using System;

	using Castle.DynamicProxy;

	using Fortis.Model;
	using Fortis.Proxy.Model.Inteceptors;

	internal static class ProxyFactory
	{
		public static T WrapAs<T>(IItemWrapper itemWrapper)
		{
			var generator = new ProxyGenerator();
			var proxy = generator.CreateInterfaceProxyWithoutTarget(typeof(T), new ItemWrapperInterceptor(itemWrapper));
			return (T)proxy;
		}

		public static T WrapAs<T>(IItemWrapper itemWrapper, Delegate impl) where T : class
		{
			var generator = new ProxyGenerator();
			var proxy = generator.CreateInterfaceProxyWithoutTarget(typeof(T), new MethodInterceptor(impl), new ItemWrapperInterceptor(itemWrapper));
			return (T)proxy;
		}
	}
}
