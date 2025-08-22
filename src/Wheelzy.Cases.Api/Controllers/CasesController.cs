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
    /// Obtiene vista completa de casos sin paginación
    /// </summary>
    [HttpGet("overview")]
    public async Task<ActionResult<IEnumerable<CaseOverview>>> GetOverviewAsync(CancellationToken ct)
    {
        var rows = await _db.CaseOverview
            .AsNoTracking()
            .ToListAsync(ct);

        return Ok(rows);
    }

    /// <summary>
    /// Lista paginada de casos con filtros, búsqueda y ordenamiento
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<Application.Common.PagedResult<Application.Features.Cases.Dtos.CaseOverviewDTO>>> GetCases(
        [FromQuery] DateTime? dateFrom,
        [FromQuery] DateTime? dateTo,
        [FromQuery] int[]? statusIds,
        [FromQuery] int? year,
        [FromQuery] string? search,
        [FromQuery] string? sort,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 25,
        CancellationToken ct = default)
    {
        var result = await _mediator.Send(
            new Application.Features.Cases.Queries.GetCases.GetCasesQuery(dateFrom, dateTo, statusIds, year, search, sort, page, pageSize), ct);
        return Ok(result);
    }

    /// <summary>
    /// Obtiene detalle completo de un caso incluyendo IDs internos
    /// </summary>
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Application.Features.Cases.Dtos.CaseDetailDTO>> GetById(int id, CancellationToken ct)
    {
        var dto = await _mediator.Send(new Application.Features.Cases.Queries.GetCaseById.GetCaseByIdQuery(id), ct);
        if (dto is null) return NotFound();
        return Ok(dto);
    }

    /// <summary>
    /// Crea un nuevo caso de auto con estado inicial
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<int>> CreateCaseAsync([FromBody] CreateCaseCommand cmd, CancellationToken ct)
    {
        var id = await _mediator.Send(cmd, ct);
        return Ok(id);
    }
}