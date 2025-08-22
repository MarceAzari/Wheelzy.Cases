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

---

## Respuesta a pregunta 2

En escenarios donde existen **datos que cambian con poca frecuencia pero que se consultan constantemente** (ej. catálogos de marcas, modelos, códigos postales, estados de casos), la estrategia más eficiente es implementar **caching**:

1. **Caching en memoria (MemoryCache)**
   - Adecuado si la aplicación corre en una sola instancia.
   - Ejemplo: `IMemoryCache` de .NET.
   - Reduce la carga en base de datos y mejora tiempos de respuesta.

2. **Caching distribuido (Redis, SQL Server cache)**
   - Necesario cuando la aplicación corre en **múltiples instancias**.
   - Redis permite:
     - Compartir caché entre instancias.
     - Expiración e invalidación automáticas.
     - Alta velocidad de acceso.

3. **Estrategia híbrida**
   - Usar Redis como caché central.
   - Complementar con `MemoryCache` local como acceso inmediato, invalidado por eventos de Redis.

**Ejemplo en .NET con Redis**:

```csharp
// Startup.cs
services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
    options.InstanceName = "WheelzyCases:";
});

// Uso en servicio
public async Task<List<CarModel>> GetCarModelsAsync()
{
    var cacheKey = "CarModels";
    var cachedData = await _cache.GetStringAsync(cacheKey);

    if (!string.IsNullOrEmpty(cachedData))
        return JsonSerializer.Deserialize<List<CarModel>>(cachedData);

    var carModels = await _dbContext.CarModels.ToListAsync();

    await _cache.SetStringAsync(
        cacheKey,
        JsonSerializer.Serialize(carModels),
        new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(12)
        });

    return carModels;
}

## Respuesta a pregunta 3

**Problemas del código original:**
- N+1 queries (una consulta por factura)
- `SaveChanges` en loop (múltiples transacciones)
- Falta validación de nulos
- Sin `async/await`
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

## Respuesta a pregunta 5

Se implementó un analizador y reescritor de código en **C#** utilizando **Roslyn (Microsoft.CodeAnalysis)**.  
Este componente recorre todos los archivos `.cs` de una carpeta y subcarpetas, aplicando las siguientes reglas:

1. **Métodos async sin sufijo `Async`**
   - Detecta métodos `async` cuyo nombre no termina en `Async`.
   - Renombra automáticamente el método para cumplir con la convención.

2. **Normalización de sufijos `Vm`, `Vms`, `Dto`, `Dtos`**
   - Reemplaza estas ocurrencias por las formas correctas:
     - `Vm` → `VM`
     - `Vms` → `VMs`
     - `Dto` → `DTO`
     - `Dtos` → `DTOs`

3. **Formato entre métodos**
   - Verifica que exista una línea en blanco entre métodos consecutivos.
   - Inserta la línea en blanco si no está presente.

### Ejemplo simplificado

Antes:
```csharp
public async Task GetUser() { ... }
public class UserDto { ... }

public async Task GetUserAsync() { ... }

public class UserDTO { ... }
