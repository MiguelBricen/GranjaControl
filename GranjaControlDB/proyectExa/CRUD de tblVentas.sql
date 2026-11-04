USE GranjaControlDB;
GO

CREATE OR ALTER PROCEDURE sp_MostrarVentas
    @buscar NVARCHAR(100) = '',
    @pagina INT = 1,
    @tamPagina INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(ISNULL(@buscar, ''));

    SELECT
        v.id,
        v.numeroVenta,
        v.idCliente,
        c.nombreCliente,
        v.fechaVenta,
        v.observacion,
        v.estado,
        ISNULL(SUM(d.cantidadPollos), 0) AS cantidadPollos,
        ISNULL(SUM(d.pesoLibras), 0) AS pesoTotalLibras,
        ISNULL(SUM(d.pesoLibras * d.precioPorLibra), 0) AS totalVenta
    FROM tblVentas v
    INNER JOIN tblClientes c
        ON c.id = v.idCliente
    LEFT JOIN tblDetalleVentas d
        ON d.idVenta = v.id
    WHERE v.numeroVenta LIKE '%' + @buscar + '%'
       OR c.nombreCliente LIKE '%' + @buscar + '%'
       OR v.estado LIKE '%' + @buscar + '%'
    GROUP BY
        v.id,
        v.numeroVenta,
        v.idCliente,
        c.nombreCliente,
        v.fechaVenta,
        v.observacion,
        v.estado
    ORDER BY v.fechaVenta DESC, v.id DESC
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO


CREATE OR ALTER PROCEDURE sp_ContarVentas
    @buscar NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(ISNULL(@buscar, ''));

    SELECT COUNT(*) AS total
    FROM tblVentas v
    INNER JOIN tblClientes c
        ON c.id = v.idCliente
    WHERE v.numeroVenta LIKE '%' + @buscar + '%'
       OR c.nombreCliente LIKE '%' + @buscar + '%'
       OR v.estado LIKE '%' + @buscar + '%';
END
GO


CREATE OR ALTER PROCEDURE sp_ObtenerVentaPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        v.id,
        v.numeroVenta,
        v.idCliente,
        c.nombreCliente,
        v.fechaVenta,
        v.observacion,
        v.estado,
        ISNULL(SUM(d.cantidadPollos), 0) AS cantidadPollos,
        ISNULL(SUM(d.pesoLibras), 0) AS pesoTotalLibras,
        ISNULL(SUM(d.pesoLibras * d.precioPorLibra), 0) AS totalVenta
    FROM tblVentas v
    INNER JOIN tblClientes c
        ON c.id = v.idCliente
    LEFT JOIN tblDetalleVentas d
        ON d.idVenta = v.id
    WHERE v.id = @Id
    GROUP BY
        v.id,
        v.numeroVenta,
        v.idCliente,
        c.nombreCliente,
        v.fechaVenta,
        v.observacion,
        v.estado;
END
GO


