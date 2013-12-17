using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fortis.Search
{
	public static class SearchExtensions
	{
		public static IQueryable<TSource> ContainsIdOr<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, IEnumerable<Guid> ids)
		{
			return ContainsId(queryable, keySelector, ids, true);
		}

		public static IQueryable<TSource> ContainsIdAnd<TSource, TKey>(this IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, IEnumerable<Guid> ids)
		{
			return ContainsId(queryable, keySelector, ids, false);
		}

		private static IQueryable<TSource> ContainsId<TSource, TKey>(IQueryable<TSource> queryable, Expression<Func<TSource, TKey>> keySelector, IEnumerable<Guid> ids, bool or)
		{
			if (!(keySelector.Body is MemberExpression))
			{
				throw new InvalidOperationException("Fortis: Expression must be a member expression");
			}

			var returnQueryable = queryable;
			var typeofT = typeof(TSource);
			var parameter = Expression.Parameter(typeofT);
			var constants = ids.Select(id => Expression.Constant(id));
			var expressions = constants.Select(constant => Expression.Call(typeof(Enumerable), "Contains", new Type[] { typeof(Guid) }, keySelector.Body, constant));

			var aggregateExpressiosn = expressions.Select(expression => (Expression)expression).Aggregate((x, y) => or ? Expression.OrElse(x, y) : Expression.AndAlso(x, y));
			var lambda = Expression.Lambda<Func<TSource, bool>>(aggregateExpressiosn, parameter);

			return returnQueryable.Where(lambda);
		}
	}
}
