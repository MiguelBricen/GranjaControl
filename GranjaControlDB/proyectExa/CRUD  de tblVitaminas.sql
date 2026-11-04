USE GranjaControlDB;
GO

--> MOSTRAR VITAMINAS ACTIVAS
CREATE OR ALTER PROC sp_MostrarVitaminasActivas
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreVitamina,
        descripcion
    FROM tblVitaminas
    WHERE activo = 1
    ORDER BY nombreVitamina;
END
GO

--> MOSTRAR VITAMINAS
CREATE OR ALTER PROC sp_MostrarVitaminas
(
    @buscar NVARCHAR(100) = NULL,
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
        nombreVitamina,
        descripcion,
        activo
    FROM tblVitaminas
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreVitamina LIKE '%' + @buscar + '%'
        OR descripcion LIKE '%' + @buscar + '%'
    ORDER BY nombreVitamina
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> CONTAR VITAMINAS
CREATE OR ALTER PROC sp_ContarVitaminas
(
    @buscar NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblVitaminas
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreVitamina LIKE '%' + @buscar + '%'
        OR descripcion LIKE '%' + @buscar + '%';
END
GO

--> OBTENER VITAMINA POR ID
CREATE OR ALTER PROC sp_ObtenerVitaminaPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreVitamina,
        descripcion,
        activo
    FROM tblVitaminas
    WHERE id = @Id;
END
GO

--> AGREGAR VITAMINA
CREATE OR ALTER PROC sp_AgregarVitamina
(
    @nombreVitamina NVARCHAR(100),
    @descripcion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreVitamina = TRIM(@nombreVitamina);
    SET @descripcion = TRIM(@descripcion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF EXISTS
            (
                SELECT 1
                FROM tblVitaminas
                WHERE nombreVitamina = @nombreVitamina
            )
                THROW 52027, 'La vitamina ya existe', 1;

            INSERT INTO tblVitaminas
            (
                nombreVitamina,
                descripcion
            )
            VALUES
            (
                @nombreVitamina,
                @descripcion
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

--> ACTUALIZAR VITAMINA
CREATE OR ALTER PROC sp_ActualizarVitamina
(
    @Id INT,
    @nombreVitamina NVARCHAR(100),
    @descripcion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreVitamina = TRIM(@nombreVitamina);
    SET @descripcion = TRIM(@descripcion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblVitaminas
                WHERE id = @Id
            )
                THROW 52028, 'La vitamina no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblVitaminas
                WHERE nombreVitamina = @nombreVitamina
                  AND id <> @Id
            )
                THROW 52027, 'La vitamina ya existe', 1;

            UPDATE tblVitaminas
            SET
                nombreVitamina = @nombreVitamina,
                descripcion = @descripcion
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

--> CAMBIAR ESTADO DE LA VITAMINA
CREATE OR ALTER PROC sp_CambiarEstadoVitamina
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
                FROM tblVitaminas
                WHERE id = @Id
            )
                THROW 52028, 'La vitamina no existe', 1;

            UPDATE tblVitaminas
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

