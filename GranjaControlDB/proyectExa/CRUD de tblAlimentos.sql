USE GranjaControlDB;
GO

 --> MOSTRAR ALIMENTOS ACTIVOS
CREATE OR ALTER PROC sp_MostrarAlimentosActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreAlimento,
        tipoAlimento,
        descripcion
    FROM tblAlimentos
    WHERE activo = 1
    ORDER BY nombreAlimento;
END
GO


--> MOSTRAR ALIMENTOS
CREATE OR ALTER PROC sp_MostrarAlimentos
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
        nombreAlimento,
        tipoAlimento,
        descripcion,
        activo
    FROM tblAlimentos
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreAlimento LIKE '%' + @buscar + '%'
        OR tipoAlimento LIKE '%' + @buscar + '%'
        OR descripcion LIKE '%' + @buscar + '%'
    ORDER BY nombreAlimento
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO


--> CONTAR ALIMENTOS
CREATE OR ALTER PROC sp_ContarAlimentos
(
    @buscar NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblAlimentos
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreAlimento LIKE '%' + @buscar + '%'
        OR tipoAlimento LIKE '%' + @buscar + '%'
        OR descripcion LIKE '%' + @buscar + '%';
END
GO


--> OBTENER ALIMENTO POR ID
CREATE OR ALTER PROC sp_ObtenerAlimentoPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreAlimento,
        tipoAlimento,
        descripcion,
        activo
    FROM tblAlimentos
    WHERE id = @Id;
END
GO


--> AGREGAR ALIMENTO 
CREATE OR ALTER PROC sp_AgregarAlimento
(
    @nombreAlimento NVARCHAR(100),
    @tipoAlimento NVARCHAR(20),
    @descripcion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreAlimento = TRIM(@nombreAlimento);
    SET @tipoAlimento = TRIM(@tipoAlimento);
    SET @descripcion = TRIM(@descripcion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE nombreAlimento = @nombreAlimento
            )
                THROW 52019, 'El alimento ya existe', 1;

            IF @tipoAlimento NOT IN ('Inicio', 'Finalizador')
                THROW 52020, 'El tipo de alimento no es válido', 1;

            INSERT INTO tblAlimentos
            (
                nombreAlimento,
                tipoAlimento,
                descripcion
            )
            VALUES
            (
                @nombreAlimento,
                @tipoAlimento,
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


--> ACTUALIZAR ALIMENTO 
CREATE OR ALTER PROC sp_ActualizarAlimento
(
    @Id INT,
    @nombreAlimento NVARCHAR(100),
    @tipoAlimento NVARCHAR(20),
    @descripcion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreAlimento = TRIM(@nombreAlimento);
    SET @tipoAlimento = TRIM(@tipoAlimento);
    SET @descripcion = TRIM(@descripcion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE id = @Id
            )
                THROW 52021, 'El alimento no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE nombreAlimento = @nombreAlimento
                  AND id <> @Id
            )
                THROW 52019, 'El alimento ya existe', 1;

            IF @tipoAlimento NOT IN ('Inicio', 'Finalizador')
                THROW 52020, 'El tipo de alimento no es válido', 1;

            UPDATE tblAlimentos
            SET
                nombreAlimento = @nombreAlimento,
                tipoAlimento = @tipoAlimento,
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


--> CAMBIAR ESTADO DEL ALIMENTO 
CREATE OR ALTER PROC sp_CambiarEstadoAlimento
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
                FROM tblAlimentos
                WHERE id = @Id
            )
                THROW 52021, 'El alimento no existe', 1;

            UPDATE tblAlimentos
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


