USE GranjaControlDB;
GO

CREATE OR ALTER PROCEDURE sp_MostrarClientesActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreCliente,
        telefono,
        direccion,
        tipoCliente,
        activo
    FROM tblClientes
    WHERE activo = 1
    ORDER BY nombreCliente;
END
GO


CREATE OR ALTER PROCEDURE sp_MostrarClientes
    @buscar NVARCHAR(100) = '',
    @pagina INT = 1,
    @tamPagina INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(ISNULL(@buscar, ''));

    SELECT
        id,
        nombreCliente,
        telefono,
        direccion,
        tipoCliente,
        activo
    FROM tblClientes
    WHERE nombreCliente LIKE '%' + @buscar + '%'
       OR telefono LIKE '%' + @buscar + '%'
       OR tipoCliente LIKE '%' + @buscar + '%'
    ORDER BY nombreCliente
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO


CREATE OR ALTER PROCEDURE sp_ContarClientes
    @buscar NVARCHAR(100) = ''
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(ISNULL(@buscar, ''));

    SELECT COUNT(*) AS total
    FROM tblClientes
    WHERE nombreCliente LIKE '%' + @buscar + '%'
       OR telefono LIKE '%' + @buscar + '%'
       OR tipoCliente LIKE '%' + @buscar + '%';
END
GO


CREATE OR ALTER PROCEDURE sp_ObtenerClientePorId
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreCliente,
        telefono,
        direccion,
        tipoCliente,
        activo
    FROM tblClientes
    WHERE id = @Id;
END
GO


CREATE OR ALTER PROCEDURE sp_AgregarCliente
    @nombreCliente NVARCHAR(100),
    @telefono NVARCHAR(25) = NULL,
    @direccion NVARCHAR(200) = NULL,
    @tipoCliente NVARCHAR(30),
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

        SET @nombreCliente = TRIM(@nombreCliente);
        SET @telefono = NULLIF(TRIM(ISNULL(@telefono, '')), '');
        SET @direccion = NULLIF(TRIM(ISNULL(@direccion, '')), '');
        SET @tipoCliente = TRIM(@tipoCliente);

        IF @nombreCliente = ''
        BEGIN
            THROW 52045, 'El nombre del cliente es obligatorio.', 1;
        END;

        IF @tipoCliente NOT IN ('Cliente específico', 'Pulpería', 'Negocio', 'Otro')
        BEGIN
            THROW 52046, 'El tipo de cliente no es válido.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblClientes
            WHERE nombreCliente = @nombreCliente
        )
        BEGIN
            THROW 52047, 'Ya existe un cliente con ese nombre.', 1;
        END;

        INSERT INTO tblClientes
        (
            nombreCliente,
            telefono,
            direccion,
            tipoCliente
        )
        VALUES
        (
            @nombreCliente,
            @telefono,
            @direccion,
            @tipoCliente
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


CREATE OR ALTER PROCEDURE sp_ActualizarCliente
    @Id INT,
    @nombreCliente NVARCHAR(100),
    @telefono NVARCHAR(25) = NULL,
    @direccion NVARCHAR(200) = NULL,
    @tipoCliente NVARCHAR(30),
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

        SET @nombreCliente = TRIM(@nombreCliente);
        SET @telefono = NULLIF(TRIM(ISNULL(@telefono, '')), '');
        SET @direccion = NULLIF(TRIM(ISNULL(@direccion, '')), '');
        SET @tipoCliente = TRIM(@tipoCliente);

        IF NOT EXISTS
        (
            SELECT 1
            FROM tblClientes
            WHERE id = @Id
        )
        BEGIN
            THROW 52048, 'El cliente no existe.', 1;
        END;

        IF @nombreCliente = ''
        BEGIN
            THROW 52045, 'El nombre del cliente es obligatorio.', 1;
        END;

        IF @tipoCliente NOT IN ('Cliente específico', 'Pulpería', 'Negocio', 'Otro')
        BEGIN
            THROW 52046, 'El tipo de cliente no es válido.', 1;
        END;

        IF EXISTS
        (
            SELECT 1
            FROM tblClientes
            WHERE nombreCliente = @nombreCliente
              AND id <> @Id
        )
        BEGIN
            THROW 52047, 'Ya existe otro cliente con ese nombre.', 1;
        END;

        UPDATE tblClientes
        SET
            nombreCliente = @nombreCliente,
            telefono = @telefono,
            direccion = @direccion,
            tipoCliente = @tipoCliente
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


CREATE OR ALTER PROCEDURE sp_CambiarEstadoCliente
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
            FROM tblClientes
            WHERE id = @Id
        )
        BEGIN
            THROW 52048, 'El cliente no existe.', 1;
        END;

        UPDATE tblClientes
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