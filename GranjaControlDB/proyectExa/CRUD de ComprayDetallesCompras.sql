USE GranjaControlDB;
GO

--> MOSTRAR COMPRAS DE ALIMENTO
CREATE OR ALTER PROC sp_MostrarComprasAlimento
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
        c.id,
        c.numeroCompra,
        c.idProveedor,
        p.nombreProveedor,
        c.fechaCompra,
        c.observacion
    FROM tblComprasAlimento c
    INNER JOIN tblProveedores p
        ON c.idProveedor = p.id
    WHERE
        ISNULL(@buscar, '') = ''
        OR c.numeroCompra LIKE '%' + @buscar + '%'
        OR p.nombreProveedor LIKE '%' + @buscar + '%'
    ORDER BY c.fechaCompra DESC, c.id DESC
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> CONTAR COMPRAS DE ALIMENTO
CREATE OR ALTER PROC sp_ContarComprasAlimento
(
    @buscar NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblComprasAlimento c
    INNER JOIN tblProveedores p
        ON c.idProveedor = p.id
    WHERE
        ISNULL(@buscar, '') = ''
        OR c.numeroCompra LIKE '%' + @buscar + '%'
        OR p.nombreProveedor LIKE '%' + @buscar + '%';
END
GO

--> OBTENER COMPRA DE ALIMENTO POR ID
CREATE OR ALTER PROC sp_ObtenerCompraAlimentoPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        c.id,
        c.numeroCompra,
        c.idProveedor,
        p.nombreProveedor,
        c.fechaCompra,
        c.observacion
    FROM tblComprasAlimento c
    INNER JOIN tblProveedores p
        ON c.idProveedor = p.id
    WHERE c.id = @Id;
END
GO

--> AGREGAR COMPRA DE ALIMENTO
CREATE OR ALTER PROC sp_AgregarCompraAlimento
(
    @numeroCompra NVARCHAR(25),
    @idProveedor INT,
    @fechaCompra DATE,
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @numeroCompra = TRIM(@numeroCompra);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF EXISTS
            (
                SELECT 1
                FROM tblComprasAlimento
                WHERE numeroCompra = @numeroCompra
            )
                THROW 52039, 'El número de compra ya existe', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE id = @idProveedor
                  AND activo = 1
                  AND tipoProveedor IN ('Alimento', 'Ambos')
            )
                THROW 52040, 'El proveedor seleccionado no puede suministrar alimento o está inactivo', 1;

            INSERT INTO tblComprasAlimento
            (
                numeroCompra,
                idProveedor,
                fechaCompra,
                observacion
            )
            VALUES
            (
                @numeroCompra,
                @idProveedor,
                @fechaCompra,
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

--> ACTUALIZAR COMPRA DE ALIMENTO
CREATE OR ALTER PROC sp_ActualizarCompraAlimento
(
    @Id INT,
    @numeroCompra NVARCHAR(25),
    @idProveedor INT,
    @fechaCompra DATE,
    @observacion NVARCHAR(250),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @numeroCompra = TRIM(@numeroCompra);
    SET @observacion = TRIM(@observacion);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            DECLARE @esHistoricoPendiente BIT = 0;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblComprasAlimento
                WHERE id = @Id
            )
                THROW 52041, 'La compra de alimento no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblComprasAlimento
                WHERE numeroCompra = @numeroCompra
                  AND id <> @Id
            )
                THROW 52039, 'El número de compra ya existe', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE id = @idProveedor
                  AND activo = 1
                  AND tipoProveedor IN ('Alimento', 'Ambos')
            )
                THROW 52040, 'El proveedor seleccionado no puede suministrar alimento o está inactivo', 1;

            UPDATE tblComprasAlimento
            SET
                numeroCompra = @numeroCompra,
                idProveedor = @idProveedor,
                fechaCompra = @fechaCompra,
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

