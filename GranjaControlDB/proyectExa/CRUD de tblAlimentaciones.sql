USE GranjaControlDB;
GO

--> MOSTRAR ALIMENTACIONES
CREATE OR ALTER PROC sp_MostrarAlimentaciones
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
        a.id,
        a.idLote,
        l.codigoLote,
        a.idAlimento,
        al.nombreAlimento,
        al.tipoAlimento,
        a.fecha,
        a.cantidad,
        a.unidadMedida,
        a.observacion
    FROM tblAlimentaciones a
    INNER JOIN tblLotes l
        ON a.idLote = l.id
    INNER JOIN tblAlimentos al
        ON a.idAlimento = al.id
    WHERE
        @idLote IS NULL
        OR a.idLote = @idLote
    ORDER BY a.fecha DESC, a.id DESC
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO


--> CONTAR ALIMENTACIONES
CREATE OR ALTER PROC sp_ContarAlimentaciones
(
    @idLote INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Total
    FROM tblAlimentaciones a
    WHERE
        @idLote IS NULL
        OR a.idLote = @idLote;
END
GO

--> OBTENER ALIMENTACIÓN POR ID 
CREATE OR ALTER PROC sp_ObtenerAlimentacionPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        a.id,
        a.idLote,
        l.codigoLote,
        a.idAlimento,
        al.nombreAlimento,
        al.tipoAlimento,
        a.fecha,
        a.cantidad,
        a.unidadMedida,
        a.observacion
    FROM tblAlimentaciones a
    INNER JOIN tblLotes l
        ON a.idLote = l.id
    INNER JOIN tblAlimentos al
        ON a.idAlimento = al.id
    WHERE a.id = @Id;
END
GO


--> AGREGAR ALIMENTACIÓN 
CREATE OR ALTER PROC sp_AgregarAlimentacion
(
    @idLote INT,
    @idAlimento INT,
    @fecha DATE,
    @cantidad DECIMAL(10,2),
    @unidadMedida NVARCHAR(20),
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @unidadMedida = TRIM(@unidadMedida);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            -- VALIDAR LOTE 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @idLote
                  AND estado <> 'Cancelado'
            )
                THROW 52022, 'El lote no existe o está cancelado', 1;


            -- VALIDAR ALIMENTO 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE id = @idAlimento
                  AND activo = 1
            )
                THROW 52023, 'El alimento no existe o está inactivo', 1;


            -- VALIDAR CANTIDAD 
            IF @cantidad <= 0
                THROW 52024, 'La cantidad de alimento debe ser mayor que cero', 1;


            -- VALIDAR UNIDAD 
            IF @unidadMedida NOT IN ('Libras', 'Kilogramos')
                THROW 52025, 'La unidad de medida no es válida', 1;


            -- REGISTRAR ALIMENTACIÓN 
            INSERT INTO tblAlimentaciones
            (
                idLote,
                idAlimento,
                fecha,
                cantidad,
                unidadMedida,
                observacion
            )
            VALUES
            (
                @idLote,
                @idAlimento,
                @fecha,
                @cantidad,
                @unidadMedida,
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


--> ACTUALIZAR ALIMENTACIÓN 
CREATE OR ALTER PROC sp_ActualizarAlimentacion
(
    @Id INT,
    @idLote INT,
    @idAlimento INT,
    @fecha DATE,
    @cantidad DECIMAL(10,2),
    @unidadMedida NVARCHAR(20),
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @unidadMedida = TRIM(@unidadMedida);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            -- VALIDAR ALIMENTACIÓN 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAlimentaciones
                WHERE id = @Id
            )
                THROW 52026, 'El registro de alimentación no existe', 1;


            -- VALIDAR LOTE 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @idLote
                  AND estado <> 'Cancelado'
            )
                THROW 52022, 'El lote no existe o está cancelado', 1;


            -- VALIDAR ALIMENTO 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE id = @idAlimento
                  AND activo = 1
            )
                THROW 52023, 'El alimento no existe o está inactivo', 1;


            -- VALIDAR CANTIDAD 
            IF @cantidad <= 0
                THROW 52024, 'La cantidad de alimento debe ser mayor que cero', 1;


            -- VALIDAR UNIDAD 
            IF @unidadMedida NOT IN ('Libras', 'Kilogramos')
                THROW 52025, 'La unidad de medida no es válida', 1;


            -- ACTUALIZAR ALIMENTACIÓN 
            UPDATE tblAlimentaciones
            SET
                idLote = @idLote,
                idAlimento = @idAlimento,
                fecha = @fecha,
                cantidad = @cantidad,
                unidadMedida = @unidadMedida,
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