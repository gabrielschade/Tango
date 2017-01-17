using System.Linq;

namespace Tango.Linq
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IOrderedQueryable<T> source, int page, int perPage)
            => page == 0 ? source : source.Skip((page - 1) * perPage)
                                          .Take(perPage);
        
    }
}
