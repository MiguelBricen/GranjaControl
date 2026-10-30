--> Mostrar roles activos

USE GranjaControlDB;
GO

CREATE OR ALTER PROC sp_MostrarRolesActivos1
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreRol
    FROM tblRoles
    WHERE activo = 1
    ORDER BY nombreRol;
END
GO

--> Mostrar roles 
CREATE OR ALTER PROC sp_MostrarRoles
(
    @buscar NVARCHAR(50) = NULL,
    @pagina INT = 1,
    @tamPagina INT = 10
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @pagina < 1 SET @pagina = 1;
    IF @tamPagina < 1 SET @tamPagina = 10;

    SET @buscar = TRIM(@buscar);

    SELECT
        id,
        nombreRol,
        activo
    FROM tblRoles
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreRol LIKE '%' + @buscar + '%'
    ORDER BY nombreRol
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> Contar roles
CREATE OR ALTER PROC sp_ContarRoles
(
    @buscar NVARCHAR(50) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblRoles
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreRol LIKE '%' + @buscar + '%';
END
GO

--> Obtener rol por ID
CREATE OR ALTER PROC sp_ObtenerRolPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreRol,
        activo
    FROM tblRoles
    WHERE id = @Id;
END
GO

--> Agregar rol
CREATE OR ALTER PROC sp_AgregarRol
(
    @nombreRol NVARCHAR(50),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreRol = TRIM(@nombreRol);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF EXISTS
            (
                SELECT 1
                FROM tblRoles
                WHERE nombreRol = @nombreRol
            )
                THROW 52001, 'El rol ya existe', 1;

            INSERT INTO tblRoles
            (
                nombreRol
            )
            VALUES
            (
                @nombreRol
            );

        COMMIT TRANSACTION;
    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @msg NVARCHAR(MAX) = ERROR_MESSAGE(),
            @num INT = ERROR_NUMBER(),
            @proc NVARCHAR(150) = ERROR_PROCEDURE(),
            @line INT = ERROR_LINE();

        EXEC sp_LogErrores_Insertar
            @msg,
            @num,
            @proc,
            @usuarioApp,
            @line;

        THROW;
    END CATCH
END
GO

--> Actualizar rol
CREATE OR ALTER PROC sp_ActualizarRol
(
    @Id INT,
    @nombreRol NVARCHAR(50),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreRol = TRIM(@nombreRol);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblRoles
                WHERE id = @Id
            )
                THROW 52002, 'El rol no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblRoles
                WHERE nombreRol = @nombreRol
                AND id <> @Id
            )
                THROW 52001, 'El rol ya existe', 1;

            UPDATE tblRoles
            SET nombreRol = @nombreRol
            WHERE id = @Id;

        COMMIT TRANSACTION;
    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @msg NVARCHAR(MAX) = ERROR_MESSAGE(),
            @num INT = ERROR_NUMBER(),
            @proc NVARCHAR(150) = ERROR_PROCEDURE(),
            @line INT = ERROR_LINE();

        EXEC sp_LogErrores_Insertar
            @msg,
            @num,
            @proc,
            @usuarioApp,
            @line;

        THROW;
    END CATCH
END
GO

--> Cambiar estado del rol
CREATE OR ALTER PROC sp_CambiarEstadoRol
(
    @Id INT,
    @activo BIT,
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblRoles
                WHERE id = @Id
            )
                THROW 52002, 'El rol no existe', 1;

            UPDATE tblRoles
            SET activo = @activo
            WHERE id = @Id;

        COMMIT TRANSACTION;
    END TRY

    BEGIN CATCH

        IF @@TRANCOUNT > 0
            ROLLBACK TRANSACTION;

        DECLARE
            @msg NVARCHAR(MAX) = ERROR_MESSAGE(),
            @num INT = ERROR_NUMBER(),
            @proc NVARCHAR(150) = ERROR_PROCEDURE(),
            @line INT = ERROR_LINE();

        EXEC sp_LogErrores_Insertar
            @msg,
            @num,
            @proc,
            @usuarioApp,
            @line;

        THROW;
    END CATCH
END
GO