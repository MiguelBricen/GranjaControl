USE GranjaControlDB
GO

CREATE OR ALTER PROCEDURE sp_MostrarDetalleVentas
    @idVenta INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.id,
        d.idVenta,
        d.idLote,
        l.codigoLote,
        d.pesoLibras,
        d.precioPorLibra,
        d.pesoLibras * d.precioPorLibra AS subtotal
    FROM tblDetalleVentas d
    INNER JOIN tblLotes l
        ON l.id = d.idLote
    WHERE d.idVenta = @idVenta
    ORDER BY d.id;
END
GO


CREATE OR ALTER PROCEDURE sp_ObtenerDetalleVentaPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.id,
        d.idVenta,
        d.idLote,
        l.codigoLote,
        d.pesoLibras,
        d.precioPorLibra,
        d.pesoLibras * d.precioPorLibra AS subtotal
    FROM tblDetalleVentas d
    INNER JOIN tblLotes l
        ON l.id = d.idLote
    WHERE d.id = @Id;
END
GO


CREATE OR ALTER PROCEDURE sp_AgregarDetalleVenta
    @idVenta INT,
    @idLote INT,
    @pesoLibras DECIMAL(10,2),
    @usuarioApp NVARCHAR(25)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @mensajeError NVARCHAR(MAX),
        @numeroError INT,
        @procedimiento NVARCHAR(150),
        @lineaError INT,
        @fechaVenta DATETIME,
        @precioPorLibra DECIMAL(10,2),
        @cantidadInicial INT,
        @totalMortalidades INT,
        @totalVendidos INT,
        @disponibles INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblVentas
            WHERE id = @idVenta
        )
        BEGIN
            THROW 52056, 'La venta no existe.', 1;
        END;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblLotes
            WHERE id = @idLote
        )
        BEGIN
            THROW 52057, 'El lote no existe.', 1;
        END;

        IF @pesoLibras <= 0
        BEGIN
            THROW 52058, 'El peso del pollo debe ser mayor que cero.', 1;
        END;

        SELECT
            @fechaVenta = fechaVenta
        FROM tblVentas
        WHERE id = @idVenta;

        SELECT
            @cantidadInicial = cantidadInicial
        FROM tblLotes
        WHERE id = @idLote
          AND estado <> 'Cancelado';

        IF @cantidadInicial IS NULL
        BEGIN
            THROW 52059, 'El lote no existe o está cancelado.', 1;
        END;

        SELECT
            @totalMortalidades = ISNULL(SUM(cantidad), 0)
        FROM tblMortalidades
        WHERE idLote = @idLote;

        SELECT
            @totalVendidos = COUNT(*)
        FROM tblDetalleVentas
        WHERE idLote = @idLote;

        SET @disponibles =
            @cantidadInicial
            - @totalMortalidades
            - @totalVendidos;

        IF @disponibles <= 0
        BEGIN
            THROW 52060, 'El lote no tiene pollos disponibles para la venta.', 1;
        END;

        SELECT TOP 1
            @precioPorLibra = precioPorLibra
        FROM tblTarifasPollo
        WHERE activo = 1
          AND CAST(@fechaVenta AS DATE) >= fechaInicio
          AND CAST(@fechaVenta AS DATE) <= ISNULL(fechaFin, '99991231')
        ORDER BY fechaInicio DESC;

        IF @precioPorLibra IS NULL
        BEGIN
            THROW 52061, 'No existe una tarifa vigente para la fecha de la venta.', 1;
        END;

        INSERT INTO tblDetalleVentas
        (
            idVenta,
            idLote,
            pesoLibras,
            precioPorLibra
        )
        VALUES
        (
            @idVenta,
            @idLote,
            @pesoLibras,
            @precioPorLibra
        );

        COMMIT;

        SELECT
            SCOPE_IDENTITY() AS id,
            @precioPorLibra AS precioPorLibra;
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


CREATE OR ALTER PROCEDURE sp_ActualizarDetalleVenta
    @Id INT,
    @idVenta INT,
    @idLote INT,
    @pesoLibras DECIMAL(10,2),
    @usuarioApp NVARCHAR(25)
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE
        @mensajeError NVARCHAR(MAX),
        @numeroError INT,
        @procedimiento NVARCHAR(150),
        @lineaError INT,
        @cantidadInicial INT,
        @totalMortalidades INT,
        @totalVendidos INT,
        @disponibles INT,
        @precioPorLibra DECIMAL(10,2),
        @fechaVenta DATETIME;

    BEGIN TRY
        BEGIN TRANSACTION;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblDetalleVentas
            WHERE id = @Id
        )
        BEGIN
            THROW 52062, 'El detalle de venta no existe.', 1;
        END;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblVentas
            WHERE id = @idVenta
        )
        BEGIN
            THROW 52056, 'La venta no existe.', 1;
        END;

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblLotes
            WHERE id = @idLote
              AND estado <> 'Cancelado'
        )
        BEGIN
            THROW 52059, 'El lote no existe o está cancelado.', 1;
        END;

        IF @pesoLibras <= 0
        BEGIN
            THROW 52058, 'El peso del pollo debe ser mayor que cero.', 1;
        END;

        SELECT
            @fechaVenta = fechaVenta
        FROM tblVentas
        WHERE id = @idVenta;

        SELECT
            @cantidadInicial = cantidadInicial
        FROM tblLotes
        WHERE id = @idLote;

        SELECT
            @totalMortalidades = ISNULL(SUM(cantidad), 0)
        FROM tblMortalidades
        WHERE idLote = @idLote;

        SELECT
            @totalVendidos = COUNT(*)
        FROM tblDetalleVentas
        WHERE idLote = @idLote
          AND id <> @Id;

        SET @disponibles =
            @cantidadInicial
            - @totalMortalidades
            - @totalVendidos;

        IF @disponibles <= 0
        BEGIN
            THROW 52060, 'El lote no tiene pollos disponibles para la venta.', 1;
        END;

        SELECT TOP 1
            @precioPorLibra = precioPorLibra
        FROM tblTarifasPollo
        WHERE activo = 1
          AND CAST(@fechaVenta AS DATE) >= fechaInicio
          AND CAST(@fechaVenta AS DATE) <= ISNULL(fechaFin, '99991231')
        ORDER BY fechaInicio DESC;

        IF @precioPorLibra IS NULL
        BEGIN
            THROW 52061, 'No existe una tarifa vigente para la fecha de la venta.', 1;
        END;

        UPDATE tblDetalleVentas
        SET
            idVenta = @idVenta,
            idLote = @idLote,
            pesoLibras = @pesoLibras,
            precioPorLibra = @precioPorLibra
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


CREATE OR ALTER PROCEDURE sp_EliminarDetalleVenta
    @Id INT,
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

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblDetalleVentas
            WHERE id = @Id
        )
        BEGIN
            THROW 52062, 'El detalle de venta no existe.', 1;
        END;

        DELETE FROM tblDetalleVentas
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