--> MOSTRAR DETALLE DE COMPRAS
CREATE OR ALTER PROC sp_MostrarDetalleComprasAlimento
(
    @idCompra INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.id,
        d.idCompra,
        c.numeroCompra,
        d.idAlimento,
        a.nombreAlimento,
        a.tipoAlimento,
        d.cantidad,
        d.unidadMedida,
        d.costoUnitario,
        d.pesoPorSaco,
        d.unidadPesoSaco,
        d.pesoTotalLibras,
        d.pesoTotalKilogramos,
        d.subtotal
    FROM tblDetalleComprasAlimento d
    INNER JOIN tblComprasAlimento c
        ON d.idCompra = c.id
    INNER JOIN tblAlimentos a
        ON d.idAlimento = a.id
    WHERE d.idCompra = @idCompra
    ORDER BY d.id;
END
GO

--> OBTENER DETALLE DE COMPRA POR ID
CREATE OR ALTER PROC sp_ObtenerDetalleCompraAlimentoPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        d.id,
        d.idCompra,
        c.numeroCompra,
        d.idAlimento,
        a.nombreAlimento,
        a.tipoAlimento,
        d.cantidad,
        d.unidadMedida,
        d.costoUnitario,
        d.pesoPorSaco,
        d.unidadPesoSaco,
        d.pesoTotalLibras,
        d.pesoTotalKilogramos,
        d.subtotal
    FROM tblDetalleComprasAlimento d
    INNER JOIN tblComprasAlimento c
        ON d.idCompra = c.id
    INNER JOIN tblAlimentos a
        ON d.idAlimento = a.id
    WHERE d.id = @Id;
END
GO

--> AGREGAR DETALLE DE COMPRA
CREATE OR ALTER PROC sp_AgregarDetalleCompraAlimento
(
    @idCompra INT,
    @idAlimento INT,
    @cantidad DECIMAL(10,2),
    @unidadMedida NVARCHAR(20),
    @costoUnitario DECIMAL(10,2),
    @pesoPorSaco DECIMAL(10,4),
    @unidadPesoSaco NVARCHAR(20),
    @pesoTotalLibras DECIMAL(18,2),
    @pesoTotalKilogramos DECIMAL(18,2),
    @subtotal DECIMAL(18,2),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @unidadMedida = TRIM(@unidadMedida);
    SET @unidadPesoSaco = TRIM(@unidadPesoSaco);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblComprasAlimento
                WHERE id = @idCompra
            )
                THROW 52041, 'La compra de alimento no existe', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE id = @idAlimento
                  AND activo = 1
            )
                THROW 52023, 'El alimento no existe o está inactivo', 1;

            IF @cantidad <= 0
                THROW 52042, 'La cantidad de alimento debe ser mayor que cero', 1;

            IF @costoUnitario < 0
                THROW 52043, 'El precio por saco no puede ser negativo', 1;

            IF @unidadMedida <> 'Sacos'
               OR @pesoPorSaco IS NULL
               OR @pesoPorSaco <= 0
               OR @unidadPesoSaco IS NULL
               OR @unidadPesoSaco NOT IN ('Libras', 'Kilogramos')
               OR @pesoTotalLibras IS NULL
               OR @pesoTotalLibras <= 0
               OR @pesoTotalKilogramos IS NULL
               OR @pesoTotalKilogramos <= 0
               OR @subtotal IS NULL
               OR @subtotal < 0
                THROW 52025, 'La compra nueva debe tener cantidad de sacos, peso por saco y valores derivados válidos', 1;

            INSERT INTO tblDetalleComprasAlimento
            (
                idCompra,
                idAlimento,
                cantidad,
                unidadMedida,
                costoUnitario,
                pesoPorSaco,
                unidadPesoSaco,
                pesoTotalLibras,
                pesoTotalKilogramos,
                subtotal
            )
            VALUES
            (
                @idCompra,
                @idAlimento,
                @cantidad,
                @unidadMedida,
                @costoUnitario,
                @pesoPorSaco,
                @unidadPesoSaco,
                @pesoTotalLibras,
                @pesoTotalKilogramos,
                @subtotal
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

--> ACTUALIZAR DETALLE DE COMPRA
CREATE OR ALTER PROC sp_ActualizarDetalleCompraAlimento
(
    @Id INT,
    @idCompra INT,
    @idAlimento INT,
    @cantidad DECIMAL(10,2),
    @unidadMedida NVARCHAR(20),
    @costoUnitario DECIMAL(10,2),
    @pesoPorSaco DECIMAL(10,4),
    @unidadPesoSaco NVARCHAR(20),
    @pesoTotalLibras DECIMAL(18,2),
    @pesoTotalKilogramos DECIMAL(18,2),
    @subtotal DECIMAL(18,2),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @unidadMedida = TRIM(@unidadMedida);
    SET @unidadPesoSaco = TRIM(@unidadPesoSaco);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            DECLARE @esHistoricoPendiente BIT = 0;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblDetalleComprasAlimento
                WHERE id = @Id
            )
                THROW 52044, 'El detalle de compra no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblDetalleComprasAlimento
                WHERE id = @Id
                  AND unidadMedida IN ('Libras', 'Kilogramos')
                  AND pesoPorSaco IS NULL
                  AND unidadPesoSaco IS NULL
                  AND pesoTotalLibras IS NULL
                  AND pesoTotalKilogramos IS NULL
                  AND subtotal IS NULL
            )
                SET @esHistoricoPendiente = 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblComprasAlimento
                WHERE id = @idCompra
            )
                THROW 52041, 'La compra de alimento no existe', 1;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblAlimentos
                WHERE id = @idAlimento
                  AND activo = 1
            )
                THROW 52023, 'El alimento no existe o está inactivo', 1;

            IF @cantidad <= 0
                THROW 52042, 'La cantidad de alimento debe ser mayor que cero', 1;

            IF @costoUnitario < 0
                THROW 52043, 'El precio por saco no puede ser negativo', 1;

            IF @esHistoricoPendiente = 1
            BEGIN
                IF @unidadMedida NOT IN ('Libras', 'Kilogramos')
                   OR @pesoPorSaco IS NOT NULL
                   OR @unidadPesoSaco IS NOT NULL
                   OR @pesoTotalLibras IS NOT NULL
                   OR @pesoTotalKilogramos IS NOT NULL
                   OR @subtotal IS NOT NULL
                    THROW 52045, 'El detalle histórico debe conservarse como unidad de peso y sin datos de saco validados', 1;
            END
            ELSE IF @unidadMedida <> 'Sacos'
                 OR @pesoPorSaco IS NULL
                 OR @pesoPorSaco <= 0
                 OR @unidadPesoSaco IS NULL
                 OR @unidadPesoSaco NOT IN ('Libras', 'Kilogramos')
                 OR @pesoTotalLibras IS NULL
                 OR @pesoTotalLibras <= 0
                 OR @pesoTotalKilogramos IS NULL
                 OR @pesoTotalKilogramos <= 0
                 OR @subtotal IS NULL
                 OR @subtotal < 0
                    THROW 52025, 'La compra nueva debe tener cantidad de sacos, peso por saco y valores derivados válidos', 1;

            UPDATE tblDetalleComprasAlimento
            SET
                idCompra = @idCompra,
                idAlimento = @idAlimento,
                cantidad = @cantidad,
                unidadMedida = @unidadMedida,
                costoUnitario = @costoUnitario,
                pesoPorSaco = @pesoPorSaco,
                unidadPesoSaco = @unidadPesoSaco,
                pesoTotalLibras = @pesoTotalLibras,
                pesoTotalKilogramos = @pesoTotalKilogramos,
                subtotal = @subtotal
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
