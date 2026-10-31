/*
    Nota de esquema:
    En la base real, tblVentas.numeroVenta es NVARCHAR(25) y
    tblVentas.observacion es NVARCHAR(250). Algunos scripts y entidades
    permiten longitudes mayores (50 y 500). No se modifica el esquema ni
    se corrigen esos limites en esta fase; los procedimientos de reportes
    no dependen de dichas longitudes.
*/

USE GranjaControlDB;
GO

CREATE OR ALTER PROCEDURE sp_DashboardResumen
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @inicioMes DATE =
        DATEFROMPARTS(YEAR(GETDATE()), MONTH(GETDATE()), 1);

    DECLARE @finMes DATE =
        DATEADD(DAY, 1, EOMONTH(GETDATE()));

    ;WITH LotesActivos AS
    (
        SELECT
            l.id,
            l.cantidadInicial
        FROM tblLotes l
        WHERE l.estado IN ('En producción', 'Listo para venta')
    ),
    Mortalidad AS
    (
        SELECT
            m.idLote,
            SUM(m.cantidad) AS Cantidad
        FROM tblMortalidades m
        WHERE m.idLote IN (SELECT id FROM LotesActivos)
        GROUP BY m.idLote
    ),
    Vendidos AS
    (
        SELECT
            d.idLote,
            SUM(d.cantidadPollos) AS Cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v
            ON v.id = d.idVenta
        WHERE d.idLote IN (SELECT id FROM LotesActivos)
          AND v.estado <> 'Anulada'
        GROUP BY d.idLote
    )
    SELECT
        (SELECT COUNT(*) FROM LotesActivos) AS LotesActivos,
        ISNULL(SUM(la.cantidadInicial), 0) AS AvesIniciales,
        ISNULL(
            SUM(
                la.cantidadInicial
                - ISNULL(m.Cantidad, 0)
                - ISNULL(v.Cantidad, 0)
            ),
            0
        ) AS AvesDisponibles,
        (
            SELECT ISNULL(SUM(d.cantidadPollos), 0)
            FROM tblDetalleVentas d
            INNER JOIN tblVentas v
                ON v.id = d.idVenta
            WHERE v.estado = 'Registrada'
              AND v.fechaVenta >= @inicioMes
              AND v.fechaVenta < @finMes
        ) AS PollosVendidosMes,
        (
            SELECT COUNT(*)
            FROM tblVentas v
            WHERE v.estado = 'Registrada'
              AND v.fechaVenta >= @inicioMes
              AND v.fechaVenta < @finMes
        ) AS VentasMes,
        (
            SELECT ISNULL(SUM(d.pesoLibras * d.precioPorLibra), 0)
            FROM tblDetalleVentas d
            INNER JOIN tblVentas v
                ON v.id = d.idVenta
            WHERE v.estado = 'Registrada'
              AND v.fechaVenta >= @inicioMes
              AND v.fechaVenta < @finMes
        ) AS IngresosMes,
        ISNULL(SUM(ISNULL(m.Cantidad, 0)), 0) AS MortalidadAcumulada,
        (
            SELECT ISNULL(SUM(dc.cantidad * dc.costoUnitario), 0)
            FROM tblComprasAlimento c
            INNER JOIN tblDetalleComprasAlimento dc
                ON dc.idCompra = c.id
            WHERE c.fechaCompra >= @inicioMes
              AND c.fechaCompra < @finMes
        ) AS ComprasMes,
        (
            SELECT COUNT(*)
            FROM tblClientes c
            WHERE c.activo = 1
        ) AS ClientesActivos
    FROM LotesActivos la
    LEFT JOIN Mortalidad m
        ON m.idLote = la.id
    LEFT JOIN Vendidos v
        ON v.idLote = la.id;
END
GO

