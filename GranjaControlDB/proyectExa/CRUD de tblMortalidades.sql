USE GranjaControlDB;
GO

--> MOSTRAR MORTALIDADES
CREATE OR ALTER PROC sp_MostrarMortalidades
(
    @idLote INT = NULL,
    @pagina INT = 1,
    @tamPagina INT = 10
)
AS
BEGIN
    SET NOCOUNT ON;

    IF @pagina < 1 SET @pagina = 1;
    IF @tamPagina < 1 SET @tamPagina = 10;

    SELECT
        m.id,
        m.idLote,
        l.codigoLote,
        m.fecha,
        m.cantidad,
        m.causa,
        m.observacion
    FROM tblMortalidades m
    INNER JOIN tblLotes l
        ON m.idLote = l.id
    WHERE
        @idLote IS NULL
        OR m.idLote = @idLote
    ORDER BY m.fecha DESC, m.id DESC
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> CONTAR MORTALIDADES
CREATE OR ALTER PROC sp_ContarMortalidades
(
    @idLote INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Total
    FROM tblMortalidades m
    WHERE
        @idLote IS NULL
        OR m.idLote = @idLote;
END
GO

--> OBTENER MORTALIDAD POR ID
CREATE OR ALTER PROC sp_ObtenerMortalidadPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        m.id,
        m.idLote,
        l.codigoLote,
        m.fecha,
        m.cantidad,
        m.causa,
        m.observacion
    FROM tblMortalidades m
    INNER JOIN tblLotes l
        ON m.idLote = l.id
    WHERE m.id = @Id;
END
GO

--> AGREGAR MORTALIDAD
CREATE OR ALTER PROC sp_AgregarMortalidad
(
    @idLote INT,
    @fecha DATE,
    @cantidad INT,
    @causa NVARCHAR(150),
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @causa = TRIM(@causa);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @idLote
                  AND estado <> 'Cancelado'
            )
                THROW 52034, 'El lote no existe o está cancelado', 1;

            IF @cantidad <= 0
                THROW 52035, 'La cantidad de mortalidad debe ser mayor que cero', 1;

            IF @causa = ''
                THROW 52036, 'La causa de mortalidad es obligatoria', 1;

            IF
            (
                SELECT cantidadInicial
                FROM tblLotes
                WHERE id = @idLote
            )
            <
            (
                SELECT ISNULL(SUM(cantidad), 0)
                FROM tblMortalidades
                WHERE idLote = @idLote
            ) + @cantidad
            THROW 52037, 'La cantidad de mortalidad supera la cantidad disponible del lote', 1;

            INSERT INTO tblMortalidades
            (
                idLote,
                fecha,
                cantidad,
                causa,
                observacion
            )
            VALUES
            (
                @idLote,
                @fecha,
                @cantidad,
                @causa,
                @observacion
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

--> ACTUALIZAR MORTALIDAD
CREATE OR ALTER PROC sp_ActualizarMortalidad
(
    @Id INT,
    @idLote INT,
    @fecha DATE,
    @cantidad INT,
    @causa NVARCHAR(150),
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @causa = TRIM(@causa);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblMortalidades
                WHERE id = @Id
            )
                THROW 52038, 'El registro de mortalidad no existe', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @idLote
                  AND estado <> 'Cancelado'
            )
                THROW 52034, 'El lote no existe o está cancelado', 1;

            IF @cantidad <= 0
                THROW 52035, 'La cantidad de mortalidad debe ser mayor que cero', 1;

            IF @causa = ''
                THROW 52036, 'La causa de mortalidad es obligatoria', 1;

            IF
            (
                SELECT cantidadInicial
                FROM tblLotes
                WHERE id = @idLote
            )
            <
            (
                SELECT ISNULL(SUM(cantidad), 0)
                FROM tblMortalidades
                WHERE idLote = @idLote
                  AND id <> @Id
            ) + @cantidad
            THROW 52037, 'La cantidad de mortalidad supera la cantidad disponible del lote', 1;

            UPDATE tblMortalidades
            SET
                idLote = @idLote,
                fecha = @fecha,
                cantidad = @cantidad,
                causa = @causa,
                observacion = @observacion
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