using System.Collections.Generic;
using System.Linq;

namespace GisqRealEstate.MaintainWeb.Common
{
    public static class IQueryableExtensions
    {
        public static List<TSource> GetPageList<TSource>(this IQueryable<TSource> source, int skipCount, int takeCount, out int totalCount)
        {
            totalCount = source.Count();
            //if (totalCount > 0)
            //{
                return source.Skip(skipCount).Take(takeCount).ToList();
            //}
        }
    }
}