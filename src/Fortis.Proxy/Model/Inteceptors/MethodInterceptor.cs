namespace Fortis.Proxy.Model.Inteceptors
{
	using System;

	using Castle.DynamicProxy;
	internal class MethodInterceptor : IInterceptor
	{
		public Delegate Delegate { get; }

		public MethodInterceptor(Delegate @delegate)
		{
			this.Delegate = @delegate;
		}

		public void Intercept(IInvocation invocation)
		{
			var result = this.Delegate.DynamicInvoke(invocation.Arguments);
			invocation.ReturnValue = result;
		}
	}
}
