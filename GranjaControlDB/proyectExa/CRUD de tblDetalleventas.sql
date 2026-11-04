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
        v.numeroVenta,
        d.idLote,
        l.codigoLote,
        d.cantidadPollos,
        d.pesoLibras,
        d.precioPorLibra,
        d.pesoLibras * d.precioPorLibra AS subtotal
    FROM tblDetalleVentas d
    INNER JOIN tblVentas v
        ON v.id = d.idVenta
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
        v.numeroVenta,
        d.idLote,
        l.codigoLote,
        d.cantidadPollos,
        d.pesoLibras,
        d.precioPorLibra,
        d.pesoLibras * d.precioPorLibra AS subtotal
    FROM tblDetalleVentas d
    INNER JOIN tblVentas v
        ON v.id = d.idVenta
    INNER JOIN tblLotes l
        ON l.id = d.idLote
    WHERE d.id = @Id;
END
GO


CREATE OR ALTER PROCEDURE sp_AgregarDetalleVenta
    @idVenta INT,
    @idLote INT,
    @cantidadPollos INT,
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
        @estadoVenta NVARCHAR(15),
        @precioPorLibra DECIMAL(10,2),
        @cantidadInicial INT,
        @totalMortalidades INT,
        @totalVendidos INT,
        @disponibles INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT
            @fechaVenta = fechaVenta,
            @estadoVenta = estado
        FROM tblVentas
        WHERE id = @idVenta;

        IF @fechaVenta IS NULL
        BEGIN
            THROW 52056, 'La venta no existe.', 1;
        END;

        IF @estadoVenta = 'Anulada'
        BEGIN
            THROW 52065, 'La venta está anulada y no puede modificarse.', 1;
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

        IF @cantidadPollos <= 0
        BEGIN
            THROW 52063, 'La cantidad de pollos debe ser mayor que cero.', 1;
        END;

        IF @pesoLibras <= 0
        BEGIN
            THROW 52058, 'El peso total en libras debe ser mayor que cero.', 1;
        END;

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
            @totalVendidos = ISNULL(SUM(d.cantidadPollos), 0)
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v
            ON v.id = d.idVenta
        WHERE d.idLote = @idLote
          AND v.estado <> 'Anulada';

        SET @disponibles =
            @cantidadInicial
            - @totalMortalidades
            - @totalVendidos;

        IF @disponibles <= 0
        BEGIN
            THROW 52060, 'El lote no tiene pollos disponibles para la venta.', 1;
        END;

        IF @cantidadPollos > @disponibles
        BEGIN
            THROW 52064, 'La cantidad de pollos supera la disponibilidad del lote.', 1;
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
            cantidadPollos,
            pesoLibras,
            precioPorLibra
        )
        VALUES
        (
            @idVenta,
            @idLote,
            @cantidadPollos,
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
    @cantidadPollos INT,
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
        @fechaVenta DATETIME,
        @estadoVenta NVARCHAR(15),
        @idVentaAnterior INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT
            @idVentaAnterior = idVenta
        FROM tblDetalleVentas
        WHERE id = @Id;

        IF @idVentaAnterior IS NULL
        BEGIN
            THROW 52062, 'El detalle de venta no existe.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblVentas
            WHERE id = @idVentaAnterior
              AND estado = 'Anulada'
        )
        BEGIN
            THROW 52065, 'La venta está anulada y no puede modificarse.', 1;
        END;

        SELECT
            @fechaVenta = fechaVenta,
            @estadoVenta = estado
        FROM tblVentas
        WHERE id = @idVenta;

        IF @fechaVenta IS NULL
        BEGIN
            THROW 52056, 'La venta no existe.', 1;
        END;

        IF @estadoVenta = 'Anulada'
        BEGIN
            THROW 52065, 'La venta está anulada y no puede modificarse.', 1;
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

        IF @cantidadPollos <= 0
        BEGIN
            THROW 52063, 'La cantidad de pollos debe ser mayor que cero.', 1;
        END;

        IF @pesoLibras <= 0
        BEGIN
            THROW 52058, 'El peso total en libras debe ser mayor que cero.', 1;
        END;

        SELECT
            @cantidadInicial = cantidadInicial
        FROM tblLotes
        WHERE id = @idLote;

        SELECT
            @totalMortalidades = ISNULL(SUM(cantidad), 0)
        FROM tblMortalidades
        WHERE idLote = @idLote;

        SELECT
            @totalVendidos = ISNULL(SUM(d.cantidadPollos), 0)
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v
            ON v.id = d.idVenta
        WHERE d.idLote = @idLote
          AND d.id <> @Id
          AND v.estado <> 'Anulada';

        SET @disponibles =
            @cantidadInicial
            - @totalMortalidades
            - @totalVendidos;

        IF @disponibles <= 0
        BEGIN
            THROW 52060, 'El lote no tiene pollos disponibles para la venta.', 1;
        END;

        IF @cantidadPollos > @disponibles
        BEGIN
            THROW 52064, 'La cantidad de pollos supera la disponibilidad del lote.', 1;
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
            cantidadPollos = @cantidadPollos,
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
        @lineaError INT,
        @idVenta INT;

    BEGIN TRY
        BEGIN TRANSACTION;

        SELECT
            @idVenta = idVenta
        FROM tblDetalleVentas
        WHERE id = @Id;

        IF @idVenta IS NULL
        BEGIN
            THROW 52062, 'El detalle de venta no existe.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblVentas
            WHERE id = @idVenta
              AND estado = 'Anulada'
        )
        BEGIN
            THROW 52065, 'La venta está anulada y no puede modificarse.', 1;
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
