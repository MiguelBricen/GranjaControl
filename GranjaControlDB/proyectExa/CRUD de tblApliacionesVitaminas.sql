USE GranjaControlDB;
GO

--> MOSTRAR APLICACIONES DE VITAMINAS
CREATE OR ALTER PROC sp_MostrarAplicacionesVitaminas
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
        av.id,
        av.idLote,
        l.codigoLote,
        av.idVitamina,
        v.nombreVitamina,
        av.fecha,
        av.cantidad,
        av.unidadMedida,
        av.observacion
    FROM tblAplicacionesVitaminas av
    INNER JOIN tblLotes l
        ON av.idLote = l.id
    INNER JOIN tblVitaminas v
        ON av.idVitamina = v.id
    WHERE
        @idLote IS NULL
        OR av.idLote = @idLote
    ORDER BY av.fecha DESC, av.id DESC
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> CONTAR APLICACIONES DE VITAMINAS
CREATE OR ALTER PROC sp_ContarAplicacionesVitaminas
(
    @idLote INT = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT COUNT(*) AS Total
    FROM tblAplicacionesVitaminas av
    WHERE
        @idLote IS NULL
        OR av.idLote = @idLote;
END
GO

--> OBTENER APLICACIÓN DE VITAMINA POR ID
CREATE OR ALTER PROC sp_ObtenerAplicacionVitaminaPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        av.id,
        av.idLote,
        l.codigoLote,
        av.idVitamina,
        v.nombreVitamina,
        av.fecha,
        av.cantidad,
        av.unidadMedida,
        av.observacion
    FROM tblAplicacionesVitaminas av
    INNER JOIN tblLotes l
        ON av.idLote = l.id
    INNER JOIN tblVitaminas v
        ON av.idVitamina = v.id
    WHERE av.id = @Id;
END
GO

--> AGREGAR APLICACIÓN DE VITAMINA
CREATE OR ALTER PROC sp_AgregarAplicacionVitamina
(
    @idLote INT,
    @idVitamina INT,
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

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @idLote
                  AND estado <> 'Cancelado'
            )
                THROW 52029, 'El lote no existe o está cancelado', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblVitaminas
                WHERE id = @idVitamina
                  AND activo = 1
            )
                THROW 52030, 'La vitamina no existe o está inactiva', 1;

            IF @cantidad <= 0
                THROW 52031, 'La cantidad de vitamina debe ser mayor que cero', 1;

            IF @unidadMedida NOT IN ('Mililitros', 'Litros', 'Gramos')
                THROW 52032, 'La unidad de medida no es válida', 1;

            INSERT INTO tblAplicacionesVitaminas
            (
                idLote,
                idVitamina,
                fecha,
                cantidad,
                unidadMedida,
                observacion
            )
            VALUES
            (
                @idLote,
                @idVitamina,
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

--> ACTUALIZAR APLICACIÓN DE VITAMINA
CREATE OR ALTER PROC sp_ActualizarAplicacionVitamina
(
    @Id INT,
    @idLote INT,
    @idVitamina INT,
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

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAplicacionesVitaminas
                WHERE id = @Id
            )
                THROW 52033, 'El registro de aplicación de vitamina no existe', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @idLote
                  AND estado <> 'Cancelado'
            )
                THROW 52029, 'El lote no existe o está cancelado', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblVitaminas
                WHERE id = @idVitamina
                  AND activo = 1
            )
                THROW 52030, 'La vitamina no existe o está inactiva', 1;

            IF @cantidad <= 0
                THROW 52031, 'La cantidad de vitamina debe ser mayor que cero', 1;

            IF @unidadMedida NOT IN ('Mililitros', 'Litros', 'Gramos')
                THROW 52032, 'La unidad de medida no es válida', 1;

            UPDATE tblAplicacionesVitaminas
            SET
                idLote = @idLote,
                idVitamina = @idVitamina,
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