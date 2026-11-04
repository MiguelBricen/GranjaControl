USE GranjaControlDB;
GO

CREATE OR ALTER PROCEDURE sp_MostrarTarifasPolloActivas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        precioPorLibra,
        fechaInicio,
        fechaFin,
        activo
    FROM tblTarifasPollo
    WHERE activo = 1
    ORDER BY fechaInicio DESC;
END
GO


CREATE OR ALTER PROCEDURE sp_MostrarTarifasPollo
    @pagina INT = 1,
    @tamPagina INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        precioPorLibra,
        fechaInicio,
        fechaFin,
        activo
    FROM tblTarifasPollo
    ORDER BY fechaInicio DESC
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO


CREATE OR ALTER PROCEDURE sp_ContarTarifasPollo
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS total
    FROM tblTarifasPollo;
END
GO


CREATE OR ALTER PROCEDURE sp_ObtenerTarifaPolloPorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        precioPorLibra,
        fechaInicio,
        fechaFin,
        activo
    FROM tblTarifasPollo
    WHERE id = @Id;
END
GO


CREATE OR ALTER PROCEDURE sp_AgregarTarifaPollo
    @precioPorLibra DECIMAL(10,2),
    @fechaInicio DATE,
    @fechaFin DATE = NULL,
    @activo BIT = 1,
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

        IF @precioPorLibra <= 0
        BEGIN
            THROW 52049, 'El precio por libra debe ser mayor que cero.', 1;
        END;

        IF @fechaFin IS NOT NULL AND @fechaFin < @fechaInicio
        BEGIN
            THROW 52050, 'La fecha fin no puede ser anterior a la fecha inicio.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblTarifasPollo
            WHERE
                @fechaInicio <= ISNULL(fechaFin, '99991231')
                AND ISNULL(@fechaFin, '99991231') >= fechaInicio
        )
        BEGIN
            THROW 52051, 'El período de la tarifa se cruza con otra tarifa existente.', 1;
        END;

        INSERT INTO tblTarifasPollo
        (
            precioPorLibra,
            fechaInicio,
            fechaFin,
            activo
        )
        VALUES
        (
            @precioPorLibra,
            @fechaInicio,
            @fechaFin,
            @activo
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


CREATE OR ALTER PROCEDURE sp_ActualizarTarifaPollo
    @Id INT,
    @precioPorLibra DECIMAL(10,2),
    @fechaInicio DATE,
    @fechaFin DATE = NULL,
    @activo BIT,
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
            FROM tblTarifasPollo
            WHERE id = @Id
        )
        BEGIN
            THROW 52052, 'La tarifa de pollo no existe.', 1;
        END;

        IF @precioPorLibra <= 0
        BEGIN
            THROW 52049, 'El precio por libra debe ser mayor que cero.', 1;
        END;

        IF @fechaFin IS NOT NULL AND @fechaFin < @fechaInicio
        BEGIN
            THROW 52050, 'La fecha fin no puede ser anterior a la fecha inicio.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblTarifasPollo
            WHERE id <> @Id
              AND @fechaInicio <= ISNULL(fechaFin, '99991231')
              AND ISNULL(@fechaFin, '99991231') >= fechaInicio
        )
        BEGIN
            THROW 52051, 'El período de la tarifa se cruza con otra tarifa existente.', 1;
        END;

        UPDATE tblTarifasPollo
        SET
            precioPorLibra = @precioPorLibra,
            fechaInicio = @fechaInicio,
            fechaFin = @fechaFin,
            activo = @activo
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


CREATE OR ALTER PROCEDURE sp_CambiarEstadoTarifaPollo
    @Id INT,
    @activo BIT,
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
            FROM tblTarifasPollo
            WHERE id = @Id
        )
        BEGIN
            THROW 52052, 'La tarifa de pollo no existe.', 1;
        END;

        UPDATE tblTarifasPollo
        SET activo = @activo
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