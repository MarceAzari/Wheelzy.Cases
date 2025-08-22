using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Wheelzy.Cases.Infrastructure.Persistence.Models;
using MediatR;
using Wheelzy.Cases.Application.Features.Cases.Commands.CreateCase;
using Wheelzy.Cases.Infrastructure.Persistence;

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
        var rows = await _db.CaseOverview
            .AsNoTracking()
            .ToListAsync(ct);

        return Ok(rows);
    }

    /// <summary>
    /// Filtrado condicional de casos
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<Application.Features.Cases.Dtos.CaseOverviewDto>>> GetCases(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] int[]? statusIds,
        [FromQuery] int? year,
        CancellationToken ct)
    {
        var data = await _mediator.Send(new Application.Features.Cases.Queries.GetCases.GetCasesQuery(dateFrom, dateTo, statusIds?.ToList(), year), ct);
        return Ok(data);
    }

    /// <summary>
    /// Detalle completo de un caso por ID
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Application.Features.Cases.Dtos.CaseDetailDto>> GetById(int id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new Application.Features.Cases.Queries.GetCaseById.GetCaseByIdQuery(id), ct);
        if (dto is null) return NotFound();
        return Ok(dto);
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