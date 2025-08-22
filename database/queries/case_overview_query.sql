-- Consulta SQL: información del auto + comprador actual + estado actual
SELECT 
    c.CarCaseId,
    c.Year,
    m.Name AS Make,
    mo.Name AS Model,
    sm.Name AS SubModel,
    z.Code AS ZipCode,
    b.Name AS CurrentBuyer,
    ccq.Amount AS CurrentQuote,
    s.Name AS CurrentStatus,
    csh.StatusDate AS CurrentStatusDate
FROM dbo.CarCase c
    INNER JOIN dbo.Make m ON c.MakeId = m.MakeId
    INNER JOIN dbo.Model mo ON c.ModelId = mo.ModelId
    LEFT JOIN dbo.SubModel sm ON c.SubModelId = sm.SubModelId
    INNER JOIN dbo.ZipCode z ON c.ZipCodeId = z.ZipCodeId
    LEFT JOIN dbo.CarCaseQuote ccq ON c.CarCaseId = ccq.CarCaseId AND ccq.IsCurrent = 1
    LEFT JOIN dbo.Buyer b ON ccq.BuyerId = b.BuyerId
    LEFT JOIN (
        SELECT 
            CarCaseId,
            StatusId,
            StatusDate,
            ROW_NUMBER() OVER (PARTITION BY CarCaseId ORDER BY StatusDate DESC, CarCaseStatusHistoryId DESC) as rn
        FROM dbo.CarCaseStatusHistory
    ) latest_status ON c.CarCaseId = latest_status.CarCaseId AND latest_status.rn = 1
    LEFT JOIN dbo.Status s ON latest_status.StatusId = s.StatusId
    LEFT JOIN dbo.CarCaseStatusHistory csh ON latest_status.CarCaseId = csh.CarCaseId 
        AND latest_status.StatusId = csh.StatusId 
        AND latest_status.StatusDate = csh.StatusDate;