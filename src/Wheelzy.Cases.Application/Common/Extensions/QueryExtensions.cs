namespace Wheelzy.Cases.Application.Common.Extensions;

public static class QueryExtensions
{
    public static IQueryable<T> WhereIf<T>(this IQueryable<T> src, bool condition, Func<IQueryable<T>, IQueryable<T>> pred)
        => condition ? pred(src) : src;
}