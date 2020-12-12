using FocusOnFlying.Application.Extensions;
using FocusOnFlying.Infrastructure.Services.PropertyMapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace FocusOnFlying.Application.Extensions
{
    public static class IQueryableExtension
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> query, string orderBy, Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return query;
            }

            var orderByString = string.Empty;

            string[] orderByAfterSplit = orderBy.Split(',');

            foreach (string orderByClause in orderByAfterSplit)
            {
                string trimmedOrderByClause = orderByClause.Trim();

                bool orderDescending = trimmedOrderByClause.EndsWith(" -1");

                int indexOfFirstSpace = trimmedOrderByClause.IndexOf(" ");
                string propertyName = indexOfFirstSpace == -1 ?
                    trimmedOrderByClause :
                    trimmedOrderByClause.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Nie znalezniono mapowania klucza {propertyName}");
                }

                PropertyMappingValue propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                {
                    throw new ArgumentNullException(nameof(propertyMappingValue));
                }

                if (propertyMappingValue.Revert)
                {
                    orderDescending = !orderDescending;
                }

                foreach (string destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    orderByString = orderByString +
                        (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ") +
                        destinationProperty +
                        (orderDescending ? " descending" : " ascending");
                }
            }

            return query.OrderBy(orderByString);
        }

        public static async Task<Common.Models.PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query, int offset, int rows) where T : class
        {
            var result = new Common.Models.PagedResult<T>();
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

        //private static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName)
        //{
        //    return source.OrderBy(ToLambda<T>(propertyName));
        //}

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
