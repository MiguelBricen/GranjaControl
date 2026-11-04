/*
    Consulta de solo lectura: identifica detalles históricos sin atribuirles
    que cantidad sea peso o número de sacos. No modifica datos.
*/
USE GranjaControlDB;
GO

SELECT
    c.id AS IdCompra,
    c.numeroCompra AS NumeroCompra,
    c.fechaCompra AS FechaCompra,
    p.nombreProveedor AS Proveedor,
    d.id AS IdDetalle,
    a.nombreAlimento AS Alimento,
    d.cantidad AS CantidadRegistrada,
    d.unidadMedida AS UnidadRegistrada,
    d.costoUnitario AS CostoUnitarioRegistrado,
    CAST(d.cantidad * d.costoUnitario AS DECIMAL(18,2)) AS ImporteSegunFormulaAnterior
FROM dbo.tblComprasAlimento c
INNER JOIN dbo.tblProveedores p
    ON p.id = c.idProveedor
INNER JOIN dbo.tblDetalleComprasAlimento d
    ON d.idCompra = c.id
INNER JOIN dbo.tblAlimentos a
    ON a.id = d.idAlimento
WHERE d.unidadMedida IN ('Libras', 'Kilogramos')
ORDER BY c.fechaCompra DESC, c.id DESC, d.id;
GO
