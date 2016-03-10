using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Fortis.Reflection
{
	using System.Collections.Concurrent;

	using StackExchange.Profiling;

	internal static class InstanceFactory
	{
		private delegate T ObjectActivator<T>(params object[] args);

		public static T CreateInstance<T>(Type type, object[] constructorArgs)
		{
			var profiler = MiniProfiler.Current;
			using (profiler.Step("InstanceFactory.CreateInstance"))
			{
				var ctor = type.GetConstructors().FirstOrDefault(x => x.GetParameters().Length == constructorArgs.Length);
				if (ctor == null)
				{
					return default(T);
				}

				var method = typeof(InstanceFactory).GetMethod("GetActivator", BindingFlags.Static | BindingFlags.NonPublic);
				var tRef = method.MakeGenericMethod(type);

				var activator = tRef.Invoke(type, new object[] { ctor }) as ObjectActivator<T>;
				if (activator == null)
				{
					return default(T);
				}

				//GetActivator(type, ctor);
				var created = activator(constructorArgs);
				return created;
			}
		}

		private static ObjectActivator<T> GetActivator<T>(ConstructorInfo ctor)
		{
			var profiler = MiniProfiler.Current;
			using (profiler.Step("InstanceFactory.GetActivator"))
			{
				var type = ctor.DeclaringType;
				var paramsInfo = ctor.GetParameters();

				//create a single param of type object[]
				var param =
					Expression.Parameter(typeof(object[]), "args");

				var argsExp =
					new Expression[paramsInfo.Length];

				//pick each arg from the params array 
				//and create a typed expression of them
				for (var i = 0; i < paramsInfo.Length; i++)
				{
					Expression index = Expression.Constant(i);
					var paramType = paramsInfo[i].ParameterType;
					Expression paramAccessorExp = Expression.ArrayIndex(param, index);
					Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
					argsExp[i] = paramCastExp;
				}

				//make a NewExpression that calls the
				//ctor with the args we just created
				var newExp = Expression.New(ctor, argsExp);

				//create a lambda with the New
				//Expression as body and our param object[] as arg
				var lambda = Expression.Lambda(typeof(ObjectActivator<T>), newExp, param);

				//compile it
				var compiled = (ObjectActivator<T>)lambda.Compile();
				return compiled;
			}
		}
	}
}