using FocusOnFlying.Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Extensions
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int offset, int rows) where T : class
        {
            var result = new PagedResult<T>();
            result.RowCount = await query.CountAsync();
            if (rows == 0)
            {
                result.Results = await query.ToListAsync();
            }
            else
            {
                result.Results = await query.Skip(offset).Take(rows).ToListAsync();
            }
            return result;
        }

        public static IQueryable<T> GetSorted<T>(this IQueryable<T> query, string sortField, int sortOrder) where T : class
        {
            if (string.IsNullOrWhiteSpace(sortField))
                return query;

            IOrderedQueryable<T> result;

            if (sortOrder > 0)
                result = query.OrderBy(sortField);
            else
                result = query.OrderByDescending(sortField);

            return result;
        }

        private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderBy(ToLambda<T>(propertyName));
        }

        private static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> source, string propertyName)
        {
            return source.OrderByDescending(ToLambda<T>(propertyName));
        }

        private static Expression<Func<T, object>> ToLambda<T>(string propertyName)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T));
            MemberExpression propeorty = Expression.Property(parameter, propertyName);
            UnaryExpression propAsObject = Expression.Convert(propeorty, typeof(object));

            return Expression.Lambda<Func<T, object>>(propAsObject, parameter);
        }
    }
}
