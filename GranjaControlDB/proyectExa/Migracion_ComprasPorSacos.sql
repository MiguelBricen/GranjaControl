/*
    Migracion aditiva para registrar nuevas compras por sacos.

    - No actualiza ni elimina compras existentes.
    - Las filas existentes conservan sus valores y campos nuevos NULL.
    - Los detalles nuevos se identifican mediante unidadMedida = 'Sacos'.
    - Las conversiones y subtotales se calculan en la entidad C# y se guardan;
      los reportes solo agregan esos valores persistidos.
    - Ejecutar manualmente en GranjaControlDB luego de respaldar la base.
*/
USE GranjaControlDB;
GO

SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF OBJECT_ID(N'dbo.tblDetalleComprasAlimento', N'U') IS NULL
        THROW 52100, 'No existe dbo.tblDetalleComprasAlimento.', 1;

    IF COL_LENGTH(N'dbo.tblDetalleComprasAlimento', N'pesoPorSaco') IS NULL
    BEGIN
        ALTER TABLE dbo.tblDetalleComprasAlimento ADD
            pesoPorSaco DECIMAL(10,4) NULL,
            unidadPesoSaco NVARCHAR(20) NULL,
            pesoTotalLibras DECIMAL(18,2) NULL,
            pesoTotalKilogramos DECIMAL(18,2) NULL,
            subtotal DECIMAL(18,2) NULL;
    END
    ELSE IF COL_LENGTH(N'dbo.tblDetalleComprasAlimento', N'unidadPesoSaco') IS NULL
         OR COL_LENGTH(N'dbo.tblDetalleComprasAlimento', N'pesoTotalLibras') IS NULL
         OR COL_LENGTH(N'dbo.tblDetalleComprasAlimento', N'pesoTotalKilogramos') IS NULL
         OR COL_LENGTH(N'dbo.tblDetalleComprasAlimento', N'subtotal') IS NULL
    BEGIN
        THROW 52101, 'Migracion parcial detectada. Revise el esquema antes de continuar.', 1;
    END;

    IF EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.tblDetalleComprasAlimento')
          AND name = N'CK_tblDetalleComprasAlimento_unidadMedida'
    )
        ALTER TABLE dbo.tblDetalleComprasAlimento
            DROP CONSTRAINT CK_tblDetalleComprasAlimento_unidadMedida;

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.tblDetalleComprasAlimento')
          AND name = N'CK_tblDetalleComprasAlimento_unidadMedida'
    )
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.tblDetalleComprasAlimento WITH CHECK
                ADD CONSTRAINT CK_tblDetalleComprasAlimento_unidadMedida
                CHECK (unidadMedida IN (''Libras'', ''Kilogramos'', ''Sacos''));';

    IF NOT EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.tblDetalleComprasAlimento')
          AND name = N'CK_tblDetalleComprasAlimento_datosSaco'
    )
        EXEC sys.sp_executesql N'
            ALTER TABLE dbo.tblDetalleComprasAlimento WITH CHECK
                ADD CONSTRAINT CK_tblDetalleComprasAlimento_datosSaco
                CHECK
                (
                    (
                        unidadMedida IN (''Libras'', ''Kilogramos'')
                        AND pesoPorSaco IS NULL
                        AND unidadPesoSaco IS NULL
                        AND pesoTotalLibras IS NULL
                        AND pesoTotalKilogramos IS NULL
                        AND subtotal IS NULL
                    )
                    OR
                    (
                        unidadMedida = ''Sacos''
                        AND pesoPorSaco IS NOT NULL
                        AND pesoPorSaco > 0
                        AND unidadPesoSaco IS NOT NULL
                        AND unidadPesoSaco IN (''Libras'', ''Kilogramos'')
                        AND pesoTotalLibras IS NOT NULL
                        AND pesoTotalLibras > 0
                        AND pesoTotalKilogramos IS NOT NULL
                        AND pesoTotalKilogramos > 0
                        AND subtotal IS NOT NULL
                        AND subtotal >= 0
                    )
                );';

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