CREATE OR ALTER PROCEDURE sp_DashboardLotesActivos
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Mortalidad AS
    (
        SELECT
            m.idLote,
            SUM(m.cantidad) AS Cantidad
        FROM tblMortalidades m
        GROUP BY m.idLote
    ),
    Vendidos AS
    (
        SELECT
            d.idLote,
            SUM(d.cantidadPollos) AS Cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v
            ON v.id = d.idVenta
        WHERE v.estado <> 'Anulada'
        GROUP BY d.idLote
    )
    SELECT
        l.id,
        l.codigoLote,
        g.nombreGalpon,
        p.nombreProveedor,
        l.fechaIngreso,
        l.estado,
        l.cantidadInicial,
        ISNULL(m.Cantidad, 0) AS Mortalidad,
        ISNULL(v.Cantidad, 0) AS Vendidos,
        l.cantidadInicial
            - ISNULL(m.Cantidad, 0)
            - ISNULL(v.Cantidad, 0) AS Disponibles,
        CAST(
            100.0 * ISNULL(m.Cantidad, 0)
            / NULLIF(l.cantidadInicial, 0)
            AS DECIMAL(5, 2)
        ) AS PorcentajeMortalidad
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON g.id = l.idGalpon
    INNER JOIN tblProveedores p
        ON p.id = l.idProveedor
    LEFT JOIN Mortalidad m
        ON m.idLote = l.id
    LEFT JOIN Vendidos v
        ON v.idLote = l.id
    WHERE l.estado IN ('En producción', 'Listo para venta')
    ORDER BY l.fechaIngreso DESC, l.codigoLote;
END
GO

CREATE OR ALTER PROCEDURE sp_DashboardVentasRecientes
    @cantidad INT = 5
AS
BEGIN
    SET NOCOUNT ON;

    IF @cantidad IS NULL OR @cantidad < 1
        SET @cantidad = 5;

    IF @cantidad > 50
        SET @cantidad = 50;

    SELECT TOP (@cantidad)
        v.id,
        v.numeroVenta,
        c.nombreCliente,
        v.fechaVenta,
        v.estado,
        ISNULL(SUM(d.cantidadPollos), 0) AS CantidadPollos,
        ISNULL(SUM(d.pesoLibras), 0) AS PesoLibras,
        ISNULL(SUM(d.pesoLibras * d.precioPorLibra), 0) AS Total
    FROM tblVentas v
    INNER JOIN tblClientes c
        ON c.id = v.idCliente
    LEFT JOIN tblDetalleVentas d
        ON d.idVenta = v.id
    GROUP BY
        v.id,
        v.numeroVenta,
        c.nombreCliente,
        v.fechaVenta,
        v.estado
    ORDER BY v.fechaVenta DESC, v.id DESC;
END
GO

CREATE OR ALTER PROCEDURE sp_ReporteVentasPeriodo
    @fechaInicio DATE = NULL,
    @fechaFin DATE = NULL,
    @idCliente INT = NULL,
    @estado NVARCHAR(15) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @idCliente IS NOT NULL AND @idCliente <= 0
        SET @idCliente = NULL;

    SET @estado = NULLIF(TRIM(ISNULL(@estado, '')), '');

    CREATE TABLE #Ventas
    (
        Id INT NOT NULL,
        NumeroVenta NVARCHAR(25) NOT NULL,
        NombreCliente NVARCHAR(100) NOT NULL,
        FechaVenta DATETIME NOT NULL,
        Estado NVARCHAR(15) NOT NULL,
        CantidadPollos INT NOT NULL,
        PesoLibras DECIMAL(18, 2) NOT NULL,
        Total DECIMAL(18, 2) NOT NULL
    );

    INSERT INTO #Ventas
    (
        Id,
        NumeroVenta,
        NombreCliente,
        FechaVenta,
        Estado,
        CantidadPollos,
        PesoLibras,
        Total
    )
    SELECT
        v.id,
        v.numeroVenta,
        c.nombreCliente,
        v.fechaVenta,
        v.estado,
        ISNULL(SUM(d.cantidadPollos), 0),
        ISNULL(SUM(d.pesoLibras), 0),
        ISNULL(SUM(d.pesoLibras * d.precioPorLibra), 0)
    FROM tblVentas v
    INNER JOIN tblClientes c
        ON c.id = v.idCliente
    LEFT JOIN tblDetalleVentas d
        ON d.idVenta = v.id
    WHERE (@fechaInicio IS NULL OR v.fechaVenta >= @fechaInicio)
      AND (@fechaFin IS NULL OR v.fechaVenta < DATEADD(DAY, 1, @fechaFin))
      AND (@idCliente IS NULL OR v.idCliente = @idCliente)
      AND (@estado IS NULL OR v.estado = @estado)
    GROUP BY
        v.id,
        v.numeroVenta,
        c.nombreCliente,
        v.fechaVenta,
        v.estado;

    SELECT
        COUNT(*) AS TotalVentas,
        ISNULL(SUM(CASE WHEN Estado = 'Registrada' THEN 1 ELSE 0 END), 0) AS Registradas,
        ISNULL(SUM(CASE WHEN Estado = 'Anulada' THEN 1 ELSE 0 END), 0) AS Anuladas,
        ISNULL(SUM(CASE WHEN Estado = 'Registrada' THEN CantidadPollos ELSE 0 END), 0) AS PollosVendidos,
        ISNULL(SUM(CASE WHEN Estado = 'Registrada' THEN PesoLibras ELSE 0 END), 0) AS LibrasVendidas,
        ISNULL(SUM(CASE WHEN Estado = 'Registrada' THEN Total ELSE 0 END), 0) AS Ingresos
    FROM #Ventas;

    SELECT
        Id,
        NumeroVenta,
        NombreCliente,
        FechaVenta,
        Estado,
        CantidadPollos,
        PesoLibras,
        Total
    FROM #Ventas
    ORDER BY FechaVenta DESC, Id DESC;

    DROP TABLE #Ventas;
