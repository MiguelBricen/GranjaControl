USE GranjaControlDB;
GO

--> MOSTRAR LOTES EN ESTADOS ACTIVOS DE PRODUCCIÓN
CREATE OR ALTER PROC sp_MostrarLotesActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        l.id,
        l.codigoLote,
        l.idGalpon,
        g.nombreGalpon,
        l.idProveedor,
        p.nombreProveedor,
        l.fechaIngreso,
        l.cantidadInicial,
        l.costoPorPollito,
        l.estado,
        l.observacion
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON l.idGalpon = g.id
    INNER JOIN tblProveedores p
        ON l.idProveedor = p.id
    WHERE l.estado IN ('En producción', 'Listo para venta')
      AND g.activo = 1
      AND p.activo = 1
    ORDER BY l.fechaIngreso DESC, l.codigoLote;
END
GO

--> MOSTRAR LOTES
CREATE OR ALTER PROC sp_MostrarLotes
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
        l.id,
        l.codigoLote,
        l.idGalpon,
        g.nombreGalpon,
        l.idProveedor,
        p.nombreProveedor,
        l.fechaIngreso,
        l.cantidadInicial,
        l.costoPorPollito,
        l.estado,
        l.observacion
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON l.idGalpon = g.id
    INNER JOIN tblProveedores p
        ON l.idProveedor = p.id
    WHERE
        ISNULL(@buscar, '') = ''
        OR l.codigoLote LIKE '%' + @buscar + '%'
        OR g.nombreGalpon LIKE '%' + @buscar + '%'
        OR p.nombreProveedor LIKE '%' + @buscar + '%'
        OR l.estado LIKE '%' + @buscar + '%'
    ORDER BY l.fechaIngreso DESC, l.codigoLote
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO


--> CONTAR LOTES
CREATE OR ALTER PROC sp_ContarLotes
(
    @buscar NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON l.idGalpon = g.id
    INNER JOIN tblProveedores p
        ON l.idProveedor = p.id
    WHERE
        ISNULL(@buscar, '') = ''
        OR l.codigoLote LIKE '%' + @buscar + '%'
        OR g.nombreGalpon LIKE '%' + @buscar + '%'
        OR p.nombreProveedor LIKE '%' + @buscar + '%'
        OR l.estado LIKE '%' + @buscar + '%';
END
GO

--> OBTENER LOTE POR ID
CREATE OR ALTER PROC sp_ObtenerLotePorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        l.id,
        l.codigoLote,
        l.idGalpon,
        g.nombreGalpon,
        l.idProveedor,
        p.nombreProveedor,
        l.fechaIngreso,
        l.cantidadInicial,
        l.costoPorPollito,
        l.estado,
        l.observacion
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON l.idGalpon = g.id
    INNER JOIN tblProveedores p
        ON l.idProveedor = p.id
    WHERE l.id = @Id;
END
GO


--> AGREGAR LOTE
CREATE OR ALTER PROC sp_AgregarLote
(
    @codigoLote NVARCHAR(25),
    @idGalpon INT,
    @idProveedor INT,
    @fechaIngreso DATE,
    @cantidadInicial INT,
    @costoPorPollito DECIMAL(10,2),
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @codigoLote = TRIM(@codigoLote);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            -- VALIDAR CÓDIGO DE LOTE
            IF EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE codigoLote = @codigoLote
            )
                THROW 52012, 'El código del lote ya existe', 1;


            -- VALIDAR GALPÓN
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblGalpones
                WHERE id = @idGalpon
                  AND activo = 1
            )
                THROW 52013, 'El galpón seleccionado no existe o está inactivo', 1;


            -- VALIDAR PROVEEDOR
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE id = @idProveedor
                  AND activo = 1
                  AND tipoProveedor IN ('Pollitos', 'Ambos')
            )
                THROW 52014, 'El proveedor seleccionado no puede suministrar pollitos o está inactivo', 1;


            -- VALIDAR CANTIDAD
            IF @cantidadInicial <= 0
                THROW 52015, 'La cantidad inicial de pollitos debe ser mayor que cero', 1;


            -- VALIDAR COSTO 
            IF @costoPorPollito < 0
                THROW 52016, 'El costo por pollito no puede ser negativo', 1;


            -- REGISTRAR LOTE
            INSERT INTO tblLotes
            (
                codigoLote,
                idGalpon,
                idProveedor,
                fechaIngreso,
                cantidadInicial,
                costoPorPollito,
                estado,
                observacion
            )
            VALUES
            (
                @codigoLote,
                @idGalpon,
                @idProveedor,
                @fechaIngreso,
                @cantidadInicial,
                @costoPorPollito,
                'En producción',
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


--> ACTUALIZAR LOTE
CREATE OR ALTER PROC sp_ActualizarLote
(
    @Id INT,
    @codigoLote NVARCHAR(25),
    @idGalpon INT,
    @idProveedor INT,
    @fechaIngreso DATE,
    @cantidadInicial INT,
    @costoPorPollito DECIMAL(10,2),
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @codigoLote = TRIM(@codigoLote);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            -- VALIDAR EXISTENCIA DEL LOTE 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @Id
            )
                THROW 52017, 'El lote no existe', 1;


            -- VALIDAR CÓDIGO 
            IF EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE codigoLote = @codigoLote
                  AND id <> @Id
            )
                THROW 52012, 'El código del lote ya existe', 1;


            -- VALIDAR GALPÓN 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblGalpones
                WHERE id = @idGalpon
                  AND activo = 1
            )
                THROW 52013, 'El galpón seleccionado no existe o está inactivo', 1;


            -- VALIDAR PROVEEDOR 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE id = @idProveedor
                  AND activo = 1
                  AND tipoProveedor IN ('Pollitos', 'Ambos')
            )
                THROW 52014, 'El proveedor seleccionado no puede suministrar pollitos o está inactivo', 1;


            -- VALIDAR CANTIDAD 
            IF @cantidadInicial <= 0
                THROW 52015, 'La cantidad inicial de pollitos debe ser mayor que cero', 1;


            -- VALIDAR COSTO 
            IF @costoPorPollito < 0
                THROW 52016, 'El costo por pollito no puede ser negativo', 1;


            -- ACTUALIZAR LOTE 
            UPDATE tblLotes
            SET
                codigoLote = @codigoLote,
                idGalpon = @idGalpon,
                idProveedor = @idProveedor,
                fechaIngreso = @fechaIngreso,
                cantidadInicial = @cantidadInicial,
                costoPorPollito = @costoPorPollito,
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


--> CAMBIAR ESTADO DEL LOTE 
CREATE OR ALTER PROC sp_CambiarEstadoLote
(
    @Id INT,
    @estado NVARCHAR(25),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @estado = TRIM(@estado);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            -- VALIDAR EXISTENCIA DEL LOTE 
            IF NOT EXISTS
            (
                SELECT 1
                FROM tblLotes
                WHERE id = @Id
            )
                THROW 52017, 'El lote no existe', 1;


            -- VALIDAR ESTADO 
            IF @estado NOT IN
            (
                'En producción',
                'Listo para venta',
                'Finalizado',
                'Cancelado'
            )
                THROW 52018, 'El estado del lote no es válido', 1;


            -- ACTUALIZAR ESTADO 
            UPDATE tblLotes
            SET estado = @estado
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