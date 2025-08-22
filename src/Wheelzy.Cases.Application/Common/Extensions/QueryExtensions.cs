using System.Linq.Expressions;

namespace Wheelzy.Cases.Application.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> WhereIf<T>(
        this IQueryable<T> source,
        bool condition,
        Expression<Func<T, bool>> predicate)
        => condition ? source.Where(predicate) : source;
}