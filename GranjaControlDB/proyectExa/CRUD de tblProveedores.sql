USE GranjaControlDB;
GO

--> MOSTRAR PROVEEDORES ACTIVOS
CREATE OR ALTER PROC sp_MostrarProveedoresActivos
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreProveedor,
        telefono,
        correoElectronico,
        direccion,
        tipoProveedor
    FROM tblProveedores
    WHERE activo = 1
    ORDER BY nombreProveedor;
END
GO


--> MOSTRAR PROVEEDORES
CREATE OR ALTER PROC sp_MostrarProveedores
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
        nombreProveedor,
        telefono,
        correoElectronico,
        direccion,
        tipoProveedor,
        activo
    FROM tblProveedores
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreProveedor LIKE '%' + @buscar + '%'
        OR telefono LIKE '%' + @buscar + '%'
        OR correoElectronico LIKE '%' + @buscar + '%'
        OR direccion LIKE '%' + @buscar + '%'
        OR tipoProveedor LIKE '%' + @buscar + '%'
    ORDER BY nombreProveedor
    OFFSET (@pagina - 1) * @tamPagina ROWS
    FETCH NEXT @tamPagina ROWS ONLY;
END
GO

--> CONTAR PROVEEDORES
CREATE OR ALTER PROC sp_ContarProveedores
(
    @buscar NVARCHAR(100) = NULL
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @buscar = TRIM(@buscar);

    SELECT COUNT(*) AS Total
    FROM tblProveedores
    WHERE
        ISNULL(@buscar, '') = ''
        OR nombreProveedor LIKE '%' + @buscar + '%'
        OR telefono LIKE '%' + @buscar + '%'
        OR correoElectronico LIKE '%' + @buscar + '%'
        OR direccion LIKE '%' + @buscar + '%'
        OR tipoProveedor LIKE '%' + @buscar + '%';
END
GO

--> OBTENER PROVEEDOR POR ID
CREATE OR ALTER PROC sp_ObtenerProveedorPorId
(
    @Id INT
)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        id,
        nombreProveedor,
        telefono,
        correoElectronico,
        direccion,
        tipoProveedor,
        activo
    FROM tblProveedores
    WHERE id = @Id;
END
GO


--> AGREGAR PROVEEDOR
CREATE OR ALTER PROC sp_AgregarProveedor
(
    @nombreProveedor NVARCHAR(100),
    @telefono NVARCHAR(25),
    @correoElectronico NVARCHAR(100),
    @direccion NVARCHAR(200),
    @tipoProveedor NVARCHAR(30),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreProveedor = TRIM(@nombreProveedor);
    SET @telefono = TRIM(@telefono);
    SET @correoElectronico = TRIM(@correoElectronico);
    SET @direccion = TRIM(@direccion);
    SET @tipoProveedor = TRIM(@tipoProveedor);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE nombreProveedor = @nombreProveedor
            )
                THROW 52009, 'El proveedor ya existe', 1;

            IF @tipoProveedor NOT IN ('Pollitos', 'Alimento', 'Ambos')
                THROW 52010, 'El tipo de proveedor no es válido', 1;

            INSERT INTO tblProveedores
            (
                nombreProveedor,
                telefono,
                correoElectronico,
                direccion,
                tipoProveedor
            )
            VALUES
            (
                @nombreProveedor,
                @telefono,
                @correoElectronico,
                @direccion,
                @tipoProveedor
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


--> ACTUALIZAR PROVEEDOR
CREATE OR ALTER PROC sp_ActualizarProveedor
(
    @Id INT,
    @nombreProveedor NVARCHAR(100),
    @telefono NVARCHAR(25),
    @correoElectronico NVARCHAR(100),
    @direccion NVARCHAR(200),
    @tipoProveedor NVARCHAR(30),
    @usuarioApp NVARCHAR(25)
)
AS
BEGIN
    SET NOCOUNT ON;

    SET @nombreProveedor = TRIM(@nombreProveedor);
    SET @telefono = TRIM(@telefono);
    SET @correoElectronico = TRIM(@correoElectronico);
    SET @direccion = TRIM(@direccion);
    SET @tipoProveedor = TRIM(@tipoProveedor);
    SET @usuarioApp = TRIM(@usuarioApp);

    BEGIN TRY
        BEGIN TRANSACTION;

            IF NOT EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE id = @Id
            )
                THROW 52011, 'El proveedor no existe', 1;

            IF EXISTS
            (
                SELECT 1
                FROM tblProveedores
                WHERE nombreProveedor = @nombreProveedor
                  AND id <> @Id
            )
                THROW 52009, 'El proveedor ya existe', 1;

            IF @tipoProveedor NOT IN ('Pollitos', 'Alimento', 'Ambos')
                THROW 52010, 'El tipo de proveedor no es válido', 1;

            UPDATE tblProveedores
            SET
                nombreProveedor = @nombreProveedor,
                telefono = @telefono,
                correoElectronico = @correoElectronico,
                direccion = @direccion,
                tipoProveedor = @tipoProveedor
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


--> CAMBIAR ESTADO DEL PROVEEDOR
CREATE OR ALTER PROC sp_CambiarEstadoProveedor
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
                FROM tblProveedores
                WHERE id = @Id
            )
                THROW 52011, 'El proveedor no existe', 1;

            UPDATE tblProveedores
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