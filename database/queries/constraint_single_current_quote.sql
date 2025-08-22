-- Constraint para garantizar solo una cotización actual por caso
ALTER TABLE dbo.CarCaseQuote
ADD CONSTRAINT CK_CarCaseQuote_SingleCurrent 
CHECK (
    CASE WHEN IsCurrent = 1 THEN 1 ELSE 0 END <= 1
);

-- Índice único para garantizar solo una cotización current por caso
CREATE UNIQUE INDEX IX_CarCaseQuote_CarCaseId_IsCurrent
ON dbo.CarCaseQuote (CarCaseId)
WHERE IsCurrent = 1;