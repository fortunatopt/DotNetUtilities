using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Utilities
{
    public static class LinqExtensions
    {
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            return source.Where(element => seenKeys.Add(keySelector(element)));
        }
        /// <summary>
        /// Read uncommitted to list
        /// </summary>
        /// <typeparam name="T">Generic Entity type</typeparam>
        /// <param name="query">IQueryable objecy</param>
        /// <returns>List of type T</returns>
        public static List<T> ToListReadUncommitted<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                List<T> toReturn = query.ToList();
                scope.Complete();
                return toReturn;
            }
        }

        /// <summary>
        /// Count uncommitted
        /// </summary>
        /// <typeparam name="T">Generic Entity type</typeparam>
        /// <param name="query">IQueryable objecy</param>
        /// <returns>int count of type T</returns>
        public static int CountReadUncommitted<T>(this IQueryable<T> query)
        {
            using (var scope = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted }))
            {
                int toReturn = query.Count();
                scope.Complete();
                return toReturn;
            }
        }
    }
}