CREATE OR ALTER PROCEDURE sp_AgregarVenta
    @numeroVenta NVARCHAR(50),
    @idCliente INT,
    @fechaVenta DATETIME = NULL,
    @observacion NVARCHAR(500) = NULL,
    @usuarioApp NVARCHAR(25)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @mensajeError NVARCHAR(MAX),
        @numeroError INT,
        @procedimiento NVARCHAR(150),
        @lineaError INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SET @numeroVenta = TRIM(@numeroVenta);
        SET @observacion = NULLIF(TRIM(ISNULL(@observacion, '')), '');

        IF @fechaVenta IS NULL
            SET @fechaVenta = GETDATE();

        IF @numeroVenta = ''
        BEGIN
            THROW 52053, 'El número de venta es obligatorio.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblVentas
            WHERE numeroVenta = @numeroVenta
        )
        BEGIN
            THROW 52054, 'Ya existe una venta con ese número.', 1;
        END;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblClientes
            WHERE id = @idCliente
              AND activo = 1
        )
        BEGIN
            THROW 52055, 'El cliente no existe o está inactivo.', 1;
        END;

        INSERT INTO tblVentas
        (
            numeroVenta,
            idCliente,
            fechaVenta,
            observacion,
            estado
        )
        VALUES
        (
            @numeroVenta,
            @idCliente,
            @fechaVenta,
            @observacion,
            'Registrada'
        );

        COMMIT;

        SELECT SCOPE_IDENTITY() AS id;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        SET @mensajeError = ERROR_MESSAGE();
        SET @numeroError = ERROR_NUMBER();
        SET @procedimiento = ERROR_PROCEDURE();
        SET @lineaError = ERROR_LINE();

        EXEC sp_LogErrores_Insertar
            @mensajeError,
            @numeroError,
            @procedimiento,
            @usuarioApp,
            @lineaError;

        THROW;
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE sp_ActualizarVenta
    @Id INT,
    @numeroVenta NVARCHAR(50),
    @idCliente INT,
    @fechaVenta DATETIME,
    @observacion NVARCHAR(500) = NULL,
    @usuarioApp NVARCHAR(25)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @mensajeError NVARCHAR(MAX),
        @numeroError INT,
        @procedimiento NVARCHAR(150),
        @lineaError INT,
        @estado NVARCHAR(15);

    BEGIN TRY
        BEGIN TRANSACTION;

        SET @numeroVenta = TRIM(@numeroVenta);
        SET @observacion = NULLIF(TRIM(ISNULL(@observacion, '')), '');

        SELECT @estado = estado
        FROM tblVentas
        WHERE id = @Id;

        IF @estado IS NULL
        BEGIN
            THROW 52056, 'La venta no existe.', 1;
        END;

        IF @estado = 'Anulada'
        BEGIN
            THROW 52065, 'La venta está anulada y no puede modificarse.', 1;
        END;

        IF @numeroVenta = ''
        BEGIN
            THROW 52053, 'El número de venta es obligatorio.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblVentas
            WHERE numeroVenta = @numeroVenta
              AND id <> @Id
        )
        BEGIN
            THROW 52054, 'Ya existe otra venta con ese número.', 1;
        END;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblClientes
            WHERE id = @idCliente
        )
        BEGIN
            THROW 52055, 'El cliente no existe.', 1;
        END;

        UPDATE tblVentas
        SET
            numeroVenta = @numeroVenta,
            idCliente = @idCliente,
            fechaVenta = @fechaVenta,
            observacion = @observacion
        WHERE id = @Id;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        SET @mensajeError = ERROR_MESSAGE();
        SET @numeroError = ERROR_NUMBER();
        SET @procedimiento = ERROR_PROCEDURE();
        SET @lineaError = ERROR_LINE();

        EXEC sp_LogErrores_Insertar
            @mensajeError,
            @numeroError,
            @procedimiento,
            @usuarioApp,
            @lineaError;

        THROW;
    END CATCH
END
GO


CREATE OR ALTER PROCEDURE sp_AnularVenta
    @Id INT,
    @usuarioApp NVARCHAR(25)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @mensajeError NVARCHAR(MAX),
        @numeroError INT,
        @procedimiento NVARCHAR(150),
        @lineaError INT,
        @estado NVARCHAR(15);

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT @estado = estado
        FROM tblVentas
        WHERE id = @Id;

        IF @estado IS NULL
        BEGIN
            THROW 52056, 'La venta no existe.', 1;
        END;

        IF @estado = 'Anulada'
        BEGIN
            THROW 52066, 'La venta ya está anulada.', 1;
        END;

        UPDATE tblVentas
        SET estado = 'Anulada'
        WHERE id = @Id;

        COMMIT;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
            ROLLBACK;

        SET @mensajeError = ERROR_MESSAGE();
        SET @numeroError = ERROR_NUMBER();
        SET @procedimiento = ERROR_PROCEDURE();
        SET @lineaError = ERROR_LINE();

        EXEC sp_LogErrores_Insertar
            @mensajeError,
            @numeroError,
            @procedimiento,
            @usuarioApp,
            @lineaError;

        THROW;
    END CATCH
END
GO
