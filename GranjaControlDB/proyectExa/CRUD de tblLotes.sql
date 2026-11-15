USE GranjaControlDB;
GO

--> MOSTRAR LOTES EN ESTADOS ACTIVOS DE PRODUCCIÓN
CREATE OR ALTER PROC sp_MostrarLotesActivos
    @idVenta INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Mortalidad AS
    (
        SELECT idLote, SUM(cantidad) AS cantidad
        FROM tblMortalidades
        GROUP BY idLote
    ),
    Vendidos AS
    (
        SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v ON v.id = d.idVenta
        WHERE v.estado = 'Registrada'
          AND (@idVenta IS NULL OR v.id <> @idVenta)
        GROUP BY d.idLote
    ),
    Disponibilidad AS
    (
        SELECT
            l.id,
            l.cantidadInicial
                - ISNULL(m.cantidad, 0)
                - ISNULL(v.cantidad, 0) AS cantidadDisponible
        FROM tblLotes l
        LEFT JOIN Mortalidad m ON m.idLote = l.id
        LEFT JOIN Vendidos v ON v.idLote = l.id
    )
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
        l.observacion,
        d.cantidadDisponible
    FROM tblLotes l
    INNER JOIN Disponibilidad d ON d.id = l.id
    INNER JOIN tblGalpones g
        ON l.idGalpon = g.id
    INNER JOIN tblProveedores p
        ON l.idProveedor = p.id
    WHERE l.estado = 'Listo para venta'
      AND d.cantidadDisponible > 0
      AND g.activo = 1
      AND p.activo = 1
    ORDER BY l.fechaIngreso DESC, l.codigoLote;
END
GO

CREATE OR ALTER PROC sp_ObtenerLoteParaVenta
    @idLote INT,
    @idVenta INT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Mortalidad AS
    (
        SELECT idLote, SUM(cantidad) AS cantidad
        FROM tblMortalidades
        GROUP BY idLote
    ),
    Vendidos AS
    (
        SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v ON v.id = d.idVenta
        WHERE v.estado = 'Registrada'
          AND (@idVenta IS NULL OR v.id <> @idVenta)
        GROUP BY d.idLote
    )
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
        l.observacion,
        l.cantidadInicial
            - ISNULL(m.cantidad, 0)
            - ISNULL(v.cantidad, 0) AS cantidadDisponible
    FROM tblLotes l
    INNER JOIN tblGalpones g ON g.id = l.idGalpon
    INNER JOIN tblProveedores p ON p.id = l.idProveedor
    LEFT JOIN Mortalidad m ON m.idLote = l.id
    LEFT JOIN Vendidos v ON v.idLote = l.id
    WHERE l.id = @idLote
      AND l.estado = 'Listo para venta'
      AND l.cantidadInicial
            - ISNULL(m.cantidad, 0)
            - ISNULL(v.cantidad, 0) > 0
      AND g.activo = 1
      AND p.activo = 1;
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

    ;WITH Mortalidad AS
    (
        SELECT idLote, SUM(cantidad) AS cantidad
        FROM tblMortalidades
        GROUP BY idLote
    ),
    Vendidos AS
    (
        SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v ON v.id = d.idVenta
        WHERE v.estado = 'Registrada'
        GROUP BY d.idLote
    )
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
        l.observacion,
        l.cantidadInicial
            - ISNULL(m.cantidad, 0)
            - ISNULL(v.cantidad, 0) AS cantidadDisponible
    FROM tblLotes l
    INNER JOIN tblGalpones g
        ON l.idGalpon = g.id
    INNER JOIN tblProveedores p
        ON l.idProveedor = p.id
    LEFT JOIN Mortalidad m ON m.idLote = l.id
    LEFT JOIN Vendidos v ON v.idLote = l.id
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

