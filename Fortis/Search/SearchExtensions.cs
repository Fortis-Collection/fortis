using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fortis.Search
{
	public static class SearchExtensions
	{
		public static IQueryable<TSource> ContainsOr<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, TKey values) where TKey : IEnumerable
		{
			return Contains(queryable, keySelector, values, true);
		}

		public static IQueryable<TSource> ContainsAnd<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, TKey values) where TKey : IEnumerable
		{
			return Contains(queryable, keySelector, values, false);
		}

		private static IQueryable<TSource> Contains<TSource, TKey>(IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, TKey values, bool or) where TKey : IEnumerable
		{
			if (!(keySelector.Body is MemberExpression))
			{
				throw new InvalidOperationException("Fortis: Expression must be a member expression");
			}

			var castedValues = values.Cast<object>();
			var returnQueryable = queryable;
			var typeofT = typeof(TSource);
			var parameter = Expression.Parameter(typeofT);
			var constants = castedValues.Select(id => Expression.Constant(id));
			var expressions = constants.Select(constant => Expression.Call(typeof(Enumerable), "Contains", new Type[] { typeof(Guid) }, keySelector.Body, constant));

			var aggregateExpressiosn = expressions.Select(expression => (Expression)expression).Aggregate((x, y) => or ? Expression.OrElse(x, y) : Expression.AndAlso(x, y));
			var lambda = Expression.Lambda<Func<TSource, bool>>(aggregateExpressiosn, parameter);

			return returnQueryable.Where(lambda);
		}
	}
}
