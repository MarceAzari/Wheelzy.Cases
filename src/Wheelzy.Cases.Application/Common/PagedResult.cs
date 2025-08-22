namespace Wheelzy.Cases.Application.Common;

/// <summary>
/// Resultado paginado con metadatos de navegación
/// </summary>
public sealed record PagedResult<T>(IReadOnlyList<T> Items, int Page, int PageSize, int TotalCount)
{
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
}