END
GO

CREATE OR ALTER PROCEDURE sp_ReporteProduccionLotes
    @fechaInicio DATE = NULL,
    @fechaFin DATE = NULL,
    @estado NVARCHAR(25) = NULL,
    @idGalpon INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    SET @estado = NULLIF(TRIM(ISNULL(@estado, '')), '');

    IF @idGalpon IS NOT NULL AND @idGalpon <= 0
        SET @idGalpon = NULL;

    CREATE TABLE #Produccion
    (
        Id INT NOT NULL,
        CodigoLote NVARCHAR(25) NOT NULL,
        NombreGalpon NVARCHAR(75) NOT NULL,
        NombreProveedor NVARCHAR(100) NOT NULL,
        FechaIngreso DATETIME NOT NULL,
        Estado NVARCHAR(25) NOT NULL,
        CantidadInicial INT NOT NULL,
        Mortalidad INT NOT NULL,
        Vendidos INT NOT NULL,
        Disponibles INT NOT NULL,
        PorcentajeMortalidad DECIMAL(5, 2) NULL
    );

    ;WITH Mortalidad AS
    (
        SELECT
            m.idLote,
            SUM(m.cantidad) AS Cantidad
        FROM tblMortalidades m
        GROUP BY m.idLote
    ),
    Vendidos AS
    (
        SELECT
            d.idLote,
            SUM(d.cantidadPollos) AS Cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v
            ON v.id = d.idVenta
        WHERE v.estado <> 'Anulada'
        GROUP BY d.idLote
    )
    INSERT INTO #Produccion
    (
        Id,
        CodigoLote,
        NombreGalpon,
        NombreProveedor,
        FechaIngreso,
        Estado,
        CantidadInicial,
        Mortalidad,
        Vendidos,
        Disponibles,
        PorcentajeMortalidad
    )
    SELECT
        l.id,
        l.codigoLote,
        g.nombreGalpon,
        p.nombreProveedor,
        l.fechaIngreso,
        l.estado,
        l.cantidadInicial,
        ISNULL(m.Cantidad, 0),
        ISNULL(v.Cantidad, 0),
        l.cantidadInicial
            - ISNULL(m.Cantidad, 0)
            - ISNULL(v.Cantidad, 0),
        CAST(
            100.0 * ISNULL(m.Cantidad, 0)
            / NULLIF(l.cantidadInicial, 0)
            AS DECIMAL(5, 2)
        )
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON g.id = l.idGalpon
    INNER JOIN tblProveedores p
        ON p.id = l.idProveedor
    LEFT JOIN Mortalidad m
        ON m.idLote = l.id
    LEFT JOIN Vendidos v
        ON v.idLote = l.id
    WHERE (@fechaInicio IS NULL OR CAST(l.fechaIngreso AS DATE) >= @fechaInicio)
      AND (@fechaFin IS NULL OR CAST(l.fechaIngreso AS DATE) <= @fechaFin)
      AND (@estado IS NULL OR l.estado = @estado)
      AND (@idGalpon IS NULL OR l.idGalpon = @idGalpon);

    SELECT
        COUNT(*) AS TotalLotes,
        ISNULL(SUM(CantidadInicial), 0) AS CantidadInicial,
        ISNULL(SUM(Mortalidad), 0) AS Mortalidad,
        ISNULL(SUM(Vendidos), 0) AS Vendidos,
        ISNULL(SUM(Disponibles), 0) AS Disponibles,
        CAST(
            100.0 * ISNULL(SUM(Mortalidad), 0)
            / NULLIF(SUM(CantidadInicial), 0)
            AS DECIMAL(5, 2)
        ) AS PorcentajeMortalidad
    FROM #Produccion;

    SELECT
        Id,
        CodigoLote,
        NombreGalpon,
        NombreProveedor,
        FechaIngreso,
        Estado,
        CantidadInicial,
        Mortalidad,
        Vendidos,
        Disponibles,
        PorcentajeMortalidad
    FROM #Produccion
    ORDER BY FechaIngreso DESC, CodigoLote;

    DROP TABLE #Produccion;
