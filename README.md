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