--> OBTENER CAPACIDAD DISPONIBLE DE UN GALPÓN
CREATE OR ALTER PROC sp_ObtenerCapacidadDisponibleGalpon
    @idGalpon INT
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH Mortalidad AS
    (
        SELECT idLote, SUM(cantidad) AS cantidad
        FROM tblMortalidades
        GROUP BY idLote
    ),
    Vendidos AS
    (
        SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
        FROM tblDetalleVentas d
        INNER JOIN tblVentas v ON v.id = d.idVenta
        WHERE v.estado = 'Registrada'
        GROUP BY d.idLote
    ),
    Ocupacion AS
    (
        SELECT
            l.idGalpon,
            SUM
            (
                CASE
                    WHEN l.estado IN ('En producción', 'Listo para venta')
                    THEN CASE
                        WHEN l.cantidadInicial
                            - ISNULL(m.cantidad, 0)
                            - ISNULL(v.cantidad, 0) > 0
                        THEN l.cantidadInicial
                            - ISNULL(m.cantidad, 0)
                            - ISNULL(v.cantidad, 0)
                        ELSE 0
                    END
                    ELSE 0
                END
            ) AS cantidadOcupada
        FROM tblLotes l
        LEFT JOIN Mortalidad m ON m.idLote = l.id
        LEFT JOIN Vendidos v ON v.idLote = l.id
        WHERE l.idGalpon = @idGalpon
        GROUP BY l.idGalpon
    )
    SELECT
        CASE
            WHEN g.capacidad - ISNULL(o.cantidadOcupada, 0) > 0
            THEN g.capacidad - ISNULL(o.cantidadOcupada, 0)
            ELSE 0
        END AS capacidadDisponible,
        g.capacidad,
        ISNULL(o.cantidadOcupada, 0) AS cantidadOcupada
    FROM tblGalpones g
    LEFT JOIN Ocupacion o ON o.idGalpon = g.id
    WHERE g.id = @idGalpon
      AND g.activo = 1;
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

    DECLARE
        @capacidadGalpon INT,
        @ocupacionActual INT,
        @capacidadDisponible INT,
        @mensajeCapacidad NVARCHAR(2048);

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
            SELECT @capacidadGalpon = capacidad
            FROM tblGalpones WITH (UPDLOCK, HOLDLOCK)
            WHERE id = @idGalpon
              AND activo = 1;

            IF @capacidadGalpon IS NULL
                THROW 52013, 'El galpón seleccionado no existe o está inactivo', 1;

            /* El bloqueo de la fila del galpón serializa las altas concurrentes. */
            ;WITH Mortalidad AS
            (
                SELECT idLote, SUM(cantidad) AS cantidad
                FROM tblMortalidades
                GROUP BY idLote
            ),
            Vendidos AS
            (
                SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
                FROM tblDetalleVentas d
                INNER JOIN tblVentas v ON v.id = d.idVenta
                WHERE v.estado = 'Registrada'
                GROUP BY d.idLote
            )
            SELECT @ocupacionActual = ISNULL(SUM
            (
                CASE
                    WHEN l.estado IN ('En producción', 'Listo para venta')
                    THEN CASE
                        WHEN l.cantidadInicial
                            - ISNULL(m.cantidad, 0)
                            - ISNULL(v.cantidad, 0) > 0
                        THEN l.cantidadInicial
                            - ISNULL(m.cantidad, 0)
                            - ISNULL(v.cantidad, 0)
                        ELSE 0
                    END
                    ELSE 0
                END
            ), 0)
            FROM tblLotes l
            LEFT JOIN Mortalidad m ON m.idLote = l.id
            LEFT JOIN Vendidos v ON v.idLote = l.id
            WHERE l.idGalpon = @idGalpon;

            SET @capacidadDisponible = @capacidadGalpon - @ocupacionActual;

            IF @cantidadInicial > @capacidadDisponible
            BEGIN
                SET @mensajeCapacidad = CONCAT(
                    'El lote contiene ', @cantidadInicial,
                    ' pollitos, pero el galpón solamente tiene capacidad disponible para ',
                    CASE WHEN @capacidadDisponible > 0 THEN @capacidadDisponible ELSE 0 END,
                    '.');
                THROW 52069, @mensajeCapacidad, 1;
            END;

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

    DECLARE
        @estadoActual NVARCHAR(25),
        @capacidadGalpon INT,
        @ocupacionOtrosLotes INT,
        @mortalidadesLote INT,
        @pollitosVendidos INT,
        @cantidadActualizada INT,
        @capacidadDisponible INT,
        @mensajeCapacidad NVARCHAR(2048);

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

            SELECT @estadoActual = estado
            FROM tblLotes WITH (UPDLOCK, HOLDLOCK)
            WHERE id = @Id;

            SELECT @capacidadGalpon = capacidad
            FROM tblGalpones WITH (UPDLOCK, HOLDLOCK)
            WHERE id = @idGalpon
              AND activo = 1;

            IF @capacidadGalpon IS NULL
                THROW 52013, 'El galpón seleccionado no existe o está inactivo', 1;

            IF @estadoActual IN ('En producción', 'Listo para venta')
            BEGIN
                SELECT @mortalidadesLote = ISNULL(SUM(cantidad), 0)
                FROM tblMortalidades
                WHERE idLote = @Id;

                SELECT @pollitosVendidos = ISNULL(SUM(d.cantidadPollos), 0)
                FROM tblDetalleVentas d
                INNER JOIN tblVentas v ON v.id = d.idVenta
                WHERE d.idLote = @Id
                  AND v.estado = 'Registrada';

                IF @cantidadInicial < @mortalidadesLote + @pollitosVendidos
                    THROW 52070, 'La cantidad inicial no puede ser menor que los pollitos ya vendidos o fallecidos', 1;

                SET @cantidadActualizada =
                    @cantidadInicial - @mortalidadesLote - @pollitosVendidos;

                ;WITH Mortalidad AS
                (
                    SELECT idLote, SUM(cantidad) AS cantidad
                    FROM tblMortalidades
                    GROUP BY idLote
                ),
                Vendidos AS
                (
                    SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
                    FROM tblDetalleVentas d
                    INNER JOIN tblVentas v ON v.id = d.idVenta
                    WHERE v.estado = 'Registrada'
                    GROUP BY d.idLote
                )
                SELECT @ocupacionOtrosLotes = ISNULL(SUM
                (
                    CASE
                        WHEN l.estado IN ('En producción', 'Listo para venta')
                        THEN CASE
                            WHEN l.cantidadInicial
                                - ISNULL(m.cantidad, 0)
                                - ISNULL(v.cantidad, 0) > 0
                            THEN l.cantidadInicial
                                - ISNULL(m.cantidad, 0)
                                - ISNULL(v.cantidad, 0)
                            ELSE 0
                        END
                        ELSE 0
                    END
                ), 0)
                FROM tblLotes l
                LEFT JOIN Mortalidad m ON m.idLote = l.id
                LEFT JOIN Vendidos v ON v.idLote = l.id
                WHERE l.idGalpon = @idGalpon
                  AND l.id <> @Id;

                SET @capacidadDisponible = @capacidadGalpon - @ocupacionOtrosLotes;

                IF @cantidadActualizada > @capacidadDisponible
                BEGIN
                    SET @mensajeCapacidad = CONCAT(
                        'El lote contiene ', @cantidadActualizada,
                        ' pollitos disponibles, pero el galpón solamente tiene capacidad disponible para ',
                        CASE WHEN @capacidadDisponible > 0 THEN @capacidadDisponible ELSE 0 END,
                        '.');
                    THROW 52069, @mensajeCapacidad, 1;
                END;
            END;


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

    DECLARE
        @idGalpon INT,
        @estadoActual NVARCHAR(25),
        @cantidadInicial INT,
        @capacidadGalpon INT,
        @ocupacionOtrosLotes INT,
        @mortalidadesLote INT,
        @pollitosVendidos INT,
        @capacidadDisponible INT,
        @mensajeCapacidad NVARCHAR(2048);

    SET @estado = TRIM(@estado);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            -- VALIDAR EXISTENCIA DEL LOTE 
            SELECT
                @idGalpon = idGalpon,
                @estadoActual = estado,
                @cantidadInicial = cantidadInicial
            FROM tblLotes WITH (UPDLOCK, HOLDLOCK)
            WHERE id = @Id;

            IF @idGalpon IS NULL
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

            IF @estado IN ('En producción', 'Listo para venta')
               AND @estadoActual NOT IN ('En producción', 'Listo para venta')
            BEGIN
                SELECT @capacidadGalpon = capacidad
                FROM tblGalpones WITH (UPDLOCK, HOLDLOCK)
                WHERE id = @idGalpon
                  AND activo = 1;

                IF @capacidadGalpon IS NULL
                    THROW 52013, 'El galpón seleccionado no existe o está inactivo', 1;

                SELECT @mortalidadesLote = ISNULL(SUM(cantidad), 0)
                FROM tblMortalidades
                WHERE idLote = @Id;

                SELECT @pollitosVendidos = ISNULL(SUM(d.cantidadPollos), 0)
                FROM tblDetalleVentas d
                INNER JOIN tblVentas v ON v.id = d.idVenta
                WHERE d.idLote = @Id
                  AND v.estado = 'Registrada';

                SET @cantidadInicial =
                    @cantidadInicial - @mortalidadesLote - @pollitosVendidos;

                ;WITH Mortalidad AS
                (
                    SELECT idLote, SUM(cantidad) AS cantidad
                    FROM tblMortalidades
                    GROUP BY idLote
                ),
                Vendidos AS
                (
                    SELECT d.idLote, SUM(d.cantidadPollos) AS cantidad
                    FROM tblDetalleVentas d
                    INNER JOIN tblVentas v ON v.id = d.idVenta
                    WHERE v.estado = 'Registrada'
                    GROUP BY d.idLote
                )
                SELECT @ocupacionOtrosLotes = ISNULL(SUM
                (
                    CASE
                        WHEN l.estado IN ('En producción', 'Listo para venta')
                        THEN CASE
                            WHEN l.cantidadInicial
                                - ISNULL(m.cantidad, 0)
                                - ISNULL(v.cantidad, 0) > 0
                            THEN l.cantidadInicial
                                - ISNULL(m.cantidad, 0)
                                - ISNULL(v.cantidad, 0)
                            ELSE 0
                        END
                        ELSE 0
                    END
                ), 0)
                FROM tblLotes l
                LEFT JOIN Mortalidad m ON m.idLote = l.id
                LEFT JOIN Vendidos v ON v.idLote = l.id
                WHERE l.idGalpon = @idGalpon
                  AND l.id <> @Id;

                SET @capacidadDisponible = @capacidadGalpon - @ocupacionOtrosLotes;

                IF @cantidadInicial > @capacidadDisponible
                BEGIN
                    SET @mensajeCapacidad = CONCAT(
                        'El lote contiene ', @cantidadInicial,
                        ' pollitos disponibles, pero el galpón solamente tiene capacidad disponible para ',
                        CASE WHEN @capacidadDisponible > 0 THEN @capacidadDisponible ELSE 0 END,
                        '.');
                    THROW 52069, @mensajeCapacidad, 1;
                END;
            END;


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
