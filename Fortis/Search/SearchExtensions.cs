using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fortis.Search
{
	public static class SearchExtensions
	{
		public static IQueryable<TSource> ContainsOr<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, IEnumerable values) where TKey : IEnumerable
		{
			return Contains(queryable, keySelector, values, true);
		}

		public static IQueryable<TSource> ContainsAnd<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, IEnumerable values) where TKey : IEnumerable
		{
			return Contains(queryable, keySelector, values, false);
		}

		private static IQueryable<TSource> Contains<TSource, TKey>(IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, IEnumerable values, bool or) where TKey : IEnumerable
		{
			const string methodName = "Contains";

			// Ensure the body of the selector is a MemberExpression
			if (!(keySelector.Body is MemberExpression))
			{
				throw new InvalidOperationException("Fortis: Expression must be a member expression");
			}

			var typeOfTSource = typeof(TSource);
			var typeOfTKey = typeof(TKey);

			// x
			var parameter = Expression.Parameter(typeOfTSource);

			// Create the enumerable of constant expressions based off of the values
			var constants = values.Cast<object>().Select(id => Expression.Constant(id));

			// Create separate MethodCallExpression objects for each constant expression created
			// type -> we need to specify the type which contains the method we want to run
			// methodName -> in this instance we need to specify the Contains method
			// typeArguments -> the type parameter from TKey
			//					e.g. if we're passing through IEnumerable<Guid> then this will pass through the Guid type
			//					this is because we're effectively running IEnumerable<Guid>.Contains(Guid guid) for each
			//					guid in our values object
			// arguments ->
			//		keySelector.Body -> this would be property we want to run the expession on e.g.
			//							IQueryable<MyPocoTemplate>.Where(x => x.MyIdListField)
			//							so keySelector.Body will contain the "x.MyIdListField" which is what we want to run
			//							each constant expression against
			//		constant -> this is the constant expression
			//
			// Each expression will effectively be like running the following;
			// x => x.MyIdListField.Contains(AnId)
			IEnumerable<MethodCallExpression> expressions = Enumerable.Empty<MethodCallExpression>();

			if (typeOfTKey.GetMethods().Any(m => m.Name.Equals(methodName)))
			{
				var method = typeOfTKey.GenericTypeArguments.Any() ? typeOfTKey.GetMethod(methodName, typeOfTKey.GenericTypeArguments) : typeOfTKey.GetMethod(methodName);

				expressions = constants.Select(constant => Expression.Call(keySelector.Body, method, constant));
			}
			else
			{
				expressions = constants.Select(constant => Expression.Call(typeof(Enumerable), methodName, typeOfTKey.GenericTypeArguments, keySelector.Body, constant));
			}

			// Combine all the expressions into one expression so you would end with something like;
			// x => x.MyIdListField.Contains(AnId) OR x.MyIdListField.Contains(AnId) OR x.MyIdListField.Contains(AnId)
			var aggregateExpressions = expressions.Select(expression => (Expression)expression).Aggregate((x, y) => or ? Expression.OrElse(x, y) : Expression.AndAlso(x, y));

			// Create the Lambda expression which can be passed to the .Where
			var lambda = Expression.Lambda<Func<TSource, bool>>(aggregateExpressions, parameter);

			return queryable.Where(lambda);
		}
	}
}