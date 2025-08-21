using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Infrastructure;
using Wheelzy.Cases.Infrastructure.Persistence.Models;
using MediatR;
using Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;

namespace Wheelzy.Cases.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class CasesController : ControllerBase
{
    private readonly WheelzyDbContext _db;
    private readonly IMediator _mediator;

    public CasesController(WheelzyDbContext db, IMediator mediator)
    {
        _db = db;
        _mediator = mediator;
    }

    /// <summary>
    /// Overview: Car info + current buyer/quote + current status/date (desde la view)
    /// </summary>
    [HttpGet("overview")]
    public async Task<ActionResult<IEnumerable<CaseOverview>>> GetOverview(CancellationToken ct)
    {
        var rows = await _db.CaseOverview.AsNoTracking().ToListAsync(ct);
        return Ok(rows);
    }

    /// <summary>
    /// Filtrado condicional de casos
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Application.Features.Cases.Dtos.CaseDto>>> GetCases(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] List<int>? statusIds,
        [FromQuery] int? year,
        CancellationToken ct)
    {
        var query = new Application.Features.Cases.Queries.GetCases.GetCasesQuery(dateFrom, dateTo, statusIds, year);
        var result = await _mediator.Send(query, ct);
        return Ok(result);
    }

    /// <summary>
    /// Ejemplo de creación de Case via CQRS (placeholder del challenge)
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> CreateCase([FromBody] CreateCaseCommand cmd, CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Ok(id);
    }
}