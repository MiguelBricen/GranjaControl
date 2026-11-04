USE GranjaControlDB;
GO

--> MOSTRAR GALPONES ACTIVOS
CREATE OR ALTER PROC sp_MostrarGalponesActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreGalpon,
        capacidad,
        descripcion
    FROM tblGalpones
    WHERE activo = 1
    ORDER BY nombreGalpon;
END
GO

--> MOSTRAR GALPONES
CREATE OR ALTER PROC sp_MostrarGalpones
(
    @buscar NVARCHAR(75) = NULL,
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
        nombreGalpon,
        capacidad,
        descripcion,
        activo
    FROM tblGalpones
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreGalpon LIKE '%' + @buscar + '%'
        OR descripcion LIKE '%' + @buscar + '%'
    ORDER BY nombreGalpon
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> CONTAR GALPONES
CREATE OR ALTER PROC sp_ContarGalpones
(
    @buscar NVARCHAR(75) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblGalpones
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreGalpon LIKE '%' + @buscar + '%'
        OR descripcion LIKE '%' + @buscar + '%';
END
GO


--> OBTENER GALPÓN POR ID
CREATE OR ALTER PROC sp_ObtenerGalponPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreGalpon,
        capacidad,
        descripcion,
        activo
    FROM tblGalpones
    WHERE id = @Id;
END
GO

--> AGREGAR GALPÓN
CREATE OR ALTER PROC sp_AgregarGalpon
(
    @nombreGalpon NVARCHAR(75),
    @capacidad INT,
    @descripcion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreGalpon = TRIM(@nombreGalpon);
    SET @descripcion = TRIM(@descripcion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF EXISTS
            (
                SELECT 1
                FROM tblGalpones
                WHERE nombreGalpon = @nombreGalpon
            )
                THROW 52006, 'El galpón ya existe', 1;

            IF @capacidad <= 0
                THROW 52007, 'La capacidad del galpón debe ser mayor que cero', 1;

            INSERT INTO tblGalpones
            (
                nombreGalpon,
                capacidad,
                descripcion
            )
            VALUES
            (
                @nombreGalpon,
                @capacidad,
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

--> ACTUALIZAR GALPÓN
CREATE OR ALTER PROC sp_ActualizarGalpon
(
    @Id INT,
    @nombreGalpon NVARCHAR(75),
    @capacidad INT,
    @descripcion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreGalpon = TRIM(@nombreGalpon);
    SET @descripcion = TRIM(@descripcion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblGalpones
                WHERE id = @Id
            )
                THROW 52008, 'El galpón no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblGalpones
                WHERE nombreGalpon = @nombreGalpon
                  AND id <> @Id
            )
                THROW 52006, 'El galpón ya existe', 1;

            IF @capacidad <= 0
                THROW 52007, 'La capacidad del galpón debe ser mayor que cero', 1;

            UPDATE tblGalpones
            SET
                nombreGalpon = @nombreGalpon,
                capacidad = @capacidad,
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


--> CAMBIAR ESTADO DEL GALPÓN
CREATE OR ALTER PROC sp_CambiarEstadoGalpon
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
                FROM tblGalpones
                WHERE id = @Id
            )
                THROW 52008, 'El galpón no existe', 1;

            UPDATE tblGalpones
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
