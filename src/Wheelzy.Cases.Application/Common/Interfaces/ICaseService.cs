namespace Wheelzy.Cases.Application.Common.Interfaces;

public interface ICaseService
{
    Task<int> CreateCaseAsync(short year, string make, string model, string? subModel, string zipCode, int customerId, CancellationToken ct);
}