END
GO

CREATE OR ALTER PROCEDURE sp_ReporteComprasAlimento
    @fechaInicio DATE = NULL,
    @fechaFin DATE = NULL,
    @idProveedor INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    IF @idProveedor IS NOT NULL AND @idProveedor <= 0
        SET @idProveedor = NULL;

    CREATE TABLE #Compras
    (
        Id INT NOT NULL,
        NumeroCompra NVARCHAR(25) NOT NULL,
        NombreProveedor NVARCHAR(100) NOT NULL,
        FechaCompra DATETIME NOT NULL,
        Libras DECIMAL(18, 2) NOT NULL,
        Kilogramos DECIMAL(18, 2) NOT NULL,
        CostoTotal DECIMAL(18, 2) NOT NULL,
        CantidadItems INT NOT NULL
    );

    INSERT INTO #Compras
    (
        Id,
        NumeroCompra,
        NombreProveedor,
        FechaCompra,
        Libras,
        Kilogramos,
        CostoTotal,
        CantidadItems
    )
    SELECT
        c.id,
        c.numeroCompra,
        p.nombreProveedor,
        c.fechaCompra,
        ISNULL(SUM(CASE WHEN dc.unidadMedida = 'Libras' THEN dc.cantidad ELSE 0 END), 0),
        ISNULL(SUM(CASE WHEN dc.unidadMedida = 'Kilogramos' THEN dc.cantidad ELSE 0 END), 0),
        ISNULL(SUM(dc.cantidad * dc.costoUnitario), 0),
        COUNT(dc.id)
    FROM tblComprasAlimento c
    INNER JOIN tblProveedores p
        ON p.id = c.idProveedor
    LEFT JOIN tblDetalleComprasAlimento dc
        ON dc.idCompra = c.id
    WHERE (@fechaInicio IS NULL OR c.fechaCompra >= @fechaInicio)
      AND (@fechaFin IS NULL OR c.fechaCompra < DATEADD(DAY, 1, @fechaFin))
      AND (@idProveedor IS NULL OR c.idProveedor = @idProveedor)
    GROUP BY
        c.id,
        c.numeroCompra,
        p.nombreProveedor,
        c.fechaCompra;

    SELECT
        COUNT(*) AS TotalCompras,
        ISNULL(SUM(Libras), 0) AS TotalLibras,
        ISNULL(SUM(Kilogramos), 0) AS TotalKilogramos,
        ISNULL(SUM(CostoTotal), 0) AS CostoTotal
    FROM #Compras;

    SELECT
        Id,
        NumeroCompra,
        NombreProveedor,
        FechaCompra,
        Libras,
        Kilogramos,
        CostoTotal,
        CantidadItems
    FROM #Compras
    ORDER BY FechaCompra DESC, Id DESC;

    DROP TABLE #Compras;
END
GO
