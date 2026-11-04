/*
    Reversion segura de Migracion_ComprasPorSacos.sql.

    Solo se permite antes de registrar compras nuevas por sacos. El script
    aborta si encuentra filas Sacos o datos nuevos poblados, para no perderlos.
    Antes de ejecutarlo, restaure tambien las definiciones anteriores de los
    procedimientos CRUD y dashboard/reportes, y la version previa de la app.
*/
USE GranjaControlDB;
GO

SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;

    IF EXISTS
    (
        SELECT 1
        FROM dbo.tblDetalleComprasAlimento
        WHERE unidadMedida = 'Sacos'
           OR pesoPorSaco IS NOT NULL
           OR unidadPesoSaco IS NOT NULL
           OR pesoTotalLibras IS NOT NULL
           OR pesoTotalKilogramos IS NOT NULL
           OR subtotal IS NOT NULL
    )
        THROW 52102, 'No se revierte: hay compras por sacos o valores nuevos que se perderian.', 1;

    IF EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.tblDetalleComprasAlimento')
          AND name = N'CK_tblDetalleComprasAlimento_datosSaco'
    )
        ALTER TABLE dbo.tblDetalleComprasAlimento
            DROP CONSTRAINT CK_tblDetalleComprasAlimento_datosSaco;

    IF EXISTS
    (
        SELECT 1
        FROM sys.check_constraints
        WHERE parent_object_id = OBJECT_ID(N'dbo.tblDetalleComprasAlimento')
          AND name = N'CK_tblDetalleComprasAlimento_unidadMedida'
    )
        ALTER TABLE dbo.tblDetalleComprasAlimento
            DROP CONSTRAINT CK_tblDetalleComprasAlimento_unidadMedida;

    ALTER TABLE dbo.tblDetalleComprasAlimento WITH CHECK
        ADD CONSTRAINT CK_tblDetalleComprasAlimento_unidadMedida
        CHECK (unidadMedida IN ('Libras', 'Kilogramos'));

    ALTER TABLE dbo.tblDetalleComprasAlimento DROP COLUMN
        pesoPorSaco,
        unidadPesoSaco,
        pesoTotalLibras,
        pesoTotalKilogramos,
        subtotal;

    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF XACT_STATE() <> 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
