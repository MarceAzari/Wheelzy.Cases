### GET /api/cases
Lista paginada con filtros, orden y búsqueda.

**Query params**
- `dateFrom`, `dateTo` (ISO-8601)
- `statusIds` (repetibles o array)
- `year`
- `search` (Make/Model/SubModel/Zip/Buyer/Status)
- `sort` (ej. `-CurrentStatusDate`, `Make`, `Model`)
- `page` (1..), `pageSize` (1..200)

**Ejemplos**
- `GET /api/cases?search=Corolla&sort=-CurrentStatusDate&page=1&pageSize=20`
- `GET /api/cases?dateFrom=2025-08-20T00:00:00&dateTo=2025-08-31T23:59:59&statusIds=3&year=2016`

### GET /api/cases/{id}
Detalle con IDs internos (CustomerId/MakeId/ModelId/ZipCodeId) y CreatedAt.

### POST /api/cases/{caseId}/quotes/{quoteId}:set-current`
Marca una cotización como “current” (SP transaccional).

### POST /api/cases/{caseId}/status`
Crea un estado en el historial. **Picked Up** requiere `statusDate`.

## Respuesta a pregunta 3

**Problemas del código original:**
- N+1 queries (una consulta por factura)
- SaveChanges en loop (múltiples transacciones)
- Falta validación de nulos
- Sin async/await
- Sin transacción explícita

**Solución optimizada:**
```csharp
public async Task UpdateCustomersBalanceByInvoicesAsync(List<Invoice> invoices, CancellationToken ct = default)
{
    if (!invoices?.Any() == true) return;

    var customerIds = invoices.Where(i => i.CustomerId.HasValue)
                             .Select(i => i.CustomerId.Value)
                             .Distinct().ToList();

    var customers = await dbContext.Customers
        .Where(c => customerIds.Contains(c.Id))
        .ToDictionaryAsync(c => c.Id, ct);

    using var transaction = await dbContext.Database.BeginTransactionAsync(ct);
    
    foreach (var invoice in invoices.Where(i => i.CustomerId.HasValue))
    {
        if (customers.TryGetValue(invoice.CustomerId.Value, out var customer))
            customer.Balance -= invoice.Total;
    }

    await dbContext.SaveChangesAsync(ct);
    await transaction.CommitAsync(ct);
}
```

**Mejoras:** De O(n) queries a O(1) query, transacción única, validación de nulos, async/await.
