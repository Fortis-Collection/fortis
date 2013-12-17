using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fortis.Search
{
	public static class SearchExtensions
	{
		public static IQueryable<T> ContainsIdOr<T>(this IQueryable<T> queryable, string propertyName, IEnumerable<Guid> ids)
		{
			return ContainsId(queryable, propertyName, ids, true);
		}

		public static IQueryable<T> ContainsIdAnd<T>(this IQueryable<T> queryable, string propertyName, IEnumerable<Guid> ids)
		{
			return ContainsId(queryable, propertyName, ids, true);
		}

		private static IQueryable<T> ContainsId<T>(IQueryable<T> queryable, string propertyName, IEnumerable<Guid> ids, bool or)
		{
			var returnQueryable = queryable;
			var typeofT = typeof(T);
			var parameter = Expression.Parameter(typeofT);
			var property = Expression.Property(parameter, propertyName);
			var constants = ids.Select(id => Expression.Constant(id));
			var expressions = constants.Select(constant => Expression.Call(typeof(Enumerable), "Contains", new Type[] { typeof(Guid) }, property, constant));

			var aggregateExpressiosn = expressions.Select(expression => (Expression)expression).Aggregate((x, y) => or ? Expression.OrElse(x, y) : Expression.AndAlso(x, y));
			var lambda = Expression.Lambda<Func<T, bool>>(aggregateExpressiosn, parameter);

			return returnQueryable.Where(lambda);
		}
	}
}
